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
    public GameObject CharacterUI;  // 캐릭터 선택 UI
    public Image characterImage;  // 선택된 캐릭터 이미지 표시
    public CharacterData[] characters;  // 선택 가능한 캐릭터 리스트
    public Text characterNameText;  // 캐릭터 이름 표시
    public Text characterDescriptionText;  // 캐릭터 설명 표시

    private int selectedCharacterIndex = 0;  // 선택된 캐릭터 인덱스

    // 캐릭터 선택 버튼 클릭 시 호출
    public void SelectCharacter(int index)
    {
        Debug.Log("선택된 캐릭터 인덱스: " + index);  // 선택된 캐릭터 인덱스 출력
        GameManager.Instance.SelectCharacter(index);
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
        // 선택된 캐릭터를 GameManager에 전달
        GameManager.Instance.SelectCharacter(selectedCharacterIndex);
        GameManager.Instance.SpawnCharacter();  // 선택된 캐릭터 스폰

        // 선택 UI를 숨기고 게임 시작
        CloseCharacterUI();
    }

    // 캐릭터 선택 UI 표시
    public void ShowCharacterUI()
    {
        CharacterUI.SetActive(true);
    }

    // 캐릭터 선택 UI 숨김
    public void CloseCharacterUI()
    {
        CharacterUI.SetActive(false);
    }
}
