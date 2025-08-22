using Physics;

namespace Vehicles
{
    public class Body : PhysicsObject
    {
        public float wheelbase;
        public float track;
        
        public Suspension[] Suspensions {get; set;}
    }
}
