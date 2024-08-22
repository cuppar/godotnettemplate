using Godot;
using System;
using GodotNetTemplate.Classes;

namespace GodotNetTemplate;

public partial class Player : CharacterBody2D, IStateMachine<Player.State>
{
    public enum State
    {
        Idle,
    }

    private StateMachine<State> _stateMachine;

    private Player()
    {
        _stateMachine = StateMachine<State>.Create(this);
    }

    public void TransitionState(State fromState, State toState)
    {
        throw new NotImplementedException();
    }

    public State GetNextState(State currentState, out bool keepCurrent)
    {
        throw new NotImplementedException();
    }

    public void TickPhysics(State currentState, double delta)
    {
        throw new NotImplementedException();
    }
}