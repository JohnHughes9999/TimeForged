using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void EndButtonClicked()
    {
        Application.Quit();
    }

    public void TryAgainClicked()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
