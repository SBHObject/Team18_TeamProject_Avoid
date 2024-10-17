using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public bool isMultiplayer = false;  // 멀티플레이 여부
    public int player1CharacterIndex = 0;  // 1P 선택한 캐릭터 인덱스
    public int player2CharacterIndex = 0;  // 2P 선택한 캐릭터 인덱스

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

    // 싱글/멀티 모드 설정
    public void SetMultiplayer(bool multiplayer)
    {
        isMultiplayer = multiplayer;
    }

    // 각 플레이어의 캐릭터 설정
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
