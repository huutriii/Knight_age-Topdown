using UnityEngine;

public class Warrior_Movement : MonoBehaviour
{
    [SerializeField] float velocity;
    float x, y = -1;
    Rigidbody2D rigibody;
    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        Vector2 movement = InputManager.Instance.GetOriginInput();
        if (movement.x != 0 && movement.y != 0)
        {
            rigibody.velocity = new Vector2(movement.x * velocity, 0);
        }
        else
        {
            rigibody.velocity = movement * velocity;
        }
    }

    void BackupMovement()
    {
        x = Input.GetAxisRaw(Constant.x);
        y = Input.GetAxisRaw(Constant.y);


        if (x != 0 && y != 0)
        {
            y = 0;
            rigibody.velocity = new Vector2(x * velocity, 0);
        }
        else if (x != 0)
        {
            rigibody.velocity = new Vector2(x * velocity, 0);
            y = 0;
        }

        else if (y != 0)
        {
            rigibody.velocity = new Vector2(0, y * velocity);
            x = 0;
        }
    }
}
