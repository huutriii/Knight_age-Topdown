using UnityEngine;

public class SkillMoving : MonoBehaviour
{
    [SerializeField] GameObject target;
    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //gameObject.SetActive(false);
    }
}
