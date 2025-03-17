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
        animator.SetBool(TagManager.run, true);
        animator.SetFloat(TagManager.x, -1);
    }
}