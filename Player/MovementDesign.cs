using UnityEngine;

public class MovementDesign : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigi;
    [SerializeField] float _x, _y, _velocity;
    float lastX, lastY;
    Animator animator;
    bool isAttack = false;
    Transform ensnare;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ensnare = transform.Find("ensnare");
    }

    public void Update()
    {
        Moving();
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ensnare.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ensnare.gameObject.SetActive(false);
        }
    }

    void Moving()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");
        if (_x != 0 || _y != 0)
        {
            rigi.velocity = new Vector2(_x * _velocity, _y * _velocity);
            lastX = _x;
            lastY = _y;
        }
        else
        {
            rigi.velocity = Vector2.zero;
            _x = lastX;
            _y = lastY;
        }
    }

    void UpdateAnimation()
    {
        if (!isAttack)
        {
            if (rigi.velocity.x != 0 || rigi.velocity.y != 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);

            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            isAttack = true;
        }
        animator.SetFloat("Horizontal", _x);
        animator.SetFloat("Vertical", _y);
    }
}
