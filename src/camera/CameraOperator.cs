using Godot;

public partial class CameraOperator : Node3D
{
    private float _zoom = 0.5f;
    private float _targetZoom = 0.5f;

    private float[] _rotations = new float[4] {0f, 90f, 180f, -90f};
    private int _rotationIndex = 0;
    private float _targetRotation = 0f;
    
    [Export] private Vector3 _cameraOffset = Vector3.Up;
    [Export] private float _walkSpeed = 30f;
    [Export] private float _maxDistance = 60f;
    [Export] private Curve _zoomCurve;

    private Node3D _gimbalH;
    private Node3D _gimbalV;
    private Camera3D _camera;


    public override void _Ready()
    {
        _gimbalH = GetNode<Node3D>("HorizontalGimbal");
        _gimbalV = GetNode<Node3D>("HorizontalGimbal/VerticalGimbal");
        _camera = GetNode<Camera3D>("HorizontalGimbal/VerticalGimbal/Camera");

        _gimbalH.Translation = _cameraOffset;
    }

    public override void _Input(InputEvent e)
    {
        if (e.IsActionPressed("camera_zoom_out"))
        {
            _targetZoom = Mathf.Clamp(_targetZoom - 0.05f, 0, 1);
        }

        if (e.IsActionPressed("camera_zoom_in"))
        {
            _targetZoom = Mathf.Clamp(_targetZoom + 0.05f, 0, 1);
        }

        if (e.IsActionPressed("camera_turn_left"))
        {
            _rotationIndex = (_rotationIndex + 1) % _rotations.Length;
            _targetRotation = _rotations[_rotationIndex];
        }

        if (e.IsActionPressed("camera_turn_right"))
        {
            _rotationIndex -= 1;
            _rotationIndex = _rotationIndex == -1 ? _rotationIndex + _rotations.Length : _rotationIndex;
            _targetRotation = _rotations[_rotationIndex];
        }
    }

    public override void _Process(float delta)
    {
        var transform = Transform;
        transform.origin = Transform.origin + GetRelativeWalkInput() * _walkSpeed * delta;
        Transform = transform;

        ProcessGimbalV(delta);

    }

    private void ProcessGimbalV(float delta)
    {
        _zoom = Mathf.Lerp(_zoom, _targetZoom, 0.1f);

        var rotationDegrees = _gimbalV.RotationDegrees;
        rotationDegrees.x = Mathf.Lerp(0, -90, _zoomCurve.Interpolate(_zoom));
        _gimbalV.RotationDegrees = rotationDegrees;

        _camera.Translation = new Vector3(0, 0, Mathf.Lerp(0, _maxDistance, _zoom));

        var rotation = Rotation;
        rotation.y = Mathf.LerpAngle(Mathf.Deg2Rad(RotationDegrees.y), Mathf.Deg2Rad(_targetRotation), 0.08f);
        Rotation = rotation;
    }

    private Vector3 GetRelativeWalkInput()
    {
        return GetWalkInput().Rotated(new Vector3(0, 1, 0), Rotation.y);
    }

    private Vector3 GetWalkInput()
    {
        int left = Input.IsActionPressed("camera_left") ? 1 : 0;
        int right = Input.IsActionPressed("camera_right") ? 1 : 0;
        int forward = Input.IsActionPressed("camera_forward") ? 1 : 0;
        int back = Input.IsActionPressed("camera_back") ? 1 : 0;

        return new Vector3(left - right, 0, forward - back).Normalized();
    }
}