using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    float inputX, inputY;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = UnityEngine.Input.GetAxisRaw("Horizontal");
        inputY = UnityEngine.Input.GetAxisRaw("Vertical");
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
