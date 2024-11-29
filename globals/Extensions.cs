using System.Linq;
using System.Threading.Tasks;
using Godot;
using GodotNetTemplate.Constants;

namespace GodotNetTemplate.Globals.Extensions;

public static class Extensions
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
}

public static class SignalExtensions
{
    /* SignalExtensions.WhenAll Usage Example:
    public partial class Test : Node
    {
        async void Method()
        {
            var task1 = ToSignal(this, SignalName.Ready);
            var task2 = ToSignal(this, SignalName.TreeEntered);
            await SignalExtensions.WhenAll(task1, task2);
            // do something
        }
    }*/
    public static async Task WhenAll(params SignalAwaiter[] awaiterArray)
    {
        var tasks = from awaiter in awaiterArray
            select awaiter.ToTask();

        await Task.WhenAll(tasks);
    }

    public static async Task ToTask(this SignalAwaiter awaiter)
    {
        await awaiter;
    }
}