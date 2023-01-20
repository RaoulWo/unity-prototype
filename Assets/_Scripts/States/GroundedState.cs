using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.States
{
    public abstract class GroundedState : PlayerState
    {
        protected GroundedState(IPlayer player) 
            : base(player)
        { }
    
        public override void Enter()
        {
            Debug.Log("Enter GroundedState");
        
            Player.PlayerInputActions.Player.Jump.performed += OnJump;
        }

        public override void HandleInput()
        {
        
        }

        public override void LogicUpdate()
        {
            if (!Player.IsGrounded)
            {
                Player.ChangeState(Player.FallingState);
            }
        }

        public override void PhysicsUpdate()
        {

        }

        public override void Exit()
        {
            Debug.Log("Exit GroundedState");

            Player.PlayerInputActions.Player.Jump.performed -= OnJump;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            Player.ChangeState(Player.JumpingState);
        }
    }
}
