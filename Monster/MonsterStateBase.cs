using System;
using UnityEngine;

public class MonsterStateBase : MonoBehaviour
{
    protected Animator _animator;
    protected string _stateName;

    public MonsterStateBase(Animator animator, string stateName) => (_animator, _stateName) = (animator, stateName);

    public void UpdateState(string stateName)
    {
        foreach (MonsterState state in Enum.GetValues(typeof(MonsterState)))
        {
            string currentState = state.ToString();
            if (currentState.Equals(stateName))
                _animator.SetBool(stateName, true);
            else
                _animator.SetBool(currentState, false);
        }
    }
}

public enum MonsterState
{
    idle,
    walk,
    run,
    hurt,
    attack
}