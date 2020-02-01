using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject powerUp;
    
    public GameObject[] powerUps;

    public float lvlTimeSecond = 20f; //Levelin süresi
    
    public Text timeText;

    private bool isExist = false;
    private float powerUpTiming;
    private float powerUpMaxSecond = 5f;
    private float powerUpMinSecond = 2f;

    
    // Start is called before the first frame update
    void Start()
    {
        powerUpTiming = Random.Range(powerUpMinSecond, powerUpMaxSecond);
    }

    // Update is called once per frame
    void Update()
    {
        lvlTimeSecond -= Time.deltaTime;
        timeText.text = "00:" + (int)lvlTimeSecond;

        if (lvlTimeSecond < 1)
        {
            SceneManager.LoadScene(1);
        }

        createPowerUp();
    }
    
    private void createPowerUp()
    {
        if (!isExist)
        {
            powerUpTiming -= Time.deltaTime;
        
            if (powerUpTiming <= 0)
            {
                powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], new Vector2(Random.Range(-22, 23), 4f), Quaternion.identity);
                
                isExist = true;
            }
            
        }
        
        if (powerUp && isExist && PowerUp.isTaked)
        {
            powerUp = null;
            
            powerUpTiming = Random.Range(powerUpMinSecond, powerUpMaxSecond);

            isExist = false;
        }
    }
}
