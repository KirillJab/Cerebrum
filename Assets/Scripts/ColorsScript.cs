using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorsScript : MonoBehaviour
{
    enum ColorWords
    {
        Black = 0,
        Brown,
        Red,
        Blue,
        Yellow,
        Orange,
        Green,
        Purple,
        Pink,
        LightBlue
    }
    System.Random rand = new System.Random();
    public Text textR1;
    public Text textR2;
    public Text textL1;
    public Text textL2;
    public Text Timer;
    public Text Score;
    public Text HighScore;
    public Text CountDownTimer;
    public Color Brown;
    public Color Orange;
    public Color Purple;
    public Color Pink;
    public int Chance;
    bool check;
    int l1;
    int l2;
    int r1;
    int r2;
    float timer;
    float countDownTimer;
    int score;
    int highScore;
    public Canvas Panel;
    public Canvas Scores;
    public Canvas CountDown;
    CanvasGroup PanelCanv;
    CanvasGroup ScoreCanv;
    CanvasGroup CountDownCanv;


    void Start()
    {
        check = false;
        countDownTimer = 4f;
        timer = 31f;
        score = 0;
        UpdateTexts();
        if (PlayerPrefs.HasKey("ColorsHighScore"))
        {
            highScore = PlayerPrefs.GetInt("ColorsHighScore");
        }
        PanelCanv = Panel.GetComponent<CanvasGroup>();
        ScoreCanv = Scores.GetComponent<CanvasGroup>();
        CountDownCanv = CountDown.GetComponent<CanvasGroup>();
        ScoreCanv.alpha = 0f;
        ScoreCanv.blocksRaycasts = false;
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        CountDownCanv.alpha = 1f;
        CountDownCanv.blocksRaycasts = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
        if (countDownTimer > 0)
        {
            countDownTimer -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(countDownTimer);
            CountDownTimer.text = string.Format("{0:0}", time.Seconds);
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                TimeSpan time = TimeSpan.FromSeconds(timer);
                Timer.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
            }
            else
            {
                timer = 0;
                if (!check)
                {
                    ShowScore();
                    check = true;
                }
            }
            if (!check)
            {
                countDownTimer = 0;
                CountDownCanv.alpha = 0f;
                CountDownCanv.blocksRaycasts = false;
                PanelCanv.alpha = 1f;
                PanelCanv.blocksRaycasts = true;
            }
        }
    }
    private void UpdateTexts()
    {
        if (Chance >= rand.Next(100))
        {
            l1 = rand.Next(10);
            r1 = l1;
        }
        else
        {
            l1 = rand.Next(10);
            r1 = rand.Next(10);
        }
        if (Chance >= rand.Next(100))
        {
            l2 = rand.Next(10);
            r2 = l2;
        }
        else
        {
            l2 = rand.Next(10);
            r2 = rand.Next(10);
        }

        textL1.text = getString(l1);
        textL2.text = getString(l2);
        textR1.text = getString(rand.Next(10));
        textR2.text = getString(rand.Next(10));

        textL1.color = getColor(rand.Next(10));
        textL2.color = getColor(rand.Next(10));
        textR1.color = getColor(r1);
        textR2.color = getColor(r2);
    }

    private Color getColor(int i)
    {
        switch(i)
        {
            case 0:
                {
                    return Color.black;
                }
            case 1:
                {
                    return new Color(0.65f, 0.14f, 0f);//Brown;
                }
            case 2:
                {
                    return Color.red;
                }
            case 3:
                {
                    return Color.blue;
                }
            case 4:
                {
                    return Color.yellow;
                }
            case 5:
                {
                    return new Color(1f, 0.4f, 0f);//Orange
                }
            case 6:
                {
                    return Color.green;
                }
            case 7:
                {
                    return new Color(0.5f, 0f, 1f);//Purple;
                }
            case 8:
                {
                    return new Color(1f, 0, 0.9f);//Pink;
                }
            case 9:
                {
                    return Color.cyan;
                }
            default:
                {
                    return Color.magenta;
                }
        }
    }
    private string getString(int i)
    {
        switch (i)
        {
            case 0:
                {
                    return "Чёрный";
                }
            case 1:
                {
                    return "Коричневый";
                }
            case 2:
                {
                    return "Красный";
                }
            case 3:
                {
                    return "Синий";
                }
            case 4:
                {
                    return "Жёлтый";
                }
            case 5:
                {
                    return "Оранжевый";
                }
            case 6:
                {
                    return "Зелёный";
                }
            case 7:
                {
                    return "Фиолетовый";
                }
            case 8:
                {
                    return "Розовый";
                }
            case 9:
                {
                    return "Голубой";
                }
            default:
                {
                    return "Нет";
                }
        }
    }
    public void CheckAnswer (Button button)
    {
        if ((l1 == r1 && l2 == r2) && button.gameObject.name == "YesButton" || (l1 != r1 || l2 != r2) && button.gameObject.name == "NoButton")
        {
            score++;
        }
        else
        {
            score--;
            if (score < 0)
            {
                score = 0;
            }
        }
        UpdateTexts();
    }
    public void ShowScore()
    {
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        ScoreCanv.alpha = 1f;
        ScoreCanv.blocksRaycasts = true;
        if (score > highScore || highScore == 0)
        {
            highScore = score;
            PlayerPrefs.SetInt("ColorsHighScore", score);
        }
        Score.text = score.ToString();
        HighScore.text = highScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(3);
    }
    public void Exit()
    {
        SceneManager.LoadScene(1);
    }
}
