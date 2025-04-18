using System.Collections;
using UnityEngine;

public class ProjectileAttackController : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject target;
    [SerializeField] GameObject explosion;
    bool isFly;

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("init");
        StartCoroutine(WaitInit());
        isFly = true;
    }

    private void Update()
    {
        if (isFly && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3 * Time.deltaTime);

            Vector2 distance = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                isFly = false;
                animator.ResetTrigger("init");
                animator.ResetTrigger("flying");
                animator.Play("impact", 0, 0f);
                StartCoroutine(HandleExplosion());
            }
        }
    }

    private IEnumerator HandleExplosion()
    {
        yield return new WaitForSeconds(1f);
        explosion.transform.rotation = Quaternion.identity;
        explosion.gameObject.SetActive(true);
        StartCoroutine(WaitExplosion());
    }

    IEnumerator WaitExplosion()
    {
        yield return new WaitForSeconds(1.3f);
        gameObject.SetActive(false);
    }

    IEnumerator WaitInit()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("flying");
    }
}
