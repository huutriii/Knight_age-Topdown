public interface IBossState
{
    void Enter();
    void Exit();
    void Update();

    IBossState HandleTransition();
}
