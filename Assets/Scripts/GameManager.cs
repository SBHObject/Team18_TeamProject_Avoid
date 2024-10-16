using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] characterPrefabs; // 캐릭터 프리팹 배열
    public Transform spawnPoint; // 캐릭터 스폰 위치
    public GameObject rain;
    public GameObject endPanel;
    public Text totalScoreTxt;

    public GameObject endObject; // 1초 동안 보여줄 비활성화된 게임 오브젝트

    private int selectedCharacterIndex = 0; // 선택된 캐릭터 인덱스
    private GameObject currentCharacter; // 현재 게임에 사용되는 캐릭터
    private int totalScore;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        // 캐릭터 스폰 전에 배열 범위 확인
        if (characterPrefabs.Length > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            SpawnCharacter(); // 게임 시작 시 선택된 캐릭터 스폰
        }
        else
        {
            Debug.LogError("유효하지 않은 캐릭터 인덱스이거나 characterPrefabs 배열이 비어 있습니다.");
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