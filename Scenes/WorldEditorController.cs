using Godot;

public partial class WorldEditorController : Node3D
{
  private GridMap floorMap;
  private GridMap wallMap;
  private GridMap furnitureMap;

  public override void _Ready()
  {
    floorMap = GetNode<GridMap>("/root/Main/World/Floor");
    wallMap = GetNode<GridMap>("/root/Main/World/WallRight");
    furnitureMap = GetNode<GridMap>("/root/Main/World/Top");
  }

  public override void _Process(double delta)
  {
    Vector2 mousePosition = GetViewport().GetMousePosition();

    var plane = new Plane(new Vector3(0, 1, 0), Vector3.Zero);

    var camera = GetViewport().GetCamera3D();
    var position3D = plane.IntersectsRay(
      camera.ProjectRayOrigin(mousePosition),
      camera.ProjectRayNormal(mousePosition));

    GD.Print(position3D);

    Vector3 roundedPosition3D = new()
    {
      X = Mathf.Round(position3D.Value.X / 2) * 2 + 1,
      Y = 0,
      Z = Mathf.Round(position3D.Value.Z / 2) * 2 + 1
    };

    GD.Print(roundedPosition3D);

    // Follow mouse cursor, snapping to grid
    GlobalPosition = roundedPosition3D;

    var globalController = GetNode<GlobalController>("/root/GlobalController");
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
            floorMap.SetCellItem(gridIndex, selectedWorldItem.MeshLibraryId);
            break;
          case WorldItemType.Walls:
            wallMap.SetCellItem(gridIndex, selectedWorldItem.MeshLibraryId);
            break;
          case WorldItemType.Furniture:
            furnitureMap.SetCellItem(gridIndex, selectedWorldItem.MeshLibraryId);
            break;
        }
      }
    }
  }
}
