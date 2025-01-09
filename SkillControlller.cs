using System.Collections.Generic;
using UnityEngine;

public class SkillControlller : MonoBehaviour
{
    [SerializeField] GameObject _electric;
    List<GameObject> pools = new();
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject skill = GetSkill();
            skill.SetActive(true);
            skill.transform.position = transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameObject electric = Instantiate(_electric, transform.position, Quaternion.identity);
            electric.SetActive(true);
            electric.transform.position = transform.position;

        }
    }

    GameObject GetSkill()
    {
        foreach (GameObject skill in pools)
        {
            if (!skill.activeSelf)
                return skill;
        }
        GameObject electric = Instantiate(_electric, transform.position, Quaternion.identity);
        pools.Add(electric);
        electric.SetActive(false);
        return electric;
    }
}
