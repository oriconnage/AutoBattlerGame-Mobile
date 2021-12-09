using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : Singleton<LevelSetup>
{
    [SerializeField]
    private GameObject TilesPrefab;

    [SerializeField]
    private Vector2 mapSize;

    [SerializeField]
    private GameObject[] Obstacle;
    [SerializeField]
    private Transform Folder;
    
    public Dictionary<Point, TilesManager> Tiles { get; set; }
    
  
    // Start is called before the first frame update
    void Start()
    {
        TileSetUp();
      
    }
    public void TileSetUp()
    {
        Tiles = new Dictionary<Point, TilesManager>();
        for (int x =0; x <mapSize.x; x++)
        {
            for(int y =0; y < mapSize.y; y++)
            {

                Placement(x, y,Folder);
                Debug.Log(Tiles);
            }
        }
    }

    private void Placement(int x , int y, Transform parent)
    {
        Vector3 tilesPos = new Vector3(-mapSize.x / 2 + 0.5f + x, 0.0f, -mapSize.y / 2 + 0.5f + y);
        //Transform newTile = Instantiate(TilesPrefab.transform, tilesPos, Quaternion.Euler(Vector3.right * 90)) as Transform;
        //newTile.localScale = Vector3.one * (0.5f);
       //newTile.SetParent(parent);

        // each tile instantiated with TileManager script
        TilesManager manager = Instantiate(TilesPrefab,tilesPos,Quaternion.Euler(Vector3.right*90)).GetComponent<TilesManager>();
       // Debug.Log(x+""+y);

        manager.Setup(new Point(x,y),tilesPos, parent);

        //delete new tiles
       // DestroyImmediate(newTile.gameObject);
    }
}

