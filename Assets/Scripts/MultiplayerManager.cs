using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public bool isMultiplayer = false;  // 멀티플레이 여부
    public int player1CharacterIndex = -1;  // 1P 선택한 캐릭터 인덱스 (-1로 초기화)
    public int player2CharacterIndex = -1;  // 2P 선택한 캐릭터 인덱스 (-1로 초기화)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject);  // 중복 방지
        }
    }

    public void SetMultiplayer(bool multiplayer)
    {
        isMultiplayer = multiplayer;
        Debug.Log($"멀티플레이 모드: {isMultiplayer}");
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

        Debug.Log($"플레이어 {player}의 캐릭터 인덱스: {index}");
    }

    public int GetPlayerCharacterIndex(int player)
    {
        if (player == 1) return player1CharacterIndex;
        else if (player == 2) return player2CharacterIndex;

        Debug.LogError($"유효하지 않은 플레이어 번호: {player}");
        return -1;  // 유효하지 않은 플레이어 번호일 경우
    }
}
