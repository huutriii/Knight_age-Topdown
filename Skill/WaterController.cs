using UnityEngine;

public class WaterController : MonoBehaviour
{
    public GameObject skill;
    void Start()
    {
        Transform water = transform.Find("water_init");

        skill = water.gameObject;
    }

    void Update()
    {
        if (InputManager.Instance.v)
        {
            skill.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            skill.gameObject.SetActive(false);
        }
    }
}
