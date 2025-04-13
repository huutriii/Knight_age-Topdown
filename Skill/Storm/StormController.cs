using System.Collections;
using UnityEngine;

public class StormController : MonoBehaviour
{
    public GameObject init;
    public GameObject form;
    Animator animatorInit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(WaitInit());
        }
    }

    IEnumerator WaitInit()
    {
        init.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(WaitForm());

    }

    IEnumerator WaitForm()
    {
        form.SetActive(true);
        yield return new WaitForSeconds(2);
    }
}
