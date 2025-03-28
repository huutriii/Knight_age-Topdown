using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    float inputX, inputY;
    Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = UnityEngine.Input.GetAxisRaw(Constant.x);
        inputY = UnityEngine.Input.GetAxisRaw(Constant.y);
        if (inputX != 0)
        {
            if (inputX == -1)
            {
                animator.SetBool("run_right", false);
                animator.SetBool("run_top", false);
                animator.SetBool("run_down", false);
                animator.SetBool("run_left", true);
            }
            else
            {
                animator.SetBool("run_top", false);
                animator.SetBool("run_down", false);
                animator.SetBool("run_left", false);
                animator.SetBool("run_right", true);
            }
        }

        if (inputY != 0)
        {
            if (inputY == -1)
            {
                animator.SetBool("run_left", false);
                animator.SetBool("run_right", false);
                animator.SetBool("run_top", false);
                animator.SetBool("run_down", true);
            }
            else
            {
                animator.SetBool("run_left", false);
                animator.SetBool("run_right", false);
                animator.SetBool("run_down", false);
                animator.SetBool("run_top", true);
            }
        }
    }
}
