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
    public delegate void SoftTransitionAction();
    /// Call this to complete the SoftTransition
    public SoftTransitionAction softTransitionChangeState;

    protected Dictionary<Type, S> stateMap;
    protected S state = null;
    protected I input = new I();

    // NOTE: Little hack to ensure Enter is called on next update after transition
    private Trigger stateChanged;
    private CharacterStateTransitionInfo info;

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
        if (stateChanged.Value)
            state.Enter(input, info);
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

    /// This can be called by any of this character's states in order to transition to a new one.
    /// The optional transition info provided is passed to the new state when it is Enter()-ed.
    public void ChangeState<N>(CharacterStateTransitionInfo transitionInfo = null) where N : S
    {
        state?.ForceCleanUp(input);
        state = stateMap[typeof(N)];
        softTransitionChangeState = null;
        stateChanged.Value = true;
        info = transitionInfo;
    }

    /// Gives the current state a gentle warning that it should transition as soon as possible to the given state N
    public void ChangeStateSoft<N>(CharacterStateTransitionInfo transitionInfo = null) where N : S
    {
        softTransitionChangeState = () => ChangeState<N>(transitionInfo);
        state.SoftTransitionWarning(input);
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

    /// Called externally to request that a state finish ASAP and call softTransitionChangeState()
    /// The default implementation will immediately transition
    public virtual void SoftTransitionWarning(I input) { character.softTransitionChangeState(); }

    /// Called externally to alert a state that it has until this function returns to clean up any internal logic
    /// There will be a state transition immediately upon returning
    public virtual void ForceCleanUp(I input) {}

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