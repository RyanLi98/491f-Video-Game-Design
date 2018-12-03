using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    static public int score = 5;
    static public bool endGame = false;
    static public int level;
    void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore" + level))
        {
            score = PlayerPrefs.GetInt("HighScore" + level);
        }
        PlayerPrefs.SetInt("HighScore" + level, score);
    }
    // Update is called once per frame
    void Update()
    {
        print(level);
        Text gt = this.GetComponent<Text>();
       

       if (endGame == true && score < PlayerPrefs.GetInt("HighScore" + level))
        {
            PlayerPrefs.SetInt("HighScore" + level, score);
            gt.text = "High Score: " + score;
        }
        else if (score == 0 && endGame == false)
        {
            gt.text = "High Score: none ";
        }
        else
            gt.text = "High Score: " + score;
    }
   
}
