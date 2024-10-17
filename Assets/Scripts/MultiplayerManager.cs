using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public bool isMultiplayer = false;  // ��Ƽ�÷��� ����
    public int player1CharacterIndex = 0;  // 1P ������ ĳ���� �ε���
    public int player2CharacterIndex = 0;  // 2P ������ ĳ���� �ε���

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

    // �̱�/��Ƽ ��� ����
    public void SetMultiplayer(bool multiplayer)
    {
        isMultiplayer = multiplayer;
    }

    // �� �÷��̾��� ĳ���� ����
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
    }
}
