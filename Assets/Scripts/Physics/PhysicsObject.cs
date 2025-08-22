using Config;
using Unity.VisualScripting;
using UnityEngine;

namespace Physics
{
    public class PhysicsObject : MonoBehaviour, IPhysicsObject
    {
        public float mass;
        
        protected Rigidbody Rigidbody { get; private set; }

        protected virtual void Awake()
        {
            Rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
            GetComponent<Rigidbody>().mass = mass;
            GetComponent<Rigidbody>().linearDamping = 0;
            GetComponent<Rigidbody>().angularDamping = 0;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        protected virtual void FixedUpdate()
        {
            Rigidbody.AddForce(Constants.Gravity, ForceMode.Acceleration);
        }

        public void ApplyForce(Vector3 force) => ApplyForce(force, Vector3.zero);
        public void ApplyForce(Vector3 force, Vector3 origin)
        {
            Rigidbody.AddForceAtPosition(force, origin);
            Debug.DrawRay(origin, force * 0.001f, Color.blue);
        }

        public Vector3 GetVelocity() => Rigidbody.linearVelocity;
        public Vector3 GetVelocityAtPoint(Vector3 worldPoint) => Rigidbody.GetPointVelocity(worldPoint);
    }
}
