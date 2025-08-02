using Source.GameState;
using Source.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Rope
{
    public class PlayerController : BaseSingleton<PlayerController>
    {
        [SerializeField] private float linearSpeed;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] private float rotationalForce;
        [SerializeField] private bool invertPitch;
        
        public Rigidbody Rigidbody { get; private set; }
        private float _pitchInput;
        private float _yawInput;
        
        void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.maxAngularVelocity = maxAngularVelocity;
        }

        void FixedUpdate()
        {
            if(GameStateManager.Instance.GameplayState.IsPlaying)
            {
                Rigidbody.linearVelocity = transform.forward * linearSpeed;
                ApplyInputTorque(_pitchInput, _yawInput);
            }
            else
            {
                Rigidbody.linearVelocity = Vector3.zero;
            }
        }

        void ApplyInputTorque(float pitch, float yaw)
        {  
            var pitchSign = invertPitch ? -1 : 1;
            var torque = transform.right * (pitch * pitchSign * rotationalForce) + transform.forward * (yaw * rotationalForce);
            Rigidbody.AddTorque(torque, ForceMode.Force);    
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
