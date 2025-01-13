using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    [SerializeField] GameObject target;
    float speed = 4f;
    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
