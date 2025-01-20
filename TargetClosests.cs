using UnityEngine;

public class TargetClosit : MonoBehaviour
{
    Transform player;
    LayerMask _targetPlayer;
    [SerializeField] Transform _currentTarget;
    private static TargetClosit _instance;
    public static TargetClosit Instance => _instance;

    private void Awake()
    {
        if (Instance != null && Instance.gameObject.GetInstanceID() != gameObject.GetInstanceID())
        {
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
            _instance = this;
    }
    private void Start()
    {
        player = GetComponent<Transform>();
        _targetPlayer = ~LayerMask.GetMask("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentTarget = FindClosestTarget();
            if (_currentTarget != null)
            {
                Debug.Log("target : " + _currentTarget.name);
            }
        }
    }

    Transform FindClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        Collider2D[] targets = Physics2D.OverlapCircleAll(player.position, 5f, _targetPlayer);
        foreach (var target in targets)
        {
            float distance = Vector2.Distance(player.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }

        return closestTarget;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(player.position, 5f);
    //}

    public Transform GetTarget() => _currentTarget;
}
