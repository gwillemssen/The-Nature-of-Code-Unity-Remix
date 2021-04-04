using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exerciseScripti2 : MonoBehaviour
{

    //We need to create a walker
    myIntroMover2 walker;
    myIntroFollower follower;

    // Start is called before the first frame update
    void Start()
    {
        walker = new myIntroMover2();
        follower = new myIntroFollower(walker);
    }

    // Update is called once per frame
    void FixedUpdate()
    {        //Have the walker choose a direction
        walker.step();
        walker.CheckEdges();

        follower.step();
        follower.CheckEdges();
    }
}
    public class myIntroMover2
    {
        // The basic properties of a mover class
        private Vector3 location;

        // The window limits
        private Vector2 minimumPos, maximumPos;

        // Gives the class a GameObject to draw on the screen
        public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        public myIntroMover2()
        {
            mover.transform.position = new Vector3(-5, 0, 0);
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
                location = new Vector2(-5, 0);
            }
            else if (location.x < minimumPos.x)
            {
                location = new Vector2(-5, 0);
            }
            if (location.y > maximumPos.y)
            {
                location = new Vector2(-5, 0);
            }
            else if (location.y < minimumPos.y)
            {
                location = new Vector2(-5, 0);
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

public class myIntroFollower
{
    // The basic properties of a mover class
    private Vector3 location;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    private myIntroMover2 target;
    public myIntroFollower(myIntroMover2 target)
    {
        mover.transform.position = new Vector3(5, 0, 0);
        Debug.Log("TEST");
        findWindowLimits();
        //location = new Vector2(-5, 0);
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));

        this.target = target;
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
            mover.transform.position = Vector3.MoveTowards(mover.transform.position, target.mover.transform.position, Time.deltaTime * 5);
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
            mover.transform.position = Vector3.MoveTowards(mover.transform.position, target.mover.transform.position, Time.deltaTime * 5);
        }

    }

    public void CheckEdges()
    {
        location = mover.transform.position;

        if (location.x > maximumPos.x)
        {
            location = new Vector2(5, 0);
        }
        else if (location.x < minimumPos.x)
        {
            location = new Vector2(5, 0);
        }
        if (location.y > maximumPos.y)
        {
            location = new Vector2(5, 0);
        }
        else if (location.y < minimumPos.y)
        {
            location = new Vector2(5, 0);
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
