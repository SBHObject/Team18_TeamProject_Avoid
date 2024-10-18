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
    public Text characterNameText;           // 캐릭터 이름 표시
    public Text characterDescriptionText;    // 캐릭터 설명 표시
    public Button confirmButton;             // 선택 완료 버튼
    public RetryButton retryButton;          // RetryButton 스크립트 참조

    private int currentPlayer = 1;           // 현재 선택 중인 플레이어 (1P 또는 2P)
    private int selectedCharacterIndex = -1; // 선택된 캐릭터 인덱스 (초기값: -1)

    private void Start()
    {
        currentPlayer = 1;  // 항상 1P부터 시작
        UpdateCharacterUI();  // 초기 UI 업데이트

        // 선택 완료 버튼 이벤트 연결
        confirmButton.onClick.AddListener(OnConfirmSelection);
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

    public void OnConfirmSelection()
    {
        if (selectedCharacterIndex == -1)
        {
            Debug.LogWarning("2p 캐릭터를 선택하지 않았습니다.");
            return;  // 캐릭터가 선택되지 않으면 진행하지 않음
        }

        // 현재 플레이어의 선택을 MultiplayerManager에 저장
        MultiplayerManager.Instance.SetPlayerCharacter(currentPlayer, selectedCharacterIndex);

        if (MultiplayerManager.Instance.isMultiplayer && currentPlayer == 1)
        {
            // 1P 선택 완료, 2P로 전환
            currentPlayer = 2;
            selectedCharacterIndex = -1;  // 2P 선택을 위해 초기화
            UpdateCharacterUI();
        }
        else
        {
            // 2P 선택 완료 또는 싱글플레이일 경우 난이도 선택 UI 표시
            retryButton.ShowLevelUI();
        }
    }
}
