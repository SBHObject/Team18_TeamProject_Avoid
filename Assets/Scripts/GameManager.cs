using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform spawnPoint;  // ĳ���� ���� ��ġ
    public GameObject Spear;
    public GameObject bomb;
    public GameObject shield;
    public GameObject endPanel;
    public Text totalScoreTxt; 
    public GameObject[] characterPrefabs;  // ĳ���� ������ �迭
    public GameObject endObject; // 1�� ���� ������ ��Ȱ��ȭ�� ���� ������Ʈ

    private int selectedCharacterIndex = 0; // ���õ� ĳ���� �ε���
    private GameObject currentCharacter; // ���� ���ӿ� ���Ǵ� ĳ����
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
        InitializeGameScene();  // ���� �� �ʱ�ȭ
    }

    // ���� ������ ĳ���� ���� �Լ�
    void InitializeGameScene()
    {
        GameObject selectedCharacterPrefab = CharacterManager.Instance.GetSelectedCharacterPrefab();  // ���õ� ĳ���� ������ ��������
        if (selectedCharacterPrefab != null)
        {
            Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);  // ĳ���� ����
        }
        else
        {
            Debug.LogError("���õ� ĳ���� �������� �����ϴ�.");
        }

        InvokeRepeating("DropItem", 1f, 1f);
        InvokeRepeating("MakeSpear", 0f, 0.2f);
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

        CancelInvoke("MakeSpear"); 
        if (totalScore >= 10 && totalScore < 200)
        {
            InvokeRepeating("MakeSpear", 0f, 0.16f);
        }
        else if (totalScore >= 200)
        {
            InvokeRepeating("MakeSpear", 0f, 0.12f);
        }
        else
        {
            InvokeRepeating("MakeSpear", 0f, 0.2f);
        }
    }

    public void EndGame()
    {

        FindObjectOfType<MainSceneBGMcontroller>()?.End();
        StartCoroutine(ShowEndObject());// // �ڷ�ƾ�� ȣ���Ͽ� 1�� ���� ��Ȱ��ȭ�� ������Ʈ�� Ȱ��ȭ�� �� �ٽ� ��Ȱ��ȭ
    }

    IEnumerator ShowEndObject()
    {
        // ������Ʈ Ȱ��ȭ
        if (endObject != null)
        {
            endObject.SetActive(true); // ������Ʈ�� Ȱ��ȭ
        }

        // 1�� ���� ��� (������Ʈ�� Ȱ��ȭ�� ����)
        yield return new WaitForSecondsRealtime(1f); // ���� �ð����� 1�� ���

        // ������Ʈ ��Ȱ��ȭ
        if (endObject != null)
        {
            endObject.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ
        }

        // End Panel Ȱ��ȭ
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // ĳ���� ���� �Լ�
    public void SpawnCharacter()
    {
        // ĳ���� �ε����� �迭 ���� ���� �ִ��� �ٽ� Ȯ��
        if (characterPrefabs.Length > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            if (currentCharacter != null)
            {
                Destroy(currentCharacter); // ���� ĳ���� ����
            }

            currentCharacter = Instantiate(characterPrefabs[selectedCharacterIndex], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("ĳ���� �ε����� ������ ����ų� ����� �� �ִ� ĳ���� �������� �����ϴ�.");
        }
    }

    // ĳ���� ���� �Լ� (ĳ���� ���� UI���� ȣ��)
    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characterPrefabs.Length)
        {
            selectedCharacterIndex = index;
        }
        else
        {
            Debug.LogError("���õ� ĳ���� �ε����� ������ ������ϴ�.");
        }
    }
}