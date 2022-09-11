using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Scroll Speed Settings")]
    [SerializeField] private float levelScrollSpeed;
    //[SerializeField] private Vector2 minMaxScrollSpeed;

    [Header("Level Chunks Settings")]
    [SerializeField] private Transform chunkDespawnTransform;
    [Tooltip("Number of chunks per level excluding Start and Finish chunks")]
    [SerializeField] private int chunksPerLevel;
    [SerializeField] private LevelChunk startChunk;
    [SerializeField] private LevelChunk finishChunk;
    [SerializeField] private List<LevelChunk> levelChunks;

    private List<LevelChunk> levelChunkInstances = new List<LevelChunk>();
    private LevelChunk startChunkInstance;
    private LevelChunk finishChunkInstance;

    private Vector3 nextChunkPosition;

    private void Awake()
    {
        //Instantiate all the potential chunks
        for (int i = 0; i < levelChunks.Count; i++)
        {
            LevelChunk newChunk = Instantiate(levelChunks[i]);
            levelChunkInstances.Add(newChunk);
            newChunk.SetUp(levelScrollSpeed, chunkDespawnTransform);
            newChunk.gameObject.SetActive(false);
        }

        if (startChunk != null && finishChunk != null)
        {
            startChunkInstance = Instantiate(startChunk);
            startChunkInstance.SetUp(levelScrollSpeed, chunkDespawnTransform);
            startChunkInstance.gameObject.SetActive(false);
            finishChunkInstance = Instantiate(finishChunk);
            finishChunkInstance.SetUp(levelScrollSpeed, chunkDespawnTransform);
            finishChunkInstance.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Start or Finish chunks are not set up!");
        }

    }

    private void OnEnable()
    {
        GameManager.onGameStateChange += ResetLevel;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChange -= ResetLevel;
    }

    //Generates a level out of unique chunks
    public void GenerateLevel()
    {
        if (chunksPerLevel > levelChunkInstances.Count)
        {
            Debug.Log("Not enough chunks to generate level of this length! Generating a shorter level instead");
            chunksPerLevel = levelChunkInstances.Count;
        }

        nextChunkPosition = Vector3.zero;

        SpawnChunk(startChunkInstance);

        for (int i = 0; i < chunksPerLevel; i++)
        {
            LevelChunk newChunk = levelChunkInstances[Random.Range(0, levelChunkInstances.Count)];
            while (newChunk.gameObject.activeInHierarchy)
            {
                newChunk = levelChunkInstances[Random.Range(0, levelChunkInstances.Count)];
            }
            SpawnChunk(newChunk);
        }

        SpawnChunk(finishChunkInstance);
    }

    //sets up the chunk at the correct position, enables it and starts moving it. 
    private void SpawnChunk(LevelChunk levelChunk)
    {
        if (levelChunk == null)
        {
            Debug.LogError("Cannot spawn chunk!");
            return;
        }

        levelChunk.transform.position = nextChunkPosition;
        nextChunkPosition = levelChunk.GetChunkEnd().position;
        levelChunk.gameObject.SetActive(true);
        levelChunk.StartMoving();
    }

    public void ResetLevel()
    {
        if (GameManager.Instance.gameState != GameState.Starting)
            return;

        startChunkInstance.StopMoving();
        startChunkInstance.gameObject.SetActive(false);
        finishChunkInstance.StopMoving();
        finishChunkInstance.gameObject.SetActive(false);

        foreach (var chunk in levelChunkInstances)
        {
            chunk.StopMoving();
            chunk.gameObject.SetActive(false);
        }

        GenerateLevel();

        GameManager.Instance.ChangeState(GameState.Running);
    }

    public float GetLevelScrollSpeed() => levelScrollSpeed;
}
