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
    public bool ChangedSpawnobj = false;
    private bool ActiveGenerator = true;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    // Start is called before the first frame update
    void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    void Update()
    {
        if (Player.position.x > spawnedChunks[spawnedChunks.Count - 1].end.position.x - 5 && ActiveGenerator)
        {
            SpawnChunk();
        }
        else if (Player.position.y < spawnedChunks[0].begin.position.y - 10)
        {
            Player.GetComponent<HeroMovement>().TakeDamage(10);
            respawn();
        }
    }

    private void SpawnChunk()
    {
        float ycoord = 0.5f;
        float xcoord = 0.3f;
        if (Player.GetComponent<HeroMovement>().DoubleJumpAccses == true)
        {
            ycoord *= 2;
        }
        if (Player.GetComponent<HeroMovement>().DashAccses == true)
        {
            xcoord *= 2;
        }
        Chunk newChunk = Instantiate(GetRandomChunk());
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
        if ((newChunk.tag == "platform" || spawnedChunks[spawnedChunks.Count - 1].tag == "platform") && ChangedSpawnobj)
        {
            newChunk.transform.position += new Vector3(Random.Range(0, xcoord), Random.Range(-ycoord, ycoord), 0);
        }
        else if (newChunk.tag == "BossChunk")
        {
            newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
            ActiveGenerator = false;
        }
        spawnedChunks.Add(newChunk);
        if (spawnedChunks.Count > 20)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }

    private void respawn()
    {
        Player.transform.position = new Vector3(spawnedChunks[1].begin.position.x + 2, spawnedChunks[1].begin.position.y + 2);
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

    public void ChangeSpawnMethod()
    {
        ChangedSpawnobj = !ChangedSpawnobj;
    }
}
