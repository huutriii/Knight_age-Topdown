using UnityEngine;

public class AttackState : MonsterStateBase, IMonsterState
{
    private float attackTimer;
    private const float ATTACK_DURATION = 2f;

    public AttackState(MonsterController monster, Animator animator) : base(monster, animator)
    {
        attackTimer = 0f;
    }

    public void Enter()
    {
        ResetAllAnimations();
        animator.SetBool(StateContaint.attack, true);
        monster.SetMovementLocked(true);
    }

    public void Exit()
    {
        animator.SetBool(StateContaint.attack, false);
        monster.SetMovementLocked(false);
        monster.SetAttacking(false);
    }

    public void Update()
    {
        attackTimer += Time.deltaTime;
    }

    public IMonsterState HandleTransition()
    {
        if (monster.IsDead)
            return new DiedState(monster, animator);

        if (attackTimer >= ATTACK_DURATION)
        {
            if (monster.IsTargetInRange)
                attackTimer = 0f;
            else
                return new IdleState(monster, animator);
        }

        return null;
    }
}