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
        inputX = Input.GetAxisRaw(Constant.x);
        inputY = Input.GetAxisRaw(Constant.y);

        if (inputX != 0f || inputY != 0f)
        {
            animator.SetBool(Constant.run, true);
            lastInputX = inputX;
            lastInputY = inputY;
        }
        else
        {
            animator.SetBool(Constant.run, false);
            inputX = lastInputX;
            inputY = lastInputY;
        }


        animator.SetFloat(Constant.x, inputX);
        animator.SetFloat(Constant.y, inputY);
    }
}
