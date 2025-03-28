
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
        UpdateDirection();
        UpdateMovementState();
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
        animator.SetFloat(Constant.x, InputManager.Instance.x);
        animator.SetFloat(Constant.y, InputManager.Instance.y);
    }

    void UpdateMovementState()
    {
        if (InputManager.Instance.CanMove())
        {
            TransitionState(run);
        }
        else
        {
            TransitionState(idle);
        }
    }
}
