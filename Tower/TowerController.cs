using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    GameObject tmp;
    List<GameObject> pools = new();
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AudioController.Instance.PlaySound(SOUND.Thunder);
            tmp = GetBullet();
            tmp.SetActive(true);
            tmp.transform.position = transform.position;
        }
    }

    GameObject GetBullet()
    {
        foreach (GameObject bulletPool in pools)
        {
            if (!bulletPool.activeSelf)
            {
                return bulletPool;
            }
        }

        GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        pools.Add(bullet);
        bullet.SetActive(false);

        return bullet;
    }
}
