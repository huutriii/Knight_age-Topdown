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
    Warrior_Attack_Reverse attackReverse;

    [Header("Flag state controller")]
    public bool isAttack;
    public bool isAttackReverse;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        run = new Warrior_Run(animator);
        idle = new Warrior_Idle(animator);
        attack = new Warrior_Attack(animator);
        attackReverse = new Warrior_Attack_Reverse(animator);
        currentState = idle;
        currentState.Enter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartAttack();
            return;
        }

        if (InputManager.Instance.v)
        {
            StartAttackReverse();
            return;
        }

        if (isAttack || isAttackReverse)
        {
            return;
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
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        while (info.normalizedTime < 1f)
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        isAttack = false;
        TransitionState(previosState);
    }

    private void UpdateDirection()
    {
        animator.SetFloat(GAME.x, InputManager.Instance.x);
        animator.SetFloat(GAME.y, InputManager.Instance.y);
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

    void StartAttack()
    {
        isAttack = true;
        previosState = currentState;
        TransitionState(attack);
        StartCoroutine(WaitAttack());
    }

    void StartAttackReverse()
    {
        isAttackReverse = true;
        previosState = currentState;
        TransitionState(attackReverse);
        StartCoroutine(WaitAttackReverse());
    }

    IEnumerator WaitAttackReverse()
    {
        yield return new WaitForSeconds(0.1f);
        AnimatorStateInfo infor = animator.GetCurrentAnimatorStateInfo(0);

        while (infor.normalizedTime < 1f)
        {
            infor = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        isAttackReverse = false;
        TransitionState(previosState);
    }
}
