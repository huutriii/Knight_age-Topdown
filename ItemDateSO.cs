using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemDateSO : ScriptableObject
{
    public ItemDataBase ItemData;

}
public struct ItemDataBase
{
    public int _ID;
    public string _name;
    public Sprite _image;
}