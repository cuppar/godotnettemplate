using Godot;
using GodotNetTemplate.Globals;

namespace GodotNetTemplate;

public partial class Camera : Camera2D
{
    #region 缩放

    private Vector2 MinZoom { get; set; } = new(0.3f, 0.3f);
    private Vector2 MaxZoom { get; set; } = new(3f, 3f);
    private float ZoomStep { get; set; } = 0.1f;

    private void _handleZoomOut()
    {
        Zoom = Zoom with
        {
            X = float.Max(MinZoom.X, Zoom.X - ZoomStep),
            Y = float.Max(MinZoom.Y, Zoom.Y - ZoomStep)
        };
    }

    private void _handleZoomIn()
    {
        Zoom = Zoom with
        {
            X = float.Min(MaxZoom.X, Zoom.X + ZoomStep),
            Y = float.Min(MaxZoom.Y, Zoom.Y + ZoomStep)
        };
    }

    #endregion

    #region 相机边界

    [ExportGroup("Limit")]
    [Export]
    public Marker2D? LeftLimit
    {
        get => _leftLimit;
        set
        {
            _leftLimit = value;
            _updateLimit();
        }
    }

    [Export]
    public Marker2D? RightLimit
    {
        get => _rightLimit;
        set
        {
            _rightLimit = value;
            _updateLimit();
        }
    }

    [Export]
    public Marker2D? TopLimit
    {
        get => _topLimit;
        set
        {
            _topLimit = value;
            _updateLimit();
        }
    }

    [Export]
    public Marker2D? BottomLimit
    {
        get => _bottomLimit;
        set
        {
            _bottomLimit = value;
            _updateLimit();
        }
    }

    private Marker2D? _leftLimit;
    private Marker2D? _rightLimit;
    private Marker2D? _bottomLimit;
    private Marker2D? _topLimit;

    private void _updateLimit()
    {
        LimitLeft = (int)(LeftLimit?.GlobalPosition.X + 1 ?? LimitLeft);
        LimitRight = (int)(RightLimit?.GlobalPosition.X - 1 ?? LimitRight);
        LimitTop = (int)(TopLimit?.GlobalPosition.Y + 1 ?? LimitTop);
        LimitBottom = (int)(BottomLimit?.GlobalPosition.Y - 1 ?? LimitBottom);
        ResetSmoothing();
    }

    #endregion

    #region 震屏

    [ExportGroup("Shake Screen")] [Export] public float Strength { get; set; }
    [Export] public float RecoverySpeed { get; set; } = 16;

    private void _onShake(float amount)
    {
        Strength += amount;
    }

    private void _initShake()
    {
        Game.ShakeCameraEvent += _onShake;
    }

    private void _cleanShake()
    {
        Game.ShakeCameraEvent -= _onShake;
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
        _updateLimit();
        _initShake();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        _tickShake(delta);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _cleanShake();
    }

    #region 用户输入

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionPressed("zoom_in")) _handleZoomIn();

        if (@event.IsActionPressed("zoom_out")) _handleZoomOut();
    }

    #endregion

    #endregion
}