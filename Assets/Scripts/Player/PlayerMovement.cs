using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // ============================================================================== COMPONENT REFERENCES
        [Header("Movement Configuration")]
        [SerializeField] private float walkSpeed = 2.0f;
        [SerializeField] private float sprintSpeed = 4.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [Header("Look Configuration")]
        [SerializeField] private float mouseXSensitivity = 1.0f;
        [SerializeField] private float mouseYSensitivity = 1.0f;
        [SerializeField] private bool invertLook =  true;
        
        // ============================================================================= COMPONENT REFERENCES
        [Header("Component References")]
        [SerializeField] private Camera fpsCamera;   
        
        // ========================================================================================== PRIVATE
        private CharacterController _characterController;
        private Vector3 _playerVelocity = new(0,0,0);
        private bool _groundedPlayer;
        private float _verticalLook;
        private bool _sprintingPlayer;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void HandleRotation(Vector2 lookInput)
        {
            if (PausedMenuScreen.gameIsPaused || GameOver.gameIsOver)
                return;
            var rotation = lookInput.x * mouseXSensitivity;
            transform.Rotate(0, rotation,0);
            
            var verticalLookDelta = (invertLook ? 1:-1) * lookInput.y * mouseYSensitivity;
            _verticalLook = Mathf.Clamp(_verticalLook + verticalLookDelta, -80f, 80f);
            fpsCamera.transform.localRotation = Quaternion.Euler(_verticalLook, 0, 0);
            
        }
        
        public void HandleMovement(Vector3 moveInput, bool jumpInput, bool sprintInput)
        {
            _groundedPlayer = _characterController.isGrounded;
            
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }
        
            // Horizontal input
            Vector3 move = new(moveInput.x, 0, moveInput.y);
            move = transform.rotation * move;
            move = Vector3.ClampMagnitude(move , 1f); // Optional: prevents faster diagonal movement
        
            // Jump
            if (jumpInput && _groundedPlayer)
            {
                _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            }
        
            // Apply gravity
            _playerVelocity.y += gravityValue * Time.deltaTime;
        
            // Combine horizontal and vertical movement
            // player speed shouldn't change if you start or stop sprinting mid-air
            _sprintingPlayer = _groundedPlayer ? sprintInput : _sprintingPlayer;
            
            var playerSpeed = _sprintingPlayer ? sprintSpeed : walkSpeed;    
            
            var finalMove = (move * playerSpeed) + (_playerVelocity.y * Vector3.up);
            _characterController.Move(finalMove * Time.deltaTime);
        
        }
    }
}
