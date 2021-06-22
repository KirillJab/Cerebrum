using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void LoadMemScene()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadAttentScene()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
