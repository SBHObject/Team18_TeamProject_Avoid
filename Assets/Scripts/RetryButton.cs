using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
public class RetryButton : MonoBehaviour
{
    public GameObject CharacterUI;
    public GameObject singleOrMultiUI;
    public GameObject LevelUI;
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ShowLevelUI()
    {
        singleOrMultiUI.SetActive(false);
        CharacterUI.SetActive(false);
        LevelUI.SetActive(true);
    }
    public void CloseLevelUI()
    {
        LevelUI.SetActive(false);
    }
    public void ShowsingleOrMultiUI()
    {
        singleOrMultiUI.SetActive(true); 
    }
    public void ClosesingleOrMultiUI()
    {
        singleOrMultiUI.SetActive(false);
    }
}


