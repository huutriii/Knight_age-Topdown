
using System.Collections;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    Animator animator;
    GameObject currentTarget;
    [SerializeField] float radius, magnitude;
    [SerializeField] float timeDuration;
    private void Start()
    {
        animator = GetComponent<Animator>();
        radius = TowerControllerTest.Instance.Radius;
        magnitude = TowerControllerTest.Instance.Magnitude;
    }


    private void OnEnable()
    {
        StartCoroutine(WaitLightning());
    }

    private void Update()
    {
        Target();
        CheckTargetInRange();
        TargetAttack();
    }

    public void TargetAttack()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction = direction.normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);

            float scaleX = 3 * magnitude;
            Vector3 scale = new Vector3(1.5f * magnitude, magnitude, 1);
            transform.localScale = scale;
        }
    }
    void Target()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform == this.transform || !hit.gameObject.CompareTag(Constant.Player))
            {
                continue;
            }
            else
                currentTarget = hit.gameObject;
        }
    }

    void CheckTargetInRange()
    {
        if (currentTarget != null)
        {
            if (Vector2.Distance(transform.position, currentTarget.transform.position) > radius)
            {
                currentTarget = null;
            }
            else
            {
                magnitude = Vector2.Distance(transform.position, currentTarget.transform.position);
            }
        }
    }
    IEnumerator WaitLightning()
    {
        yield return new WaitForSeconds(timeDuration);
        gameObject.SetActive(false);
    }
}