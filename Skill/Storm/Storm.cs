using System.Collections;
using UnityEngine;

public class Storm : MonoBehaviour
{
    float speed = 5f;
    Animator animator;
    bool isDisabling = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDisabling)
            return;

        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            isDisabling = true;
            StartCoroutine(DisableStorm());
        }
    }

    private void OnDisable()
    {
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    IEnumerator DisableStorm()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        yield return null;
    }
}
