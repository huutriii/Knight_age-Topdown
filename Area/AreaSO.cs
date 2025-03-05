using UnityEngine;

[CreateAssetMenu(fileName = "Area_", menuName = "Area/world")]
public class AreaSO : ScriptableObject
{
    public Area coordinate;
    public float radius;
    public string monsterName;
    public string itemsName;
    public Vector2 pivot;
}

public enum Area
{
    North,
    South,
    East,
    West,
    NorthEast,
    NorthCenter,
    NorhtWeast,
    Southeast,
    Northwest,
    Southwest,
    Center,
    InnerCity,
    OutSkirt
}