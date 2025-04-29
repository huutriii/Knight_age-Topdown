using UnityEngine;

public class Flying : MonoBehaviour
{
    public float velocity = 5f;
    void Update()
    {

    }

    public void Fly(Vector3 pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, velocity * Time.deltaTime);
        Vector2 distance = (pos - transform.position).normalized;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
