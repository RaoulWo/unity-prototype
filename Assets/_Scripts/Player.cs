using _Scripts.States;
using UnityEngine;

namespace _Scripts
{
    public class Player : MonoBehaviour, IPlayer
    {
        public PlayerInputActions PlayerInputActions => _playerInputActions;
        private PlayerInputActions _playerInputActions;
    
        public CharacterController Controller => _controller;
        private CharacterController _controller;

        public Transform Transform => this.transform;
    
        public float AirControl => airControl;
        public float Gravity => gravity;
        public float JumpHeight => jumpHeight;
        public float RunningSpeed => runningSpeed;
        public float SneakingSpeed => sneakingSpeed;
        public float WalkingSpeed => walkingSpeed;
        public bool IsGrounded => _controller.isGrounded;

        [Header("PLAYER MOVEMENT")]
        [SerializeField] private float airControl = 5f;
        [SerializeField] private float gravity = 10f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float runningSpeed = 10f;
        [SerializeField] private float sneakingSpeed = 2f;
        [SerializeField] private float walkingSpeed = 5f;
    
        public PlayerState CurrentState => _currentState;
        public PlayerState PreviousState => _previousState;
        private PlayerState _currentState;
        private PlayerState _previousState;

        public CrouchingState CrouchingState { get; private set; }
        public FallingState FallingState { get; private set; }
        public JumpingState JumpingState { get; private set; }
        public RunningState RunningState { get; private set; }
        public SneakingState SneakingState { get; private set; }
        public StandingState StandingState { get; private set; }
        public WalkingState WalkingState { get; private set; }

        private bool _stateIsInitialized;

        private void Awake()
        {
            // Cache a reference to the CharacterController
            _controller = GetComponent<CharacterController>();
        
            // Instantiate a new instance of the input action asset
            _playerInputActions = new PlayerInputActions();
        
            // Instantiate the player states
            CrouchingState = new CrouchingState(this);
            FallingState = new FallingState(this);
            JumpingState = new JumpingState(this);
            RunningState = new RunningState(this);
            SneakingState = new SneakingState(this);
            StandingState = new StandingState(this);
            WalkingState = new WalkingState(this);

            // Initialize the player in StandingState
            InitializeState(StandingState);
        }

        private void Update()
        {
            _currentState.HandleInput();
            _currentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _currentState.PhysicsUpdate();
        }

        private void OnEnable()
        {
            // Enable the input actions
            _playerInputActions.Player.Move.Enable();
            _playerInputActions.Player.Jump.Enable();
            _playerInputActions.Player.Crouch.Enable();
            _playerInputActions.Player.Run.Enable();
        }

        private void OnDisable()
        {
            // Disable the input actions
            _playerInputActions.Player.Move.Disable();
            _playerInputActions.Player.Jump.Disable();
            _playerInputActions.Player.Crouch.Disable();
            _playerInputActions.Player.Run.Disable();
        }

        public void InitializeState(PlayerState startingPlayerState)
        {
            if (_stateIsInitialized) return;
            if (startingPlayerState == null) return;
        
            _currentState = startingPlayerState;
            _currentState.Enter();

            _stateIsInitialized = true;
        }

        public void ChangeState(PlayerState newPlayerState)
        {
            if (!_stateIsInitialized) return;
            if (newPlayerState == null) return;
        
            _currentState.Exit();

            _previousState = _currentState;
            _currentState = newPlayerState;

            _currentState.Enter();
        }
    }
}
