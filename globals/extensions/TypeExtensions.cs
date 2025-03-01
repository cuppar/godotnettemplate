using System;

namespace MonsterTower.Globals.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// 使用Activator创建实例
    /// </summary>
    public static object CreateInstance(this Type type, bool nonPublic = true)
    {
        return Activator.CreateInstance(type, nonPublic)!;
    }

    /// <summary>
    /// 使用Activator创建实例，并转换为T类型
    /// </summary>
    public static T CreateInstance<T>(this Type type, bool nonPublic = true)
    {
        return (T)type.CreateInstance(nonPublic);
    }
}