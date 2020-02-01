using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static bool isGameStart = false;
    public static bool isGameResume = false;
    public static bool isGamePasue = false;
    public static bool isGameOver = false;
    public static bool isGameWin = false;

    public Text GirlsCountText;

    public GameObject[] UIObjects;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {

            UIObjects[1].SetActive(false);
            UIObjects[3].SetActive(true);

            isGameOver = false;
            isGameStart = false;
            isGamePasue = false;
            isGameWin = false;
        }
        else
        {


        }

        LvlWin();

    }

    public void OnClickStart()
    {
        UIObjects[0].SetActive(false);
        UIObjects[1].SetActive(true);
        
        isGameStart = true;
        isGameWin = false;
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

        isGamePasue = false;
        isGameStart = false;

        int temp = SceneManager.GetActiveScene().buildIndex;
        LevelLoader(temp);

    }

    public void OnClickNextLevel()
    {
        UIObjects[4].SetActive(false);
        UIObjects[0].SetActive(true);

        isGameWin = false;
        
        int temp = SceneManager.GetActiveScene().buildIndex;
        temp += 1;
        LevelLoader(temp);
    }

    public void OnClickExit()
    {
        UIObjects[2].SetActive(false);
        UIObjects[0].SetActive(true);

        isGamePasue = false;
        isGameStart = false;

        int temp = SceneManager.GetActiveScene().buildIndex;
        LevelLoader(temp);

    }

    public void OnClickBack()
    {
        UIObjects[5].SetActive(false);
        UIObjects[0].SetActive(true);
    }

    public void LevelLoader(int lvlIndex)
    {
        SceneManager.LoadScene(lvlIndex);
    }

    private void LvlWin()
    {
        if (isGameWin)
        {
            UIObjects[1].SetActive(false);
            UIObjects[4].SetActive(true);

            isGameStart = false;
            isGamePasue = false;
        }
    }
}