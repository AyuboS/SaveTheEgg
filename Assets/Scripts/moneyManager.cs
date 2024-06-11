using UnityEngine;

public class moneyManager : MonoBehaviour
{
    [Header("Score System")]
    [SerializeField] private scoreSystem scoreSystem;
    private float rotatingSpeed;
    private float startY;
    private float amplitude = 0.3f; 
    private float updateInterval = 0.1f;
    private float nextUpdateTime = 0f;

    

    private void Start()
    {
        rotatingSpeed = Random.Range(20f, 30f);
        startY = 1.1f;
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
        if (other.CompareTag("Player") && scoreSystem != null)
        {
            scoreSystem.AddMoney(1);
            scoreSystem.UpdateMoneyText();
            Destroy(gameObject);
        }
    }
}
