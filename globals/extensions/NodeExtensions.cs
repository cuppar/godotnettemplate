using System;
using System.Threading.Tasks;
using Godot;
using GodotNetTemplate.Constants;

namespace GodotNetTemplate.Globals.Extensions;

public static class NodeExtensions
{
    /* EnsureReadyAsync Usage Example:
    public partial class Test : Node2D
    {
        // live template shortcut: "es"
        #region Health

        private double _health;

        [Export]
        public double Health
        {
            get => _health;
            set => SetHealth(value);
        }

        private async void SetHealth(double value)
        {
            await this.EnsureReadyAsync();
            _health = value;
        }

        #endregion
    }*/
    public static async Task EnsureReadyAsync(this Node node)
    {
        if (!node.IsNodeReady())
            await node.ToSignal(node, Node.SignalName.Ready);
    }

    /* DelayAsync Usage Example:
    public partial class Test : Node2D
    {
        async Task Method()
        {
            await this.DelayAsync(10);
            // Do Something
        }
    }*/
    public static async Task DelayAsync(this Node node, float seconds)
    {
        var tree = node.GetTree();
        using var timer = tree.CreateTimer(seconds);
        await node.ToSignal(timer, SceneTreeTimer.SignalName.Timeout);
    }

    /* EnsureToolReadyAsync Usage Example:
    [Tool]
    public partial class Test : Node2D, ISerializationListener
    {
        private readonly TaskCompletionSource _toolReadyTCS = new();

        #region ISerializationListener Members

        public void OnBeforeSerialize()
        {
            SetMeta(MetaNames.Reloading, true);
        }

        public void OnAfterDeserialize()
        {
            SetMeta(MetaNames.Reloading, false);
            _toolReadyTCS.SetResult();
        }

        #endregion

        // live template shortcut: "tes"
        #region Health

        private double _health;

        [Export]
        public double Health
        {
            get => _health;
            set => SetHealth(value);
        }

        private async void SetHealth(double value)
        {
            await this.EnsureToolReadyAsync(_toolReadyTCS);
            _health = value;
        }

        #endregion
    }*/
    public static async Task EnsureToolReadyAsync(this Node node, TaskCompletionSource toolReadyTCS)
    {
        if (!node.HasMeta(MetaNames.Reloading))
            node.SetMeta(MetaNames.Reloading, false);

        var isReloading = (bool)node.GetMeta(MetaNames.Reloading);

        if (isReloading)
            await toolReadyTCS.Task;
        else
            await node.EnsureReadyAsync();
    }

    // return the local outline rect of the Node
    // return null when node has not `outline`
    // The method need parent node is `Node2D`
    public static Rect2? GetOutlineRect(this Node node)
    {
        var globalRect = node.GetOutlineGlobalRect();
        if (!globalRect.HasValue)
            return null;

        var (globalPos, globalScale) = node.GetParent() switch
        {
            Node2D node2D => (node2D.GlobalPosition, node2D.GlobalScale),
            _ => throw new InvalidOperationException("This method need the parent node is `Node2D`.")
        };

        return new Rect2((globalRect.Value.Position - globalPos) / globalScale, globalRect.Value.Size / globalScale);
    }

    // return the global outline rect of the Node
    // return null when node has not outline
    public static Rect2? GetOutlineGlobalRect(this Node node)
    {
        Rect2? selfRect = node switch
        {
            Control control => control.GetGlobalRect(),
            Sprite2D sprite => GetSprite2DRect(sprite),
            _ => null
        };

        foreach (var child in node.GetChildren())
        {
            var childRect = GetOutlineGlobalRect(child);
            if (!childRect.HasValue) continue;

            selfRect = selfRect?.Merge(childRect.Value) ?? childRect;
        }

        return selfRect;

        Rect2 GetSprite2DRect(Sprite2D sprite)
        {
            var pos = sprite.GlobalPosition;
            var textureSize = sprite.Texture.GetSize() * sprite.GlobalScale;
            return sprite.Centered
                ? new Rect2(pos - textureSize / 2, textureSize)
                : new Rect2(pos, textureSize);
        }
    }
}