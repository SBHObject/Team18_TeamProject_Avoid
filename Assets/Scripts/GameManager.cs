
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ���� ��ġ
    public Transform spawnPoint1;  // 1P ���� ��ġ
    public Transform spawnPoint2;  // 2P ���� ��ġ

    // ���� ������Ʈ
    public GameObject Spear;
    public GameObject bomb;
    public GameObject shield;
    public GameObject endPanel;
    public GameObject[] characterPrefabs;  // ĳ���� ������ �迭

    // UI ���
    public Text totalScoreTxt;
    public Text highScoreTxt;
    public Text StartTxt;
    public GameObject endObject;  // ���� ������Ʈ

    private GameObject player1;
    private GameObject player2;
    private int totalScore;
    private float startDelay = 2f;
    private float spawnInterval = 0.4f;
    private float lastSpearSpawnTime = 0f;
    private int spearCountMultiplier = 1;  // ���̵��� ���� â ���� ���� ����

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
            BGMManager.Instance.SetBGMSpeed(RetryButton.gameSpeed);  // gameSpeed�� �°� BGM �ӵ��� ����
        }

        Time.timeScale = RetryButton.gameSpeed; // ���� �ð��� ���� �ӵ��� ���� (1�ʷ� ����)
        spearCountMultiplier = RetryButton.level == 2 ? 2 : 1; // â ���� ���� ���� (���� 2�� �� 2��� ����)

        lastSpearSpawnTime = 0f;  // â ���� �ð��� 0���� �ʱ�ȭ
        UpdateHighScore();
        //InitializeGameScene();

        InvokeRepeating("DropItem", 1f, 1f); // ������ ���� �ֱ������� ����

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

        InitializeGameScene();  // ���� �� �ʱ�ȭ
    }

    void Update()
    {
        // �ֱ������� â ����
        if (Time.time - lastSpearSpawnTime >= spawnInterval)
        {
            for (int i = 0; i < spearCountMultiplier; i++)  // ���̵��� ���� â ���� ���� ����
            {
                Instantiate(Spear, spawnPoint1.position, Quaternion.identity);
            }
            lastSpearSpawnTime = Time.time;
        }
    }

    // ���� �� �ʱ�ȭ �Լ�
    void InitializeGameScene()
    {
        SpawnCharacter(1, spawnPoint1);

        if (MultiplayerManager.Instance.isMultiplayer)
        {
            SpawnCharacter(2, spawnPoint2);
            player1.GetComponent<InputContoller>().IsPlayer2(player2.GetComponent<CharacterMoveBase>());
        }
    }

    // �÷��̾� ���� �Լ�
    void SpawnCharacter(int playerNumber, Transform spawnPoint)
    {
        int characterIndex = MultiplayerManager.Instance.GetPlayerCharacterIndex(playerNumber);

        if (characterIndex == -1 || characterPrefabs[characterIndex] == null)
        {
            Debug.LogError($"�÷��̾� {playerNumber}�� ĳ���� �������� ��ȿ���� �ʽ��ϴ�. �ε���: {characterIndex}");
            return;
        }

        // ĳ���� ����
        GameObject player = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, Quaternion.identity);
        player.tag = "Player";

        // CharacterMoveBase ������Ʈ ��������
        CharacterMoveBase moveBase = player.GetComponent<CharacterMoveBase>();
        if (moveBase != null)
        {
            moveBase.SetHardMode(RetryButton.level >= 2);
        }
        else
        {
            Debug.LogError($"�÷��̾� {playerNumber}�� CharacterMoveBase ������Ʈ�� �����ϴ�.");
        }

        // �÷��̾� ����
        if (playerNumber == 1) player1 = player;
        else player2 = player;
    }

    // ������ ���� �Լ�
    void DropItem()
    {
        GameObject item = Random.Range(0f, 1f) < 0.5f ? shield : bomb;
        Instantiate(item);
    }

    // ���� �߰� �Լ�
    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreTxt.text = totalScore.ToString();
        UpdateSpearSpawnInterval();
        UpdateHighScore();
    }

    // â ���� ���� ����
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

    // �ְ� ���� ������Ʈ
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

    // ���� ���� ó��
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

    // Ư�� �±��� ��� ������Ʈ ����
    void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    // ���� UI ǥ��
    IEnumerator ShowEndObject()
    {
        if (endObject != null) endObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        if (endObject != null) endObject.SetActive(false);

        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}