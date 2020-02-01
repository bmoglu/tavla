using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private GameObject powerUp;
    
    public GameObject[] powerUps;

    public float lvlTimeSecond = 60; //Levelin süresi
    
    public Text timeText;

    private bool isExist = false;
    private float powerUpTiming;
    private float powerUpMaxSecond = 10f;
    private float powerUpMinSecond = 5f;

    public GameObject player;
    public AudioClip[] AudioClips;
    private AudioSource _audio;
    private void Awake()
    {
        _audio=gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        powerUpTiming = Random.Range(powerUpMinSecond, powerUpMaxSecond);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.isGameStart && !UIController.isGamePasue)
        {
            lvlTimeSecond -= Time.deltaTime;
            timeText.text = "00:" + (int) lvlTimeSecond;

            if (lvlTimeSecond < 1)
            {
                UIController.isGameOver = true;
                lvlTimeSecond = 999;
            }

            createPowerUp();
        }
    }
    
    private void createPowerUp()
    {
        if (null == powerUp)
        {
            powerUpTiming -= Time.deltaTime;
        
            if (powerUpTiming <= 0)
            {
                powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], new Vector2(Mathf.Clamp(Random.Range(player.transform.position.x - 5, player.transform.position.x + 5), -22, 22), 4f), Quaternion.identity);
                PowerUp.isTaked = false;
                powerUpTiming = Random.Range(powerUpMinSecond, powerUpMaxSecond);
            }
        } 
        else if (!powerUp.gameObject.activeSelf)
        {
            powerUpTiming -= Time.deltaTime;
            if (powerUpTiming <= 0)
            {
                powerUpTiming = Random.Range(powerUpMinSecond, powerUpMaxSecond);
                powerUp = powerUps[Random.Range(0, powerUps.Length)];
                powerUp.transform.position = new Vector2(Mathf.Clamp(Random.Range(player.transform.position.x - 5, player.transform.position.x + 5), -22, 22), 4f);
            }
        }
    }

    private void BadMode()
    {
         _audio.clip=AudioClips[1];
         _audio.Play();
    }

    private void GoodMode()
    {
        _audio.clip=AudioClips[0];
        _audio.Play();
    }
}
