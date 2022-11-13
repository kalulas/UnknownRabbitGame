#region FILE HEADER
// Filename: Singleton.cs
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

namespace Framework.DesignPattern
{
    public interface ISingleton
    {
        void OnSingletonInit();
        void OnSingletonDisposed();
    }
    
    /// <summary>
    /// reference: https://github.com/liangxiegame/QFramework/blob/master/QFramework.NetworkExtension/Assets/QFramework/Toolkits/_CoreKit/SingletonKit/Scripts/Singleton.cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        private static T m_Instance;

        /// <summary>
        /// 标签锁：确保当一个线程位于代码的临界区时，另一个线程不进入临界区。
        /// 如果其他线程试图进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放
        /// </summary>
        private static object m_Lock = new object();
        
        public static T Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = SingletonCreator.CreateSingleton<T>();
                        m_Instance.OnSingletonInit();
                    }
                }

                return m_Instance;
            }
        }

        public void Dispose()
        {
            m_Instance.OnSingletonDisposed();
            m_Instance = null;
        }


        public abstract void OnSingletonInit();
        public abstract void OnSingletonDisposed();
    }
}