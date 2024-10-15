using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform spawnPoint;  // 캐릭터 스폰 위치
    public GameObject rain;
    public GameObject endPanel;
    public Text totalScoreTxt;

    private int totalScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGameScene();  // 게임 씬 초기화
    }

    // 게임 씬에서 캐릭터 스폰 함수
    void InitializeGameScene()
    {
        GameObject selectedCharacterPrefab = CharacterManager.Instance.GetSelectedCharacterPrefab();  // 선택된 캐릭터 프리팹 가져오기
        if (selectedCharacterPrefab != null)
        {
            Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);  // 캐릭터 스폰
        }
        else
        {
            Debug.LogError("선택된 캐릭터 프리팹이 없습니다.");
        }

        InvokeRepeating("MakeRain", 0f, 1f);  // 비 내리기 실행
    }

    void MakeRain()
    {
        Instantiate(rain);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreTxt.text = totalScore.ToString();
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
