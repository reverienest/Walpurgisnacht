using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


// A character IS a state machine
public abstract class Character<C, S, I> : MonoBehaviour
where C : Character<C, S, I>
where S : CharacterState<C, S, I>
where I : CharacterStateInput, new()
{
    protected Dictionary<Type, S> stateMap;
    protected S state = null;
    protected I input = new I();

    protected void Start()
    {
        stateMap = new Dictionary<Type, S>();
        Init();

        //Gets all inherited classes of S
        foreach (Type type in Assembly.GetAssembly(typeof(S)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(S))))
        {
            S newState = (S)Activator.CreateInstance(type);
            newState.character = this;
            newState.Init(input);
            stateMap.Add(type, newState);
        }

        SetInitialState();
    }

    /// Initialization within the subclass
    /// Setup any initial values here
    protected virtual void Init() {}

    ///Init initial state here
    protected abstract void SetInitialState();

    protected virtual void UpdateInput() {}

    protected void LateUpdate()
    {
        UpdateInput();
        state.Update(input);
    }

    protected void FixedUpdate()
    {
        state.FixedUpdate(input);
    }

    protected void OnAnimationEvent(string eventName)
    {
        state.OnAnimationEvent(eventName);
    }

    // This can be called by any of this character's states in order to transition to a new one.
    // The optional transition info provided is passed to the new state when it is Enter()-ed.

    public void ChangeState<N>(CharacterStateTransitionInfo transitionInfo = null) where N : S
    {
        state = stateMap[typeof(N)];
        state.Enter(input, transitionInfo);
    }
}

public abstract class CharacterState<C, S, I>
where C : Character<C, S, I>
where S : CharacterState<C, S, I>
where I : CharacterStateInput, new()
{
    public Character<C, S, I> character;

    public virtual void Init(I input) {}

    public virtual void Enter(I input, CharacterStateTransitionInfo transitionInfo = null) {}

    public virtual void Update(I input) {}

    public virtual void FixedUpdate(I input) {}

    public virtual void OnAnimationEvent(string eventName) {}
}

public abstract class CharacterStateInput
{
}

/// Extend this class for custom state transition info
/// Cast it within the Enter() function of the state that uses it
public abstract class CharacterStateTransitionInfo
{
}