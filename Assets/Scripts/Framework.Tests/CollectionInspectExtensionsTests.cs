using System;
using System.Collections.Generic;
using Framework.Debug;
using NUnit.Framework;

namespace Framework.Tests
{
    public class CollectionInspectExtensionsTests
    {
        [Test]
        public void InspectNullEnumerable()
        {
            List<int> list = null;
            var ret = list.Inspect();
            Assert.IsTrue(ret == "Inspect: null");
        }
    
        [Test]
        public void InspectEmptyEnumerable()
        {
            var list = new List<int>();
            var ret = list.Inspect();
            Assert.IsTrue(ret == "Inspect: []");
        }

        [Test]
        public void InspectValidEnumerable()
        {
            var arr = new int[] {1, 2, 3, 4, 5};
            var ret = arr.Inspect();
            Assert.IsTrue(ret == "Inspect: [1, 2, 3, 4, 5]");
        }

        [Test]
        public void InspectStringEnumerable()
        {
            var list = new List<string> { "test", "hello", "world" };
            var ret = list.Inspect();
            Assert.IsTrue(ret == "Inspect: [\"test\", \"hello\", \"world\"]");
        }

        [Test]
        public void InspectObjectEnumerable()
        {
            var list = new List<object> { 1, "test", null, 3.14f };
            var ret = list.Inspect();
            Assert.IsTrue(ret == "Inspect: [1, \"test\", null, 3.14]");
        }

        [Test]
        public void InspectNullDictionary()
        {
            Dictionary<string, int> dict = null;
            var ret = dict.Inspect();
            Assert.IsTrue(ret == "Inspect: null");
        }

        [Test]
        public void InspectEmptyDictionary()
        {
            var dict = new Dictionary<string, int>();
            var ret = dict.Inspect();
            Assert.IsTrue(ret == "Inspect: {}");
        }

        [Test]
        public void InspectValidDictionary()
        {
            var dict = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            var ret = dict.Inspect();
            Assert.IsTrue(ret == "Inspect: {\"one\": 1, \"two\": 2, \"three\": 3}");
        }

        [Test]
        public void InspectMixedTypeDictionary()
        {
            var dict = new Dictionary<string, object>
            {
                { "int", 1 },
                { "string", "value" },
                { "null", null },
                { "float", 3.14f }
            };

            var ret = dict.Inspect();
            Assert.IsTrue(ret == "Inspect: {\"int\": 1, \"string\": \"value\", \"null\": null, \"float\": 3.14}");
        }

        [Test]
        public void InspectNestedEnumerable()
        {
            var nested = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 3, 4 }
            };

            var ret = nested.Inspect();
            Assert.IsTrue(ret == "Inspect: [[1, 2], [3, 4]]");
        }

        [Test]
        public void InspectNestedDictionary()
        {
            var nested = new Dictionary<string, Dictionary<string, int>>
            {
                {
                    "first", new Dictionary<string, int>
                    {
                        { "a", 1 },
                        { "b", 2 }
                    }
                },
                {
                    "second", new Dictionary<string, int>
                    {
                        { "c", 3 },
                        { "d", 4 }
                    }
                }
            };

            var ret = nested.Inspect();
            Assert.IsTrue(ret == "Inspect: {\"first\": {\"a\": 1, \"b\": 2}, \"second\": {\"c\": 3, \"d\": 4}}");
        }

        [Test]
        public void InspectComplexNestedStructure()
        {
            var complex = new Dictionary<string, object>
            {
                { "array", new[] { 1, 2, 3 } },
                { "dict", new Dictionary<string, string> { { "key", "value" } } },
                { "mixed", new List<object> { 1, "string", new[] { 4, 5 } } }
            };

            var ret = complex.Inspect();
            Assert.IsTrue(ret == "Inspect: {\"array\": [1, 2, 3], \"dict\": {\"key\": \"value\"}, \"mixed\": [1, \"string\", [4, 5]]}");
        }

        [Test]
        public void InspectWithCustomTitle()
        {
            var list = new List<int> { 1, 2, 3 };
            var ret = list.Inspect("Custom Title");
            Assert.IsTrue(ret == "Custom Title: [1, 2, 3]");
        }

        [Test]
        public void InspectWithIndentation()
        {
            var list = new List<int> { 1, 2, 3 };
            var ret = list.Inspect(indent: true);
            var expected = string.Format("Inspect: [{0}    1,{0}    2,{0}    3{0}]", Environment.NewLine);
            Assert.IsTrue(ret == expected);
        }

        [Test]
        public void InspectDictionaryWithIndentation()
        {
            var dict = new Dictionary<string, int>
            {
                { "a", 1 },
                { "b", 2 }
            };
        
            var ret = dict.Inspect(indent: true);
            var expected = string.Format("Inspect: {{{0}    \"a\": 1,{0}    \"b\": 2{0}}}", Environment.NewLine);
            Assert.IsTrue(ret == expected);
        }

        [Test]
        public void InspectDeepNestedStructure()
        {
            // This should test the maximum nesting level limit
            var level1 = new List<object>();
            var level2 = new List<object>();
            var level3 = new List<object>();
            var level4 = new List<object>();
            var level5 = new List<object>();
            var level6 = new List<object>();
            var level7 = new List<object>();
            var level8 = new List<object>();
            var level9 = new List<object> { "deepest" };
        
            level8.Add(level9);
            level7.Add(level8);
            level6.Add(level7);
            level5.Add(level6);
            level4.Add(level5);
            level3.Add(level4);
            level2.Add(level3);
            level1.Add(level2);
        
            var ret = level1.Inspect();
            // Should indicate maximum nesting level reached
            Assert.IsTrue(ret.Contains("exceeds maximum nesting level"));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator CollectionInspectWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}
