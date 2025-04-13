#region FILE HEADER

// Filename: InspectionContext.cs
// Author: boming.chen
// Create: 2024-11-19
// Description: 

#endregion

using System;
using System.Text;

namespace Framework.Debug
{
    public static partial class CollectionInspectExtensions
    {
        private class InspectionContext
        {
            /// <summary>
            /// The maximum nesting level allowed 
            /// </summary>
            public readonly int MaximumNestingLevel;

            private bool Inspecting { get; set; }
            private bool Indentation { get; set; }
            public StringBuilder Builder { get; private set; }

            public InspectionContext(int maximumNestingLevel)
            {
                MaximumNestingLevel = maximumNestingLevel;
                Inspecting = false;
                Indentation = false;
                Builder = new StringBuilder();
            }

            public int IncreaseIndent(int indentLevel)
            {
                return indentLevel + (Indentation ? 1 : -1);
            }

            public void AppendTitle(string title)
            {
                var outputTitle = title ?? "Inspect";
                Builder.Append(outputTitle);
                Builder.Append(": ");
            }

            public void AppendNewLineIfIndentation()
            {
                if (Indentation)
                {
                    Builder.AppendLine();
                }
            }

            public void AppendObjectSeparator()
            {
                if (!Indentation)
                {
                    Builder.Append(", ");
                }
                else
                {
                    Builder.Append(",");
                    Builder.AppendLine();
                }
            }

            public void AppendSuffix(InspectionResult ret)
            {
                switch (ret)
                {
                    case InspectionResult.Continue:
                        break;
                    case InspectionResult.StopWithNestingLevel:
                        Builder.Append("(... exceeds maximum nesting level)");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(ret), ret, "not supported result");
                }
            }

            public void Prepare(bool indent)
            {
                Inspecting = true;
                Indentation = indent;
            }
            
            public string Output()
            {
                var output = Builder.ToString();
                Inspecting = false;
                return output;
            }
        }
    }
}