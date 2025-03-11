using UnityEngine;

public class RightWingController : MonoBehaviour
{
    [SerializeField] AreaSO area;
    [SerializeField] Vector2 pos;
    [SerializeField] float radius;
    [SerializeField] GameObject[] pools;
    [SerializeField] int count;

    private static RightWingController _instance;
    public static RightWingController Instance
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
