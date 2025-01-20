using UnityEngine;

public class SouthWeatController : MonoBehaviour
{
    [SerializeField] AreaSO area;
    [SerializeField] Vector2 pos;
    [SerializeField] float radius;
    GameObject[] pools;
    [SerializeField] int count;

    private static SouthWeatController _instance;
    public static SouthWeatController Instance
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
        pools = new GameObject[count];
        pos = area.pivot;
        radius = area.radius;
    }
    public Vector2 GetPos() => pos;
    public float GetRadius() => radius;
}
