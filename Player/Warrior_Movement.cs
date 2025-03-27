using UnityEngine;

public class Warrior_Movement : MonoBehaviour
{
    private static Warrior_Movement instance;
    public static Warrior_Movement Instance => instance;
    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }
    [SerializeField] float velocity;
    float x, y = -1;
    public float X => x;
    public float Y => y;
    Rigidbody2D rigibody;
    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");


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


        else
        {
            rigibody.velocity = Vector2.zero;
        }
    }

    void UpdateDirection()
    {

    }
}
