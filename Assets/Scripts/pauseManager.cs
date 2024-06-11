using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject P_Pause;
    [SerializeField] Text countdownText;
    [SerializeField] GameObject _self;
    public void B_Pause()
    {
        P_Pause.SetActive(true);
        Time.timeScale = 0;
    }


    public void B_Resume()
    {
        _self.SetActive(false);
        countdownText.gameObject.SetActive(true);
        P_Pause.SetActive(false);
        StartCoroutine(CountdownAndResume());
        
    }

    IEnumerator CountdownAndResume()
    {
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            Debug.Log(count);

            // Wait for 1 second using realtimeSinceStartup to ensure it works even if Time.timeScale is 0
            float endTime = Time.realtimeSinceStartup + 1;
            while (Time.realtimeSinceStartup < endTime)
            {
                yield return null;
            }

            count--;
        }
        _self.SetActive(true);
        countdownText.text = "Game Resumed";
        Debug.Log("Game Resumed");

        // Optional: Wait for a moment before hiding the text and resuming the game
        float resumeTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < resumeTime)
        {
            yield return null;
        }

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }






    public void B_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
