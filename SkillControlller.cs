using UnityEngine;

public class SkillControlller : MonoBehaviour
{
    [SerializeField] GameObject electric;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject electric = Instantiate(this.electric, transform.position, Quaternion.identity);
            electric.SetActive(true);
            electric.transform.position = transform.position;
        }
    }
}
