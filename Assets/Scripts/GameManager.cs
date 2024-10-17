using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform spawnPoint;  // 캐릭터 스폰 위치
    public GameObject Spear;
    public GameObject bomb;
    public GameObject shield;
    public GameObject endPanel;
    public Text totalScoreTxt;
    public GameObject[] characterPrefabs;  // 캐릭터 프리팹 배열
    public GameObject endObject; // 1초 동안 보여줄 비활성화된 게임 오브젝트

    private int selectedCharacterIndex = 0; // 선택된 캐릭터 인덱스
    private GameObject currentCharacter; // 현재 게임에 사용되는 캐릭터
    private int totalScore;
    private float randomValue;
    private Coroutine spearSpawnCoroutine; // 창 생성 코루틴
    private float spawnInterval = 0.4f;
    private float lastSpearSpawnTime = 0f;

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

    void Update()
    {
        // 현재 시간에서 마지막 생성 시간의 차가 spawnInterval 이상일 경우 창 생성
        if (Time.time - lastSpearSpawnTime >= spawnInterval)
        {
            Instantiate(Spear, spawnPoint.position, Quaternion.identity);
            lastSpearSpawnTime = Time.time;
        }
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
    }

    void DropItem()
    {
        randomValue = Random.Range(0f, 1f);
        if (randomValue <= 1f)
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

    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreTxt.text = totalScore.ToString();

        UpdateSpearSpawnInterval();
    }

    void UpdateSpearSpawnInterval()  // 점수에 따라서 spawnInterval의 주기가 짧아짐
    {
        if (totalScore >= 200)
        {
            spawnInterval = 0.15f;
            Debug.Log("3단계");
        }
        else if (totalScore >= 100)
        {
            spawnInterval = 0.25f;
            Debug.Log("2단계");
        }
        else
        {
            Debug.Log("1단계");
        }
    }

    public void EndGame()
    {

        FindObjectOfType<MainSceneBGMcontroller>()?.End();
        StartCoroutine(ShowEndObject());// // 코루틴을 호출하여 1초 동안 비활성화된 오브젝트를 활성화한 후 다시 비활성화
    }

    IEnumerator ShowEndObject()
    {
        // 오브젝트 활성화
        if (endObject != null)
        {
            endObject.SetActive(true); // 오브젝트를 활성화
        }

        // 1초 동안 대기 (오브젝트가 활성화된 상태)
        yield return new WaitForSecondsRealtime(1f); // 실제 시간으로 1초 대기

        // 오브젝트 비활성화
        if (endObject != null)
        {
            endObject.SetActive(false); // 오브젝트를 비활성화
        }

        // End Panel 활성화
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 캐릭터 스폰 함수
    public void SpawnCharacter()
    {
        // 캐릭터 인덱스가 배열 범위 내에 있는지 다시 확인
        if (characterPrefabs.Length > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            if (currentCharacter != null)
            {
                Destroy(currentCharacter); // 기존 캐릭터 제거
            }

            currentCharacter = Instantiate(characterPrefabs[selectedCharacterIndex], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("캐릭터 인덱스가 범위를 벗어났거나 사용할 수 있는 캐릭터 프리팹이 없습니다.");
        }
    }

    // 캐릭터 선택 함수 (캐릭터 선택 UI에서 호출)
    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characterPrefabs.Length)
        {
            selectedCharacterIndex = index;
        }
        else
        {
            Debug.LogError("선택된 캐릭터 인덱스가 범위를 벗어났습니다.");
        }
    }
}