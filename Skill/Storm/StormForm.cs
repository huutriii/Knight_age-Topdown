using System.Collections.Generic;
using UnityEngine;

public class StormForm : MonoBehaviour
{
    public List<GameObject> lists;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            lists.Add(child.gameObject);
        }
    }


    private void OnEnable()
    {
        ActiveChildren();
    }

    void ActiveChildren()
    {
        foreach (var child in lists)
        {
            child.SetActive(true);
        }
    }
    public void OnChildDeactivated()
    {
        gameObject.SetActive(false);
    }
}
