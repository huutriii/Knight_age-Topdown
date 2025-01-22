using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator animator;
    float inputX, inputY;
    float lastInputX, lastInputY;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0f || inputY != 0f)
        {
            animator.SetBool("Run", true);
            lastInputX = inputX;
            lastInputY = inputY;
        }
        else
        {
            animator.SetBool("Run", false);
            inputX = lastInputX;
            inputY = lastInputY;
        }


        animator.SetFloat("Horizontal", inputX);
        animator.SetFloat("Vertical", inputY);
    }
}
