using UnityEngine;

namespace Vehicles
{
    public class Wheel : MonoBehaviour
    {
        public Suspension Suspension { get; set; }

        public float diameter;
        public float width;
        public float mass;
        
        public float Radius => diameter * 0.5f;

        private void FixedUpdate()
        {
            ApplySteering();
        }

        private void ApplySteering()
        {
            var anchorVelocity = Suspension.GetVelocity();
            var rightVelocity = Vector3.Dot(transform.right, Suspension.WheelVelocity);
            var wheelForce = -rightVelocity * GetGrip(anchorVelocity.magnitude) / Time.fixedDeltaTime;
            var force = wheelForce * Vector3.ProjectOnPlane(transform.right, Suspension.GroundNormal);
            Suspension.ApplyForce(force);
        }

        private float GetGrip(float carVelocity)
        {
            // TODO: Implement grip curve
            return 100f;
        }
    }
}
