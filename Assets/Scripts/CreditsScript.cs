using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Exit();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
