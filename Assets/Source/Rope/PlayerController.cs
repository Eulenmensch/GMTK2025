using System;
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
        [SerializeField] private float inputSmoothing;
        [SerializeField] private float stopInputSmoothing;
        
        public Rigidbody Rigidbody { get; private set; }
        private float _pitchInput;
        private float _yawInput;
        private float _smoothedPitch;
        private float _smoothedYaw;
        
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
            else if(Rigidbody.isKinematic == false)
            {
                Rigidbody.linearVelocity = Vector3.zero;
            }
        }

        void ApplyInputTorque(float pitch, float yaw)
        {  
            
            _smoothedPitch = Mathf.Abs(pitch) > 0 ? Mathf.Lerp(_smoothedPitch, pitch, inputSmoothing) : Mathf.Lerp(_smoothedPitch, pitch, stopInputSmoothing);
            _smoothedYaw = Mathf.Abs(yaw) >0 ? Mathf.Lerp(_smoothedYaw, yaw, inputSmoothing) : Mathf.Lerp(_smoothedYaw, yaw, stopInputSmoothing);
            var pitchSign = invertPitch ? -1 : 1;
            var torque = transform.right * (_smoothedPitch * pitchSign * rotationalForce) + transform.forward * (_smoothedYaw * rotationalForce);
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
