using System.Collections;
using UnityEngine;

public class StormInit : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitOnComplete());
    }

    IEnumerator WaitOnComplete()
    {
        AnimatorStateInfo infor = animator.GetCurrentAnimatorStateInfo(0);
        while (infor.normalizedTime < 1)
        {
            yield return null;
            infor = animator.GetCurrentAnimatorStateInfo(0);
        }
        gameObject.SetActive(false);
    }
}
