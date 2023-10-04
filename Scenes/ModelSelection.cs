using Godot;

public partial class ModelSelection : Control
{
  private GridContainer FurnitureContainer;
  private GridContainer FlooringContainer;
  private GridContainer WallsContainer;

  public override void _Ready()
  {
    FurnitureContainer = GetNode<GridContainer>("MarginContainer/TabContainer/Furniture/ScrollContainer/GridContainer");

    FlooringContainer = GetNode<GridContainer>("MarginContainer/TabContainer/Flooring/ScrollContainer/GridContainer");

    WallsContainer = GetNode<GridContainer>("MarginContainer/TabContainer/Walls/ScrollContainer/GridContainer");

    PackedScene ItemGUI = GD.Load<PackedScene>("res://Scenes/ItemGUI.tscn");

    var worldItemDatabase = GetNode<WorldItemDatabase>("/root/WorldItemDatabase");

    foreach (WorldItem item in worldItemDatabase.worldItems)
    {
      Node newItemGUI = ItemGUI.Instantiate();
      newItemGUI.Set("WorldItem", item);

      // Load items into grid containers
      switch (item.WorldItemType)
      {
        case WorldItemType.Furniture:
          FurnitureContainer.AddChild(newItemGUI);
          break;
        case WorldItemType.Flooring:
          FlooringContainer.AddChild(newItemGUI);
          break;
        case WorldItemType.Walls:
          WallsContainer.AddChild(newItemGUI);
          break;
      }
    }
  }
}
