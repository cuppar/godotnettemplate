using System.Diagnostics;
using System.Linq;
using Godot;

namespace Sandbox.Globals.Extensions;

public static class Area2DExtensions
{
    /* GetCollisionChildRect Usage Example:
    public partial class Test : Area2D
    {
        // 1
        var rect = this.GetCollisionChildRect();
        // 2
        var playerTop = (playerGraphicsArea.GetCollisionChildRect().Position * playerGraphicsArea.GlobalScale +
                         playerGraphicsArea.GlobalPosition).Y;
    }*/
    public static Rect2 GetCollisionChildRect(this Area2D area)
    {
        float right = float.MinValue, bottom = float.MinValue, left = float.MaxValue, top = float.MaxValue;
        foreach (var child in area.GetChildren())
        {
            switch (child)
            {
                case CollisionShape2D collisionShape2D:
                    var (currentLeft, currentTop) =
                        collisionShape2D.Shape.GetRect().Position + collisionShape2D.Position;
                    var (currentRight, currentBottom) =
                        collisionShape2D.Shape.GetRect().End + collisionShape2D.Position;
                    if (currentRight > right)
                        right = currentRight;
                    if (currentBottom > bottom)
                        bottom = currentBottom;
                    if (currentLeft < left)
                        left = currentLeft;
                    if (currentTop < top)
                        top = currentTop;
                    break;
                case CollisionPolygon2D collisionPolygon2D:
                    var vertices = collisionPolygon2D.Polygon.Select(v => v + collisionPolygon2D.Position);
                    foreach (var vertex in vertices)
                    {
                        if (vertex.X > right)
                            right = vertex.X;
                        if (vertex.Y > bottom)
                            bottom = vertex.Y;
                        if (vertex.X < left)
                            left = vertex.X;
                        if (vertex.Y < top)
                            top = vertex.Y;
                    }

                    break;
            }
        }

        Debug.Assert(right > left);
        Debug.Assert(bottom > top);
        return new Rect2(new Vector2(left, top), new Vector2(right - left, bottom - top));
    }
}