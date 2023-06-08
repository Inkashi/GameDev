using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChunkPlace : MonoBehaviour
{

    public Transform Player;
    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    // Start is called before the first frame update
    void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    void Update()
    {
        if (Player.position.x > spawnedChunks[spawnedChunks.Count - 1].end.position.x - 5)
        {
            SpawnChunk();
        }
        else if (Player.position.y < spawnedChunks[0].begin.position.y - 10)
        {
            respawn();
        }
    }

    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(GetRandomChunk());
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
        spawnedChunks.Add(newChunk);
        if (spawnedChunks.Count > 10)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }

    private void respawn()
    {
        Player.transform.position = new Vector3(spawnedChunks[0].begin.position.x + 2, spawnedChunks[0].begin.position.y + 2);
    }

    private Chunk GetRandomChunk()
    {
        List<float> chanches_chunks = new List<float>();
        for (int i = 0; i < ChunkPrefabs.Length; i++)
        {
            chanches_chunks.Add(ChunkPrefabs[i].Chance.Evaluate(Player.transform.position.x));
        }
        float temp_chunk = Random.Range(0, chanches_chunks.Sum());
        float sum = 0;
        for (int i = 0; i < chanches_chunks.Count; i++)
        {
            sum += chanches_chunks[i];
            if (temp_chunk < sum)
            {
                return ChunkPrefabs[i];
            }
        }
        return ChunkPrefabs[ChunkPrefabs.Length - 1];
    }
}
