using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnStartScenen : MonoBehaviour
{
    public GameObject LevelUI;
    public void Return()
    {
        // 기본 BGM 재생 코드 추가
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBGM(BGMManager.Instance.defaultBGM); // 기본 BGM 재생
        }
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


