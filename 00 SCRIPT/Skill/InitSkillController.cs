using System.Collections;
using UnityEngine;

public class InitSkillController : MonoBehaviour
{
    Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WaitInit());
    }

    IEnumerator WaitInit()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
