using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnStartScenen : MonoBehaviour
{
    public GameObject LevelUI;
    public void Return()
    {
        SceneManager.LoadScene("StartScene");
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


