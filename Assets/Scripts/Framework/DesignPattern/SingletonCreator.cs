#region FILE HEADER
// Filename: SingletonCreator.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
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