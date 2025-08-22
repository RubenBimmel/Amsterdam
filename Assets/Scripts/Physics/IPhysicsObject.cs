using UnityEngine;

namespace Physics
{
    public interface IPhysicsObject
    {
        public abstract void ApplyForce(Vector3 force);
        public abstract void ApplyForce(Vector3 force, Vector3 origin);
        public abstract Vector3 GetVelocity();
        public abstract Vector3 GetVelocityAtPoint(Vector3 worldPoint);
    }
}
