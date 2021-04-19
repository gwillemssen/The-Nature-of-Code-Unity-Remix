using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecosystemCreature1Script : MonoBehaviour
{
    creatureMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = new creatureMover();
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 dir = mover.subtractVectors(charPos.position, mover.location);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mover.subtractVectors(mousePos, mover.location);
        mover.acceleration = mover.multiplyVector(dir.normalized, (-1 / dir.magnitude));

       
        if (dir.magnitude < 5 && dir.magnitude > 2)
        {
            mover.acceleration = mover.multiplyVector(dir.normalized, -2f);
            if (dir.magnitude > 5)
            {
                mover.acceleration = Vector2.zero;
                mover.velocity = Vector2.zero;
            }
        }
        else if (dir.magnitude < 2)
        {
            mover.acceleration = mover.multiplyVector(dir.normalized, -5f);
            if (dir.magnitude > 5)
            {
                mover.acceleration = Vector2.zero;
                mover.velocity = Vector2.zero;
            }
        }

        mover.Update();
    }
    public IEnumerator timer()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(mover.newStep());
        StartCoroutine(timer2());
    }

    public IEnumerator timer2()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(timer());
    }
}

public class creatureMover
{

    // The basic properties of a mover class
    public Vector2 location, velocity, acceleration;
    private float topSpeed;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Gives the class a GameObject to draw on the screen
    private GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public creatureMover()
    {
        findWindowLimits();
        location = Vector2.zero; // Vector2.zero is a (0, 0) vector
        velocity = Vector2.zero;
        acceleration = new Vector2(-0.1F, -1F);
        topSpeed = 5;

        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public IEnumerator newStep()
    {
        yield return new WaitForSeconds(10);
        location = mover.transform.position;
        velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        location += velocity * Time.deltaTime; // Time.deltaTime is the time passed since the last frame.

        // Updates the GameObject of this movement
        mover.transform.position = new Vector2(location.x, location.y);
    }

    public void Update()
    {
        CheckEdges();
        if (velocity.magnitude <= topSpeed)
        {
            // Speeds up the mover
            velocity += acceleration * Time.deltaTime;

            // Limit Velocity to the top speed
            velocity = Vector2.ClampMagnitude(velocity, topSpeed);

            // Moves the mover
            location += velocity * Time.deltaTime;

            // Updates the GameObject of this movement
            mover.transform.position = new Vector3(location.x, location.y, 0);

        }
        else
        {
            velocity -= acceleration * Time.deltaTime;
            location += velocity * Time.deltaTime;
            mover.transform.position = new Vector3(location.x, location.y, 0);
        }
    }

    public void CheckEdges()
    {
        if (location.x > maximumPos.x)
        {
            location.x -= maximumPos.x - minimumPos.x;
            acceleration = Vector2.zero;
            velocity = Vector2.zero;
        }
        else if (location.x < minimumPos.x)
        {
            location.x += maximumPos.x - minimumPos.x;
            acceleration = Vector2.zero;
            velocity = Vector2.zero;
        }
        if (location.y > maximumPos.y)
        {
            location.y -= maximumPos.y - minimumPos.y;
            acceleration = Vector2.zero;
            velocity = Vector2.zero;
        }
        else if (location.y < minimumPos.y)
        {
            location.y += maximumPos.y - minimumPos.y;
            acceleration = Vector2.zero;
            velocity = Vector2.zero;
        }
    }

    private void findWindowLimits()
    {
        // The code to find the information on the camera as seen in Figure 1.2

        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }


    // This method calculates A - B component wise
    // subtractVectors(vecA, vecB) will yield the same output as Unity's built in operator: vecA - vecB
    public Vector2 subtractVectors(Vector2 vectorA, Vector2 vectorB)
    {
        float newX = vectorA.x - vectorB.x;
        float newY = vectorA.y - vectorB.y;
        return new Vector2(newX, newY);
    }

    // This method calculates A * b component wise
    // multiplyVector(vector, factor) will yield the same output as Unity's built in operator: vector * factor
    public Vector2 multiplyVector(Vector2 toMultiply, float scaleFactor)
    {
        float x = toMultiply.x * scaleFactor;
        float y = toMultiply.y * scaleFactor;
        return new Vector2(x, y);
    }

}

