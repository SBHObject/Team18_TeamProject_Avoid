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
    public static int level;

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
        // ��� ���� �ʱ�ȭ
        ResetGameState();
    }


    // ���� 1 ��ư
    public void OnLevel1Button()
    {
        level = 1;
        SceneManager.LoadScene("MainScene"); // ���� �� �ε�

    }

    // ���� 2 ��ư
    public void OnLevel2Button()
    {
        level = 2;
        SceneManager.LoadScene("MainScene"); // ���� �� �ε�
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void ResetGameState()
    {
        // MultiplayerManager �ʱ�ȭ
        MultiplayerManager.Instance.SetPlayerCharacter(1, -1);  // 1P �ε��� �ʱ�ȭ
        MultiplayerManager.Instance.SetPlayerCharacter(2, -1);  // 2P �ε��� �ʱ�ȭ

        // �̱�/��Ƽ�÷��� ��� �ʱ�ȭ
        MultiplayerManager.Instance.SetMultiplayer(false);

        // ĳ���� ���� �ε��� �ʱ�ȭ (�ʿ� �� �߰�)
        CharacterSelection characterSelection = FindObjectOfType<CharacterSelection>();
        if (characterSelection != null)
        {
            characterSelection.ResetSelection();
        }
    }
}

