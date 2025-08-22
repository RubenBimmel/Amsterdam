using Physics;

namespace Vehicles
{
    public class Body : PhysicsObject
    {
        public float wheelbase;
        public float track;
        
        private Suspension[] _suspensions;

        public Suspension[] Suspensions => _suspensions ??= GetComponentsInChildren<Suspension>();
    }
}
