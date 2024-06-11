using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    public Transform leftWheelVisual;
    public Transform rightWheelVisual;
}

public class carMovement : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource S_carEngine;
    [SerializeField] private AudioClip engineSound;
    [SerializeField] private AudioClip engineIdleSound;

    [Header("Wheels")]
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private void Start()
    {
        // Play the idle sound at the start
        S_carEngine.clip = engineIdleSound;
        S_carEngine.loop = true; // Make sure the idle sound loops
        S_carEngine.Play();
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform visualWheel)
    {
        if (visualWheel == null)
        {
            return;
        }

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.position = position;
        visualWheel.rotation = rotation;

        float rotationDegree = (collider.rpm / 60) * 360 * Time.deltaTime;
        visualWheel.Rotate(Vector3.right, rotationDegree);
    }

    public void FixedUpdate()
    {
        float motor = 0;
        float steering = 0;
        bool isTurning = false;

        if (Input.touchCount > 0)
        {
            motor = maxMotorTorque; // Move only when the screen is touched
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                steering = -maxSteeringAngle; // Turn left
                isTurning = true;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                steering = maxSteeringAngle; // Turn right
                isTurning = true;
            }

            // Switch to engine sound when moving
            if (!S_carEngine.isPlaying || S_carEngine.clip != engineSound)
            {
                S_carEngine.clip = engineSound;
                S_carEngine.loop = true;
                S_carEngine.Play();
            }
        }
        else
        {
            // Stop the car and switch to idle sound when not touching the screen
            if (S_carEngine.clip != engineIdleSound)
            {
                S_carEngine.clip = engineIdleSound;
                S_carEngine.loop = true;
                if (!S_carEngine.isPlaying)
                {
                    S_carEngine.Play();
                }
            }
        }

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheelVisual);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheelVisual);
        }
    }
}
