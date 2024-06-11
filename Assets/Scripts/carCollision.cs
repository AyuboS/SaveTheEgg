using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class carCollision : MonoBehaviour
{
    private Rigidbody carRb;
    private float nitroForce = 6f;
    private float springForce = 10000f;
    private float carMass = 1500f;

    [Header("UI")]
    [SerializeField] GameObject T_GameOver;

    [Header("Score System")]
    [SerializeField] private scoreSystem scoreSystem;

    [Header("Sounds")]
    [SerializeField] private AudioSource S_carEngine;
    [SerializeField] private AudioClip S_springJump;
    [SerializeField] private AudioClip S_hammerHit;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        S_carEngine.volume = 0.5f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hammer":
                EndGame();
                S_carEngine.PlayOneShot(S_hammerHit);
                break;

            case "Obstacle":
                EndGame();

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) 
        {
            case "Finish":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case "Spring":
                SpringJump();
                Debug.Log("Nitro is triggered");
                break;
        }
    }

    private void SpringJump()
    {
        carRb.AddForce(transform.up * springForce, ForceMode.Impulse);

         S_carEngine.PlayOneShot(S_springJump);

    }

    public void boostPlayer()
    {
        Vector3 nitroImpulse = transform.forward * nitroForce * carMass; 
        carRb.AddForce(nitroImpulse, ForceMode.Impulse); 
    }


    public void EndGame()
    {
        StopCarMovement();
        
        scoreSystem.GameOver();
        scoreSystem.scoreIsUpdating = false;
    }
    public void StopCarMovement()
    {
        carRb.drag = 10;
        T_GameOver.SetActive(true);
    }

}
