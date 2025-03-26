
using UnityEngine;

public class WarriorStateController : MonoBehaviour
{
    Animator animator;
    public WarriorStateBase currentState;
    public Warrior_Run run;
    public Warrior_Idle idle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        run = new Warrior_Run(animator);
        idle = new Warrior_Idle(animator);
        currentState = idle;
        currentState.Enter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentState != null)
            {
                TransitionState(run);
                Debug.Log('c');

            }

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TransitionState(idle);
            Debug.Log('i');
        }

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

}
