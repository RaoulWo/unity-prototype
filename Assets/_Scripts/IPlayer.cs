using UnityEngine;

public interface IPlayer
{
    PlayerInputActions PlayerInputActions { get; }
    CharacterController Controller { get; }
    Transform Transform { get; }
    float AirControl { get; }
    float Gravity { get; }
    float JumpHeight { get; }
    float RunningSpeed { get; }
    float SneakingSpeed { get; }
    float WalkingSpeed { get; }
    PlayerState CurrentState { get; }
    PlayerState PreviousState { get; }
    public CrouchingState CrouchingState { get; }

    public JumpingState JumpingState { get; }

    public RunningState RunningState { get; }

    public SneakingState SneakingState { get; }

    public StandingState StandingState { get; }

    public WalkingState WalkingState { get; }
    void InitializeState(PlayerState startingPlayerState);
    void ChangeState(PlayerState newPlayerState);
}