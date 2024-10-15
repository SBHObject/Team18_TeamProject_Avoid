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
    public GameObject endPanel;
    public Text totalScoreTxt;

    public GameObject endAnimationImage; // 1�� ���� ������ �̹��� (�ִϸ��̼�)

    private int selectedCharacterIndex = 0; // ���õ� ĳ���� �ε���
    private GameObject currentCharacter; // ���� ���ӿ� ���Ǵ� ĳ����
    private int totalScore;

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

        InvokeRepeating("MakeRain", 0f, 1f);
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
        // �ڷ�ƾ�� ȣ���Ͽ� 1�� ���� �̹��� �ִϸ��̼��� �����ְ� �� �Ŀ� EndPanel�� ���
        StartCoroutine(ShowEndAnimation());
    }

    IEnumerator ShowEndAnimation()
    {
        // �ִϸ��̼� �̹��� Ȱ��ȭ
        if (endAnimationImage != null)
        {
            endAnimationImage.SetActive(true); // �ִϸ��̼� �̹����� Ȱ��ȭ
        }

        // 1�� ���� ��� (�ִϸ��̼��� �������� �ð�)
        yield return new WaitForSecondsRealtime(1f); // ���� �ð����� 1�� ���

        // �ִϸ��̼� �̹��� ��Ȱ��ȭ
        if (endAnimationImage != null)
        {
            endAnimationImage.SetActive(false); // �ִϸ��̼� �̹����� ��Ȱ��ȭ
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