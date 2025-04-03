public interface IMonsterState
{
    void Enter();
    void Exit();
    void Update();

    IMonsterState HandleTransition();
}
