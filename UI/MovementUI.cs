using UnityEngine;
using UnityEngine.UI;

public class MovementUI : MonoBehaviour
{
    public Image topButton;
    public Image bottomButton;
    public Image leftButton;
    public Image rightButton;

    private void Start()
    {
        topButton = transform.Find("up")?.GetComponent<Image>();
        bottomButton = transform.Find("down")?.GetComponent<Image>();
        leftButton = transform.Find("left")?.GetComponent<Image>();
        rightButton = transform.Find("right")?.GetComponent<Image>();

        if (topButton == null) Debug.LogError("[MovementUI] 'up' không có Image component!");
        if (bottomButton == null) Debug.LogError("[MovementUI] 'down' không có Image component!");
        if (leftButton == null) Debug.LogError("[MovementUI] 'left' không có Image component!");
        if (rightButton == null) Debug.LogError("[MovementUI] 'right' không có Image component!");
    }

    private void Update()
    {
        if (DirectionsButtonList.Instance == null)
        {
            Debug.LogError("[MovementUI] DirectionsButtonList.Instance bị null!");
            return;
        }

        float x = Input.GetAxisRaw(Constant.x);
        float y = Input.GetAxisRaw(Constant.y);

        if (y == -1)
        {
            if (bottomButton != null) bottomButton.sprite = DirectionsButtonList.Instance.lightDown;
        }
        else if (y == 1)
        {
            if (topButton != null) topButton.sprite = DirectionsButtonList.Instance.lightTop;
        }
        else
        {
            if (topButton != null) topButton.sprite = DirectionsButtonList.Instance.top;
            if (bottomButton != null) bottomButton.sprite = DirectionsButtonList.Instance.down;
        }

        if (x == -1)
        {
            if (leftButton != null) leftButton.sprite = DirectionsButtonList.Instance.lightLeft;
        }
        else if (x == 1)
        {
            if (rightButton != null) rightButton.sprite = DirectionsButtonList.Instance.lightRight;
        }
        else
        {
            if (leftButton != null) leftButton.sprite = DirectionsButtonList.Instance.left;
            if (rightButton != null) rightButton.sprite = DirectionsButtonList.Instance.right;
        }
    }
}
