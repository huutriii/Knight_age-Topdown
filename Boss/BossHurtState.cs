using UnityEngine;

public class BossHurtState : BossBaseState, IBossState
{
    [SerializeField] float hurtDurable;
    public BossHurtState(BossController boss, Animator animator) : base(boss, animator)
    {
    }

    public override IBossState HandleTransition()
    {
        throw new System.NotImplementedException();
    }
}
