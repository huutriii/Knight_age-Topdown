using UnityEngine;

public class BossDeathState : BossBaseState, IBossState
{
    public BossDeathState(BossController boss, Animator animator) : base(boss, animator)
    {
    }

    public override IBossState HandleTransition()
    {
        throw new System.NotImplementedException();
    }
}
