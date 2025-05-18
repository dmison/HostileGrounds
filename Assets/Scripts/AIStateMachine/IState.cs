using AIStateMachine.AIStates;

namespace AIStateMachine
{
    public interface IState
    {
        StateId GetId();
        void StateEnter(AiAgent agent);
        void StateUpdate(AiAgent agent);
        void StateExit(AiAgent agent);
    }
}
