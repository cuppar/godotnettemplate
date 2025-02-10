using Godot;

namespace Sandbox.Globals.Extensions;

public static class CanvasItemExtensions
{
    /* GetEffectiveZIndex Usage Example:
    public partial class Test : CanvasItem
    {
        var zIndex = this.GetEffectiveZIndex();
    }*/
    public static int GetEffectiveZIndex(this CanvasItem canvasItem)
    {
        if (!canvasItem.ZAsRelative)
            return canvasItem.ZIndex;
        var parent = canvasItem.GetParent();
        if (parent is not CanvasItem parentCanvasItem)
            return canvasItem.ZIndex;
        return parentCanvasItem.GetEffectiveZIndex() + canvasItem.ZIndex;
    }

    /* GetEffectiveZIndex Usage Example:
    public partial class Test : CanvasItem
    {
        var zIndex = 100;
        this.SetEffectiveZIndex(zIndex);
    }*/
    public static void SetEffectiveZIndex(this CanvasItem canvasItem, int zIndex)
    {
        if (!canvasItem.ZAsRelative)
        {
            canvasItem.ZIndex = zIndex;
            return;
        }

        var parent = canvasItem.GetParent();
        if (parent is not CanvasItem parentCanvasItem)
        {
            canvasItem.ZIndex = zIndex;
            return;
        }

        canvasItem.ZIndex = zIndex - parentCanvasItem.GetEffectiveZIndex();
    }
}