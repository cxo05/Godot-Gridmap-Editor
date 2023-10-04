using Godot;

public partial class GlobalController : Node
{
  public bool CanPlace = false;

  public WorldItem SelectedWorldItem { get; set; }

  public World World { get; set; }

  public override void _Ready()
  {
    if (!ResourceLoader.Exists("res://CustomWorld.tres"))
    {
      GD.Print("No saved world, creating new world...");
      World = new World(10);
      ResourceSaver.Save(World, "res://CustomWorld.tres");
      GD.Print("New world created");
      return;
    }

    World = ResourceLoader.Load<World>("res://CustomWorld.tres");

    PopulateGridMaps();

    GD.Print("World Loaded");
  }

  public void PopulateGridMaps()
  {
    var worldItemDatabase = GetNode<WorldItemDatabase>("/root/WorldItemDatabase");

    GridMap floorMap = GetNode<GridMap>("/root/Main/World/Floor");

    GridMap furnitureMap = GetNode<GridMap>("/root/Main/World/Top");

    for (int i = 0; i < World.Size; i++)
    {
      for (int j = 0; j < World.Size; j++)
      {
        // WorldItem[] walls = World.Cells[i][j].Walls;

        WorldItem floortile = World.Cells[i][j].Floortile;
        floorMap.SetCellItem(new Vector3I(i, 0, j), floortile.MeshLibraryId);

        // WorldItem furniture = World.Cells[i][j].Furniture;

      }
    }
  }
}
