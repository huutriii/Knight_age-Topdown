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
    [SerializeField] private float restDuration = 8f;

    [Header("Area Settings")]
    [SerializeField] private AreaSO patrolArea;
    private Vector2 pivot;
    private float radius;

    [Header("Target")]
    private GameObject currentPlayerTarget;
    private Vector2 currentPositionTarget;
    private float lastTargetCheckTime;

    [Header("State")]
    private bool isMove = true;
    private bool isPatrol = true;
    private bool isResting = false;
    private bool isChasing = false;

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
            Debug.Log($"Monster {gameObject.name} initialized with patrol area: radius={radius}, pivot={pivot}");
        }
        else
        {
            Debug.LogWarning($"Patrol Area not assigned to {gameObject.name}");
        }
    }

    private void Update()
    {
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
            if (hit.gameObject == gameObject || hit.gameObject.CompareTag(TagManager.Monster)) continue;

            if (IsInFieldOfView(hit.gameObject))
            {
                float distance = Vector2.Distance(transform.position, hit.gameObject.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = hit.gameObject;
                }
            }
        }

        if (target == null && currentPlayerTarget != null)
        {
            Debug.Log($"Monster {gameObject.name} lost target, returning to patrol");
            isChasing = false;
            isResting = false;
            SetNewPatrolTarget();
        }

        currentPlayerTarget = target;
    }

    private bool IsInFieldOfView(GameObject target)
    {
        Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.right, directionToTarget);
        return angle <= fieldOfView / 2;
    }

    private void HandleMovement()
    {
        if (currentPlayerTarget != null && currentPlayerTarget.CompareTag(TagManager.Player))
        {
            ChaseTarget();
        }
        else
        {
            PatrolArea();
        }
    }

    private void ChaseTarget()
    {
        if (isMove && currentPlayerTarget != null)
        {
            isChasing = true;
            Vector3 targetPosition = currentPlayerTarget.transform.position;
            MoveTowardsTarget(targetPosition, chaseSpeed);
            UpdateFacingDirection(targetPosition);
            Debug.DrawLine(transform.position, targetPosition, Color.red);
        }
    }

    private void PatrolArea()
    {
        if (isPatrol && !isChasing)
        {
            float distanceToTarget = Vector2.Distance(transform.position, currentPositionTarget);
            if (distanceToTarget < minDistanceToTarget)
            {
                if (!isResting)
                {
                    Debug.Log($"Monster {gameObject.name} reached patrol point, starting rest");
                    StartCoroutine(WaitMonsterRest());
                }
            }
            else
            {
                Vector3 targetPos = new Vector3(currentPositionTarget.x, currentPositionTarget.y, transform.position.z);
                MoveTowardsTarget(targetPos, patrolSpeed);
                UpdateFacingDirection(targetPos);
                Debug.DrawLine(transform.position, targetPos, Color.green);
            }
        }
    }

    private System.Collections.IEnumerator WaitMonsterRest()
    {
        isResting = true;
        Debug.Log($"Monster {gameObject.name} resting for {restDuration} seconds");
        yield return new WaitForSeconds(restDuration);

        SetNewPatrolTarget();
        isResting = false;
        Debug.Log($"Monster {gameObject.name} finished resting, new patrol target set");
    }

    private void MoveTowardsTarget(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        Debug.Log($"Moving towards target: {target}, Current position: {transform.position}, Direction: {direction}");
    }

    private void UpdateFacingDirection(Vector3 target)
    {
        Vector3 scale = transform.localScale;
        scale.x = (transform.position.x < target.x) ? 1 : -1;
        transform.localScale = scale;
    }

    private void SetNewPatrolTarget()
    {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(radius * 0.5f, radius);
        float x = pivot.x + Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float y = pivot.y + Mathf.Sin(angle * Mathf.Deg2Rad) * distance;
        currentPositionTarget = new Vector2(x, y);
        Debug.Log($"Monster {gameObject.name} new patrol target set at {currentPositionTarget}");
    }

    public void SetMovementState(bool canMove)
    {
        isMove = canMove;
        Debug.Log($"Monster {gameObject.name} movement state set to {canMove}");
    }

    public void SetPatrolState(bool isPatrolling)
    {
        isPatrol = isPatrolling;
        Debug.Log($"Monster {gameObject.name} patrol state set to {isPatrolling}");
    }

    public GameObject GetCurrentTarget()
    {
        return currentPlayerTarget;
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
        Gizmos.DrawSphere(currentPositionTarget, 0.1f);
    }
}