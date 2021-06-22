using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemoryScript : MonoBehaviour
{
    List<string> words;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Dictionary"))
        {
            PlayerPrefs.SetString("Dictionary", "уж,яд,як,щи,боа,шея,оса,Вий,рай,жена,Гюго,гид,жук,гуща,губа");
        }
        string str;
        str = PlayerPrefs.GetString("Dictionary");
        words = str.Split(new char[] { ',' }).ToList();
    }
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
    }
    public void LoadPracticeNums()
    {

        SceneManager.LoadScene(6);
    }
    public void LoadPracticeWords()
    {

        SceneManager.LoadScene(7);
    }
    public void LoadDictionary()
    {
        SceneManager.LoadScene("Dictionary");
    }
    public void LoadTheory()
    {
        SceneManager.LoadScene(5);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
