
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
    public Text StartTxt;
    public GameObject endObject;  // 종료 오브젝트

    private GameObject player1;
    private GameObject player2;
    private int totalScore;
    private float startDelay = 2f;
    private float spawnInterval = 0.4f;
    private float lastSpearSpawnTime = 0f;
    private int spearCountMultiplier = 1;  // 난이도에 따라 창 생성 개수 조정

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

        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetBGMSpeed(RetryButton.gameSpeed);  // gameSpeed에 맞게 BGM 속도를 설정
        }

        Time.timeScale = RetryButton.gameSpeed; // 게임 시간을 정상 속도로 설정 (1초로 설정)
        spearCountMultiplier = RetryButton.level == 2 ? 2 : 1; // 창 생성 개수 조정 (레벨 2일 때 2배로 생성)

        lastSpearSpawnTime = 0f;  // 창 생성 시간을 0으로 초기화
        UpdateHighScore();
        //InitializeGameScene();

        InvokeRepeating("DropItem", 1f, 1f); // 아이템 생성 주기적으로 실행

        Time.timeScale = 0f;

        StartCoroutine(StartGameDelay());
    }

    IEnumerator StartGameDelay()
    {
        if (StartTxt != null)
        {
            StartTxt.text = "Game Start!";
            StartTxt.gameObject.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(startDelay);

        if (StartTxt != null)
        {
            StartTxt.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;

        InitializeGameScene();  // 게임 씬 초기화
    }

    void Update()
    {
        // 주기적으로 창 생성
        if (Time.time - lastSpearSpawnTime >= spawnInterval)
        {
            for (int i = 0; i < spearCountMultiplier; i++)  // 난이도에 따라 창 생성 개수 조정
            {
                Instantiate(Spear, spawnPoint1.position, Quaternion.identity);
            }
            lastSpearSpawnTime = Time.time;
        }
    }

    // 게임 씬 초기화 함수
    void InitializeGameScene()
    {
        SpawnCharacter(1, spawnPoint1);

        if (MultiplayerManager.Instance.isMultiplayer)
        {
            SpawnCharacter(2, spawnPoint2);
            player1.GetComponent<InputContoller>().IsPlayer2(player2.GetComponent<CharacterMoveBase>());
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
            moveBase.SetHardMode(RetryButton.level >= 2);
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