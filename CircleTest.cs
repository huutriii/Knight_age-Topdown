using UnityEngine;

public class CircleTest : MonoBehaviour
{
    Rigidbody2D rigi;
    float speed = 5f;
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            rigi.velocity = new Vector2(inputX * speed, inputY * speed);
        }
        else
        {
            rigi.velocity = Vector2.zero;
        }
    }
}
