using System.Collections;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    [SerializeField] AreaSO Area;
    [SerializeField] float speedArround = 0.5f;
    float radius;
    [SerializeField] float hp = 100;
    Vector2 pivot;
    [SerializeField] GameObject currentPlayerTarget;
    [SerializeField] Vector2 currentPositionTarget;
    [SerializeField]
    bool isMove = true, isPatrol = true;
    Rigidbody2D rb;
    private void Awake()
    {
        currentPositionTarget = Area.pivot;
        radius = Area.radius;
        pivot = Area.pivot;

    }
    private void OnEnable()
    {
        hp = MonsterStat.hp;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveArroundArea();
        Target();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMove = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isMove = true;
    }


    void Target()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 4f);
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

        if (target != null)
        {
            currentPlayerTarget = target;
        }
        else
        {
            currentPlayerTarget = null;
        }
    }

    void MovingAttackTarget()
    {
        if (currentPlayerTarget != null && isMove && currentPlayerTarget.CompareTag(GAME.Player))
        {
            float distance = Vector2.Distance(transform.position, currentPlayerTarget.transform.position);
            if (distance < 0.4f)
                return;

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
                if (Vector2.Distance(transform.position, currentPositionTarget) < 0.3f)
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
    IEnumerator WaitMonsterRest()
    {
        isPatrol = false;
        yield return new WaitForSeconds(8);
        float x = Random.Range(pivot.x, pivot.x + radius);
        float y = Random.Range(pivot.y, pivot.y + radius);

        currentPositionTarget = new Vector2(x, y);
        isPatrol = true;
    }
}