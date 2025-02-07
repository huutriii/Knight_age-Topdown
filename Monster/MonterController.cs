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
    IMonsterState currentState;
    IdleState idleState;
    WalkState walkState;
    RunState runState;
    AttackState attackState;
    HurtState hurtState;
    DiedState diedState;
    [SerializeField] bool isCorotineRunning = false;
    bool isHurt = false;
    bool isAttack = false;
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
        diedState = new DiedState(animator);
        currentState = idleState;
        idleState.Enter();
    }

    void Update()
    {
        MoveArroundArea();
        Target();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHurt && !isAttack)
        {
            UpdateState(attackState);

            isMove = false;
            isAttack = true;

            if (!isCorotineRunning)
            {
                StartCoroutine(WaitAttack());
            }
        }
    }

    IEnumerator WaitAttack()
    {
        isCorotineRunning = true;

        yield return new WaitForSeconds(2);
        if (isAttack)
        {
            UpdateState(attackState);
        }
        else
        {
            isAttack = false;
        }
        isCorotineRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != hurtState)
        {
            StartCoroutine(WaitHurt());
        }
        Debug.Log("hit");
        currentState = hurtState;
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
        yield return new WaitForSeconds(8);
        float x = Random.Range(pivot.x, pivot.x + radius);
        float y = Random.Range(pivot.y, pivot.y + radius);

        currentPositionTarget = new Vector2(x, y);
        isPatrol = true;
    }

    IEnumerator Died()
    {
        isCorotineRunning = true;
        UpdateState(diedState);

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
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
        currentState = transitionState;
        currentState.Enter();
    }

    IEnumerator WaitHurt()
    {
        isCorotineRunning = true;
        yield return new WaitForSeconds(2);
        isHurt = false;

        if (isAttack)
            UpdateState(attackState);
        isCorotineRunning = false;
    }
}