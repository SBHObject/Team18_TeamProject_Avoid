using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public string description;

    public Sprite characterSprite;

    public GameObject characterPrefab;  // 선택된 캐릭터의 프리팹
}
public class CharacterSelection : MonoBehaviour
{
    public GameObject CharacterUI;

    public Image characterImage;

    public CharacterData[] characters;  // 선택 가능한 캐릭터들

    public Text characterNameText;  // 선택된 캐릭터의 이름
    public Text characterDescriptionText;  // 선택된 캐릭터의 설명

    private int selectedCharacterIndex = 0;  // 선택된 캐릭터 인덱스

    // 캐릭터 선택 버튼 클릭 시 호출
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        UpdateCharacterUI();
    }

    // UI 업데이트 함수
    private void UpdateCharacterUI()
    {
        CharacterData character = characters[selectedCharacterIndex];
        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;
        characterDescriptionText.text = character.description;
    }

    // 게임 시작 버튼 클릭 시 호출
    public void StartGame()
    {
        // 선택된 캐릭터로 게임 시작
        GameObject selectedCharacter = Instantiate(characters[selectedCharacterIndex].characterPrefab);
        // 씬 간 캐릭터 데이터 전달 또는 GameManager에 저장 가능
        // 예: GameManager.Instance.SetSelectedCharacter(selectedCharacter);
    }

    public void ShowCharacterUI()
    {
        CharacterUI.SetActive(true);
    }
    public void CloseCharacterUI()
    {
        CharacterUI.SetActive(false);
    }
}