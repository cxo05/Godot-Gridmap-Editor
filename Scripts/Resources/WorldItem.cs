using Godot;

public enum WorldItemType
{
  None,
  Flooring,
  Furniture,
  Walls
}

public partial class WorldItem : Resource
{
  public string Name { get; set; }
  public MeshLibrary MeshLibrary { get; set; }
  public int MeshLibraryId { get; set; }
  public WorldItemType WorldItemType { get; set; }
  public Transform3D MeshTransform { get; set; }
  public Mesh Mesh { get; set; }

  public WorldItem() : this("", WorldItemType.None, null, 0) { }

  public WorldItem(string name, WorldItemType worldItemType, MeshLibrary meshLibary, int meshLibraryId)
  {
    Name = name;
    WorldItemType = worldItemType;
    MeshLibrary = meshLibary;
    MeshLibraryId = meshLibraryId;

    MeshTransform = MeshLibrary.GetItemMeshTransform(MeshLibraryId);
    Mesh = MeshLibrary.GetItemMesh(MeshLibraryId);
  }
}
