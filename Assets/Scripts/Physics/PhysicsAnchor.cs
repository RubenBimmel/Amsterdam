using UnityEngine;

namespace Physics
{
    public class PhysicsAnchor : MonoBehaviour, IPhysicsObject
    {
        public PhysicsObject physicsObject;

        public void ApplyForce(Vector3 force) => ApplyForce(force, transform.position);

        public void ApplyForce(Vector3 force, Vector3 origin)
        {
            physicsObject.ApplyForce(force, origin);
        }

        public Vector3 GetVelocity() => physicsObject.GetVelocityAtPoint(transform.position);
        public Vector3 GetVelocityAtPoint(Vector3 worldPoint) => physicsObject.GetVelocityAtPoint(worldPoint);
    }
}
