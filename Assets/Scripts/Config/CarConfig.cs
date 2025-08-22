using UnityEngine;
using Vehicles;

namespace Config
{
    [CreateAssetMenu(fileName = "CarConfig", menuName = "Config/Car", order = 0)]
    public class CarConfig : ScriptableObject
    {
        public Body bodyPrefab;
        public Wheel wheelPrefab;
        public SpringConfig suspensionConfig;
    }
}