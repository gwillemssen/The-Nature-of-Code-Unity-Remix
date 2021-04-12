using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinTerrain : MonoBehaviour
{
    List<Vector3> terrainArray = new List<Vector3>();
    public GameObject terrainCube;
    public int cols;
    public int rows;


    // Start is called before the first frame update
    void Start()
    {
        GameObject terrain = new GameObject();
        terrain.name = "terrain";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
