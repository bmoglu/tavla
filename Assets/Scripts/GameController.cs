using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public float lvlTimeSecond = 20f; //Levelin süresi
    
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        
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
        
        
    }
}
