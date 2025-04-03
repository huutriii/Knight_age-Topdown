using UnityEngine;

public class AreaCenterController : MonoBehaviour
{
    [SerializeField] AreaSO area;
    [SerializeField] Vector2 pos;
    [SerializeField] float radius;
    [SerializeField] GameObject[] pools;
    [SerializeField] int count;

    private static AreaCenterController _instance;
    public static AreaCenterController Instance
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
        //if (_instance == null)
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        pos = area.pivot;
        radius = area.radius;
    }
    public Vector2 GetPos() => pos;
    public float GetRadius() => radius;
}
