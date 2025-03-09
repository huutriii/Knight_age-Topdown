//using System.Collections;
//using UnityEngine;

//public class MonsterController : MonoBehaviour
//{
//    [Header("Area Settings")]
//    [SerializeField] private AreaSO Area;
//    [SerializeField] private float speedArround = 0.5f;
//    [SerializeField] private float distanceTarget = 4f;

//    [Header("Monster Stats")]
//    [SerializeField] private float hp = 100;

//    [Header("State Management")]
//    private Animator animator;
//    private IMonsterState currentState;

//    [Header("Movement")]
//    private Vector2 pivot;
//    private float radius;
//    private Vector2 currentPositionTarget;
//    private GameObject currentPlayerTarget;
//    private bool isPatrolling = true;

//    // Properties for states
//    public bool IsDead => hp <= 0;
//    public bool IsHurt { get; private set; }
//    public bool IsAttacking { get; private set; }
//    public bool IsMovementLocked { get; private set; }
//    public bool IsTargetInRange { get; private set; }
//    public bool ShouldRun => Vector2.Distance(transform.position, currentPositionTarget) > 0.3f;
//    public Vector2 CurrentPositionTarget => currentPositionTarget;
//    public Vector2 Pivot => pivot;
//    public float Speed => speedArround;
//    public bool IsPatrolling => isPatrolling;

//    private void Awake()
//    {
//        SetupInitialValues();
//    }

//    private void OnEnable()
//    {
//        hp = MonsterStat.hp;
//    }

//    private void Start()
//    {
//        SetupComponents();
//        TransitionToState(new IdleState(this, animator));
//    }

//    private void Update()
//    {
//        if (currentState != null)
//        {
//            currentState.Update();

//            var newState = currentState.HandleTransition();
//            if (newState != null)
//            {
//                TransitionToState(newState);
//            }
//        }

//        UpdateGameLogic();
//    }

//    private void SetupInitialValues()
//    {
//        currentPositionTarget = Area.pivot;
//        radius = Area.radius;
//        pivot = Area.pivot;
//    }

//    private void SetupComponents()
//    {
//        animator = GetComponentInChildren<Animator>();
//    }

//    private void UpdateGameLogic()
//    {
//        UpdateTarget();
//        UpdateMovement();
//    }

//    private void UpdateTarget()
//    {
//        GameObject target = null;
//        float minDistance = Mathf.Infinity;

//        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceTarget);
//        foreach (Collider2D hit in hits)
//        {
//            if (hit.gameObject == gameObject || hit.gameObject.CompareTag(TagManager.Monster)) continue;

//            float distance = Vector2.Distance(transform.position, hit.gameObject.transform.position);
//            if (distance < minDistance)
//            {
//                minDistance = distance;
//                target = hit.gameObject;
//            }
//        }

//        currentPlayerTarget = target;
//    }

//    private void UpdateMovement()
//    {
//        if (IsMovementLocked) return;

//        if (currentPlayerTarget != null && currentPlayerTarget.CompareTag(TagManager.Player))
//        {
//            MoveToTarget(currentPlayerTarget.transform.position);
//        }
//        else if (isPatrolling)
//        {
//            if (Vector2.Distance(transform.position, currentPositionTarget) < 0.3f)
//            {
//                StartCoroutine(WaitMonsterRest());
//            }
//            else
//            {
//                MoveToTarget(currentPositionTarget);
//            }
//        }
//    }

//    public void MoveToTarget(Vector2 target)
//    {
//        transform.position = Vector2.MoveTowards(
//            transform.position,
//            target,
//            speedArround * Time.deltaTime
//        );

//        UpdateFacingDirection(target);
//    }

//    private void UpdateFacingDirection(Vector2 target)
//    {
//        Vector3 scale = transform.localScale;
//        scale.x = (transform.position.x < target.x) ? 1 : -1;
//        transform.localScale = scale;
//    }

//    private IEnumerator WaitMonsterRest()
//    {
//        isPatrolling = false;
//        yield return new WaitForSeconds(8);

//        float x = Random.Range(pivot.x - radius, pivot.x + radius);
//        float y = Random.Range(pivot.y - radius, pivot.y + radius);
//        currentPositionTarget = new Vector2(x, y);

//        isPatrolling = true;
//    }

//    private void TransitionToState(IMonsterState newState)
//    {
//        if (currentState != null)
//            currentState.Exit();

//        currentState = newState;
//        currentState.Enter();
//    }

//    // State Management Methods
//    public void SetAttacking(bool isAttacking) => IsAttacking = isAttacking;
//    public void SetHurt(bool isHurt) => IsHurt = isHurt;
//    public void SetMovementLocked(bool isLocked) => IsMovementLocked = isLocked;
//    public void TakeDamage(float damage) => hp -= damage;

//    // Collision Handling
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag(TagManager.Player))
//        {
//            IsAttacking = true;
//            IsTargetInRange = true;
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag(TagManager.Player))
//        {
//            IsTargetInRange = false;
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (!IsAttacking && !IsHurt)
//        {
//            IsHurt = true;
//            TakeDamage(30);
//        }
//    }

//    private void OnDisable()
//    {
//        if (TargetClosit.Instance != null && TargetClosit.Instance.GetTarget() == transform)
//        {
//            TargetClosit.Instance.ClearTarget();
//        }
//    }

//    // Debug Visualization
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(pivot, radius);
//        Gizmos.color = Color.red;
//        Gizmos.DrawSphere(currentPositionTarget, 0.1f);
//    }
////}