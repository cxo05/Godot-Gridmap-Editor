using Godot;

public partial class CameraControl : Node3D
{
  [Export]
  public float MaxZoom { get; set; } = 20;

  [Export]
  public float MinZoom { get; set; } = 5;

  [Export]
  public float ZoomFactor { get; set; } = 0.2f;

  [Export]
  public float TranslateSpeed { get; set; } = 1.0f;

  [Export]
  private Camera3D Camera3D { get; set; }
  private Vector3 direction = Vector3.Zero;

  public override void _PhysicsProcess(double delta)
  {
    if (Input.IsActionJustReleased("zoom_in"))
    {
      float zoomLevel = Mathf.Clamp(Camera3D.Size - 1, MinZoom, MaxZoom);
      Camera3D.Size = Mathf.Lerp(Camera3D.Size, zoomLevel, (float)delta * ZoomFactor);
    }
    if (Input.IsActionJustReleased("zoom_out"))
    {
      float zoomLevel = Mathf.Clamp(Camera3D.Size + 1, MinZoom, MaxZoom);
      Camera3D.Size = Mathf.Lerp(Camera3D.Size, zoomLevel, (float)delta * ZoomFactor);
    }
    if (Input.IsActionPressed("move_right"))
    {
      direction.X += 1.0f;
    }
    if (Input.IsActionPressed("move_left"))
    {
      direction.X -= 1.0f;
    }
    if (Input.IsActionPressed("move_down"))
    {
      direction.Z += 1.0f;
    }
    if (Input.IsActionPressed("move_up"))
    {
      direction.Z -= 1.0f;
    }

    if (direction != Vector3.Zero)
    {
      // Get Projection of Camera Forward Vector on XZ Plane  
      Vector3 CameraForward = Camera3D.GlobalTransform.Basis.Z;
      Vector3 CameraRight = Camera3D.GlobalTransform.Basis.X;

      CameraForward = CameraForward.Slide(Vector3.Up).Normalized();

      direction = direction.Normalized();

      CameraForward *= direction.Z;
      CameraRight *= direction.X;

      Position += (CameraForward + CameraRight) * TranslateSpeed;

      direction = Vector3.Zero;
    }
  }
}
