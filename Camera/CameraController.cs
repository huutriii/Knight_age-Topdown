using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector2 offset;
    void Start()
    {

    }

    void Update()
    {
        Vector3 pos = player.position;
        pos.z = Camera.main.transform.localPosition.z;

        transform.position = pos + (Vector3)offset;
    }
}
