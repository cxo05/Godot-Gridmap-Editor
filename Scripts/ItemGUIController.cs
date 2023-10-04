using Godot;

public partial class ItemGUIController : MarginContainer
{
  [Export]
  public WorldItem WorldItem { get; set; }

  public override void _Ready()
  {
    GetNode<MeshInstance3D>("Panel/SubViewport/MeshInstance").Mesh = WorldItem.Mesh;
    GetNode<MeshInstance3D>("Panel/SubViewport/MeshInstance").Transform = WorldItem.MeshTransform;
  }

  public override void _GuiInput(InputEvent @event)
  {
    if (@event.IsActionReleased("left_click"))
    {
      var globalController = GetNode<GlobalController>("/root/GlobalController");
      globalController.SelectedWorldItem = WorldItem;
      globalController.CanPlace = true;
    }
  }
}
