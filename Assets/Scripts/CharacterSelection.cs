using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public string characterName;  // 캐릭터 이름
    public string description;    // 캐릭터 설명 추가
    public Sprite characterSprite;  // 캐릭터 이미지
    public GameObject characterPrefab;  // 선택된 캐릭터의 프리팹
}
public class CharacterSelection : MonoBehaviour
{
    public GameObject CharacterUI;  // 캐릭터 선택 UI
    public Image characterImage;  // 선택된 캐릭터 이미지 표시
    public CharacterData[] characters;  // 선택 가능한 캐릭터 리스트
    public Text characterNameText;  // 캐릭터 이름 표시
    public Text characterDescriptionText;  // 캐릭터 설명 표시

    private int selectedCharacterIndex = 0;  // 선택된 캐릭터 인덱스

    // 캐릭터 선택 버튼 클릭 시 호출
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;  // 선택한 캐릭터 인덱스 저장
        UpdateCharacterUI();  // UI 업데이트
        CharacterManager.Instance.SelectCharacter(selectedCharacterIndex);  // 캐릭터 매니저에 선택된 인덱스 전달
    }

    // UI 업데이트 함수
    private void UpdateCharacterUI()
    {
        CharacterData character = characters[selectedCharacterIndex];
        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;
        characterDescriptionText.text = character.description;  // 설명 업데이트
    }

    // 게임 시작 버튼 클릭 시 호출
    public void StartGame()
    {
        // SceneManager.LoadScene("MainScene"); 를 통해 메인 씬으로 이동
    }
}
