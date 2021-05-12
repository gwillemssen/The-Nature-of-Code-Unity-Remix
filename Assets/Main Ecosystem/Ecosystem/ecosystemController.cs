using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecosystemController : MonoBehaviour
{
    //this here will control the whole ecosystem

    //Chapter 1 creature
    public List<GameObject> chapterOneCreatures = new List<GameObject>();
    public GameObject chapterOneCreature;
    public int chapterOneCreaturePopulation;

    //chapter 2 creature
    public List<GameObject> chapterTwoCreatures = new List<GameObject>();
    public GameObject chapterTwoCreature;
    public int chapterTwoCreaturePopulation;

    //chapter 3 creature
    public List<GameObject> chapterThreeCreatures = new List<GameObject>();
    public GameObject chapterThreeCreature;
    public int chapterThreeCreaturePopulation;

    //chapter 6 creature
    public List<GameObject> chapterSixCreatures = new List<GameObject>();
    public GameObject chapterSixCreature;
    public int chapterSixCreaturePopulation;

    //chapter 7 creature
    public List<GameObject> chapterSevenCreatures = new List<GameObject>();
    public GameObject chapterSevenCreature;
    public int chapterSevenCreaturePopulation;

    //chapter 8 creature
    public List<GameObject> chapterEightCreatures = new List<GameObject>();
    public GameObject chapterEightCreature;
    public int chapterEightCreaturePopulation;


    //terrain
    public perlinTerrain terrain;
    public float terrainMin;
    

    // Start is called before the first frame update
    void Start()
    {
        //chapter one creature spawn
        for (int i = 0; i < chapterOneCreaturePopulation; i++)
        {
            GameObject chapterOneC = Instantiate(chapterOneCreature, new Vector3(Random.Range(terrainMin, terrain.cols), Random.Range(10f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterOneCreatures.Add(chapterOneC);
        }
        for (int i = 0; i < chapterTwoCreaturePopulation; i++)
        {
            GameObject chapterTwoC = Instantiate(chapterTwoCreature, new Vector3(Random.Range(terrainMin, terrain.cols), Random.Range(10f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterTwoCreatures.Add(chapterTwoC);
        }
        for (int i = 0; i < chapterThreeCreaturePopulation; i++)
        {
            GameObject chapterThreeC = Instantiate(chapterThreeCreature, new Vector3(Random.Range(terrainMin, terrain.cols), Random.Range(10f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterThreeCreatures.Add(chapterThreeC);
        }
        for (int i = 0; i < chapterSixCreaturePopulation; i++)
        {
            GameObject chapterSixC = Instantiate(chapterSixCreature, new Vector3(Random.Range(terrainMin, terrain.cols), Random.Range(10f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterSixCreatures.Add(chapterSixC);
        }
        for (int i = 0; i < chapterSevenCreaturePopulation; i++)
        {
            GameObject chapterSevenC = Instantiate(chapterSevenCreature, new Vector3(Random.Range(terrainMin, terrain.cols), Random.Range(10f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterSevenCreatures.Add(chapterSevenC);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
