using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private bool invertMouseX;
        [SerializeField] private bool invertMouseY;
        [SerializeField] private bool invertRoll;
        
        [Header("Movement Parameters")]
        [SerializeField] private float moveSpeed = 50f;
        [SerializeField] private float rollSpeed = 100f;
        [SerializeField] private float mouseSensitivity = 2f;

        private InputActions _actions;
        private Rigidbody _rb;

        private float _moveInput;
        private float _rollInput;
        private Vector2 _steerInput;

        private void Awake()
        {
            _actions = new InputActions();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable() => _actions.Spaceship.Enable();
        private void OnDisable() => _actions.Spaceship.Disable();

        private void Update()
        {
            _moveInput = _actions.Spaceship.Move.ReadValue<float>();
            _rollInput = _actions.Spaceship.Roll.ReadValue<float>();
            _steerInput = _actions.Spaceship.Steer.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            var yaw = _steerInput.x * mouseSensitivity * (invertMouseX ? -1 : 1);
            var pitch =  -_steerInput.y * mouseSensitivity * (invertMouseY ? -1 : 1);
            var roll = -_rollInput * rollSpeed * (invertRoll ? -1 : 1);

            _rb.angularVelocity = transform.TransformDirection(new Vector3(pitch, yaw, roll)) * Mathf.Deg2Rad;
            _rb.linearVelocity = transform.forward * (_moveInput * moveSpeed);
        }
    }
}