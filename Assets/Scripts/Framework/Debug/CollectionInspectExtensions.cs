#region FILE HEADER

// Filename: CollectionInspectExtensions.cs
// Author: boming.chen
// Create: 2024-05-29
// Description: 

#endregion

using System;
using System.Collections;
using System.Text;

namespace Framework.Debug
{
    public static partial class CollectionInspectExtensions
    {
        private enum InspectionResult : byte
        {
            Continue,
            StopWithNestingLevel,
            // StopWithLength?
        }

        private const int m_CachedIndentationLevel = 4;

        /// <summary>
        /// The maximum nesting level allowed 
        /// </summary>
        private const int m_DefaultMaximumNestingLevel = 7;

        private const string m_Indentation = "    ";

        /// <summary>
        /// Cached from <see cref="string.Empty"/> (* 0) -> <see cref="m_Indentation"/> * <see cref="m_CachedIndentationLevel"/>
        /// </summary>
        private static readonly string[] m_CachedIndentationStr;

        #region Constructor

        static CollectionInspectExtensions()
        {
            var indentationBuilder = new StringBuilder();
            m_CachedIndentationStr = new string[m_CachedIndentationLevel + 1];
            m_CachedIndentationStr[0] = string.Empty;
            for (int i = 1; i <= m_CachedIndentationLevel; i++)
            {
                indentationBuilder.Append(m_Indentation);
                m_CachedIndentationStr[i] = indentationBuilder.ToString();
            }
        }

        #endregion

        #region Indentation (private)

        private static void IndentationIfNeeded(InspectionContext context, int indentSize)
        {
            if (indentSize <= 0)
            {
                return;
            }

            if (indentSize <= m_CachedIndentationLevel)
            {
                context.Builder.Append(m_CachedIndentationStr[indentSize]);
            }
            else
            {
                // Use MemoryExtensions.AsSpan for efficient string operations
                var indentSpan = m_Indentation.AsSpan();
                for (int i = 0; i < indentSize; i++)
                {
                    context.Builder.Append(indentSpan);
                }
            }
        }
        
        #endregion

        #region Inspection (private)

        private static InspectionResult InspectObject(InspectionContext context, object obj, int indentSize)
        {
            InspectionResult ret;
            if (obj == null)
            {
                context.Builder.Append("null");
                ret = InspectionResult.Continue;
            }
            else if (obj is ValueType)
            {
                context.Builder.Append(obj);
                ret = InspectionResult.Continue;
            }
            else if (obj is string) // string is also IEnumerable
            {
                context.Builder.Append("\"");
                context.Builder.Append(obj);
                context.Builder.Append("\"");
                ret = InspectionResult.Continue;
            }
            else if (obj is IDictionary)
            {
                ret = InspectDictionary(context, (IDictionary) obj, indentSize);
            }
            else if (obj is IEnumerable)
            {
                ret = InspectEnumerable(context, (IEnumerable) obj, indentSize);
            }
            else // for other types, we just use ToString()
            {
                context.Builder.Append("\"");
                context.Builder.Append(obj);
                context.Builder.Append("\"");
                ret = InspectionResult.Continue;
            }

            return ret;
        }

        private static void InspectAsElementObject(InspectionContext context, object obj)
        {
            if (obj == null)
            {
                context.Builder.Append("null");
            }
            else if (obj is ValueType)
            {
                context.Builder.Append(obj);
            }
            else if (obj is string)
            {
                context.Builder.Append("\"");
                context.Builder.Append(obj);
                context.Builder.Append("\"");
            }
            else // for other types, we just use ToString()
            {
                context.Builder.Append("\"");
                context.Builder.Append(obj);
                context.Builder.Append("\"");
            }
        }

        private static InspectionResult InspectEnumerable(InspectionContext context, IEnumerable enumerable,
            int indentSize)
        {
            var prevIndentLevel = indentSize;
            var curIndentLevel = context.IncreaseIndent(indentSize);
            if (Math.Abs(curIndentLevel) > context.MaximumNestingLevel)
            {
                return InspectionResult.StopWithNestingLevel;
            }

            if (enumerable == null)
            {
                context.Builder.Append("null");
                return InspectionResult.Continue;
            }

            var index = 0;
            context.Builder.Append("[");
            context.AppendNewLineIfIndentation();
            foreach (var element in enumerable)
            {
                if (index != 0)
                {
                    context.AppendObjectSeparator();
                }

                IndentationIfNeeded(context, curIndentLevel);
                var ret = InspectObject(context, element, curIndentLevel);
                if (ret != InspectionResult.Continue)
                {
                    return ret;
                }

                index++;
            }

            context.AppendNewLineIfIndentation();
            IndentationIfNeeded(context, prevIndentLevel);
            context.Builder.Append("]");
            return InspectionResult.Continue;
        }

        private static InspectionResult InspectDictionary(InspectionContext context, IDictionary dictionary,
            int indentSize)
        {
            var prevIndentLevel = indentSize;
            var curIndentLevel = context.IncreaseIndent(indentSize);
            if (Math.Abs(curIndentLevel) > context.MaximumNestingLevel)
            {
                return InspectionResult.StopWithNestingLevel;
            }

            if (dictionary == null)
            {
                context.Builder.Append("null");
                return InspectionResult.Continue;
            }

            var index = 0;
            context.Builder.Append("{");
            context.AppendNewLineIfIndentation();
            foreach (DictionaryEntry entry in dictionary)
            {
                if (index != 0)
                {
                    context.AppendObjectSeparator();
                }

                IndentationIfNeeded(context, curIndentLevel);
                InspectAsElementObject(context, entry.Key); // consider key as element, not collection
                context.Builder.Append(": ");
                var ret = InspectObject(context, entry.Value, curIndentLevel);
                if (ret != InspectionResult.Continue)
                {
                    return ret;
                }

                index++;
            }

            context.AppendNewLineIfIndentation();
            IndentationIfNeeded(context, prevIndentLevel);
            context.Builder.Append("}");
            return InspectionResult.Continue;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Make an inspection on the given <see cref="IEnumerable"/> object.
        /// Notice: This should be used for debug purpose only.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="title"></param>
        /// <param name="indent"> if True, return a formatted output with indentation </param>
        /// <returns></returns>
        public static string Inspect(this IEnumerable enumerable, string title = null, bool indent = false)
        {
            var context = new InspectionContext(m_DefaultMaximumNestingLevel);
            context.Prepare(indent);
            context.AppendTitle(title);
            var ret = InspectEnumerable(context, enumerable, 0);
            context.AppendSuffix(ret);
            return context.Output();
        }

        /// <summary>
        /// Make an inspection on the given <see cref="IDictionary"/> object.
        /// Notice: This should be used for debug purpose only.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="title"></param>
        /// <param name="indent"> if True, return a formatted output with indentation </param>
        public static string Inspect(this IDictionary dictionary, string title = null, bool indent = false)
        {
            var context = new InspectionContext(m_DefaultMaximumNestingLevel);
            context.Prepare(indent);
            context.AppendTitle(title);
            var ret = InspectDictionary(context, dictionary, 0);
            context.AppendSuffix(ret);
            return context.Output();
        }

        #endregion
    }
}