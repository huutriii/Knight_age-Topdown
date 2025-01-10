using UnityEngine;

public class SkillTargetDirection : MonoBehaviour
{
    [SerializeField] float speedMagnitude;
    [SerializeField] GameObject target;
    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speedMagnitude * Time.deltaTime);
        Vector3 distance = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
