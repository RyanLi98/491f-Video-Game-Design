﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;


    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitButton;
    public Text uitReset;
    public Text uitCollision;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    static public int level;
    public int levelMax;
    public int shotsTaken;
    public int collidedWith;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        HighScore.endGame = false;
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }
    void StartLevel()
    {
        HighScore.endGame = true;
        if (castle != null)
        {
            Destroy(castle);
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject pTemp in gos)
        {

            Destroy(pTemp);

        }
        
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        collidedWith = 0;
        SwitchView("Show Both");
        ProjectileLine.S.Clear();
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
    }
    void UpdateGUI()
    {
        HighScore.endGame = false;
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
        uitCollision.text = "Collided with: " + collidedWith; 
    }

    void Update()
    {

        UpdateGUI();
        if((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }
    
    void NextLevel()
    {
    
        level++;
      
        if (level == levelMax)
        {
            level = 0;
        }
        HighScore.level = level;
        StartLevel();
    }
    public void SwitchView(string eView = "")
    { 
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch(showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }
    public void Reset()
    {
        uitReset.text = "reset";
        PlayerPrefs.SetInt("HighScore" + level, 0);
        HighScore.score = 0;
    }

    public static void ShotFired()
    {
        
        S.shotsTaken++;
        HighScore.score = S.shotsTaken;
    }
    public static void CollisionUp()
    {
        S.collidedWith++;
    }
}