using UnityEngine;

public class HurtState : MonsterStateBase, IMonsterState
{
    private float hurtTimer;
    private const float HURT_DURATION = 2f;

    public HurtState(MonsterController monster, Animator animator) : base(monster, animator)
    {
        hurtTimer = 0f;
    }

    public void Enter()
    {
        ResetAllAnimations();
        animator.SetBool(STATE.hurt, true);
        monster.SetMovementLocked(true);
    }

    public void Exit()
    {
        animator.SetBool(STATE.hurt, false);
        monster.SetMovementLocked(false);
        monster.SetHurt(false);
    }

    public void Update()
    {
        hurtTimer += Time.deltaTime;
    }

    public IMonsterState HandleTransition()
    {
        if (monster.IsDead)
            return new DiedState(monster, animator);

        if (hurtTimer >= HURT_DURATION)
        {
            if (monster.IsAttacking)
                return new AttackState(monster, animator);
            return new IdleState(monster, animator);
        }

        return null;
    }
}