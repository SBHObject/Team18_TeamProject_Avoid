using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public bool isMultiplayer = false;  // ��Ƽ�÷��� ����
    public int player1CharacterIndex = -1;  // 1P ������ ĳ���� �ε��� (-1�� �ʱ�ȭ)
    public int player2CharacterIndex = -1;  // 2P ������ ĳ���� �ε��� (-1�� �ʱ�ȭ)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ� ����
        }
    }

    public void SetMultiplayer(bool multiplayer)
    {
        isMultiplayer = multiplayer;
        Debug.Log($"��Ƽ�÷��� ���: {isMultiplayer}");
    }

    public void SetPlayerCharacter(int player, int index)
    {
        if (player == 1)
        {
            player1CharacterIndex = index;
        }
        else if (player == 2)
        {
            player2CharacterIndex = index;
        }

        Debug.Log($"�÷��̾� {player}�� ĳ���� �ε���: {index}");
    }

    public int GetPlayerCharacterIndex(int player)
    {
        if (player == 1) return player1CharacterIndex;
        else if (player == 2) return player2CharacterIndex;

        Debug.LogError($"��ȿ���� ���� �÷��̾� ��ȣ: {player}");
        return -1;  // ��ȿ���� ���� �÷��̾� ��ȣ�� ���
    }
}
