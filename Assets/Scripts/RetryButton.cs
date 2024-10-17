using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public GameObject singleOrMultiUI;
    public GameObject characterUI;
    public GameObject levelUI;

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

    // ���̵� ���� �� �� �̵�
    public void SelectDifficulty(string difficulty)
    {
        // ���̵� ���� �� ���� ����
        SceneManager.LoadScene("MainScene");
    }
}
