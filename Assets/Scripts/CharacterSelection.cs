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
    public GameObject characterUI;           // ĳ���� ���� UI
    public Image characterImage;             // ���õ� ĳ���� �̹���
    public CharacterData[] characters;       // ĳ���� ������ �迭
    public Text characterNameText;           // ĳ���� �̸� ǥ��
    public Text characterDescriptionText;    // ĳ���� ���� ǥ��
    public Button confirmButton;             // ���� �Ϸ� ��ư
    public RetryButton retryButton;          // RetryButton ��ũ��Ʈ ����

    private int currentPlayer = 1;           // ���� ���� ���� �÷��̾� (1P �Ǵ� 2P)
    private int selectedCharacterIndex = -1; // ���õ� ĳ���� �ε��� (�ʱⰪ: -1)

    private void Start()
    {
        currentPlayer = 1;  // �׻� 1P���� ����
        UpdateCharacterUI();  // �ʱ� UI ������Ʈ

        // ���� �Ϸ� ��ư �̺�Ʈ ����
        confirmButton.onClick.AddListener(OnConfirmSelection);
    }

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;  // ���õ� ĳ���� �ε��� ����
        UpdateCharacterUI();
    }

    private void UpdateCharacterUI()
    {
        if (selectedCharacterIndex >= 0)  // ��ȿ�� �ε������� Ȯ��
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
            Debug.LogWarning("2p ĳ���͸� �������� �ʾҽ��ϴ�.");
            return;  // ĳ���Ͱ� ���õ��� ������ �������� ����
        }

        // ���� �÷��̾��� ������ MultiplayerManager�� ����
        MultiplayerManager.Instance.SetPlayerCharacter(currentPlayer, selectedCharacterIndex);

        if (MultiplayerManager.Instance.isMultiplayer && currentPlayer == 1)
        {
            // 1P ���� �Ϸ�, 2P�� ��ȯ
            currentPlayer = 2;
            selectedCharacterIndex = -1;  // 2P ������ ���� �ʱ�ȭ
            UpdateCharacterUI();
        }
        else
        {
            // 2P ���� �Ϸ� �Ǵ� �̱��÷����� ��� ���̵� ���� UI ǥ��
            retryButton.ShowLevelUI();
        }
    }
}
