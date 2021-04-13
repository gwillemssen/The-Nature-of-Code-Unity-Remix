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

        float xOff = 0;
        for(int i = 0; i < cols; i++)
        {
            float yOff = 0;
            for(int j = 0; j < rows; j++)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
