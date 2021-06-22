using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishGameScript : MonoBehaviour
{
    System.Random rand = new System.Random();
    public Canvas Panel;
    public Canvas Scores;
    public Canvas CountDown;
    CanvasGroup PanelCanv;
    CanvasGroup ScoreCanv;
    CanvasGroup CountDownCanv;
    public GameObject prefFish1;
    public GameObject prefFish2;
    public GameObject prefFish3;
    public GameObject prefFish4;
    public GameObject prefFish5;
    public GameObject prefFish6;
    public GameObject prefFish7;
    GameObject obj = null;
    FishRotateScript fishes;
    public Text Score;
    public Text nScore;
    public Text HighScore;
    public Text Timer;
    public Text CountDownTimer;
    int score;
    int highScore;
    float timer;
    float countDownTimer;
    bool check;
    bool checkdown;


    void Start()
    {
        if (PlayerPrefs.HasKey("FishHighScore"))
        {
            highScore = PlayerPrefs.GetInt("FishHighScore");
        }
        check = false;
        checkdown = false;
        countDownTimer = 4;
        timer = 31;
        score = 0;
        Score.text = "0";
        PanelCanv = Panel.GetComponent<CanvasGroup>();
        ScoreCanv = Scores.GetComponent<CanvasGroup>();
        CountDownCanv = CountDown.GetComponent<CanvasGroup>();
        ScoreCanv.alpha = 0f;
        ScoreCanv.blocksRaycasts = false;
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
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
            System.TimeSpan time = System.TimeSpan.FromSeconds(countDownTimer);
            CountDownTimer.text = string.Format("{0:0}", time.Seconds);
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                System.TimeSpan time = System.TimeSpan.FromSeconds(timer);
                Timer.text = string.Format("00:{0:00}", time.Seconds);
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
            if (!checkdown)
            {
                countDownTimer = 0;
                CountDownCanv.alpha = 0f;
                CountDownCanv.blocksRaycasts = false;
                PanelCanv.alpha = 1f;
                PanelCanv.blocksRaycasts = true;
                obj = SpawnFishSchool();
                fishes = obj.GetComponent<FishRotateScript>();
                checkdown = true;
            }
        }
    }

    private GameObject SpawnFishSchool()
    {
        Destroy(obj);
        switch (rand.Next(7))
        {
            case 0:
                {
                    return Instantiate(prefFish1);
                }
            case 1:
                {
                    return Instantiate(prefFish2);
                }
            case 2:
                {
                    return Instantiate(prefFish3);
                }
            case 3:
                {
                    return Instantiate(prefFish4);
                }
            case 4:
                {
                    return Instantiate(prefFish5);
                }
            case 5:
                {
                    return Instantiate(prefFish6);
                }
            case 6:
                {
                    return Instantiate(prefFish7);
                }
        }
        return null;
    }
    public void CheckRotation(Button button)
    {
        if (int.Parse(button.tag) == fishes.rotation / 90)
        {
            score++;
        }
        else
        {
            if (score != 0)
            {
                score--;
            }
        }
        fishes.RotateFishes();
        Score.text = score.ToString();
        obj = SpawnFishSchool();
        fishes = obj.GetComponent<FishRotateScript>();
    }
    void ShowScore()
    {
        Destroy(obj);
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        ScoreCanv.alpha = 1f;
        ScoreCanv.blocksRaycasts = true;
        if (score > highScore || highScore == 0)
        {
            highScore = score;
            PlayerPrefs.SetInt("FishHighScore", score);
        }
        nScore.text = score.ToString();
        HighScore.text = highScore.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene("Fish");
    }

    public void Exit()
    {
        SceneManager.LoadScene(1);
    }
}
