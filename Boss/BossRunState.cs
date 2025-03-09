using UnityEngine;

public class BossRunState : BossBaseState, IBossState
{
    public BossRunState(BossController boss, Animator animator) : base(boss, animator)
    {
    }

    public override IBossState HandleTransition()
    {
        throw new System.NotImplementedException();
    }
}
