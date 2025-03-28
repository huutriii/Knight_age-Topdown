using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    #region Singleton
    private static BossController _instance;
    public static BossController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<BossController>();
                if (_instance == null)
                {
                    Debug.Log("No BossController Instance is found !");
                    return null;
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }
        _instance = this;
    }

    #endregion


    private Animator animator;
    private IBossState currentState;

    [SerializeField] float x = 0, y = 0;

    [SerializeField] AreaSO area;
    public Vector2 pivot;
    public float radius;

    [SerializeField] float speedArround, speedAttack;
    [SerializeField] float detectRange;
    public bool isIdle;

    public bool isWalk;
    public bool isPatrolling;

    public bool isRun;
    public Vector2 currentPositionTarget;

    public GameObject currentPlayerTarget;

    public bool isHurt;
    public bool isAttack;
    public bool isDeath;

    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        InitialSO();
    }

    void InitialSO()
    {
        pivot = area.pivot;
        radius = area.radius;
        currentPositionTarget = pivot;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();

            var newState = currentState.HandleTransition();

            if (newState != null)
            {
                TransitionToState(newState);
            }
        }

        Target();
        CheckPlayerInRange();
        UpdateBosMovement();

        Attack();
    }

    private void TransitionToState(IBossState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }


    private void UpdateBosMovement()
    {
        if (currentPlayerTarget != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);

            if (distanceToPlayer > 3f)
            {
                MoveToTarget(currentPlayerTarget.transform.position);
                animator.SetBool(Constant.walk, false);
                animator.SetBool(Constant.idle, false);
                animator.SetBool(Constant.run, true);
                isAttack = false;
            }
            else
            {
                animator.SetBool(Constant.run, false);
                if (canAttack)
                {
                    StartCoroutine(WaitAttack());
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, currentPositionTarget) > 0.3f)
            {
                animator.SetBool(Constant.run, false);
                animator.SetBool(Constant.walk, true);
                MoveToTarget(currentPositionTarget);
                isPatrolling = true;
            }
            else
            {
                if (isPatrolling)
                    StartCoroutine(WaitBossRest());
            }
        }
    }

    IEnumerator WaitAttack()
    {
        isAttack = true;
        canAttack = false;

        yield return new WaitForSeconds(2);

        isAttack = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
    }

    IEnumerator WaitBossRest()
    {
        isPatrolling = false;
        animator.SetBool(Constant.run, false);
        animator.SetBool(Constant.walk, false);
        animator.SetBool(Constant.idle, true);
        yield return new WaitForSeconds(8);
        animator.SetBool(Constant.idle, false);
        float x = Random.Range(pivot.x - radius, pivot.x + radius);
        float y = Random.Range(pivot.y - radius, pivot.y + radius);
        currentPositionTarget = new Vector2(x, y);

        isPatrolling = true;
    }

    private void Target()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.CompareTag(Constant.Player))
                continue;

            currentPlayerTarget = hit.gameObject;
            return;
        }

        currentPlayerTarget = null;
    }


    public void CheckPlayerInRange()
    {
        if (currentPlayerTarget == null)
            return;

        float distance = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);

        if (distance > detectRange)
        {
            currentPlayerTarget = null;
            isRun = false;
            isWalk = true;
        }
        else
        {
            isWalk = false;
            isRun = true;
        }
    }

    private void MoveToTarget(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speedAttack * Time.deltaTime);
        CalculateDirection(target);
        TransitionRunState(x, y);
    }

    void CalculateDirection(Vector2 target)
    {
        if (Mathf.Abs(target.x - transform.position.x) > Mathf.Abs(target.y - transform.position.y))
        {
            x = transform.position.x > target.x ? -1 : 1;
            y = 0;
        }
        else
        {
            x = 0;
            y = transform.position.y > target.y ? -1 : 1;
        }

    }

    void Attack()
    {
        if (isAttack)
        {
            animator.SetBool(Constant.idle, false);
            animator.SetBool(Constant.walk, false);
            animator.SetBool(Constant.run, false);
            animator.SetBool(Constant.attack, true);
        }
        else
        {
            animator.SetBool(Constant.attack, false);
        }
    }

    void TransitionRunState(float x, float y)
    {
        animator.SetFloat(Constant.x, x);
        animator.SetFloat(Constant.y, y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPositionTarget, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}