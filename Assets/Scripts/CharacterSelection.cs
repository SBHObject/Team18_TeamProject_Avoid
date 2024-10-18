#if UNITY_EDITOR
using UnityEditor.U2D.Animation;
#endif
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public string description;
    public Sprite characterSprite;
    public GameObject characterPrefab;
}

public class CharacterSelection : MonoBehaviour
{
    public GameObject characterUI;           // 캐릭터 선택 UI

    public Image characterImage;             // 선택된 캐릭터 이미지

    public CharacterData[] characters;       // 캐릭터 데이터 배열

    public Text statusMessage;               // 상태 메시지 표시 (1P/2P 선택 상태)
    public Text warningMessage;              // 경고 메시지 표시 (캐릭터 미선택 경고)
    public Text characterNameText;           // 캐릭터 이름 표시
    public Text characterDescriptionText;    // 캐릭터 설명 표시

    public Button confirmButton;             // 선택 완료 버튼
    public RetryButton retryButton;          // RetryButton 스크립트 참조

    private int currentPlayer = 1;           // 현재 선택 중인 플레이어 (1P 또는 2P)
    private int selectedCharacterIndex = -1; // 선택된 캐릭터 인덱스 (초기값: -1)

    private void Start()
    {
        currentPlayer = 1;  // 항상 1P부터 시작

        // 기본 캐릭터 설정: 배열에 캐릭터가 있으면 첫 번째 캐릭터로 초기화
        if (characters.Length > 0)
        {
            selectedCharacterIndex = 0;  // 첫 번째 캐릭터 기본 선택
            UpdateCharacterUI();
        }

        // 선택 완료 버튼 이벤트 연결
        confirmButton.onClick.AddListener(OnConfirmSelection);

        // 초기 상태 메시지 설정
        UpdateStatusMessage();

        // 경고 메시지 숨기기
        warningMessage.gameObject.SetActive(false);
    }
    public void ResetSelection()
    {
        currentPlayer = 1;  // 항상 1P부터 시작
        selectedCharacterIndex = -1;  // 선택된 캐릭터 인덱스 초기화
        UpdateCharacterUI();
        UpdateStatusMessage();  // 상태 메시지 업데이트
    }
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;  // 선택된 캐릭터 인덱스 저장
        UpdateCharacterUI();
    }

    private void UpdateCharacterUI()
    {
        if (selectedCharacterIndex >= 0)  // 유효한 인덱스인지 확인
        {
            CharacterData character = characters[selectedCharacterIndex];
            characterImage.sprite = character.characterSprite;
            characterNameText.text = character.characterName;
            characterDescriptionText.text = character.description;
        }
    }

    private void UpdateStatusMessage()
    {
        // 경고 메시지가 비활성화 상태일 때만 상태 메시지를 표시
        if (!warningMessage.gameObject.activeSelf)
        {
            statusMessage.gameObject.SetActive(true);
            statusMessage.text = currentPlayer == 1
                ? "1P 캐릭터를 선택하세요!"
                : "2P 캐릭터를 선택하세요!";
        }
    }

    public void OnConfirmSelection()
    {
        if (selectedCharacterIndex == -1)
        {
            // 상태 메시지를 숨기고 경고 메시지를 표시
            statusMessage.gameObject.SetActive(false);
            warningMessage.text = "아직 캐릭터를 선택 못한 플레이어가 있습니다.";
            warningMessage.gameObject.SetActive(true);

            // 일정 시간 후 경고 메시지를 숨기고 상태 메시지 복원
            Invoke(nameof(HideWarningMessage), 0.5f);
            return;
        }

        // 경고 메시지 숨기기
        warningMessage.gameObject.SetActive(false);

        // 선택한 캐릭터를 MultiplayerManager에 저장
        MultiplayerManager.Instance.SetPlayerCharacter(currentPlayer, selectedCharacterIndex);

        if (MultiplayerManager.Instance.isMultiplayer && currentPlayer == 1)
        {
            // 1P 선택 완료 후 2P로 전환
            currentPlayer = 2;
            selectedCharacterIndex = -1;  // 2P 선택을 위해 초기화
            UpdateCharacterUI();
            UpdateStatusMessage();  // 상태 메시지 업데이트 (2P 선택 중)
        }
        else
        {
            // 2P 선택 완료 또는 싱글플레이일 경우 난이도 선택 UI 표시
            retryButton.ShowLevelUI();
        }
    }

    // 경고 메시지 숨기기 함수
    private void HideWarningMessage()
    {
        warningMessage.gameObject.SetActive(false);
        UpdateStatusMessage();  // 상태 메시지 복원
    }
}
