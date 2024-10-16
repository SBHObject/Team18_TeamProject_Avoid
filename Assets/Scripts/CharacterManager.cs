using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    public GameObject[] characterPrefabs;  // ĳ���� ������ �迭
    private int selectedCharacterIndex = 0;  // ���õ� ĳ���� �ε���

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ��� �ν��Ͻ� �ı�
        }
    }

    // ĳ���� ���� �� ȣ��Ǵ� �Լ� (CharacterSelection���� ȣ��)
    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
    }

    // ���õ� ĳ������ �������� ��ȯ
    public GameObject GetSelectedCharacterPrefab()
    {
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            return characterPrefabs[selectedCharacterIndex];
        }
        else
        {
            Debug.LogError("��ȿ���� ���� ĳ���� �ε����Դϴ�.");
            return null;
        }
    }
}
