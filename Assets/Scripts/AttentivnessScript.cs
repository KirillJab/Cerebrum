using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttentivnessScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
    }
    public void LoadShulte()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadColors()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadFishes()
    {
        SceneManager.LoadScene("Fish");
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
