using System.Collections;
using UnityEngine;

public class BossInitSkill : MonoBehaviour
{
    GameObject init;

    private void Awake()
    {
        Transform initChild = transform.Find("BossInitAttack");
        if (initChild != null)
        {
            init = initChild.gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ActivateAndDisable());
        }
    }

    IEnumerator ActivateAndDisable()
    {
        init.SetActive(true);
        yield return new WaitForSeconds(2f);
        init.SetActive(false);
    }
}
