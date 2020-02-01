using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static bool isGameStart = false;
    public static bool isGameResume = false;
    public static bool isGamePasue = false;
    public static bool isGameOver = false;
    public static bool isGameWin = false;

    public GameObject[] UIObjects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        UIObjects[0].SetActive(false);
        UIObjects[1].SetActive(true);

        isGameStart = true;
    }
    
    public void OnClickLevels()
    {
        UIObjects[0].SetActive(false);
        UIObjects[5].SetActive(true);
    }
    
    public void OnClickPause()
    {
        UIObjects[1].SetActive(false);
        UIObjects[2].SetActive(true);

        isGamePasue = true;
    }
    
    public void OnClickResume()
    {
        UIObjects[2].SetActive(false);
        UIObjects[1].SetActive(true);

        isGamePasue = false;
    }
    
    public void OnClickTryAgain()
    {
        UIObjects[3].SetActive(false);
        UIObjects[0].SetActive(true);
        
        SceneManager.LoadScene(0);
    }

    public void OnClickNextLevel()
    {
        UIObjects[4].SetActive(false);
        UIObjects[0].SetActive(true);
    }

    public void OnClickExit()
    {
        UIObjects[2].SetActive(false);
        UIObjects[0].SetActive(true);
        isGamePasue = false;
        isGameStart = false;
        
        SceneManager.LoadScene(0);
    }

    public void OnClickBack()
    {
        UIObjects[5].SetActive(false);
        UIObjects[0].SetActive(true);
    }
}
