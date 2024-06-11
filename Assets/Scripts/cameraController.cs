//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class cameraController : MonoBehaviour
//{
//    public Transform carTransform;
//    public float smoothSpeed = 0.125f;
//    public Vector3 locationOffset;
//    public Vector3 rotationOffset;

//    void Start()
//    {

//        transform.position = carTransform.position + carTransform.rotation * locationOffset;
//        transform.rotation = carTransform.rotation * Quaternion.Euler(rotationOffset);
//    }

//    void FixedUpdate()
//    {

//        Vector3 toCarPos = carTransform.position + carTransform.rotation * locationOffset;
//        Quaternion toCarRot = carTransform.rotation * Quaternion.Euler(rotationOffset);


//        Vector3 smoothedPosition = Vector3.Lerp(transform.position, toCarPos, smoothSpeed);
//        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, toCarRot, smoothSpeed);


//        transform.position = smoothedPosition;
//        transform.rotation = smoothedRotation;
//    }


//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform carTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    // Raycast variables
    public float raycastDistance = 10f; // Maximum distance for raycast
    public float obstacleAngleThreshold = 45f; // Angle threshold for considering obstacles

    void Start()
    {
        transform.position = carTransform.position + carTransform.rotation * locationOffset;
        transform.rotation = carTransform.rotation * Quaternion.Euler(rotationOffset);
    }

    void FixedUpdate()
    {
        Vector3 toCarPos = carTransform.position + carTransform.rotation * locationOffset;
        Quaternion toCarRot = carTransform.rotation * Quaternion.Euler(rotationOffset);

        // Raycast for obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // Check if obstacle is within angle threshold
            Vector3 obstacleDirection = hit.point - transform.position;
            float angle = Vector3.Angle(obstacleDirection, transform.forward);
            if (angle <= obstacleAngleThreshold)
            {
                // Adjust camera position to reveal obstacle
                toCarPos += Vector3.Lerp(Vector3.zero, obstacleDirection.normalized * 0.5f, Mathf.Clamp01(angle / obstacleAngleThreshold)); // Adjust offset based on angle
            }
        }

        // Lerp camera position and rotation
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, toCarPos, smoothSpeed);
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, toCarRot, smoothSpeed);

        transform.position = smoothedPosition;
        transform.rotation = smoothedRotation;
    }
}

