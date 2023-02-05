using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        PlayerPrefs.SetInt("WIN", 0);
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("TIME", Time.time - startTime);
        
    }

    public void Win()
    {
        PlayerPrefs.SetInt("WIN", 1);      
        StartCoroutine(WaitLoadScene());
    }

    private IEnumerator WaitLoadScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("EndGame");
    }

    public void Lost()
    {
        PlayerPrefs.SetInt("WIN", 0);
        StartCoroutine(WaitLoadScene());
    }
}
