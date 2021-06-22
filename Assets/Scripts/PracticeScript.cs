using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PracticeScript : MonoBehaviour
{
    public Font arial;
    public Font myFont;
    public Canvas PanelC;
    public Canvas ScoresC;
    public Canvas StartC;
    public Canvas AnswerC;
    CanvasGroup PanelCanv;
    CanvasGroup ScoreCanv;
    CanvasGroup StartCanv;
    CanvasGroup AnswerCanv;
    public Button HelpButton;
    public Button NextButton;
    public InputField InpMin;
    public InputField InpMax;
    public InputField MaxTimer;
    public InputField Count;
    public InputField Answer;
    public Image CheckImage;
    public Image CrossImage;
    public Text Word;
    public Text Timer;
    public Text Counter;
    public Text nScore;
    public Text HighScore;
    public Text AnsCounter;
    public Text TrueNumber;
    public Text TrueWord;
    float timer;
    float maxTimer;
    int score;
    int highScore;
    int count;
    int curCount;
    int min;
    int max;
    bool set;
    List<int> nums = new List<int>();
    List<string> dictionary = new List<string>();

    void Start()
    {
        string str;
        str = PlayerPrefs.GetString("Dictionary");
        dictionary = str.Split(new char[] { ',' }).ToList();
        highScore = PlayerPrefs.HasKey("PracticeHighScore") ? PlayerPrefs.GetInt("PracticeHighScore") : 0;
        set = false;
        InpMax.text = (dictionary.Count - 1).ToString();
        InpMin.text = "1";
        MaxTimer.text = "10";
        Count.text = "10";
        curCount = 0;
        count = 10;

        PanelCanv = PanelC.GetComponent<CanvasGroup>();
        ScoreCanv = ScoresC.GetComponent<CanvasGroup>();
        StartCanv = StartC.GetComponent<CanvasGroup>();
        AnswerCanv = AnswerC.GetComponent<CanvasGroup>();
        ScoreCanv.alpha = 0f;
        ScoreCanv.blocksRaycasts = false;
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        AnswerCanv.alpha = 0f;
        AnswerCanv.blocksRaycasts = false;
        StartCanv.alpha = 1f;
        StartCanv.blocksRaycasts = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
        if (set && timer > 0)
        {
            timer -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timer);
            Timer.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);
        }
        else
        {
            UpdateNumb();
        }
        if (set && curCount == count)
        {
            ShowAnswers();
            set = false;
        }
    }

    public void StartGame()
    {
        max = InpMax.text != "" ? int.Parse(InpMax.text) > 0 && int.Parse(InpMax.text) < dictionary.Count ? int.Parse(InpMax.text) : dictionary.Count : dictionary.Count;
        min = InpMin.text != "" ? (int.Parse(InpMin.text) > 0) && (int.Parse(InpMin.text) < max) ? int.Parse(InpMin.text) : 1 : 1;
        maxTimer = MaxTimer.text != "" ? int.Parse(MaxTimer.text) > 0 ? int.Parse(MaxTimer.text) : 10 : 10;
        count = Count.text != "" ? int.Parse(Count.text) > 0 ? int.Parse(Count.text) : 10 : 10;
        curCount = -1;
        set = true;
        StartCanv.alpha = 0f;
        StartCanv.blocksRaycasts = false;
        PanelCanv.alpha = 1f;
        PanelCanv.blocksRaycasts = true;
        UpdateNumb();
    }
    public void ShowScore()
    {
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        AnswerCanv.alpha = 0f;
        AnswerCanv.blocksRaycasts = false;
        ScoreCanv.alpha = 1f;
        ScoreCanv.blocksRaycasts = true;
        if (highScore == 0 || score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("PracticeHighScore", score);
        }
        nScore.text = score.ToString();
        HighScore.text = highScore.ToString();
    }
    public void UpdateNumb()
    {
        if (timer <= maxTimer - 1 && curCount < count)
        {
            curCount++;
            Counter.text = curCount + 1 + "/" + count;
            System.Random rand = new System.Random();
            nums.Add(rand.Next(min, max + 1));
            Word.text = nums[curCount].ToString();
            HelpButton.GetComponentInChildren<Text>().text = "?";
            timer = maxTimer;
            HelpButton.GetComponentInChildren<Text>().font = myFont;
        }
    }
    public void ShowAnswers()
    {
        PanelCanv.alpha = 0f;
        PanelCanv.blocksRaycasts = false;
        AnswerCanv.alpha = 1f;
        AnswerCanv.blocksRaycasts = true;
        curCount = 0;
        AnsCounter.text = curCount + "/" + count;
    }
    public void UpdateAns()
    {
        if (int.Parse(Answer.text) == nums[curCount])
        {
            score++;
            StartCoroutine(DisplayCheck());
        }
        else
        {
            StartCoroutine(DisplayCross());
        }
    }
    public void OnHelpClick()
    {
        if (dictionary[nums[curCount] - 1] != "")
        {
            HelpButton.GetComponentInChildren<Text>().text = dictionary[nums[curCount] - 1].ToString();
        }
        else
        {
            HelpButton.GetComponentInChildren<Text>().font = arial;
            HelpButton.GetComponentInChildren<Text>().text = @"¯\_( ͡❛ ‿ ͡❛)_/¯";
        }
    }
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene("MemPractice");
    }
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }
    IEnumerator DisplayCheck()
    {
        TrueWord.font = myFont;
        CheckImage.color = Color.green;
        TrueNumber.text = nums[curCount].ToString();
        if (dictionary[nums[curCount] - 1] != "")
        {
            TrueWord.text = dictionary[nums[curCount] - 1].ToString();
        }
        else
        {
            TrueWord.font = arial;
            TrueWord.text = @"¯\_( ͡❛ ‿ ͡❛)_/¯";
        }
        yield return new WaitForSeconds(1);
        CheckImage.color = Color.clear;
        TrueNumber.text = "";
        TrueWord.text = "";
        curCount++;
        AnsCounter.text = curCount + 1 + "/" + count;
        Answer.text = "";
        if (curCount == count)
        {
            ShowScore();
        }
    }
    IEnumerator DisplayCross()
    {
        TrueWord.font = myFont;
        CrossImage.color = Color.red;
        TrueNumber.text = nums[curCount].ToString();
        if (dictionary[nums[curCount] - 1] != "")
        {
            TrueWord.text = dictionary[nums[curCount] - 1].ToString();
        }
        else
        {
            TrueWord.font = arial;
            TrueWord.text = @"¯\_( ͡❛ ‿ ͡❛)_/¯";
        }
        NextButton.enabled = false;
        yield return new WaitForSeconds(1);
        NextButton.enabled = true; ;
        CrossImage.color = Color.clear;
        TrueNumber.text = "";
        TrueWord.text = "";
        curCount++;
        AnsCounter.text = curCount + 1 + "/" + count;
        Answer.text = "";
        if (curCount == count)
        {
            ShowScore();
        }
    }
}
