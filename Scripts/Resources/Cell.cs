using Godot;
/*
  World is made up of Cells in a grid that can contain mulitple WorldItems
*/
public partial class Cell
{
  // 4 sides of walls
  [Export]
  public WorldItem[] Walls { get; set; }
  // 1 floor tile
  [Export]
  public WorldItem Floortile { get; set; }

  [Export]
  public WorldItem Furniture { get; set; }

  public Cell()
  {
  }

  public Cell(WorldItem[] walls, WorldItem floortile, WorldItem furniture)
  {
    Walls = walls;
    Floortile = floortile;
    Furniture = furniture;
  }
}
