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

    private void Awake()
    {
        currentPositionTarget = SounthWeast.pivot;
        radius = SounthWeast.radius;
        pivot = SounthWeast.pivot;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MoveArroundArea();
        Target();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMove = false;
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("run", false);
            animator.SetBool("hurt", false);
            animator.SetBool("attack", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("run", false);
        animator.SetBool("attack", false);
        animator.SetBool("hurt", true);
        Debug.LogWarning("hit efx");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("hurt", false);
        animator.SetBool("attack", false);
        animator.SetBool("run", true);
        isMove = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, 1f);
    //}

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
        if (currentPlayerTarget != null && isMove)
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
        yield return new WaitForSeconds(2);

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

}
