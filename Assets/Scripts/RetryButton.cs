using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public GameObject singleOrMultiUI;
    public GameObject characterUI;
    public GameObject levelUI;
    // 난이도 선택 및 씬 이동
    // 정적 변수로 난이도 설정
    public static float gameSpeed = 1.0f;
    public static int level;

    private void Start()
     { 
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetBGMSpeed(1f);  // gameSpeed에 맞게 BGM 속도를 설정
        }
     }

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
        // 모든 상태 초기화
        ResetGameState();
    }


    // 레벨 1 버튼
    public void OnLevel1Button()
    {
        level = 1;
        SceneManager.LoadScene("MainScene"); // 메인 씬 로드

    }

    // 레벨 2 버튼
    public void OnLevel2Button()
    {
        level = 2;
        SceneManager.LoadScene("MainScene"); // 메인 씬 로드
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void ResetGameState()
    {
        // MultiplayerManager 초기화
        MultiplayerManager.Instance.SetPlayerCharacter(1, -1);  // 1P 인덱스 초기화
        MultiplayerManager.Instance.SetPlayerCharacter(2, -1);  // 2P 인덱스 초기화

        // 싱글/멀티플레이 모드 초기화
        MultiplayerManager.Instance.SetMultiplayer(false);

        // 캐릭터 선택 인덱스 초기화 (필요 시 추가)
        CharacterSelection characterSelection = FindObjectOfType<CharacterSelection>();
        if (characterSelection != null)
        {
            characterSelection.ResetSelection();
        }
    }
}

