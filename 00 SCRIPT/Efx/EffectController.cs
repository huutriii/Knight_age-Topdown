using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speedMagnitude;
    bool isMove = false;
    void Update()
    {
        if (!isMove)
        {
            Vector3 distance = (player.transform.position - transform.position).normalized;
            transform.position += distance * Time.deltaTime * speedMagnitude;
            float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            if (Vector3.Distance(transform.position, player.transform.position) < 0.2f)
            {
                isMove = true;
            }
        }
    }
}
