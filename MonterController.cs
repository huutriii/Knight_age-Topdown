using UnityEngine;

public class MonterController : MonoBehaviour
{
    [SerializeField] Vector3 position;
    [SerializeField] float maxX, minX;
    [SerializeField] float speed = 1f;
    Animator animator;

    [SerializeField] GameObject currentTarget;
    [SerializeField] bool isMove = true;
    void Start()
    {
        position.x = minX;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Moving();
    }

    private void FixedUpdate()
    {
        Target();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("run", false);
        animator.SetBool("hurt", true);
        Debug.Log("hit");
        isMove = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("hurt", false);
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

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject) continue;
            float distance = Vector2.Distance(transform.position, hit.gameObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = hit.gameObject;
            }
        }

        if (target != null)
        {
            currentTarget = target;
        }
        else
            currentTarget = null;
    }

    void Moving()
    {
        if (isMove && currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);

            Vector3 scale = transform.localScale;
            if (transform.position.x < currentTarget.transform.position.x)
                scale.x = 1;
            else
                scale.x = -1;

            transform.localScale = scale;
        }

    }

}
