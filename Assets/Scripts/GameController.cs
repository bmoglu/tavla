using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject powerUp;
        public GameObject buket;
    
    public GameObject[] powerUps;

    public float lvlTimeSecond = 60; //Levelin süresi
    
    public Text timeText;

    private bool isExist = false;
    private float powerUpTiming;
    private float powerUpMaxSecond = 10f;
    private float powerUpMinSecond = 5f;

    
    // Start is called before the first frame update
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
                //
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
                powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], new Vector2(Random.Range(-22, 23), 4f), Quaternion.identity);
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
                powerUp.transform.position = new Vector2(Random.Range(-22, 23), 4f);
            }
        }
        
        
    }
}
