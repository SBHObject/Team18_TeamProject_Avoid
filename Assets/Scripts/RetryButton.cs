using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButton : MonoBehaviour
{
    public GameObject LevelUI;
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ShowLevelUI()
    {
        LevelUI.SetActive(true);
    }
    public void CloseLevelUI()
    {
        LevelUI.SetActive(false);
    }
}


