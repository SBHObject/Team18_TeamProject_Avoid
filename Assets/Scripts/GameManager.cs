using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] characterPrefabs; // ĳ���� ������ �迭
    public Transform spawnPoint; // ĳ���� ���� ��ġ
    public GameObject rain;
    public GameObject bomb;
    public GameObject shield;
    public GameObject endPanel;
    public Text totalScoreTxt;

    private int selectedCharacterIndex = 0; // ���õ� ĳ���� �ε���
    private GameObject currentCharacter; // ���� ���ӿ� ���Ǵ� ĳ����
    private int totalScore;
    private float randomValue;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        // ĳ���� ���� ���� �迭 ���� Ȯ��
        if (characterPrefabs.Length > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            SpawnCharacter(); // ���� ���� �� ���õ� ĳ���� ����
        }
        else
        {
            Debug.LogError("��ȿ���� ���� ĳ���� �ε����̰ų� characterPrefabs �迭�� ��� �ֽ��ϴ�.");
        }

        InvokeRepeating("MakeRain", 0f, 0.5f);
        InvokeRepeating("DropItem", 1f, 1f);
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
