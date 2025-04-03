using UnityEngine;

public class BossAttackState : BossBaseState, IBossState
{
    public BossAttackState(BossController boss, Animator animator) : base(boss, animator)
    {
    }

    public override IBossState HandleTransition()
    {
        throw new System.NotImplementedException();
    }
}
