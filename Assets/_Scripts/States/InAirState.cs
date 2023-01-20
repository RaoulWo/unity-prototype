namespace _Scripts.States
{
    public abstract class InAirState : PlayerState
    {
        protected float Speed;
    
        protected InAirState(IPlayer player) 
            : base(player)
        { }
    
        public override void Enter()
        {
        
        }

        public override void HandleInput()
        {
        
        }

        public override void LogicUpdate()
        {

        }

        public override void PhysicsUpdate()
        {
        
        }

        public override void Exit()
        {
        
        }
    
        protected float GetSpeed()
        {
            if (Player.PreviousState == Player.RunningState)
            {
                return Player.RunningSpeed;
            }

            if (Player.PreviousState == Player.SneakingState)
            {
                return Player.SneakingSpeed;
            }

            if (Player.PreviousState == Player.WalkingState)
            {
                return Player.WalkingSpeed;
            }

            return 0f;
        }
    }
}
