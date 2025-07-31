using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Rope
{
    public class PlayerRopeController : MonoBehaviour
    {
        [SerializeField] private float linearSpeed;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] private float rotationalForce;
        [SerializeField] private bool invertPitch;
        
        private Rigidbody _rigidbody;
        private float _pitchInput;
        private float _yawInput;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxAngularVelocity = maxAngularVelocity;
        }

        void FixedUpdate()
        {
            _rigidbody.linearVelocity = transform.forward * linearSpeed;
            ApplyInputTorque(_pitchInput, _yawInput);
        }

        void ApplyInputTorque(float pitch, float yaw)
        {  
            var pitchSign = invertPitch ? -1 : 1;
            var torque = transform.right * pitch * pitchSign * rotationalForce + transform.forward * yaw * rotationalForce;
            _rigidbody.AddTorque(torque, ForceMode.Force);    
        }
        
        public void Pitch(InputAction.CallbackContext context)
        {
            _pitchInput = context.ReadValue<float>();
        }
        public void Yaw(InputAction.CallbackContext context)
        {
            _yawInput = context.ReadValue<float>();
        }
    }
}
