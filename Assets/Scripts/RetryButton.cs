using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public GameObject singleOrMultiUI;
    public GameObject characterUI;
    public GameObject levelUI;
    // ���̵� ���� �� �� �̵�
    // ���� ������ ���̵� ����
    public static float gameSpeed = 1.0f;

    private void Start()
     { 
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetBGMSpeed(1f);  // gameSpeed�� �°� BGM �ӵ��� ����
        }
     }

// �̱�/��Ƽ UI Ȱ��ȭ
public void ShowSingleOrMultiUI()
    {
        singleOrMultiUI.SetActive(true);
    }

    // �̱��÷��� ���� ��
    public void OnSinglePlayerSelected()
    {
        MultiplayerManager.Instance.SetMultiplayer(false);
        characterUI.SetActive(true);
        singleOrMultiUI.SetActive(false);
    }

    // ��Ƽ�÷��� ���� ��
    public void OnMultiPlayerSelected()
    {
        MultiplayerManager.Instance.SetMultiplayer(true);
        characterUI.SetActive(true);
        singleOrMultiUI.SetActive(false);
    }

    // ���̵� ���� UI Ȱ��ȭ
    public void ShowLevelUI()
    {
        levelUI.SetActive(true);
        characterUI.SetActive(false);
    }

    // UI�� ��Ȱ��ȭ�ϴ� �Լ� (X ��ư�� ����)
    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }


    // ���� 1 ��ư
    public void OnLevel1Button()
    {
        gameSpeed = 1.0f; // �⺻ �ӵ�
        SceneManager.LoadScene("MainScene"); // ���� �� �ε�

    }

    // ���� 2 ��ư
    public void OnLevel2Button()
    {
        gameSpeed = 2.0f; // 2�� �ӵ�
        SceneManager.LoadScene("MainScene"); // ���� �� �ε�
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}

