using Godot;

public partial class ItemTexture : SubViewportContainer
{
  private Node3D EditorObject;
  private MeshInstance3D CursorObject;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    EditorObject = GetNode<Node3D>("/root/Main/EditorObject");
    CursorObject = EditorObject.GetNode<MeshInstance3D>("MeshInstance3D");
  }

  public void _on_gui_input(InputEvent inputEvent)
  {
    if (inputEvent.IsActionPressed("left_click"))
    {
      GD.Print("Clicked");
      CursorObject.Mesh = GetNode<MeshInstance3D>("MeshInstance").Mesh;
    }
  }
}
