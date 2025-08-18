using UnityEngine;

public class Car : MonoBehaviour
{
    public CarConfig prefabConfig;

    private Body _body;
    private readonly Wheel[] _wheels = new Wheel[4];

    private void Awake()
    {
        if (prefabConfig != null) Initialize(prefabConfig);
    }

    private void Initialize(CarConfig config)
    {
        _body = Instantiate(config.bodyPrefab, transform);

        for (var i = 0; i < _wheels.Length; i++)
        {
            var position = (WheelPosition)i;

            _wheels[i] = Instantiate(
                config.wheelPrefab, 
                position.GetLocalPosition(_body), 
                position.GetLocalRotation(), 
                transform
            );
        }
    }
}
