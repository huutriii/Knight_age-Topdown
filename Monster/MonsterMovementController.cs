using UnityEngine;

public class MonsterMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float patrolSpeed = 0.5f;
    [SerializeField] private float chaseSpeed = 1f;
    [SerializeField] private float detectionRange = 4f;
    [SerializeField] private float fieldOfView = 90f;
    [SerializeField] private float targetCheckInterval = 0.5f;
    [SerializeField] private float minDistanceToTarget = 0.3f;
    [SerializeField] private float restDuration = 5f;

    [Header("Area Settings")]
    [SerializeField] private AreaSO patrolArea;
    private Vector2 pivot;
    private float radius;

    [Header("Target")]
    private GameObject currentPlayerTarget;
    private Vector2 currentPositionTarget;
    private float lastTargetCheckTime;

    [Header("Patrol Points")]
    public Vector2 currentPatrolPoint;
    public float currentRestTime;

    [Header("Debug Info")]
    public float distanceToTarget;
    public Vector2 currentPosition;
    public Vector2 targetPosition;
    public Vector2 moveDirection;

    [Header("State")]
    public bool isMove = true;
    public bool isPatrol = true;
    public bool isResting = false;
    public bool isChasing = false;

    private string FormatLog(string message, string color = "white")
    {
        return $"<color={color}>{message}</color>";
    }

    private void Awake()
    {
        InitializeArea();
        SetNewPatrolTarget();
    }

    private void InitializeArea()
    {
        if (patrolArea != null)
        {
            radius = patrolArea.radius;
            pivot = patrolArea.pivot;
            Debug.Log(FormatLog($"Monster {gameObject.name} initialized with patrol area: radius={radius}, pivot={pivot}", "cyan"));
        }
        else
        {
            Debug.LogWarning(FormatLog($"Patrol Area not assigned to {gameObject.name}", "red"));
        }
    }

    private void Update()
    {
        currentPosition = transform.position;

        if (Time.time >= lastTargetCheckTime + targetCheckInterval)
        {
            CheckForTarget();
            lastTargetCheckTime = Time.time;
        }

        HandleMovement();
    }

    private void CheckForTarget()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(TagManager.Player))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = hit.gameObject;
                }
            }
        }

        if (target != null)
        {
            currentPlayerTarget = target;
            isChasing = true;
            isPatrol = false;
            Debug.Log(FormatLog($"Monster {gameObject.name} detected player, starting chase", "yellow"));
        }
        else if (currentPlayerTarget != null)
        {
            Debug.Log(FormatLog($"Monster {gameObject.name} lost target, returning to patrol", "yellow"));
            currentPlayerTarget = null;
            isChasing = false;
            isPatrol = true;
            SetNewPatrolTarget();
        }
    }

    private void HandleMovement()
    {
        if (!isMove) return;

        if (isChasing && currentPlayerTarget != null)
        {
            ChaseTarget();
        }
        else if (isPatrol)
        {
            PatrolArea();
        }
    }

    private void ChaseTarget()
    {
        if (currentPlayerTarget != null)
        {
            Vector3 targetPosition = currentPlayerTarget.transform.position;
            MoveTowardsTarget(targetPosition, chaseSpeed);
            UpdateFacingDirection(targetPosition);
            Debug.DrawLine(transform.position, targetPosition, Color.red);
        }
    }

    private void PatrolArea()
    {
        if (!isMove || !isPatrol || isChasing) return;

        currentPosition = transform.position;
        targetPosition = currentPatrolPoint;
        distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

        if (isResting)
        {
            currentRestTime -= Time.deltaTime;
            if (currentRestTime <= 0)
            {
                isResting = false;
                SetNewPatrolTarget();
            }
        }
        else
        {
            Debug.Log(FormatLog($"Distance to target: {distanceToTarget}, Current: {currentPosition}, Target: {targetPosition}", "yellow"));

            if (distanceToTarget <= minDistanceToTarget)
            {
                StartResting();
            }
            else
            {
                moveDirection = (targetPosition - currentPosition).normalized;

                Vector2 newPosition = currentPosition + (moveDirection * patrolSpeed * Time.deltaTime);
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

                Debug.Log(FormatLog($"Moving - Direction: {moveDirection}, Speed: {patrolSpeed}", "white"));
            }
        }
    }

    private void StartResting()
    {
        isResting = true;
        currentRestTime = restDuration;
        Debug.Log(FormatLog($"Starting rest for {restDuration} seconds", "blue"));
    }

    private void MoveTowardsTarget(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void UpdateFacingDirection(Vector3 target)
    {
        Vector3 scale = transform.localScale;
        scale.x = (transform.position.x < target.x) ? 1 : -1;
        transform.localScale = scale;
    }

    private void SetNewPatrolTarget()
    {
        if (patrolArea == null) return;

        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(radius * 0.5f, radius);

        float x = pivot.x + Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float y = pivot.y + Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        currentPatrolPoint = new Vector2(x, y);
        Debug.Log(FormatLog($"New patrol point set at: {currentPatrolPoint}, Distance from pivot: {Vector2.Distance(pivot, currentPatrolPoint)}", "green"));
    }

    private void OnDrawGizmos()
    {
        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw field of view
        Vector3 leftRayRotation = Quaternion.Euler(0, 0, fieldOfView / 2) * transform.right;
        Vector3 rightRayRotation = Quaternion.Euler(0, 0, -fieldOfView / 2) * transform.right;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftRayRotation * detectionRange);
        Gizmos.DrawRay(transform.position, rightRayRotation * detectionRange);

        // Draw patrol area
        if (patrolArea != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pivot, radius);
        }

        // Draw current target position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentPatrolPoint, 0.3f);

        // Vẽ đường từ monster đến điểm tuần tra
        if (isPatrol && !isChasing)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, currentPatrolPoint);
        }
    }
}