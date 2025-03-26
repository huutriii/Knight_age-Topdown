using System.Collections;
using UnityEngine;

public class WaitComplete : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitPerform());
    }

    IEnumerator WaitPerform()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
