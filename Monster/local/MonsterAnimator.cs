using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator animator;
    private IMonsterState currentState;
    private MonsterController monsterController;

    public void Initialize(MonsterController controller)
    {
        monsterController = controller;
        if (monsterController == null)
        {
            Debug.LogError($"[{gameObject.name}] MonsterController reference is required!");
            return;
        }

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError($"[{gameObject.name}] Failed to find Animator component in children!");
            return;
        }

        TransitionToState(new IdleState(monsterController, animator));
    }
    public void UpdateState()
    {
        if (currentState == null) return;

        var newState = currentState.HandleTransition();
        if (newState != null && newState.GetType() != currentState.GetType())
        {
            TransitionToState(newState);
        }
    }
    protected virtual void TransitionToState(IMonsterState newState)
    {
        if (newState == null)
        {
            Debug.LogWarning($"[{gameObject.name}] Attempted to transition to a null state!");
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public Animator GetAnimator() => animator;

    public IMonsterState GetCurrentState() => currentState;
}