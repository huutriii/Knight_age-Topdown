using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_skill : MonoBehaviour
{
    [SerializeField] GameObject skill1;
    List<GameObject> pools = new();
    //GameObject initSkill;
    bool countdownSkill = true;
    private void Awake()
    {
        Transform P_init = transform.Find("P_effect");
        //initSkill = P_init.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            countdownSkill = false;
            UsingSkill();
            StartCoroutine(CountDown());
            skill1.gameObject.SetActive(true);
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3);
        countdownSkill = true;
    }

    GameObject GetSkill()
    {
        foreach (GameObject skill in pools)
        {
            if (!skill.activeSelf)
                return skill;
        }
        GameObject electric = Instantiate(skill1, transform.position, Quaternion.identity);
        pools.Add(electric);
        electric.SetActive(false);
        return electric;
    }

    public void UsingSkill()
    {
        GameObject skill = GetSkill();
        skill.SetActive(true);
        skill.transform.position = transform.position;

        //initSkill.SetActive(true);
    }
}
