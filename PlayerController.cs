using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigi;
    [SerializeField] float inputX, inputY, speedMagnitude;
    float lastX, lastY;
    [SerializeField] float directionX, directionY;
    Animator animator;
    bool isAttack = false;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        Moving();
        UpdateAnimation();
    }

    void Moving()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            rigi.velocity = new Vector2(inputX * speedMagnitude, inputY * speedMagnitude);
            lastX = inputX;
            lastY = inputY;
        }
        else
        {
            rigi.velocity = Vector2.zero;
            inputX = lastX;
            inputY = lastY;
        }
    }

    void UpdateAnimation()
    {
        if (!isAttack)
        {
            if (rigi.velocity.x != 0 || rigi.velocity.y != 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);

            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            StartCoroutine(WaitAttack());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            isAttack = true;
        }
        animator.SetFloat("Horizontal", inputX);
        animator.SetFloat("Vertical", inputY);
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Monter"))
        {
        }
    }

    IEnumerator WaitAttack()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        while (currentState.normalizedTime < 1)
        {
            currentState = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        isAttack = false;
        animator.SetBool("Attack", false);
    }
}
