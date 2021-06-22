using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictScript : MonoBehaviour
{
    public RectTransform field;
    public Text countText;
    public RectTransform content;
    List<string> words = new List<string>();

    void Start()
    {
        string str;
        str = PlayerPrefs.GetString("Dictionary");
        words = str.Split(new char[] { ',' }).ToList();
        for (int i = 0; i < words.Count; i++)
        {
            AddItem(i);
        }
    }
    void AddItem(int i)
    {
        GameObject instance = Instantiate(field.gameObject);
        instance.transform.SetParent(content, false);
        instance.transform.Find("Numb").GetComponent<Text>().text = i.ToString();
        instance.transform.GetComponent<InputField>().text = words[i];

    }
}