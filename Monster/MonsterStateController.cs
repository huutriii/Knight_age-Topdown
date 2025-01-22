using System.Collections;
using UnityEngine;

public class MonsterStateController : MonoBehaviour
{
    Animator animator;

    IMonsterState currentState;
    IMonsterState previousState;
    IdleState idleState;
    WalkState walkState;
    RunState runState;
    AttackState attackState;
    HurtState hurtState;
    bool isHurtProcess;
    private void Awake()
    {

        animator = GetComponent<Animator>();
        idleState = new IdleState(animator);
        walkState = new WalkState(animator);
        runState = new RunState(animator);
        attackState = new AttackState(animator);
        hurtState = new HurtState(animator);
    }

    public void UpdateState(IMonsterState transitionState)
    {
        currentState.Exit();
        if (isHurtProcess && transitionState != hurtState)
            return;
        currentState = transitionState;
        currentState.Enter();
    }
    IEnumerator WaitHurt()
    {
        AnimatorStateInfo stateInfor = animator.GetCurrentAnimatorStateInfo(0);
        UpdateState(hurtState);
        while (stateInfor.normalizedTime < 1f)
        {
            yield return null;
            stateInfor = animator.GetCurrentAnimatorStateInfo(0);
        }
        isHurtProcess = false;
        Debug.Log("isHurt false");
    }
}
