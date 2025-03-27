
using System.Collections;
using UnityEngine;

public class WarriorStateController : MonoBehaviour
{
    Animator animator;
    WarriorStateBase currentState;
    WarriorStateBase previosState;
    Warrior_Run run;
    Warrior_Idle idle;
    Warrior_Attack attack;

    [SerializeField] float _x, _y, lastX, lastY;

    [Header("Flag state controller")]
    public bool isAttack;
    private void Start()
    {
        animator = GetComponent<Animator>();
        run = new Warrior_Run(animator);
        idle = new Warrior_Idle(animator);
        attack = new Warrior_Attack(animator);
        currentState = idle;
        currentState.Enter();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            previosState = currentState;
            TransitionState(attack);
            StartCoroutine(WaitAttack());
        }

        UpdateInput();
        UpdateDirection();
        UpdateMovementState();
    }

    void UpdateInput()
    {
        _x = Input.GetAxisRaw(TagManager.x);
        _y = Input.GetAxisRaw(TagManager.y);
        if (_x != 0 && _y != 0)
        {
            _y = 0;
        }
        if (_x != 0 || _y != 0)
        {
            lastX = _x;
            lastY = _y;

        }
        else
        {
            _x = lastX;
            _y = lastY;
        }
    }

    void TransitionState(WarriorStateBase newState)
    {
        if (currentState != null && newState != null)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }

    IEnumerator WaitAttack()
    {
        AnimatorStateInfo infor = animator.GetCurrentAnimatorStateInfo(0);
        while (infor.normalizedTime < 1f && infor.normalizedTime > 0)
        {
            infor = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        TransitionState(previosState);
    }


    private void UpdateDirection()
    {
        animator.SetFloat(TagManager.x, _x);
        animator.SetFloat(TagManager.y, _y);
    }

    void UpdateMovementState()
    {
        if (Warrior_Movement.Instance.X != 0 || Warrior_Movement.Instance.Y != 0)
        {
            TransitionState(run);
        }
        else
        {
            TransitionState(idle);
        }
    }
}
