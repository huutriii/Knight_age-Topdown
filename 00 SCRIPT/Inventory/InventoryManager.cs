using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] private Item testItem1;

    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItemToSlot(testItem1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveItemSlot();
            Debug.Log("this");
        }

    }

    public void AddItemToSlot(Item item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (!slot.CheckSlot())
            {
                slot.SetItem(item);
                break;
            }
        }
    }

    public void RemoveItemSlot()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.CheckSlot())
            {
                slot.ClearItem();
                break;
            }
        }
    }
}
