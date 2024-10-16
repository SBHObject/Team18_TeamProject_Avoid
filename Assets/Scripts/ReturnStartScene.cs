using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnStartScenen : MonoBehaviour
{
    public GameObject LevelUI;
    public void Return()
    {
        // �⺻ BGM ��� �ڵ� �߰�
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBGM(BGMManager.Instance.defaultBGM); // �⺻ BGM ���
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


