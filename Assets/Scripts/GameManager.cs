using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform spawnPoint;  // ĳ���� ���� ��ġ
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

        InvokeRepeating("MakeRain", 0f, 1f);  // �� ������ ����
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
