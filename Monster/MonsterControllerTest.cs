//using System.Collections;
//using UnityEngine;

//public class MonterControllerTest : MonoBehaviour
//{
//    [SerializeField] AreaSO Area;
//    [SerializeField] float speedArround = 0.5f;
//    [SerializeField] float distanceTarget = 4f;
//    float radius;
//    [SerializeField] float hp = 100;
//    Vector2 pivot;
//    [SerializeField] GameObject currentPlayerTarget;
//    [SerializeField] Vector2 currentPositionTarget;
//    [SerializeField]
//    bool isMove = true, isPatrol = true;
//    #region State
//    Animator animator;
//    IMonsterState currentState;
//    IdleState idleState;
//    WalkState walkState;
//    RunState runState;
//    AttackState attackState;
//    HurtState hurtState;
//    DiedState diedState;
//    [SerializeField] bool isCorotineRunning = false;
//    bool isHurt = false;
//    bool isAttack = false;

//    #endregion
//    private void Awake()
//    {
//        currentPositionTarget = Area.pivot;
//        radius = Area.radius;
//        pivot = Area.pivot;

//    }
//    private void OnEnable()
//    {
//        hp = MonsterStat.hp;
//    }
//    void Start()
//    {
//        animator = GetComponentInChildren<Animator>();

//        idleState = new IdleState(animator);
//        walkState = new WalkState(animator);
//        runState = new RunState(animator);
//        attackState = new AttackState(animator);
//        hurtState = new HurtState(animator);
//        diedState = new DiedState(animator);
//        currentState = idleState;
//        idleState.Enter();
//    }

//    void Update()
//    {
//        MoveArroundArea();
//        Target();
//        UpdateState();
//        if (hp <= 0)
//        {
//            StartCoroutine(Died());
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (!collision.gameObject.CompareTag(TagManager.Player))
//            return;
//        if (!isHurt && !isAttack)
//        {
//            animator.SetBool(StateContaint.run, false);
//            animator.SetBool(StateContaint.idle, false);
//            animator.SetBool(StateContaint.hurt, false);
//            animator.SetBool(StateContaint.attack, true);
//            isMove = false;
//            isAttack = true;

//            if (!isCorotineRunning)
//            {
//                StartCoroutine(WaitAttack());
//            }
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        isAttack = false;
//        isMove = true;
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (!isHurt && !isAttack)  // Không thể chuyển sang hurt khi đang tấn công
//        {
//            isHurt = true;
//            animator.SetBool(StateContaint.run, false);
//            animator.SetBool(StateContaint.idle, false);
//            animator.SetBool(StateContaint.attack, false);
//            animator.SetBool(StateContaint.hurt, true);

//            if (!isCorotineRunning)
//            {
//                StartCoroutine(WaitHurt());
//            }
//        }
//        hp -= 30;
//    }

//    IEnumerator WaitHurt()
//    {
//        isCorotineRunning = true;
//        yield return new WaitForSeconds(2f); // Thời gian bị thương

//        isHurt = false;
//        animator.SetBool(StateContaint.hurt, false);

//        // Kiểm tra nếu quái vật đang tấn công và bị thương, thì tiếp tục tấn công khi hết hurt
//        if (isAttack)
//        {
//            animator.SetBool(StateContaint.attack, true);
//        }

//        isCorotineRunning = false;
//    }

//    IEnumerator Died()
//    {
//        isCorotineRunning = true;
//        animator.SetBool(StateContaint.run, false);
//        animator.SetBool(StateContaint.idle, false);
//        animator.SetBool(StateContaint.hurt, false);
//        animator.SetBool(StateContaint.attack, false);
//        animator.SetBool(StateContaint.died, true);
//        yield return new WaitForSeconds(2);
//        this.gameObject.SetActive(false);

//        SouthestController.Instance.ReSpawnMonster(this.gameObject, pivot);
//    }

//    IEnumerator WaitAttack()
//    {
//        isCorotineRunning = true;
//        yield return new WaitForSeconds(2f); // Thời gian tấn công

//        // Kiểm tra nếu quái vật còn va chạm với player, tiếp tục tấn công
//        if (isAttack)
//        {
//            animator.SetBool(StateContaint.attack, true);
//        }
//        else
//        {
//            isAttack = false;
//            animator.SetBool(StateContaint.attack, false);
//        }
//        isCorotineRunning = false;
//    }

//    void Target()
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

//        if (target != null)
//        {
//            currentPlayerTarget = target;
//        }
//        else
//        {
//            currentPlayerTarget = null;
//        }
//    }

//    void MovingAttackTarget()
//    {
//        if (currentPlayerTarget != null && isMove && currentPlayerTarget.CompareTag(TagManager.Player))
//        {
//            transform.position = Vector3.MoveTowards(transform.position, currentPlayerTarget.transform.position, speedArround * Time.deltaTime);

//            Vector3 scale = transform.localScale;
//            scale.x = (transform.position.x < currentPlayerTarget.transform.position.x) ? 1 : -1;

//            transform.localScale = scale;
//        }
//    }

//    public void MoveArroundArea()
//    {
//        if (currentPlayerTarget == null)
//        {
//            if (isPatrol)
//            {
//                if (Vector2.Distance(transform.position, currentPositionTarget) < 0.3f)
//                {
//                    StartCoroutine(WaitMonsterRest());
//                }
//                else
//                {
//                    MoveToTarget();
//                }
//            }
//        }
//        else
//        {
//            MovingAttackTarget();
//        }
//    }

//    void MoveToTarget()
//    {
//        transform.position = Vector2.MoveTowards(transform.position, currentPositionTarget, speedArround * Time.deltaTime);

//        Vector3 scale = transform.localScale;
//        scale.x = (transform.position.x < currentPositionTarget.x) ? 1 : -1;
//        transform.localScale = scale;
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(pivot, radius);
//        Gizmos.color = Color.red;
//        Gizmos.DrawSphere(currentPositionTarget, 0.1f);
//    }

//    void UpdateState()
//    {
//        if (!isHurt && !isAttack)
//        {
//            if (Vector2.Distance(currentPositionTarget, transform.position) > 0.3f)
//            {
//                animator.SetBool(StateContaint.idle, false);
//                animator.SetBool(StateContaint.hurt, false);
//                animator.SetBool(StateContaint.attack, false);
//                animator.SetBool(StateContaint.run, true);
//            }
//            else
//            {
//                animator.SetBool(StateContaint.hurt, false);
//                animator.SetBool(StateContaint.run, false);
//                animator.SetBool(StateContaint.attack, false);
//                animator.SetBool(StateContaint.idle, true);
//            }
//        }
//    }
//    IEnumerator WaitMonsterRest()
//    {
//        isPatrol = false;
//        yield return new WaitForSeconds(8);
//        float x = Random.Range(pivot.x, pivot.x + radius);
//        float y = Random.Range(pivot.y, pivot.y + radius);

//        currentPositionTarget = new Vector2(x, y);
//        isPatrol = true;
//    }

//    private void OnDisable()
//    {
//        if (TargetClosit.Instance != null && TargetClosit.Instance.GetTarget() == transform)
//        {
//            TargetClosit.Instance.ClearTarget();
//        }
//    }
//}
