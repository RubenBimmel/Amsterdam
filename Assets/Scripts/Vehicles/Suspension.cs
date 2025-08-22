using Config;
using Physics;
using UnityEngine;

namespace Vehicles
{
    public class Suspension : PhysicsAnchor
    {
        public Wheel Wheel { get; set; }
        public SpringConfig Config { get; set; }
        
        public Vector3 WheelVelocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public Vector3 GroundNormal { get; private set; }

        public Vector3 SuspensionDirection => -transform.up;

        private float _springLength;
        private Vector3 _wheelPosition;
        private Vector3 _previousWheelPosition;

        private const float GroundedMargin = 0.02f;

        public void Start()
        {
            _springLength = Config.restPosition;
            _wheelPosition = Wheel.transform.position;
            _previousWheelPosition = _wheelPosition;
        }

        private void Update()
        {
            Wheel.transform.position = _wheelPosition;
        }

        private void FixedUpdate()
        {
            // Grounded check
            var isInGroundRange = UnityEngine.Physics.Raycast(transform.position, SuspensionDirection, out var hit, Config.maxLength + Wheel.Radius);
            IsGrounded = isInGroundRange && _springLength + GroundedMargin >= hit.distance - Wheel.Radius;
            GroundNormal = IsGrounded ? hit.normal : Vector3.up;

            // Clamp wheel position with ground
            var maxSpringLength = IsGrounded ? Mathf.Max(hit.distance - Wheel.Radius, 0f) : Config.maxLength;
            ClampWheelPosition(maxSpringLength);

            // Calculate forces
            WheelVelocity = (_wheelPosition - _previousWheelPosition) / Time.fixedDeltaTime;
            var anchorVelocity = GetVelocity();
            var velocity = Vector3.Dot(anchorVelocity - WheelVelocity, SuspensionDirection);
            var force = CalculateSuspensionForce(velocity);

            // If wheel is grounded, apply normal force to car
            if (IsGrounded)
            {
                ApplyForce(SuspensionDirection * -force);
            }

            // Apply forces to spring
            velocity += force / Wheel.mass * Time.fixedDeltaTime;
            _springLength += velocity * Time.fixedDeltaTime;

            // Clamp wheel position with ground
            ClampWheelPosition(maxSpringLength);

            // Save wheel position to calculate velocity next frame
            _previousWheelPosition = _wheelPosition;
        }

        private float CalculateSuspensionForce(float velocity)
        {
            var compression = Config.restPosition - _springLength;
            var springForce = Config.stiffness * compression;
            var damperForce = Config.damping * velocity;
            var force = springForce + damperForce;

            // Apply bump stop force when necessary
            if (_springLength <= Config.minLength)
            {
                var penetration = Config.minLength - _springLength;
                force += Config.bumpStop * penetration * penetration;
            }

            return force;
        }

        private void ClampWheelPosition(float maxSpringLength)
        {
            _springLength = Mathf.Min(_springLength, maxSpringLength);
            _wheelPosition = transform.position + SuspensionDirection * _springLength;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }
}