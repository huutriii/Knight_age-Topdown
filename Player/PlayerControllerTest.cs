using System.Collections;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigi;
    [SerializeField] float inputX, inputY, speedMagnitude;
    float lastX, lastY;
    [SerializeField] float directionX, directionY;
    Animator animator;
    bool isAttack = false;
    [SerializeField] GameObject atkEffect;
    [SerializeField] GameObject atkHitEffect;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        Moving();
        UpdateAnimation();
    }

    void Moving()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            rigi.velocity = new Vector2(inputX * speedMagnitude, inputY * speedMagnitude);
            lastX = inputX;
            lastY = inputY;
        }
        else
        {
            rigi.velocity = Vector2.zero;
            inputX = lastX;
            inputY = lastY;
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
        else
        {
            StartCoroutine(WaitAttack());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            GameObject atkEfx = Instantiate(this.atkEffect, transform.position, Quaternion.identity);
            atkEffect.SetActive(true);
            atkEffect.transform.position = transform.position;
            isAttack = true;
        }
        animator.SetFloat("Horizontal", inputX);
        animator.SetFloat("Vertical", inputY);
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GAME.Monster))
        {
            GameObject atkEfx = Instantiate(atkHitEffect, transform.position, Quaternion.identity);
            atkEffect.SetActive(true);
            atkEffect.transform.position = transform.position;
        }
    }

    IEnumerator WaitAttack()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        while (currentState.normalizedTime < 1)
        {
            currentState = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        isAttack = false;
        animator.SetBool("Attack", false);
    }
}
