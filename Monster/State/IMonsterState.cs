public interface IMonsterState
{
    void Enter();
    void Exit();

    IMonsterState HandleTransition();
}
