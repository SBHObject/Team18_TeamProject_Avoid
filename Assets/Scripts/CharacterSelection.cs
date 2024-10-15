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

    public GameObject characterPrefab;  // ���õ� ĳ������ ������
}
public class CharacterSelection : MonoBehaviour
{
    public GameObject CharacterUI;

    public Image characterImage;

    public CharacterData[] characters;  // ���� ������ ĳ���͵�

    public Text characterNameText;  // ���õ� ĳ������ �̸�
    public Text characterDescriptionText;  // ���õ� ĳ������ ����

    private int selectedCharacterIndex = 0;  // ���õ� ĳ���� �ε���

    // ĳ���� ���� ��ư Ŭ�� �� ȣ��
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        UpdateCharacterUI();
    }

    // UI ������Ʈ �Լ�
    private void UpdateCharacterUI()
    {
        CharacterData character = characters[selectedCharacterIndex];
        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;
        characterDescriptionText.text = character.description;
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void StartGame()
    {
        // ���õ� ĳ���ͷ� ���� ����
        GameObject selectedCharacter = Instantiate(characters[selectedCharacterIndex].characterPrefab);
        // �� �� ĳ���� ������ ���� �Ǵ� GameManager�� ���� ����
        // ��: GameManager.Instance.SetSelectedCharacter(selectedCharacter);
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