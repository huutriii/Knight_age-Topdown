using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] GameObject skill1;
    GameObject skill2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skill1 != null)
        {
            skill1.transform.position = transform.position;
            skill1.SetActive(true);

            Flying flying = skill1.GetComponent<Flying>();
            Vector3 target = Target.Instance.GetCurrentTarget();
            flying.Fly(target);
        }

    }
}
