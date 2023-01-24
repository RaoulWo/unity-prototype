using _Scripts.Hex;
using _Scripts.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class Player : MonoBehaviour, IPlayer
    {
        public PlayerInputActions PlayerInputActions => _playerInputActions;
        private PlayerInputActions _playerInputActions;
    
        public CharacterController Controller => _controller;
        private CharacterController _controller;

        public Transform Transform => transform;
    
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

        public GameObject tileMapObj;
        public TileMap tileMap;

        private Tile previousHoveredTile;
        private Tile currentHoveredTile;

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

            
            
            tileMap = tileMapObj.GetComponent<TileMap>();
        }

        private void Update()
        {
            _currentState.HandleInput();
            _currentState.LogicUpdate();

            var lookInput = _playerInputActions.Player.Look.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(lookInput);
            if (Physics.Raycast(ray, out var hit, 100))
            {
                var pos = hit.transform.position;
                
                var fractionalHexPos = tileMap.Layout.PosToFractionalHex(new Vector2(pos.x, pos.z));
                var hexPos = fractionalHexPos.RoundToHex();

                Debug.Log((hexPos.Q, hexPos.R, hexPos.S));

                previousHoveredTile = currentHoveredTile;
                currentHoveredTile = tileMap.Get((hexPos.Q, hexPos.R));

                if (previousHoveredTile != null)
                    previousHoveredTile.GetComponent<MeshRenderer>().material = previousHoveredTile.gray;
                if (currentHoveredTile != null)
                    currentHoveredTile.GetComponent<MeshRenderer>().material = currentHoveredTile.yellow;
            }
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
            _playerInputActions.Player.Look.Enable();
        }

        private void OnDisable()
        {
            // Disable the input actions
            _playerInputActions.Player.Move.Disable();
            _playerInputActions.Player.Jump.Disable();
            _playerInputActions.Player.Crouch.Disable();
            _playerInputActions.Player.Run.Disable();
            _playerInputActions.Player.Look.Disable();
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
