using UnityEngine;

public interface IPlayer
{
    PlayerInputActions PlayerInputActions { get; }
    CharacterController Controller { get; }
    float AirControl { get; }
    float Gravity { get; }
    float JumpHeight { get; }
    float RunningSpeed { get; }
    float SneakingSpeed { get; }
    float WalkingSpeed { get; }
    PlayerState CurrentState { get; }
    PlayerState PreviousState { get; }
    void InitializeState(PlayerState startingPlayerState);
    void ChangeState(PlayerState newPlayerState);
}