using System;
using UnityEngine;

public enum WheelPosition
{
    FrontLeft = 0,
    FrontRight = 1,
    RearLeft = 2,
    RearRight = 3,
}

public static class WheelPositionExtensions
{
    public static Vector3 GetLocalPosition(this WheelPosition wheelPosition, Body body)
    {
        return wheelPosition switch
        {
            WheelPosition.FrontLeft => new Vector3(-body.track / 2f, 0f, body.wheelbase / 2f),
            WheelPosition.FrontRight => new Vector3(body.track / 2f, 0f, body.wheelbase / 2f),
            WheelPosition.RearLeft => new Vector3(-body.track / 2f, 0f, -body.wheelbase / 2f),
            WheelPosition.RearRight => new Vector3(body.track / 2f, 0f, -body.wheelbase / 2f),
            _ => throw new ArgumentOutOfRangeException(nameof(wheelPosition), wheelPosition, null)
        };
    }

    public static Quaternion GetLocalRotation(this WheelPosition wheelPosition)
    {
        return wheelPosition switch
        {
            WheelPosition.FrontLeft => Quaternion.LookRotation(Vector3.left),
            WheelPosition.FrontRight => Quaternion.LookRotation(Vector3.right),
            WheelPosition.RearLeft => Quaternion.LookRotation(Vector3.left),
            WheelPosition.RearRight => Quaternion.LookRotation(Vector3.right),
            _ => throw new ArgumentOutOfRangeException(nameof(wheelPosition), wheelPosition, null)
        };
    }
}