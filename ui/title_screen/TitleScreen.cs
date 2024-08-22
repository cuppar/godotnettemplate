using Godot;
using GodotNetTemplate.Autoload;

namespace GodotNetTemplate.UI;

public partial class TitleScreen : Control
{
    public override void _Ready()
    {
        base._Ready();
        AutoloadManager.SoundManager.SetupUISounds(this);
        StartButton.Pressed += OnStartButtonPressed;
        QuitButton.Pressed += OnQuitButtonPressed;
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnStartButtonPressed()
    {
        GD.Print($"Start Game!");
    }


    #region Child

    [ExportGroup("ChildDontChange")]
    [Export]
    public Button StartButton { get; set; } = null!;

    [Export] public Button QuitButton { get; set; } = null!;

    #endregion
}