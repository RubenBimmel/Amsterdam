using Config;
using UnityEngine;
using Vehicles;

namespace Tools
{
    public class CarSpawner : MonoBehaviour
    {
        public CarConfig config;

        private void Start()
        {
            if (config == null) return;

            var car = new GameObject("Car").AddComponent<Car>();
            car.transform.position = transform.position;
            car.transform.rotation = transform.rotation;

            car.body = Instantiate(config.bodyPrefab, car.transform);
            foreach (var suspension in car.body.Suspensions)
            {
                suspension.physicsObject = car.body;
                suspension.config = config.suspensionConfig;
                suspension.wheel = Instantiate(config.wheelPrefab, suspension.transform);
            }
        }
    }
}