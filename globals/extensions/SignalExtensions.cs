using System.Linq;
using System.Threading.Tasks;
using Godot;

namespace GodotNetTemplate.Globals.Extensions;

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