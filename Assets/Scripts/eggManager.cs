using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] AudioSource eggCrack;

    [Header("Car Reference")]
    [SerializeField] carCollision carCollision;


    private float checkInterval = 0.5f;
    private bool isEggCracked = false;

    private float maxDropLength = -10f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road" && !isEggCracked)
        {
            isEggCracked = true;
            EggCrack();
        }

        if (collision.gameObject.tag == "Plane" && !isEggCracked)
        {
            isEggCracked = true;
            EggCrack();
        }

    }

    private void EggCrack()
    {
        Debug.Log("Egg cracked on road");
        eggCrack.Play();
        carCollision.EndGame();
    }

    void Start()
    {
        StartCoroutine(CheckPositionRoutine());
    }

    IEnumerator CheckPositionRoutine()
    {
        while (true)
        {
            if (transform.position.y < 0)
            {
                carCollision.EndGame(); 
            }

            else if (transform.position.y < maxDropLength)
            {
                Time.timeScale = 0;
                break;
            }

            yield return new WaitForSeconds(checkInterval); 
        }
    }
}
   
