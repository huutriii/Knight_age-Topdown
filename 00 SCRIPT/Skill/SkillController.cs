using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] SkillData flame1;
    List<GameObject> pools = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        this.gameObject.SetActive(false);
    }

    GameObject GetFlame()
    {
        foreach (GameObject pool in pools)
        {
            if (!pool.activeSelf)
                return pool;
        }

        GameObject flame = Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        pools.Add(flame);

        flame.SetActive(false);

        return flame;
    }
}
