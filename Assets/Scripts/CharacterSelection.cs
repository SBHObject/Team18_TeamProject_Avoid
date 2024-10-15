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
    public GameObject CharacterUI;  // ĳ���� ���� UI
    public Image characterImage;  // ���õ� ĳ���� �̹��� ǥ��
    public CharacterData[] characters;  // ���� ������ ĳ���� ����Ʈ
    public Text characterNameText;  // ĳ���� �̸� ǥ��
    public Text characterDescriptionText;  // ĳ���� ���� ǥ��

    private int selectedCharacterIndex = 0;  // ���õ� ĳ���� �ε���

    // ĳ���� ���� ��ư Ŭ�� �� ȣ��
    public void SelectCharacter(int index)
    {
        Debug.Log("���õ� ĳ���� �ε���: " + index);  // ���õ� ĳ���� �ε��� ���
        GameManager.Instance.SelectCharacter(index);
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
        // ���õ� ĳ���͸� GameManager�� ����
        GameManager.Instance.SelectCharacter(selectedCharacterIndex);
        GameManager.Instance.SpawnCharacter();  // ���õ� ĳ���� ����

        // ���� UI�� ����� ���� ����
        CloseCharacterUI();
    }

    // ĳ���� ���� UI ǥ��
    public void ShowCharacterUI()
    {
        CharacterUI.SetActive(true);
    }

    // ĳ���� ���� UI ����
    public void CloseCharacterUI()
    {
        CharacterUI.SetActive(false);
    }
}
