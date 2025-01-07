using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigi;
    [SerializeField] float inputX, inputY, speedMagnitude;
    [SerializeField] float directionX, directionY;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Moving();
    }

    void Moving()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0)
        {
            //UpdateDirectionX(inputX);
            rigi.velocity = new Vector2(inputX * speedMagnitude, rigi.velocity.y);
        }
        else
        {
            rigi.velocity = new Vector2(0, rigi.velocity.y);
        }

        if (inputY != 0)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, inputY * speedMagnitude);
        }
        else
        {
            rigi.velocity = new Vector2(rigi.velocity.x, 0);
        }
    }

    void UpdateDirectionX(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
    void UpdateDirectionY(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y) * direction;
        transform.localScale = scale;
    }
}
