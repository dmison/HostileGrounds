using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputActionAsset playerControls;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private WeaponsManager weaponsManager;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        private InputAction _attackAction;
        private InputAction _swapAction;
        private InputAction _reloadAction;
        private InputAction _throwAction;
        private InputAction _pauseAction;

        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _jumpInput;
        private bool _sprintInput;
        private bool _isAttacking;

        private void Update()
        {
            playerMovement.HandleMovement(_moveInput, _jumpInput, _sprintInput);
            playerMovement.HandleRotation(_lookInput);
            if(_isAttacking)weaponsManager.Shoot();
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _moveAction = playerControls.FindActionMap("Player").FindAction("Move");
            _moveAction.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => _moveInput = Vector2.zero;

            _lookAction = playerControls.FindActionMap("Player").FindAction("Look");
            _lookAction.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
            _lookAction.canceled += _ => _lookInput = Vector2.zero;

            _jumpAction = playerControls.FindActionMap("Player").FindAction("Jump");
            _jumpAction.performed += _ => _jumpInput = true;
            _jumpAction.canceled += _ => _jumpInput = false;

            _sprintAction = playerControls.FindActionMap("Player").FindAction("Sprint");
            _sprintAction.performed += _ => _sprintInput = true;
            _sprintAction.canceled += _ => _sprintInput = false;

            _swapAction = playerControls.FindActionMap("Player").FindAction("Swap");
            _swapAction.performed += _ => { weaponsManager.SwapWeapons(); };

            _attackAction = playerControls.FindActionMap("Player").FindAction("Attack");
            _attackAction.performed += _ => _isAttacking = true;
            _attackAction.canceled += _ => _isAttacking = false;

            _reloadAction = playerControls.FindActionMap("Player").FindAction("Reload");
            _reloadAction.performed += _ => weaponsManager.Reload();

            _throwAction = playerControls.FindActionMap("Player").FindAction("Throw");
            _throwAction.performed += _ => weaponsManager.PrepareThrow();
            _throwAction.canceled += _ => weaponsManager.ReleaseThrow();

            _pauseAction = playerControls.FindActionMap("Player").FindAction("Pause");
            _pauseAction.performed += _ => PausedMenuScreen.TogglePause();
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _lookAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();
            _attackAction.Enable();
            _reloadAction.Enable();
            _throwAction.Enable();
            _pauseAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _jumpAction.Disable();
            _sprintAction.Disable();
            _attackAction.Disable();
            _reloadAction.Disable();
            _throwAction.Disable();
            _pauseAction.Disable();
        }
    }
}
