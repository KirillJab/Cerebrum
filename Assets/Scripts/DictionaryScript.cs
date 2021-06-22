using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DictionaryScript : MonoBehaviour
{
    public RectTransform field;
    public InputField input;
    public Text numb;
    public RectTransform content;
    List<string> words = new List<string>();

    void Start()
    {
        string str;
        str = PlayerPrefs.GetString("Dictionary");
        words = str.Split(new char[] { ',' }).ToList();
        if(words[words.Count - 1] == "")
        words.RemoveAt(words.Count - 1);
        for (int i = 0; i < words.Count; i++)
        {
            AddItem(i);
        }
        numb.text = (words.Count + 1).ToString() + ":";
    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
    }
    public void OnAddClick()
    {
        if (input.text != "")
        {
            AddItem(words.Count + 1, input.text.ToLower());
        }
        numb.text = (words.Count + 1).ToString() + ":";
    }
    void AddItem(int i, string str)
    {
        GameObject instance = Instantiate(field.gameObject);
        instance.transform.SetParent(content, false);
        instance.transform.Find("Numb").GetComponent<Text>().text = i.ToString() + ": ";
        instance.transform.GetComponent<InputField>().text = str;
        words.Add(str);
    }
    void AddItem(int i)
    {
        GameObject instance = Instantiate(field.gameObject);
        instance.transform.SetParent(content, false);
        instance.transform.Find("Numb").GetComponent<Text>().text = (i + 1).ToString() + ": ";
        instance.transform.GetComponent<InputField>().text = words[i];
    }
    public void Exit()
    {
        string str = "";
        foreach (InputField word in content.GetComponentsInChildren<InputField>())
        {
            str += word.text.Split()[0] + ",";
        }
        str.Trim(new char[] { ' ', ',' });
        PlayerPrefs.SetString("Dictionary", str);
        SceneManager.LoadScene("MemoryScene");
    }
}