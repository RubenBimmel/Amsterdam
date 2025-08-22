using Config;
using Physics;
using UnityEngine;

namespace Vehicles
{
    public class Suspension : PhysicsAnchor
    {
        public Wheel wheel;
        public SpringConfig config;

        public Vector3 SuspensionDirection => -transform.up;

        private float _force;
        private float _springLength;
        private Vector3 _wheelPosition;
        private Vector3 _previousWheelPosition;

        public void Start()
        {
            _springLength = config.restPosition;
            _wheelPosition = transform.position + SuspensionDirection * _springLength;
            _previousWheelPosition = _wheelPosition;
            wheel.transform.position = _wheelPosition;
        }

        private void Update()
        {
            wheel.transform.position = _wheelPosition;
        }

        private void FixedUpdate()
        {
            var didHit = UnityEngine.Physics.Raycast(transform.position, SuspensionDirection, out var hit, config.maxLength + wheel.Radius);
            var maxSpringLength = didHit ? hit.distance - wheel.Radius : config.maxLength;
            _springLength = Mathf.Clamp(_springLength, 0f, maxSpringLength);
            _wheelPosition = transform.position + SuspensionDirection * _springLength;

            var wheelVelocity = (_wheelPosition - _previousWheelPosition) / Time.fixedDeltaTime;
            var anchorVelocity = GetVelocity();
            var velocity = Vector3.Dot(anchorVelocity - wheelVelocity, SuspensionDirection);

            UpdateSuspension(velocity);

            var isGrounded = didHit && _springLength + 0.1f >= hit.distance - wheel.Radius;
            if (isGrounded)
            {
                ApplyForce(SuspensionDirection * -_force);
            }

            velocity += _force / wheel.mass * Time.fixedDeltaTime;
            _springLength += velocity * Time.fixedDeltaTime;
            _springLength = Mathf.Clamp(_springLength, 0f, maxSpringLength);
            _wheelPosition = transform.position + SuspensionDirection * _springLength;
            _previousWheelPosition = _wheelPosition;
        }

        private void UpdateSuspension(float velocity)
        {
            // 2. Compute spring compression
            var compression = config.restPosition - _springLength;

            // 4. Spring-damper force
            var springForce = config.stiffness * compression;
            var damperForce = config.damping * -velocity;
            _force = springForce + damperForce;

            // 5. Bump stops
            if (_springLength <= config.minLength)
            {
                var penetration = config.minLength - _springLength;
                _force += config.bumpStop * penetration * penetration;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }
}