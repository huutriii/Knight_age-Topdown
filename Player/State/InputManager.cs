using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    public float x, y;
    public float lastX, lastY;
    public float originX, originY;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        x = Input.GetAxisRaw(Constant.x);
        y = Input.GetAxisRaw(Constant.y);

        originX = x;
        originY = y;

        if (x != 0 && y != 0)
        {
            y = 0;
        }
        else if (x != 0 || y != 0)
        {
            lastX = x;
            lastY = y;
        }
        else
        {
            x = lastX;
            y = lastY;
        }
    }

    public Vector2 GetInput() => new Vector2(x, y);
    public Vector2 GetLastInput() => new Vector2(lastX, lastY);

    public bool CanMove() => (originX == 0 && originY == 0) ? false : true;

    public Vector2 GetOriginInput() => new Vector2(originX, originY);
}
