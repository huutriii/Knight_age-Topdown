using System.Collections;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speedArround = 0.5f;
    [SerializeField] private float distanceTarget = 4f;

    [Header("Area Settings")]
    [SerializeField] private AreaSO Area;
    private Vector2 pivot;
    public float radius;
    private Vector2 currentPositionTarget;
    private GameObject currentPlayerTarget;
    private bool isPatrolling = true;
    private bool isMovementLocked;

    public bool IsMovementLocked => isMovementLocked;
    public bool IsPatrolling => isPatrolling;
    public Vector2 CurrentPositionTarget => currentPositionTarget;
    public Vector2 Pivot => pivot;
    public float Speed => speedArround;
    public bool ShouldRun => Vector2.Distance(transform.position, currentPositionTarget) > 0.3f;

    private void Start()
    {
        SetupInitialValues();
    }

    private void Update()
    {
        UpdateTarget();
        UpdateMovement();
    }

    private void SetupInitialValues()
    {
        if (Area != null)
        {
            currentPositionTarget = Area.pivot;
            radius = Area.radius;
            pivot = Area.pivot;
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] AreaSO is not assigned!");
        }
    }

    public void UpdateTarget()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceTarget);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject || hit.gameObject.CompareTag(GAME.Monster)) continue;

            float distance = Vector2.Distance(transform.position, hit.gameObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = hit.gameObject;
            }
        }

        currentPlayerTarget = target;
    }

    private void UpdateMovement()
    {
        if (isMovementLocked) return;

        if (currentPlayerTarget != null && currentPlayerTarget.CompareTag(GAME.Player))
        {
            MoveToTarget(currentPlayerTarget.transform.position);
        }
        else if (isPatrolling)
        {
            if (Vector2.Distance(transform.position, currentPositionTarget) < 0.3f)
            {
                StartCoroutine(WaitMonsterRest());
            }
            else
            {
                MoveToTarget(currentPositionTarget);
            }
        }
    }

    public void MoveToTarget(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            speedArround * Time.deltaTime
        );

        UpdateFacingDirection(target);
    }

    private void UpdateFacingDirection(Vector2 target)
    {
        Vector3 scale = transform.localScale;
        scale.x = (transform.position.x < target.x) ? 1 : -1;
        transform.localScale = scale;
    }

    private IEnumerator WaitMonsterRest()
    {
        isPatrolling = false;
        yield return new WaitForSeconds(8);

        float x = Random.Range(pivot.x - radius, pivot.x + radius);
        float y = Random.Range(pivot.y - radius, pivot.y + radius);
        currentPositionTarget = new Vector2(x, y);

        isPatrolling = true;
    }

    public void SetMovementLocked(bool locked) => isMovementLocked = locked;

    private void OnDrawGizmos()
    {
        if (Area != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pivot, radius);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(currentPositionTarget, 0.1f);
        }
    }
}
