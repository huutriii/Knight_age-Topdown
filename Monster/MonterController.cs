using System.Collections;
using UnityEngine;

public class MonterController : MonoBehaviour
{
    [SerializeField] AreaSO SounthWeast;
    [SerializeField] float speedArround = 0.5f;
    Animator animator;
    float radius;
    Vector2 pivot;
    [SerializeField] GameObject currentPlayerTarget;
    [SerializeField] Vector2 currentPositionTarget;
    [SerializeField]
    bool isMove = true, isPatrol = true;
    [SerializeField] bool isHurtProcess = false;
    IMonsterState currentState;
    IMonsterState previousState;
    IdleState idleState;
    WalkState walkState;
    RunState runState;
    AttackState attackState;
    HurtState hurtState;
    private void Awake()
    {
        currentPositionTarget = SounthWeast.pivot;
        radius = SounthWeast.radius;
        pivot = SounthWeast.pivot;

    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        idleState = new IdleState(animator);
        walkState = new WalkState(animator);
        runState = new RunState(animator);
        attackState = new AttackState(animator);
        hurtState = new HurtState(animator);

        currentState = idleState;
        idleState.Enter();
    }

    void Update()
    {
        MoveArroundArea();
        Target();
        if (isPatrol && !isHurtProcess)
        {
            UpdateState(runState);
        }

        if (Vector2.Distance(transform.position, currentPositionTarget) < 0.2f && currentPlayerTarget == null && !isHurtProcess)
        {
            UpdateState(idleState);
        }

        if (isHurtProcess)
        {
            currentState = hurtState;
        }

        FollowStatus(currentState);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isMove = false;
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        UpdateState(attackState);
    //    }
    //    else
    //    {
    //        UpdateState(hurtState);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHurtProcess = true;
        if (currentState != hurtState)
        {
            StartCoroutine(WaitHurt());
            Debug.Log("hit");
        }
    }
    void Target()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 4f);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject || hit.gameObject.CompareTag("Monster")) continue;
            float distance = Vector2.Distance(transform.position, hit.gameObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = hit.gameObject;
            }
        }

        if (target != null)
        {
            currentPlayerTarget = target;
        }
        else
            currentPlayerTarget = null;
    }

    void MovingAttackTarget()
    {
        if (currentPlayerTarget != null && isMove && currentPlayerTarget.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPlayerTarget.transform.position, speedArround * Time.deltaTime);

            Vector3 scale = transform.localScale;
            scale.x = (transform.position.x < currentPlayerTarget.transform.position.x) ? 1 : -1;

            transform.localScale = scale;
        }

    }

    public void MoveArroundArea()
    {
        if (currentPlayerTarget == null)
        {
            if (isPatrol)
            {
                if (Vector2.Distance(transform.position, currentPositionTarget) < 0.1f)
                {
                    StartCoroutine(WaitMonsterRest());
                }
                else
                {
                    MoveToTarget();
                }
            }
        }
        else
        {
            MovingAttackTarget();
        }
    }
    IEnumerator WaitMonsterRest()
    {
        isPatrol = false;
        if (!isHurtProcess)
            UpdateState(idleState);
        yield return new WaitForSeconds(8);
        float x = Random.Range(pivot.x, pivot.x + radius);
        float y = Random.Range(pivot.y, pivot.y + radius);

        currentPositionTarget = new Vector2(x, y);
        isPatrol = true;
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPositionTarget, speedArround * Time.deltaTime);

        Vector3 scale = transform.localScale;
        scale.x = (transform.position.x < currentPositionTarget.x) ? 1 : -1;
        transform.localScale = scale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pivot, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentPositionTarget, 0.1f);
    }

    public void UpdateState(IMonsterState transitionState)
    {
        currentState.Exit();
        if (isHurtProcess && transitionState != hurtState)
            return;
        currentState = transitionState;
        currentState.Enter();
    }

    IEnumerator WaitHurt()
    {
        AnimatorStateInfo stateInfor = animator.GetCurrentAnimatorStateInfo(0);
        UpdateState(hurtState);
        while (stateInfor.normalizedTime < 1f)
        {
            yield return null;
            stateInfor = animator.GetCurrentAnimatorStateInfo(0);
        }
        isHurtProcess = false;
        Debug.Log("isHurt false");
    }

    void FollowStatus(IMonsterState state)
    {
        state.Enter();
    }
}