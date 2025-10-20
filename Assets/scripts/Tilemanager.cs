using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemanager : MonoBehaviour
{
    public GameObject[] tiles;

    public float zSpawn = 0;
    public float tileLength = 30;
    public  int numberofTile = 5;
    public Transform playerTransform;
    private List<GameObject> activeTiles =  new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberofTile; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tiles.Length));

        }
    }
        // Update is called once per frame
        void Update()
        {
        if(playerTransform.position.z -35 > zSpawn - (numberofTile * tileLength)){

            SpawnTile(Random.Range(0, tiles.Length));
            DeleteTile();
        }
        }

    public void SpawnTile(int tileIndex)
    {
       GameObject go =  Instantiate(tiles[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    }
