using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuCanvas : MonoBehaviour {
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");

    }
    public void Exit()
    {
        Application.Quit();

    }
}
