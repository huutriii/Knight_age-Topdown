using UnityEngine;

public class Storm : MonoBehaviour
{
    float speed = 5f;
    Animator animator;
    bool isDisabling = false;
    private Vector3 initialPosition;
    private StormForm parentForm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.localPosition;
        parentForm = transform.parent?.GetComponent<StormForm>();
    }

    private void OnEnable()
    {
        isDisabling = false;
        transform.localPosition = initialPosition;
        animator.Play(0); // Reset animation
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
            if (parentForm != null)
            {
                parentForm.OnChildDeactivated();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
