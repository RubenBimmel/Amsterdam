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

            car.Body = Instantiate(config.bodyPrefab, car.transform);
            car.Body.Suspensions = car.Body.GetComponentsInChildren<Suspension>();

            foreach (var suspension in car.Body.Suspensions)
            {
                suspension.physicsObject = car.Body;
                suspension.Config = config.suspensionConfig;

                suspension.Wheel = Instantiate(config.wheelPrefab, suspension.transform);
                suspension.Wheel.Suspension = suspension;
                suspension.Wheel.transform.position = suspension.transform.position + suspension.SuspensionDirection * suspension.Config.restPosition;
            }
        }
    }
}