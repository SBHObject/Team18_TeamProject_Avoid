
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 스폰 위치
    public Transform spawnPoint1;  // 1P 스폰 위치
    public Transform spawnPoint2;  // 2P 스폰 위치

    // 게임 오브젝트
    public GameObject Spear;
    public GameObject bomb;
    public GameObject shield;
    public GameObject endPanel;
    public GameObject[] characterPrefabs;  // 캐릭터 프리팹 배열

    // UI 요소
    public Text totalScoreTxt;
    public Text highScoreTxt;
    public GameObject endObject;  // 종료 오브젝트

    private GameObject player1;
    private GameObject player2;
    private int totalScore;
    private float spawnInterval = 0.4f;
    private float lastSpearSpawnTime = 0f;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;  // 게임 시간을 정상 속도로 설정 (1초로 설정)

        lastSpearSpawnTime = 0f;  // 창 생성 시간을 0으로 초기화

        InitializeGameScene();  // 게임 씬 초기화
        UpdateHighScore();
        InvokeRepeating("DropItem", 1f, 1f);
    }



    void Update()
    {
        // spawnPoint1이 파괴되지 않았는지 확인
        if (spawnPoint1 != null && Time.time - lastSpearSpawnTime >= spawnInterval)
        {
            Instantiate(Spear, spawnPoint1.position, Quaternion.identity);
            lastSpearSpawnTime = Time.time;
        }
        else if (spawnPoint1 == null)
        {
            Debug.LogWarning("spawnPoint1이 파괴되었습니다.");
        }
    }

    // 게임 씬 초기화 함수
    void InitializeGameScene()
    {
        SpawnCharacter(1, spawnPoint1);

        if (MultiplayerManager.Instance.isMultiplayer)
        {
            SpawnCharacter(2, spawnPoint2);
        }
    }

    // 플레이어 스폰 함수
    void SpawnCharacter(int playerNumber, Transform spawnPoint)
    {
        int characterIndex = MultiplayerManager.Instance.GetPlayerCharacterIndex(playerNumber);

        if (characterIndex == -1 || characterPrefabs[characterIndex] == null)
        {
            Debug.LogError($"플레이어 {playerNumber}의 캐릭터 프리팹이 유효하지 않습니다. 인덱스: {characterIndex}");
            return;
        }

        // 캐릭터 생성
        GameObject player = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, Quaternion.identity);
        player.tag = "Player";

        // CharacterMoveBase 컴포넌트 가져오기
        CharacterMoveBase moveBase = player.GetComponent<CharacterMoveBase>();
        if (moveBase != null)
        {
            InputContoller inputController = player.GetComponent<InputContoller>();
            if (inputController != null)
            {
                // 플레이어 번호에 따른 입력 설정
                inputController.isPlayer2(playerNumber == 2);
            }
        }
        else
        {
            Debug.LogError($"플레이어 {playerNumber}에 CharacterMoveBase 컴포넌트가 없습니다.");
        }

        // 플레이어 저장
        if (playerNumber == 1) player1 = player;
        else player2 = player;
    }



    // 아이템 생성 함수
    void DropItem()
    {
        GameObject item = Random.Range(0f, 1f) < 0.5f ? shield : bomb;
        Instantiate(item);
    }

    // 점수 추가 함수
    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreTxt.text = totalScore.ToString();
        UpdateSpearSpawnInterval();
        UpdateHighScore();
    }

    // 창 생성 간격 조정
    void UpdateSpearSpawnInterval()
    {
        if (totalScore >= 200)
        {
            spawnInterval = 0.15f;
        }
        else if (totalScore >= 100)
        {
            spawnInterval = 0.25f;
        }
        else
        {
            spawnInterval = 0.4f;
        }
    }

    // 최고 점수 업데이트
    void UpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", totalScore);
            highScore = totalScore;
        }

        highScoreTxt.text = highScore.ToString();
    }

    // 게임 종료 처리
    public void EndGame()
    {
        CancelInvoke("DropItem");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            CharacterMoveBase rtan = player.GetComponent<CharacterMoveBase>();
            rtan.IsDead = true;
        }

        StartCoroutine(ShowEndObject());

        DestroyAllObjectsWithTag("Item");
        DestroyAllObjectsWithTag("Obstacle");
    }

    // 특정 태그의 모든 오브젝트 제거
    void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    // 종료 UI 표시
    IEnumerator ShowEndObject()
    {
        if (endObject != null) endObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        if (endObject != null) endObject.SetActive(false);

        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}