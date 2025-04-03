using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Sprite originImage;
    public bool isSlotUsed = false;

    private void Start()
    {
        itemImage = GetComponent<Image>();
        originImage = itemImage.sprite;
    }

    public void SetItem(Item newItem)
    {
        itemImage.sprite = newItem.ItemIcon;
        isSlotUsed = true;
    }

    public void ClearItem()
    {
        itemImage.sprite = originImage;
        isSlotUsed = false;
    }

    public bool CheckSlot() => isSlotUsed;
}
