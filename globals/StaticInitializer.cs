using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GodotNetTemplate.Globals;

// 1. 定义特性标记需要注册的类
[AttributeUsage(AttributeTargets.Class)]
public class AutoRegisterAttribute : Attribute
{
}

public static class StaticInitializer
{
    // 2. 初始化方法：触发所有标记类的静态构造函数
    public static void Initialize()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var type in assemblies.SelectMany(a => a.GetTypes()))
            if (type.GetCustomAttribute<AutoRegisterAttribute>() != null)
                // 3. 强制运行静态构造函数
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
    }
}