using UnityEngine;

public class TargetArround : MonoBehaviour
{
    float targetingRange;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] GameObject electric;
    public GameObject targetObject => currentTarget;
    private static TargetArround _instance;
    public static TargetArround Instance => _instance;
    [SerializeField] bool isTargetSkill = false;
    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            return;
        }
        if (Instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    void Update()
    {
        FindNearestTarget();
        UseSkill();
        isTargetSkill = (currentTarget == null) ? false : true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, 5f);

    }

    void FindNearestTarget()
    {
        Collider2D[] targetInRange = Physics2D.OverlapCircleAll(transform.position, 4f);
        GameObject targetNearest = null;
        float distanceNearest = Mathf.Infinity;

        foreach (Collider2D target in targetInRange)
        {
            if (target.gameObject == gameObject) continue;

            float currentDistance = Vector2.Distance(transform.position, target.transform.position);
            if (distanceNearest > currentDistance)
            {
                distanceNearest = currentDistance;
                targetNearest = target.gameObject;
            }
        }

        currentTarget = targetNearest;
        if (currentTarget != null)
            Debug.DrawLine(transform.position, currentTarget.transform.position, Color.green);
    }

    void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.O) && isTargetSkill)
        {
            GameObject electric = Instantiate(this.electric, transform.position, Quaternion.identity);
            electric.SetActive(true);
            electric.transform.position = transform.position;
        }
    }
}
