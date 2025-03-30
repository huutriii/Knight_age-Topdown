using System.Collections;
using UnityEngine;

public class WaterInit : MonoBehaviour
{
    public GameObject Water;
    private void OnEnable()
    {
        StartCoroutine(WaitInit());
    }

    private void OnDisable()
    {
        Water.SetActive(true);
    }

    IEnumerator WaitInit()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
