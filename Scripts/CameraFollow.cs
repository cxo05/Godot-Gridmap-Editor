using Godot;
using System;

public partial class CameraFollow : Camera3D
{
  [Export]
  public int camAccel = 2;

  private Node3D CameraRoot;

  public override void _Ready()
  {
    CameraRoot = GetParentOrNull<Node3D>();
  }

  public override void _Process(double delta)
  {
    Position = Position.Lerp(CameraRoot.Position, (float)delta * camAccel);
  }
}
