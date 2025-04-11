using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("Area Setting")]
    [SerializeField] AreaSO Area;
    [SerializeField] float speedArround = 1f;
    [SerializeField] float distanceTarget = 4f;
    [Header("Monster Stats")]
    [SerializeField] float hp;

    [Header("STATE Management")]
    private Animator animator;
    private IMonsterState currentState;
    [Header("Movement")]
    [SerializeField] Vector2 pivot;
    private float radius;
    [SerializeField] Vector2 currentPositionTarget;
    [SerializeField] GameObject currentPlayerTarget;
    [SerializeField] bool isPatrolling = true;

    [Header("Death setting")]
    [SerializeField] float deathAnimationDuration = 1f;
    private bool isDying = false;
    public bool IsDead => hp <= 0;

    public bool IsHurt { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsMovementLocked { get; private set; }
    public bool IsTargetInRange { get; private set; }
    public bool ShouldRun => Vector2.Distance(transform.position, currentPositionTarget) > 0.3f;
    public Vector2 CurrentPositionTarget => currentPositionTarget;
    public Vector2 Pivot => pivot;
    public float Speed => speedArround;
    public bool IsPatrolling => isPatrolling;



    private void Awake()
    {
        SetupInitialValues();
    }

    private void OnEnable()
    {
        hp = MonsterStat.hp;
    }

    private void Start()
    {
        SetupComponents();
        TransitionToState(new IdleState(this, animator));
    }
    private void SetupInitialValues()
    {
        currentPositionTarget = Area.pivot;
        radius = Area.radius;
        pivot = Area.pivot;
    }

    private void SetupComponents()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (currentState != null)
        {

            var newState = currentState.HandleTransition();

            if (newState != null)
            {
                TransitionToState(newState);
            }
        }

        UpdateGameLogic();
    }


    private void UpdateGameLogic()
    {
        UpdateTarget();
        UpdateMovement();
    }

    private void UpdateTarget()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceTarget);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this || hit.gameObject.CompareTag(GAME.Monster))
                continue;

            float distance = Vector2.Distance(transform.position, hit.transform.position);

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
        if (IsMovementLocked) return;

        if (currentPlayerTarget != null && currentPlayerTarget.CompareTag(GAME.Player))
        {
            MoveToTarget(currentPlayerTarget.transform.position);
            return;
        }

        if (isPatrolling)
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

    private void MoveToTarget(Vector2 target)
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

        scale.x = (transform.position.x > target.x) ? -1 : 1;

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


    private void TransitionToState(IMonsterState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GAME.Player))
        {
            SetAttacking(true);
            IsTargetInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GAME.Player))
        {
            IsTargetInRange = false;
            SetAttacking(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(30);
        SetHurt(true);
    }

    #region setter State flags
    public void SetAttacking(bool isAttacking) => IsAttacking = isAttacking;
    public void SetHurt(bool isHurt) => IsHurt = isHurt;
    public void SetMovementLocked(bool isLocked) => IsMovementLocked = isLocked;
    #endregion

    #region Death Sequence
    public void TakeDamage(float damage)
    {
        if (isDying) return;
        hp -= damage;

        if (IsDead)
        {
            isDying = true;
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        SetMovementLocked(true);
        isPatrolling = false;

        TransitionToState(new DiedState(this, animator));

        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        gameObject.SetActive(false);
    }

    #endregion

    private void OnDisable()
    {
        if (TargetClosit.Instance != null && TargetClosit.Instance.GetTarget() == transform)
        {
            TargetClosit.Instance.ClearTarget();
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pivot, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPositionTarget, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceTarget);

        Gizmos.color = Color.white;
        if (currentPlayerTarget != null)
        {
            Gizmos.DrawLine(transform.position, currentPlayerTarget.transform.position);
        }
        else if (isPatrolling)
        {
            Gizmos.DrawLine(transform.position, currentPositionTarget);
        }
    }
}