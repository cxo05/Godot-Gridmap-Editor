using Godot;
/*
  World is made up of Cells that can contain mulitple WorldItems
*/
public partial class World : Resource
{
  [Export]
  public int Size { get; set; }

  public Cell[][] Cells { get; set; }

  public World() : this(10) { }

  public World(int size)
  {
    Size = size;
  }

  public void SetCell(int x, int y, Cell cell)
  {
    if (x < 0 || x > Size || y < 0 || y > Size)
    {
      GD.Print($"Not within world bounds: %d x %d", Size);
      return;
    }

    Cells[x][y] = cell;
  }
}
