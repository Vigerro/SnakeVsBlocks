using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _distanceBetweenFullLines;
    [SerializeField] private int _distanceBetweenRandomLines;
    [SerializeField] private int _repeatCount;
    [Header("Spawn templates")]
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private Wall _wallTemplate;
    [Header("Chance in %")]
    [SerializeField] private int _blockSpawnChance;
    [SerializeField] private int _wallSpawnChance;

    private BlockSpawnPosition[] _blockSpawnPositions;
    private WallSpawnPosition[] _wallSpawnPositions;

    private void Start()
    {
        _blockSpawnPositions = GetComponentsInChildren<BlockSpawnPosition>();
        _wallSpawnPositions = GetComponentsInChildren<WallSpawnPosition>();

        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenFullLines);
            GenerateRandomLine(_wallSpawnPositions, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenFullLines / 2f, _distanceBetweenFullLines / 4f);
            GenerateFullLine(_blockSpawnPositions, _blockTemplate.gameObject);
            MoveSpawner(_distanceBetweenRandomLines);
            GenerateRandomLine(_wallSpawnPositions, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenRandomLines / 2f, _distanceBetweenRandomLines / 4f);
            GenerateRandomLine(_blockSpawnPositions, _blockTemplate.gameObject, _blockSpawnChance);
        }
    }

    private void GenerateFullLine(SpawnPosition[] spawnPositions, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GenerateElement(spawnPositions[i].transform.position, generatedElement);
        }
    }

    private void GenerateRandomLine(SpawnPosition[] spawnPositions, GameObject generatedElement, int spawnChance, float scaleY = 1, float offsetY = 0)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if(Random.Range(0, 100) < spawnChance)
            {
                GameObject spawnedElement = GenerateElement(spawnPositions[i].transform.position, generatedElement, offsetY);
                spawnedElement.transform.localScale = new Vector3(spawnedElement.transform.localScale.x, scaleY, spawnedElement.transform.localScale.z);
            }
        }
    }

    private GameObject GenerateElement(Vector3 spawnPosition, GameObject generatedElement, float offsetY = 0)
    {
        spawnPosition.y -= offsetY;
        return Instantiate(generatedElement, spawnPosition, Quaternion.identity, _container);
    }

    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.x);
    }
}
