using UnityEngine;

namespace Vehicles
{
    public class Wheel : MonoBehaviour
    {
        public float diameter;
        public float width;
        public float mass;
        
        public float Radius => diameter * 0.5f;
    }
}
