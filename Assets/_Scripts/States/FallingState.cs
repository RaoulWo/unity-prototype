using UnityEngine;

namespace _Scripts.States
{
    public class FallingState : InAirState
    {
        public FallingState(IPlayer player) 
            : base(player)
        { }

        private Vector3 _movementDir;
        private Vector2 _movementInput;
    
        public override void Enter()
        {
            Debug.Log("Enter FallingState");
        
            Speed = GetSpeed();
        
            // Start the fall
            Fall();
        }

        public override void HandleInput()
        {
            // Store value of the input action Move
            _movementInput = Player.PlayerInputActions.Player.Move.ReadValue<Vector2>();
        }

        public override void LogicUpdate()
        {
            if (Player.IsGrounded && Player.Controller.velocity.x == 0f && Player.Controller.velocity.z == 0f)
            {
                if (Player.PreviousState == Player.CrouchingState || Player.PreviousState == Player.SneakingState)
                {
                    Player.ChangeState(Player.CrouchingState);
                }
            
                Player.ChangeState(Player.StandingState);
            }
            else if (Player.IsGrounded && Player.Controller.velocity != Vector3.zero)
            {
                if (Player.PreviousState == Player.StandingState || Player.PreviousState == Player.WalkingState)
                {
                    Player.ChangeState(Player.WalkingState);
                }
        
                if (Player.PreviousState == Player.CrouchingState || Player.PreviousState == Player.SneakingState)
                {
                    Player.ChangeState(Player.SneakingState);
                }
        
                if (Player.PreviousState == Player.RunningState)
                {
                    Player.ChangeState(Player.RunningState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            // Calculate the desired direction
            var desiredDir = new Vector3(_movementInput.x, _movementDir.y, _movementInput.y);
        
            // Linearly interpolate between current direction and desired direction and update _movementDir
            _movementDir = Vector3.Lerp(_movementDir, desiredDir, Player.AirControl * Time.deltaTime);

            // Apply gravity over time to the y component of _movementDir
            _movementDir.y -= Player.Gravity * Time.deltaTime;
        
            // Calculate the movement vector
            var movementVector = _movementDir * Time.deltaTime;

            // Move the player controller
            Player.Controller.Move(movementVector);
        }

        public override void Exit()
        {
            Debug.Log("Exit FallingState");

        }

        private void Fall()
        {
            // Cache the value of the input action Move
            var movementInput = Player.PlayerInputActions.Player.Move.ReadValue<Vector2>();

            // Store the movement direction
            _movementDir = new Vector3(movementInput.x * Speed, 0f,
                movementInput.y * Speed);

            // Calculate the movement vector
            var movementVector = _movementDir * Time.deltaTime;
        
            // Transform the vector from local to global coordinates
            movementVector = Player.Transform.TransformDirection(movementVector);
        
            // Move the player controller
            Player.Controller.Move(movementVector);
        }
    }
}
