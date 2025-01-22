using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    [SerializeField] GameObject meteorite;
    [SerializeField] float minX, maxX;
    [SerializeField] float minY, maxY;
    List<GameObject> pools = new();
    Vector2 pos;
    void Update()
    {
        int x = (int)Random.Range(minX, maxX);
        int y = (int)Random.Range(minY, maxY);
        pos = new Vector2(x, y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject meteorite = GetMeteorite();

            meteorite.SetActive(true);
            meteorite.transform.position = pos;
        }
    }

    GameObject GetMeteorite()
    {
        foreach (GameObject m in pools)
        {
            if (!m.activeSelf)
                return m;
        }
        GameObject meteorite = Instantiate(this.meteorite, pos, Quaternion.identity);
        pools.Add(meteorite);
        meteorite.gameObject.SetActive(false);
        return meteorite;
    }
}
