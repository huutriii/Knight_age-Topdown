using System.Collections;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForAnimation()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
