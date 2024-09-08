using Godot;
using GodotNetTemplate.Classes;

namespace GodotNetTemplate;

public partial class Player : CharacterBody2D, IStateMachine<Player.State>
{
    #region State enum

    public enum State
    {
        Idle
    }

    #endregion

    private StateMachine<State> _stateMachine;

    private Player()
    {
        _stateMachine = StateMachine<State>.Create(this);
    }

    #region IStateMachine<State> Members

    public void TransitionState(State fromState, State toState)
    {
    }

    public State GetNextState(State currentState, out bool keepCurrent)
    {
        keepCurrent = true;
        return currentState;
    }

    public void TickPhysics(State currentState, double delta)
    {
    }

    #endregion
}