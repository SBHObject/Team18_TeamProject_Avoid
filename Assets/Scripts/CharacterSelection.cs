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

    public Text statusMessage;               // ���� �޽��� ǥ�� (1P/2P ���� ����)
    public Text warningMessage;              // ��� �޽��� ǥ�� (ĳ���� �̼��� ���)
    public Text characterNameText;           // ĳ���� �̸� ǥ��
    public Text characterDescriptionText;    // ĳ���� ���� ǥ��

    public Button confirmButton;             // ���� �Ϸ� ��ư
    public RetryButton retryButton;          // RetryButton ��ũ��Ʈ ����

    private int currentPlayer = 1;           // ���� ���� ���� �÷��̾� (1P �Ǵ� 2P)
    private int selectedCharacterIndex = -1; // ���õ� ĳ���� �ε��� (�ʱⰪ: -1)

    private void Start()
    {
        currentPlayer = 1;  // �׻� 1P���� ����

        // �⺻ ĳ���� ����: �迭�� ĳ���Ͱ� ������ ù ��° ĳ���ͷ� �ʱ�ȭ
        if (characters.Length > 0)
        {
            selectedCharacterIndex = 0;  // ù ��° ĳ���� �⺻ ����
            UpdateCharacterUI();
        }

        // ���� �Ϸ� ��ư �̺�Ʈ ����
        confirmButton.onClick.AddListener(OnConfirmSelection);

        // �ʱ� ���� �޽��� ����
        UpdateStatusMessage();

        // ��� �޽��� �����
        warningMessage.gameObject.SetActive(false);
    }
    public void ResetSelection()
    {
        currentPlayer = 1;  // �׻� 1P���� ����
        selectedCharacterIndex = -1;  // ���õ� ĳ���� �ε��� �ʱ�ȭ
        UpdateCharacterUI();
        UpdateStatusMessage();  // ���� �޽��� ������Ʈ
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

    private void UpdateStatusMessage()
    {
        // ��� �޽����� ��Ȱ��ȭ ������ ���� ���� �޽����� ǥ��
        if (!warningMessage.gameObject.activeSelf)
        {
            statusMessage.gameObject.SetActive(true);
            statusMessage.text = currentPlayer == 1
                ? "1P ĳ���͸� �����ϼ���!"
                : "2P ĳ���͸� �����ϼ���!";
        }
    }

    public void OnConfirmSelection()
    {
        if (selectedCharacterIndex == -1)
        {
            // ���� �޽����� ����� ��� �޽����� ǥ��
            statusMessage.gameObject.SetActive(false);
            warningMessage.text = "���� ĳ���͸� ���� ���� �÷��̾ �ֽ��ϴ�.";
            warningMessage.gameObject.SetActive(true);

            // ���� �ð� �� ��� �޽����� ����� ���� �޽��� ����
            Invoke(nameof(HideWarningMessage), 0.5f);
            return;
        }

        // ��� �޽��� �����
        warningMessage.gameObject.SetActive(false);

        // ������ ĳ���͸� MultiplayerManager�� ����
        MultiplayerManager.Instance.SetPlayerCharacter(currentPlayer, selectedCharacterIndex);

        if (MultiplayerManager.Instance.isMultiplayer && currentPlayer == 1)
        {
            // 1P ���� �Ϸ� �� 2P�� ��ȯ
            currentPlayer = 2;
            selectedCharacterIndex = -1;  // 2P ������ ���� �ʱ�ȭ
            UpdateCharacterUI();
            UpdateStatusMessage();  // ���� �޽��� ������Ʈ (2P ���� ��)
        }
        else
        {
            // 2P ���� �Ϸ� �Ǵ� �̱��÷����� ��� ���̵� ���� UI ǥ��
            retryButton.ShowLevelUI();
        }
    }

    // ��� �޽��� ����� �Լ�
    private void HideWarningMessage()
    {
        warningMessage.gameObject.SetActive(false);
        UpdateStatusMessage();  // ���� �޽��� ����
    }
}
