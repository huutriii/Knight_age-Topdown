using UnityEngine;

[CreateAssetMenu(fileName = "DES_", menuName = "Map/Area")]
public class MapSO : ScriptableObject
{
    public float center;
    Map typeMap;
}

public enum Map
{
    Forest,
    Desert,
    Cave,
    Swamp,
    Moutain,
    Village,
    River,
    Plains
}