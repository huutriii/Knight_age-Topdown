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

    Animator animator;
    BossBaseState currentState;
    BossBaseState previosState;
    BossBaseState idle;
    BossBaseState walk;
    BossBaseState run;
    BossBaseState hurt;
    BossBaseState attack;
    BossBaseState death;
    [SerializeField] float x = 0, y = 0;

    [SerializeField] AreaSO area;
    public Vector2 pivot;
    public float radius;

    [SerializeField] float speedArround, speedAttack;
    [SerializeField] float detectRange;
    public bool isIdle;

    public bool isWalk;
    public bool isPatrolling;
    [SerializeField] float timeDuration = 8f;

    public bool isRun;
    public Vector2 currentPositionTarget;

    public GameObject currentPlayerTarget;

    public bool isHurt;
    public bool isDeath;

    private bool canAttack;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        InitialSO();

        idle = new BossIdleState(animator);
        walk = new BossWalkState(animator);
        run = new BossRunState(animator);
        hurt = new BossHurtState(animator);
        attack = new BossAttackState(animator);
        death = new BossDeathState(animator);

        currentState = idle;
    }

    void InitialSO()
    {
        pivot = area.pivot + new Vector2(0.1f, 0.1f);
        radius = area.radius;
        currentPositionTarget = pivot;
    }

    private void Update()
    {
        Target();
        CheckTargetInRange();
        UpdateBossMovement();
        UpdateBossState();
    }



    #region Movement
    private void UpdateBossMovement()
    {
        if (currentPlayerTarget != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);

            if (distanceToPlayer > 3f)
            {
                MoveToTarget(currentPlayerTarget.transform.position);
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, currentPositionTarget) > 0.3f)
            {
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

    IEnumerator WaitBossRest()
    {
        isPatrolling = false;
        Transition(idle);
        yield return new WaitForSeconds(timeDuration);
        float x = Random.Range(pivot.x - radius, pivot.x + radius);
        float y = Random.Range(pivot.y - radius, pivot.y + radius);
        Debug.Log(x + ", " + y);
        currentPositionTarget = new Vector2(x, y);
        isPatrolling = true;
    }

    private void MoveToTarget(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speedArround * Time.deltaTime);
        CalculateDirection(target);
        SetDirectionState(x, y);
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

    #endregion

    #region target

    private void Target()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.CompareTag(GAME.Player))
                continue;

            currentPlayerTarget = hit.gameObject;
            return;
        }

        currentPlayerTarget = null;
    }

    public void CheckTargetInRange()
    {
        if (currentPlayerTarget == null || !currentPlayerTarget.gameObject.activeSelf)
            return;

        float distance = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);

        if (distance > detectRange)
        {
            currentPlayerTarget = null;
        }
    }

    #endregion

    #region State
    void UpdateBossState()
    {
        if (currentPlayerTarget != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);

            if (distanceToPlayer > 3f)
            {
                Transition(run);
                canAttack = true;
            }
            else
            {
                if (canAttack)
                {
                    previosState = currentState;
                    Transition(attack);
                    StartCoroutine(WaitAttack());
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, currentPositionTarget) > 0.3f)
            {
                Transition(walk);
            }
        }
    }

    IEnumerator WaitAttack()
    {
        canAttack = false;
        AnimatorStateInfo infor = animator.GetCurrentAnimatorStateInfo(0);
        while (infor.normalizedTime < 1)
        {
            yield return null;
            infor = animator.GetCurrentAnimatorStateInfo(0);
        }
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
        Transition(previosState);
    }
    void SetDirectionState(float x, float y)
    {
        animator.SetFloat(GAME.x, x);
        animator.SetFloat(GAME.y, y);
    }
    private void Transition(BossBaseState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPositionTarget, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}