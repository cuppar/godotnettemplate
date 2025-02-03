using Godot;
using GodotNetTemplate.Globals;

namespace GodotNetTemplate;

public partial class Camera : Camera2D
{
    #region 相机边界

    [ExportGroup("Limit")] [Export] public Marker2D LeftLimit = null!;
    [Export] public Marker2D RightLimit = null!;
    [Export] public Marker2D TopLimit = null!;
    [Export] public Marker2D BottomLimit = null!;

    private void _initLimit()
    {
        LimitLeft = (int)LeftLimit.GlobalPosition.X + 1;
        LimitRight = (int)RightLimit.GlobalPosition.X - 1;
        LimitTop = (int)TopLimit.GlobalPosition.Y + 1;
        LimitBottom = (int)BottomLimit.GlobalPosition.Y - 1;
        ResetSmoothing();
    }

    #endregion

    #region 震屏

    [ExportGroup("Shake Screen")] [Export] public float Strength { get; set; }
    [Export] public float RecoverySpeed { get; set; } = 16;

    private void _initShake()
    {
        Game.ShakeCameraEvent += amount => Strength += amount;
    }

    private void _tickShake(double delta)
    {
        Offset = new Vector2((float)GD.RandRange(-Strength, Strength), (float)GD.RandRange(-Strength, Strength));
        Strength = Mathf.MoveToward(Strength, 0, RecoverySpeed * (float)delta);
    }

    #endregion

    #region 生命周期

    public override void _Ready()
    {
        base._Ready();
        _initLimit();
        _initShake();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        _tickShake(delta);
    }

    #endregion
}