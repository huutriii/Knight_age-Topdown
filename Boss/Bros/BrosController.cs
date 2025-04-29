using UnityEngine;

public class BrosController : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject target;
    [SerializeField] Vector2 dir;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (target != null)
        {
            dir = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 2f * Time.deltaTime);
        }
        animator.SetFloat(GAME.x, dir.x);
        animator.SetFloat(GAME.y, dir.y);

    }
}
