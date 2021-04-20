using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecosystemCreature3Script : MonoBehaviour
{
    List<oscillatorWings> oscilattors = new List<oscillatorWings>();
    oscillatorBody ob;
    void Start()
    {
        ob = new oscillatorBody();
        while (oscilattors.Count < 2)
        {
            oscillatorWings o = new oscillatorWings();
            o.oGameObject.transform.SetParent(ob.body.transform);
            oscilattors.Add(o);
        }
        oscilattors[1].setAmplitude(new Vector2(8f, -3));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ob.Update();
        foreach (oscillatorWings o in oscilattors)
        {
            //Each oscillator object oscillating on the x-axis
            float x = Mathf.Sin(o.angle.x) * o.amplitude.x;
            //Each oscillator object oscillating on the y-axis
            float y = Mathf.Sin(o.angle.y) * o.amplitude.y;
            //Add the oscillator's velocity to its angle
            o.angle += o.velocity;
            // Draw the line for each oscillator
            o.lineRender.SetPosition(0, ob.body.transform.position);
            o.lineRender.SetPosition(1, o.oGameObject.transform.position);
            //Move the oscillator
            o.oGameObject.transform.transform.Translate(new Vector2(x, y) * Time.deltaTime);
        }
        
    }
}

public class oscillatorBody
{
    public Vector3 velocity, acceleration, location;
    public GameObject body;
    public float topSpeed;
    public float minX, maxX, minY, maxY, minZ, maxZ;

    public oscillatorBody()
    {
        body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        location = this.body.transform.position;
        velocity = new Vector3(0.1F, 0, 1F);
        acceleration = new Vector3(0.1F, 0, 1F);
        topSpeed = 5;
        minX = 10f;
        maxX = 99f;
        minY = 7f;
        maxY = 20f;
        minZ = 10f;
        maxZ = 99f;
    }

    public void Update()
    {
        checkEdges();
        Move();
        //Debug.Log(location + ""+"" + velocity + ""+"" + acceleration);
    }

    public void Move()
    {
        location = this.body.transform.position;

        if (velocity.magnitude <= topSpeed)
        {
            // Speeds up the mover
            velocity += acceleration * Time.deltaTime;

            // Limit Velocity to the top speed
            velocity = Vector3.ClampMagnitude(velocity, topSpeed);

            // Moves the mover
            location += velocity * Time.deltaTime;

            // Updates the GameObject of this movement
            this.body.transform.position = new Vector3(location.x, location.y, location.z);

        }
        else
        {
            velocity -= acceleration * Time.deltaTime;
            location += velocity * Time.deltaTime;
            this.body.transform.position = new Vector3(location.x, location.y, location.z);
        }
    }

    void checkEdges()
    {
        location = this.body.transform.position;

        if (location.x >= maxX)
        {
            location.x = minX + 1;
            //acceleration = Vector3.zero;
            //velocity = Vector3.zero;
        }
        else if (location.x <= minX)
        {
            location.x += maxX - minX;
            //acceleration = Vector3.zero;
            //velocity = Vector3.zero;
        }
        if (location.y >= maxY)
        {
            location.y = minY + 1;
            //acceleration = Vector3.zero;
            //velocity = Vector3.zero;
        }
        else if (location.y <= minY)
        {
            location.y += maxY - minY;
            //acceleration = Vector3.zero;
            //velocity = Vector3.zero;
        }
        if (location.z >= maxZ)
        {
            location.z -= maxZ - minZ;
            //acceleration = Vector3.zero;
            //velocity = Vector3.zero;
        }
        else if (location.z <= minZ)
        {
            location.z += maxZ - minZ;
            //acceleration = Vector3.zero;
            //velocity = Vector3.zero;
        }

        this.body.transform.position = location;
    }
}


public class oscillatorWings
{

    // The basic properties of an oscillator class
    public Vector2 velocity, angle, amplitude;

    // The window limits
    private Vector2 maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject oGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    //Create variables for rendering the line between two vectors
    public LineRenderer lineRender;

    public oscillatorWings()
    {
        //findWindowLimits();
        angle = Vector2.zero;
        velocity = new Vector2(.08f, .08f);
        amplitude = new Vector2(-8f, -3f);

        //We need to create a new material for WebGL
        Renderer r = oGameObject.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));

        // Create a GameObject that will be the line
        GameObject lineDrawing = new GameObject();
        //Add the Unity Component "LineRenderer" to the GameObject lineDrawing.
        lineRender = lineDrawing.AddComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Diffuse"));
        //Begin rendering the line between the two objects. Set the first point (0) at the centerSphere Position
        //Make sure the end of the line (1) appears at the new Vector3
        Vector2 center = new Vector2(0f, 0f);
        lineRender.SetPosition(0, center);
    }

    public void setAmplitude(Vector2 ampValue)
    {
        this.amplitude = ampValue;
    }

    //private void findWindowLimits()
    //{
    //    // We want to start by setting the camera's projection to Orthographic mode
    //    Camera.main.orthographic = true;
    //    // Next we grab the minimum and maximum position for the screen
    //    maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    //}
}
