//Main game-level state machine

using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;

    public State previousState { get; private set; }
    public State currentState { get; private set; }
    public State nextState { get; private set; }

    private enum TransitionState
    {
        PAUSED,
        ENTER,
        EXIT,
    }
    private TransitionState FSM = TransitionState.PAUSED;
    private Queue<State> stateQueue;

    private void Awake()
    {
        Instance = this;
        stateQueue = new Queue<State>();
    }

    private void Start()
    {
        AddStateToQueue(new GameIntroState());
        AddStateToQueue(new OpenMainMenuState());
    }

    private void Update()
    {
        if (FSM == TransitionState.ENTER)
        {
            currentState.Update();
            if (currentState.IsDoneEntering())
            {
                currentState.OnFinishEnter();
                if (stateQueue.Count > 0)
                {
                    AdvanceState(stateQueue.Dequeue());
                }
                else
                {
                    FSM = TransitionState.PAUSED;
                }
            }
        }
        else if (FSM == TransitionState.EXIT)
        {
            currentState.Update();
            if (currentState.IsDoneExiting())
            {
                currentState.OnFinishExit();
                previousState = currentState;
                currentState = nextState;
                currentState.Enter();
                FSM = TransitionState.ENTER;
            }
        }
    }

    public void AddStateToQueue(State _state)
    {
        if (stateQueue.Count == 0 && (currentState == null || FSM == TransitionState.PAUSED))
        {
            AdvanceState(_state);
        }
        else
        {
            stateQueue.Enqueue(_state);
        }
    }

    public void ForceReset()
    {
        stateQueue.Clear();
        FSM = TransitionState.PAUSED;
        previousState = null;
        currentState = null;
        nextState = null;
    }

    private void AdvanceState(State _state)
    {
        nextState = _state;
        if (currentState != null)
        {
            currentState.Exit();
            FSM = TransitionState.EXIT;
        }
        else
        {
            currentState = nextState;
            currentState.Enter();
            FSM = TransitionState.ENTER;
        }
    }
}