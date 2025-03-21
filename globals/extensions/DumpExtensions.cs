using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using Godot;

namespace GodotNetTemplate.Globals.Extensions;

public static class DumpExtensions
{
    public static void Dump<T>(this T? obj, [CallerArgumentExpression(nameof(obj))] string name = "")
    {
        string message;
        if (obj == null)
            message = "Null";
        else if (obj is string str)
            message = $"{name}: {str}";
        else if (obj is IEnumerable enumerable)
        {
            GD.Print($"{name}: Enumerable");
            var index = 0;
            foreach (var item in enumerable)
            {
                GD.Print($"{index++}: {item}");
            }

            return;
        }
        else
            message = obj switch
            {
                float floatValue => floatValue.ToString("F3", CultureInfo.InvariantCulture),
                double doubleValue => doubleValue.ToString("F3", CultureInfo.InvariantCulture),
                Vector2 float2Value => $"({float2Value.X:F2}, {float2Value.Y:F2})",
                IConvertible convertible => convertible.ToString(CultureInfo.InvariantCulture),
                _ => obj.ToString() ?? "Null",
            };

        GD.Print($"{name}: {message}");
    }
}