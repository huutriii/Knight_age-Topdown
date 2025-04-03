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
            StartAttack();
            return;
        }

        if (isAttack)
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

    void StartAttack()
    {
        Debug.Log("StartAttack called");
        isAttack = true;
        previosState = currentState;
        TransitionState(attack);
        StartCoroutine(WaitAttack());
    }
}
