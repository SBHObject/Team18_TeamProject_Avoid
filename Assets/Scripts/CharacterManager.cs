using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    public GameObject[] characterPrefabs;  // 캐릭터 프리팹 배열
    private int selectedCharacterIndex = 0;  // 선택된 캐릭터 인덱스

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 중복된 인스턴스 파괴
        }
    }

    // 캐릭터 선택 시 호출되는 함수 (CharacterSelection에서 호출)
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
    }

    // 선택된 캐릭터의 프리팹을 반환
    public GameObject GetSelectedCharacterPrefab()
    {
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            return characterPrefabs[selectedCharacterIndex];
        }
        else
        {
            Debug.LogError("유효하지 않은 캐릭터 인덱스입니다.");
            return null;
        }
    }
}
