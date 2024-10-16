using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform spawnPoint;  // 캐릭터 스폰 위치
    public GameObject Spear;
    public GameObject bomb;
    public GameObject shield;
    public GameObject endPanel;
    public Text totalScoreTxt;

    private int totalScore;
    private float randomValue;

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

        InvokeRepeating("DropItem", 1f, 1f);
        InvokeRepeating("MakeSpear", 0f , 0.2f);

    }

    void DropItem()
    {
        randomValue = Random.Range(0f, 1f);
        if (randomValue <= 0.1f)
        {
            randomValue = Random.Range(0f, 1f);
            if (randomValue <= 0.5f)
            {
                Instantiate(shield);
            }
            else
            {
                Instantiate(bomb);
            }
        }
        
    }


    void MakeSpear()
    {
        Instantiate(Spear);
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
