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
        inputX = Input.GetAxisRaw(TagManager.x);
        inputY = Input.GetAxisRaw(TagManager.y);

        if (inputX != 0f || inputY != 0f)
        {
            animator.SetBool(TagManager.run, true);
            lastInputX = inputX;
            lastInputY = inputY;
        }
        else
        {
            animator.SetBool(TagManager.run, false);
            inputX = lastInputX;
            inputY = lastInputY;
        }


        animator.SetFloat(TagManager.x, inputX);
        animator.SetFloat(TagManager.y, inputY);
    }
}
