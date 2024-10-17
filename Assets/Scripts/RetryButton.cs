using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public GameObject singleOrMultiUI;
    public GameObject characterUI;
    public GameObject levelUI;

    // 싱글/멀티 UI 활성화
    public void ShowSingleOrMultiUI()
    {
        singleOrMultiUI.SetActive(true);
    }

    // 싱글플레이 선택 시
    public void OnSinglePlayerSelected()
    {
        MultiplayerManager.Instance.SetMultiplayer(false);
        characterUI.SetActive(true);
        singleOrMultiUI.SetActive(false);
    }

    // 멀티플레이 선택 시
    public void OnMultiPlayerSelected()
    {
        MultiplayerManager.Instance.SetMultiplayer(true);
        characterUI.SetActive(true);
        singleOrMultiUI.SetActive(false);
    }

    // 난이도 선택 UI 활성화
    public void ShowLevelUI()
    {
        levelUI.SetActive(true);
        characterUI.SetActive(false);
    }

    // UI를 비활성화하는 함수 (X 버튼에 연결)
    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    // 난이도 선택 및 씬 이동
    public void SelectDifficulty(string difficulty)
    {
        // 난이도 설정 및 게임 시작
        SceneManager.LoadScene("MainScene");
    }
}
