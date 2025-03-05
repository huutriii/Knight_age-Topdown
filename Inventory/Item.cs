using UnityEngine;


[CreateAssetMenu(menuName = "Item_", fileName = "ItemSO_")]
public class Item : ScriptableObject
{
    [SerializeField] int _itemID;
    [SerializeField] ItemType _itemType;
    [SerializeField] Sprite _itemIcon;

    public Sprite ItemIcon => _itemIcon;
}

public enum ItemType
{
    Equipment,
    Consumable,
    Mount,
    Material,
    QuestItem
}
