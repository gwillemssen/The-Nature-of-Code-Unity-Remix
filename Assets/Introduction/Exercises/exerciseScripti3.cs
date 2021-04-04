using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exerciseScripti3 : MonoBehaviour
{
    //We need to create a walker
    myIntroWalker1 walker1;
    myIntroWalker2 walker2;
    myIntroFollower1 follower;

    // Start is called before the first frame update
    void Start()
    {
        walker1 = new myIntroWalker1();
        walker2 = new myIntroWalker2();
        follower = new myIntroFollower1(walker1, walker2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {        //Have the walker choose a direction
        walker1.step();
        walker1.CheckEdges();

        walker2.step();
        walker2.CheckEdges();

        follower.step();
        follower.CheckEdges();
    }
}

public class myIntroWalker1
{
    // The basic properties of a mover class
    private Vector3 location;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public myIntroWalker1()
    {
        mover.transform.position = new Vector3(-10, 0, 0);
        Debug.Log("TEST");
        findWindowLimits();
        //location = new Vector2(-5, 0);
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public void step()
    {

        float num = Random.Range(0f, 1f);
        Debug.Log(num);
        Vector3 heading = new Vector3(0, 0, 0);
        //Each frame choose a new Random number 0,1,2,3, 
        //If the number is equal to one of those values, take a step
        //Moving using velocity instead of position because using postion recursively moved sphere in direction of first vector

        if (num < .25f)
        {
            Debug.Log("Right");
            heading.x++;
            mover.transform.position += heading * Time.deltaTime * 10;
        }
        else if (num >= .25f && num < .5f)
        {
            Debug.Log("Down");
            heading.y--;
            mover.transform.position += heading * Time.deltaTime * 10;
        }
        else if (num >= .5f && num < .75f)
        {
            Debug.Log("Up");
            heading.y++;
            mover.transform.position += heading * Time.deltaTime * 10;
        }
        else
        {
            Debug.Log("Left");
            heading.x--;
            mover.transform.position += heading * Time.deltaTime * 10;
        }

    }

    public void CheckEdges()
    {
        location = mover.transform.position;

        if (location.x > maximumPos.x)
        {
            location = new Vector2(-10, 0);
        }
        else if (location.x < minimumPos.x)
        {
            location = new Vector2(-10, 0);
        }
        if (location.y > maximumPos.y)
        {
            location = new Vector2(-10, 0);
        }
        else if (location.y < minimumPos.y)
        {
            location = new Vector2(-10, 0);
        }
        mover.transform.position = location;
    }

    private void findWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}

public class myIntroWalker2
{
    // The basic properties of a mover class
    private Vector3 location;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public myIntroWalker2()
    {
        mover.transform.position = new Vector3(10, 0, 0);
        Debug.Log("TEST");
        findWindowLimits();
        //location = new Vector2(-5, 0);
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public void step()
    {

        float num = Random.Range(0f, 1f);
        Debug.Log(num);
        Vector3 heading = new Vector3(0, 0, 0);
        //Each frame choose a new Random number 0,1,2,3, 
        //If the number is equal to one of those values, take a step
        //Moving using velocity instead of position because using postion recursively moved sphere in direction of first vector

        if (num < .25f)
        {
            Debug.Log("Right");
            heading.x++;
            mover.transform.position += heading * Time.deltaTime * 10;
        }
        else if (num >= .25f && num < .5f)
        {
            Debug.Log("Down");
            heading.y--;
            mover.transform.position += heading * Time.deltaTime * 10;
        }
        else if (num >= .5f && num < .75f)
        {
            Debug.Log("Up");
            heading.y++;
            mover.transform.position += heading * Time.deltaTime * 10;
        }
        else
        {
            Debug.Log("Left");
            heading.x--;
            mover.transform.position += heading * Time.deltaTime * 10;
        }

    }

    public void CheckEdges()
    {
        location = mover.transform.position;

        if (location.x > maximumPos.x)
        {
            location = new Vector2(10, 0);
        }
        else if (location.x < minimumPos.x)
        {
            location = new Vector2(10, 0);
        }
        if (location.y > maximumPos.y)
        {
            location = new Vector2(10, 0);
        }
        else if (location.y < minimumPos.y)
        {
            location = new Vector2(10, 0);
        }
        mover.transform.position = location;
    }

    private void findWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}

public class myIntroFollower1
{
    // The basic properties of a mover class
    private Vector3 location;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    private myIntroWalker1 target1;
    private myIntroWalker2 target2;
    public myIntroFollower1(myIntroWalker1 target1, myIntroWalker2 target2)
    {
        mover.transform.position = new Vector3(0, 0, 0);
        Debug.Log("TEST");
        findWindowLimits();
        //location = new Vector2(-5, 0);
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));

        this.target1 = target1;
        this.target2 = target2;
    }

    public void step()
    {

        float num = Random.Range(0f, 1f);
        Debug.Log(num);
        Vector3 heading = new Vector3(0, 0, 0);
        //Each frame choose a new Random number 0,1,2,3, 
        //If the number is equal to one of those values, take a step
        //Moving using velocity instead of position because using postion recursively moved sphere in direction of first vector

        if (num < .25f)
        {
            mover.transform.position = Vector3.MoveTowards(mover.transform.position, target1.mover.transform.position, Time.deltaTime * 5);
        }
        else if (num >= .25f && num < .5f)
        {
            mover.transform.position = Vector3.MoveTowards(mover.transform.position, target2.mover.transform.position, Time.deltaTime * 5);
        }
        else if (num >= .5f && num < .75f)
        {
            mover.transform.position = Vector3.MoveTowards(mover.transform.position, target2.mover.transform.position, Time.deltaTime * 5);
        }
        else
        {
            mover.transform.position = Vector3.MoveTowards(mover.transform.position, target1.mover.transform.position, Time.deltaTime * 5);
        }

    }

    public void CheckEdges()
    {
        location = mover.transform.position;

        if (location.x > maximumPos.x)
        {
            location = new Vector2(0, 0);
        }
        else if (location.x < minimumPos.x)
        {
            location = new Vector2(0, 0);
        }
        if (location.y > maximumPos.y)
        {
            location = new Vector2(0, 0);
        }
        else if (location.y < minimumPos.y)
        {
            location = new Vector2(0, 0);
        }
        mover.transform.position = location;
    }

    private void findWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}

