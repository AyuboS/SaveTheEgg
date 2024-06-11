using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nitroManager : MonoBehaviour
{
    private float rotatingSpeed;
    private float amplitude = 0.2f;
    private float updateInterval = 0.75f;
    private float nextUpdateTime = 0f;
    private float startY;


    [Header("Car Reference")]
    [SerializeField] carCollision CarCollision;


    private void Start()
    {
        rotatingSpeed = Random.Range(10f, 15f);
        startY = 1.4f;
    }

    private void Update()
    {
        transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);

        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + updateInterval;
            float newY = startY + amplitude * Mathf.PingPong(Time.time, amplitude);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CarCollision.boostPlayer();
            Destroy(gameObject);
        }
    }
}
