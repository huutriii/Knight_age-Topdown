using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerTest : MonoBehaviour
{
    [SerializeField] List<GameObject> slots = new();
    private void Start()
    {
        foreach (GameObject slot in slots)
        {
            slot.gameObject.SetActive(true);
        }
    }
}
