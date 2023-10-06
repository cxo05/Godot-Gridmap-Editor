using Godot;

public partial class WorldEditorController : Node3D
{
  private GridMap floorMap;
  private GridMap wallMap;
  private GridMap furnitureMap;
  private int[] rotations = { 0, 16, 10, 22 };
  private int current_rotation = 0;

  private GlobalController globalController;

  public override void _Ready()
  {
    floorMap = GetNode<GridMap>("/root/Main/World/Floor");
    wallMap = GetNode<GridMap>("/root/Main/World/WallRight");
    furnitureMap = GetNode<GridMap>("/root/Main/World/Top");

    globalController = GetNode<GlobalController>("/root/GlobalController");
  }

  public override void _Process(double delta)
  {
    Vector2 mousePosition = GetViewport().GetMousePosition();

    var plane = new Plane(new Vector3(0, 1, 0), Vector3.Zero);

    var camera = GetViewport().GetCamera3D();
    var position3D = plane.IntersectsRay(
      camera.ProjectRayOrigin(mousePosition),
      camera.ProjectRayNormal(mousePosition));

    Vector3 roundedPosition3D = new()
    {
      X = Mathf.Round((position3D.Value.X + 1) / 2) * 2 - 1,
      Y = 0,
      Z = Mathf.Round((position3D.Value.Z + 1) / 2) * 2 - 1
    };

    // Follow mouse cursor, snapping to grid
    GlobalPosition = roundedPosition3D;

    WorldItem selectedWorldItem = globalController.SelectedWorldItem;

    Vector3I gridIndex = floorMap.LocalToMap(position3D.Value);

    if (selectedWorldItem != null)
    {
      GetNode<MeshInstance3D>("MeshInstance3D").Mesh = selectedWorldItem.Mesh;
      GetNode<MeshInstance3D>("MeshInstance3D").Transform = selectedWorldItem.MeshTransform;

      if (Input.IsActionJustPressed("left_click") && globalController.CanPlace)
      {
        switch (selectedWorldItem.WorldItemType)
        {
          case WorldItemType.Flooring:
            floorMap.SetCellItem(gridIndex, selectedWorldItem.MeshLibraryId, rotations[current_rotation]);
            break;
          case WorldItemType.Walls:
            wallMap.SetCellItem(gridIndex, selectedWorldItem.MeshLibraryId, rotations[current_rotation]);
            break;
          case WorldItemType.Furniture:
            furnitureMap.SetCellItem(gridIndex, selectedWorldItem.MeshLibraryId, rotations[current_rotation]);
            break;
        }
      }

      if (Input.IsActionJustPressed("rotate"))
      {
        RotateY(Mathf.Pi / 2);
        current_rotation = (current_rotation + 1) % 4;
      }
    }
  }

  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("ui_cancel") && globalController.SelectedWorldItem != null)
    {
      globalController.SelectedWorldItem = null;
    }
  }
}
