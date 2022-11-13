#region FILE HEADER
// Filename: SingletonCreator.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// 
/*
MIT License

Copyright (c) 2022 凉鞋

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */
#endregion

using System;
using System.Reflection;

namespace Framework.DesignPattern
{
    /// <summary>
    /// reference: https://github.com/liangxiegame/QFramework/blob/master/QFramework.NetworkExtension/Assets/QFramework/Toolkits/_CoreKit/SingletonKit/Scripts/SingletonCreator.cs
    /// </summary>
    internal static class SingletonCreator
    {
        static T CreateNonPublicConstructorObject<T>() where T : class
        {
            var type = typeof(T);
            // 获取私有构造函数
            var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + type);
            }

            return ctor.Invoke(null) as T;
        }

        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            // var type = typeof(T);
            // var monoBehaviourType = typeof(MonoBehaviour);
            // if (monoBehaviourType.IsAssignableFrom(type))
            // {
            //     return CreateMonoSingleton<T>();
            // }

            var instance = CreateNonPublicConstructorObject<T>();
            return instance;

        }
    }
}