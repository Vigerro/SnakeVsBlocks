using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private int _distanceBetweenFullLines;
    [SerializeField] private int _distanceBetweenRandomLines;
    [SerializeField] private int _repeatCount;
    [Space(1)]
    [Header("Chance in %")]
    [SerializeField] private int _blockSpawnChance;

    private SpawnPosition[] _spawnPositions;

    private void Start()
    {
        _spawnPositions = GetComponentsInChildren<SpawnPosition>();

        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenFullLines);
            GenerateFullLine(_spawnPositions, _blockTemplate.gameObject);
            MoveSpawner(_distanceBetweenRandomLines);
            GenerateRandomLine(_spawnPositions, _blockTemplate.gameObject, _blockSpawnChance);
        }
    }

    private void GenerateFullLine(SpawnPosition[] spawnPositions, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GenerateElement(spawnPositions[i].transform.position, generatedElement);
        }
    }

    private void GenerateRandomLine(SpawnPosition[] spawnPositions, GameObject generatedElement, int spawnChance)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if(Random.Range(0, 100) < spawnChance)
            {
                GenerateElement(spawnPositions[i].transform.position, generatedElement);
            }
        }
    }

    private GameObject GenerateElement(Vector3 spawnPosition, GameObject generatedElement)
    {
        spawnPosition.y -= generatedElement.transform.localScale.y;
        return Instantiate(generatedElement, spawnPosition, Quaternion.identity, _container);
    }

    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.x);
    }
}
