using UnityEngine;

/// Used in each character's ForceMove state
public class ForceMoveTransitionInfo : CharacterStateTransitionInfo
{
    /// A function to be called once everything is in position
    public delegate void AllInPositionAction();

    public delegate void CompleteMoveAction(AllInPositionAction onAllInPosition);
    /// The function to be called once the character has reached the destination
    public CompleteMoveAction onCompleteMove;

    public Vector2 moveDestination;
    public float moveTime;
    public bool isLastWordCaster;
}