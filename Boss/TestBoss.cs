using UnityEngine;
public class TestBoss : MonoBehaviour
{

    Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        animator.SetBool(Constant.run, true);
        animator.SetFloat(Constant.x, -1);
    }
}