using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinTerrain : MonoBehaviour
{
    List<Vector3> terrainArray = new List<Vector3>();
    public GameObject terrainCube;
    public int cols;
    public int rows;
    public Color color1, color2, color3, color4, color5, color6, color7, color8;

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
                //want a cube at 0,0 to be mapped to a random height - unity perlin noise only lets us do 0-1 so we are multiplying by 10
                float theta = ExtensionMethods.map(Mathf.PerlinNoise(xOff, yOff), 0f, 1f, 0f, 25f);

                float rotationTheta = ExtensionMethods.map(Mathf.PerlinNoise(xOff, yOff), 0f, 1f, 0f, 8f);

                Quaternion perlinRotation = new Quaternion();
                Vector3 perlinRotationVector3 = new Vector3(Mathf.Cos(rotationTheta), Mathf.Sin(rotationTheta), 0f);
                perlinRotation.eulerAngles = perlinRotationVector3 * 100f;

                terrainCube = Instantiate(terrainCube, new Vector3(i, theta, j), perlinRotation);
                terrainCube.transform.SetParent(terrain.transform);

                Renderer terrainRenderer = terrainCube.GetComponent<Renderer>();
                terrainRenderer.material.SetColor("_Color", colorTerrain(terrainCube.transform.position));

                yOff += .03f;
            }

            xOff += .03f;
        }
    }

    private Color colorTerrain (Vector3 terrainCubePosition)
    {
        Color terrainColor = new Vector4(1f, 1f, 1f);

        if (terrainCubePosition.y >= 0 && terrainCubePosition.y < 3)
        {
            terrainColor = color1;
        }
        else if (terrainCubePosition.y >= 3 && terrainCubePosition.y < 6)
        {
            terrainColor = color2;
        }
        else if (terrainCubePosition.y >= 6 && terrainCubePosition.y < 9)
        {
            terrainColor = color3;
        }
        else if (terrainCubePosition.y >= 9 && terrainCubePosition.y < 12)
        {
            terrainColor = color4;
        }
        else if (terrainCubePosition.y >= 12 && terrainCubePosition.y < 15)
        {
            terrainColor = color5;
        }
        else if (terrainCubePosition.y >= 15 && terrainCubePosition.y < 18)
        {
            terrainColor = color6;
        }
        else if (terrainCubePosition.y >= 18 && terrainCubePosition.y < 21)
        {
            terrainColor = color7;
        }
        else 
        {
            terrainColor = color8;
        }

        return terrainColor;
    }
}
