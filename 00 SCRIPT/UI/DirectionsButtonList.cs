using UnityEngine;
public class DirectionsButtonList : MonoBehaviour
{
    private static DirectionsButtonList instance;
    public static DirectionsButtonList Instance => instance;

    public Sprite top;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public Sprite lightTop;
    public Sprite lightDown;
    public Sprite lightLeft;
    public Sprite lightRight;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
