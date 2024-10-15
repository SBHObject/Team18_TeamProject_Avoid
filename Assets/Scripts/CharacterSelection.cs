using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public string characterName;  // ĳ���� �̸�
    public string description;    // ĳ���� ���� �߰�
    public Sprite characterSprite;  // ĳ���� �̹���
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
        selectedCharacterIndex = index;  // ������ ĳ���� �ε��� ����
        UpdateCharacterUI();  // UI ������Ʈ
        CharacterManager.Instance.SelectCharacter(selectedCharacterIndex);  // ĳ���� �Ŵ����� ���õ� �ε��� ����
    }

    // UI ������Ʈ �Լ�
    private void UpdateCharacterUI()
    {
        CharacterData character = characters[selectedCharacterIndex];
        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;
        characterDescriptionText.text = character.description;  // ���� ������Ʈ
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void StartGame()
    {
        // SceneManager.LoadScene("MainScene"); �� ���� ���� ������ �̵�
    }
}
