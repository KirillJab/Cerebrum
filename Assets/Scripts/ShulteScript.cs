using System.Collections;
using System.Collections.Generic;
using System;
using static System.Math;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShulteScript : MonoBehaviour
{
    int difficulty;
    int num;
    System.Random rand = new System.Random();
    public Canvas Panel;
    public Canvas Diff;
    public Canvas Scores;
    CanvasGroup PanelCanv;
    CanvasGroup ScoreCanv;
    CanvasGroup DifficultyCanv;
    public Text Score;
    public Text HighScore;
    public GameObject Buttons;
    public Text Number;
    float highScore;
    float score;
    bool playing;


    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        score = 0;
        num = 1;
        Number.text = "1";
        difficulty = 1;
        PanelCanv = Panel.GetComponent<CanvasGroup>();
        ScoreCanv = Scores.GetComponent<CanvasGroup>();
        DifficultyCanv = Diff.GetComponent<CanvasGroup>();
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        ScoreCanv.alpha = 0f;
        ScoreCanv.blocksRaycasts = false;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            score += Time.deltaTime;
        }

        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
    }
    private void ResetTable()
    {
        for (int i = 0; i < 25; i++)
        {
            Buttons.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void ResetToZero()
    {
        for (int i = 0; i < 25; i++)
        {
            Buttons.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text = "0";
        }
    }
    private void Shuffle()
    {
        for (int i = 1; i < 26; i++)
        {
            while (true)
            {
                int j = rand.Next(25);
                if (Buttons.transform.GetChild(j).gameObject.GetComponentInChildren<Text>().text == "0")
                {
                    Buttons.transform.GetChild(j).gameObject.GetComponentInChildren<Text>().text = i.ToString();
                    break;
                }
            }
        }
    }
    public void OnShulteClick(GameObject button)
    {
        playing = true;
        if (button.GetComponentInChildren<Text>().text == num.ToString())
        {
            switch (difficulty)
            {
                case 1:
                    {
                        button.SetActive(false);
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        ResetToZero();
                        Shuffle();
                        break;
                    }
            }
            if (num == 25)
            {
                playing = false;
                ShowScore();
            }
            num++;
            Number.text = num.ToString();
        }
        else
        {
            score += 2;
        }
    }

    public void OnDifficultyClick(GameObject button)
    {
        switch (int.Parse(button.tag))
        {
            case 1:
                {
                    difficulty = 1;
                    break;
                }
            case 2:
                {
                    difficulty = 2;
                    break;
                }
            case 3:
                {
                    difficulty = 3;
                    break;
                }
        }
        if (PlayerPrefs.HasKey("ShulteHighScore" + difficulty))
        {
            highScore = PlayerPrefs.GetFloat("ShulteHighScore" + difficulty);
        }
        PanelCanv.alpha = 1f;
        PanelCanv.blocksRaycasts = true;
        DifficultyCanv.alpha = 0f;
        DifficultyCanv.blocksRaycasts = false;
    }

    public void ShowScore()
    {
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        ScoreCanv.alpha = 1f;
        ScoreCanv.blocksRaycasts = true;

        if (score < highScore || highScore == 0)
        {
            highScore = score;
            PlayerPrefs.SetFloat("ShulteHighScore" + difficulty, score);
        }
        TimeSpan time = TimeSpan.FromSeconds(score);
        Score.text = string.Format("{0:00}:{1:00}:{2:00}", time.TotalMinutes, time.Seconds, time.Milliseconds);
        time = TimeSpan.FromSeconds(highScore); 
        HighScore.text = string.Format("{0:00}:{1:00}:{2:00}", time.TotalMinutes, time.Seconds, time.Milliseconds);
    }

    public void Restart()
    {
        ScoreCanv.alpha = 0f;
        ScoreCanv.blocksRaycasts = false;
        PanelCanv.alpha = 1f;
        PanelCanv.blocksRaycasts = true;
        num = 1;
        score = 0;
        ResetTable();
        ResetToZero();
        Shuffle();
    }
    public void ChangeDiff()
    {
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        SceneManager.LoadScene(1);
    }
}