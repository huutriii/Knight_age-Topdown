using System.Collections;
using UnityEngine;

public class SouthestController : MonoBehaviour
{
    [SerializeField] AreaSO area;
    [SerializeField] Vector2 pos;
    [SerializeField] float radius;
    [SerializeField] GameObject[] pools;
    [SerializeField] int count;

    private static SouthestController _instance;
    public static SouthestController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("SouthWeast _instance is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        pos = area.pivot;
        radius = area.radius;
    }

    public void ReSpawnMonster(GameObject monster, Vector2 spawnPos)
    {
        StartCoroutine(ReSpawnCorotine(monster, spawnPos));
    }

    IEnumerator ReSpawnCorotine(GameObject monster, Vector2 spawnPos)
    {
        yield return new WaitForSeconds(6f);
        monster.transform.position = spawnPos;
        monster.SetActive(true);
    }
    public Vector2 GetPos() => pos;
    public float GetRadius() => radius;
}
