using System.Collections.Generic;
using Godot;

partial class WorldItemDatabase : Node
{
  public List<WorldItem> worldItems = new();

  public MeshLibrary FloorLibrary { get; set; }

  public MeshLibrary FurnitureLibrary { get; set; }

  public MeshLibrary WallLibrary { get; set; }

  public override void _Ready()
  {
    FloorLibrary = ResourceLoader.Load<MeshLibrary>("res://MeshLibraries/FloorLibrary.tres");
    FurnitureLibrary = ResourceLoader.Load<MeshLibrary>("res://MeshLibraries/FurnitureLibrary.tres");
    WallLibrary = ResourceLoader.Load<MeshLibrary>("res://MeshLibraries/WallLibrary.tres");

    AddWorldItemsFromLibrary(FloorLibrary, WorldItemType.Flooring);

    AddWorldItemsFromLibrary(FurnitureLibrary, WorldItemType.Furniture);

    AddWorldItemsFromLibrary(WallLibrary, WorldItemType.Walls);

    // foreach (WorldItem item in worldItems)
    // {
    //   GD.Print($"Name: {item.Name}");
    //   GD.Print($"WorldItemType: {item.WorldItemType}");
    // }

    GD.Print("World Items Loaded: " + worldItems.Count);
  }

  public void AddWorldItemsFromLibrary(MeshLibrary library, WorldItemType type)
  {
    foreach (int i in library.GetItemList())
    {
      worldItems.Add(new WorldItem(library.GetItemName(i), type, library, i));
    }
  }
}
