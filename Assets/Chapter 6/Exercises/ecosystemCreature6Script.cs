﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecosystemCreature6Script : MonoBehaviour
{
    public float maxSpeed = 2, maxForce = 2;

    public Mesh coneMesh; // If you want to use your own cone mesh, drop it into the editor here.

    private List<myBoid> boids; // Declare a List of Vehicle objects.
    private Vector3 minimumPos, maximumPos;
    public Vector3 Nexus;

    // Start is called before the first frame update
    void Start()
    {
        //findWindowLimits();
        minimumPos = new Vector3(0, 10, 0);
        maximumPos = new Vector3(99, 20, 99);
        boids = new List<myBoid>(); // Initilize and fill the List with a bunch of Vehicles
        for (int i = 0; i < 25; i++)
        {
            float ranX = Random.Range(-1.0f, 1.0f);
            float ranY = Random.Range(-1.0f, 1.0f);
            float ranZ = Random.Range(-1.0f, 1.0f);
            boids.Add(new myBoid(new Vector3(ranX, ranY, ranZ), minimumPos, maximumPos, maxSpeed, maxForce, coneMesh, Nexus));
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach (myBoid v in boids)
        {
            v.Flock(boids);
        }

    }

    //private void findWindowLimits()
    //{
    //    Camera.main.orthographic = true;
    //    Camera.main.orthographicSize = 20;
    //    minimumPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
    //    maximumPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    //}
}


class myBoid
{
    private Vector3 gNexus;
    // To make it easier on ourselves, we use Get and Set as quick ways to get the location of the vehicle
    public Vector3 location
    {
        get { return myVehicle.transform.position; }
        set { myVehicle.transform.position = value; }
    }
    public Vector3 velocity
    {
        get { return rb.velocity; }
        set { rb.velocity = value; }
    }

    private float maxSpeed, maxForce;
    private Vector3 minPos, maxPos;
    private GameObject myVehicle;
    private Rigidbody rb;

    public myBoid(Vector3 initPos, Vector3 _minPos, Vector3 _maxPos, float _maxSpeed, float _maxForce, Mesh coneMesh, Vector3 n)
    {
        minPos = _minPos - Vector3.one;
        maxPos = _maxPos + Vector3.one;
        maxSpeed = _maxSpeed;
        maxForce = _maxForce;
        gNexus = n;

        myVehicle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = myVehicle.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.gray;
        GameObject.Destroy(myVehicle.GetComponent<BoxCollider>());

        myVehicle.transform.position = new Vector3(initPos.x, initPos.y, initPos.z);

        myVehicle.AddComponent<Rigidbody>();
        rb = myVehicle.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false; // Remember to ignore gravity!


        /* We want to double check if a custom mesh is
         * being used. If not, we will scale a cube up
         * instead ans use that for our boids. */
        if (coneMesh != null)
        {
            MeshFilter filter = myVehicle.GetComponent<MeshFilter>();
            filter.mesh = coneMesh;
        }
        else
        {
            myVehicle.transform.localScale = new Vector3(1f, 2f, 1f);
        }
    }

    private void checkBounds()
    {
        if (location.x > maxPos.x)
        {
            location = new Vector3(minPos.x, location.y, location.z);
        }
        else if (location.x < minPos.x)
        {
            location = new Vector3(maxPos.x, location.y, location.z);
        }
        if (location.y > maxPos.y)
        {
            location = new Vector3(location.x, minPos.y, location.z);
        }
        else if (location.y < minPos.y)
        {
            location = new Vector3(location.x, maxPos.y, location.z);
        }
        if (location.z > maxPos.z)
        {
            location = new Vector3(location.x, location.y, minPos.z);
        }
        else if (location.z < minPos.z)
        {
            location = new Vector3(location.x, location.y, maxPos.z);
        }
    }

    private void lookForward()
    {
        /* We want our boids to face the same direction
         * that they're going. To do that, we take our location
         * and velocity to see where we're heading. */
        Vector3 futureLocation = location + velocity;
        myVehicle.transform.LookAt(futureLocation); // We can use the built in 'LookAt' function to automatically face us the right direction

        /* In the case our model is facing the wrong direction,
         * we can adjust it using Eular Angles. */
        Vector3 eular = myVehicle.transform.rotation.eulerAngles;
        myVehicle.transform.rotation = Quaternion.Euler(eular.x + 90, eular.y + 0, eular.z + 0); // Adjust these numbers to make the boids face different directions!
    }

    public void Flock(List<myBoid> boids)
    {
        Vector3 sep = Separate(boids); // The three flocking rules
        Vector3 ali = Align(boids);
        Vector3 coh = Cohesion(boids);

        sep *= 5.0f; // Arbitrary weights for these forces (Try different ones!)
        ali *= 1.5f;
        coh *= 0.5f;

        ApplyForce(sep); // Applying all the forces
        ApplyForce(ali);
        ApplyForce(coh);

        checkBounds(); // To loop the world to the other side of the screen.
        lookForward(); // Make the boids face forward.
    }

    public Vector3 Align(List<myBoid> boids)
    {
        float neighborDist = 6f; // This is an arbitrary value and could vary from boid to boid.

        /* Add up all the velocities and divide by the total to
         * calculate the average velocity. */
        Vector3 sum = Vector3.zero;
        int count = 0;
        foreach (myBoid other in boids)
        {
            float d = Vector3.Distance(location, other.location);
            if ((d > 0) && (d < neighborDist))
            {
                sum += other.velocity;
                count++; // For an average, we need to keep track of how many boids are within the distance.
            }
        }

        if (count > 0)
        {
            sum /= count;

            sum = sum.normalized * maxSpeed; // We desite to go in that direction at maximum speed.

            Vector3 steer = sum - velocity; // Reynolds's steering force formula.
            steer = Vector3.ClampMagnitude(steer, maxForce);
            return steer;
        }
        else
        {
            return Vector3.zero; // If we don't find any close boids, the steering force is Zero.
        }
    }

    public Vector3 Cohesion(List<myBoid> boids)
    {
        float neighborDist = 6f;
        Vector3 sum = Vector3.zero;
        int count = 0;
        foreach (myBoid other in boids)
        {
            float d = Vector3.Distance(location, gNexus);
            if ((d > 0) && (d < neighborDist))
            {
                sum += gNexus; // Adding up all the other's locations
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            /* Here we make use of the Seek() function we wrote in
             * Example 6.8. The target we seek is thr average
             * location of our neighbors. */
            return Seek(sum);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - location;
        desired.Normalize();
        desired *= maxSpeed;
        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxForce);

        return steer;
    }

    public Vector3 Separate(List<myBoid> boids)
    {
        Vector3 sum = Vector3.zero;
        int count = 0;

        float desiredSeperation = myVehicle.transform.localScale.x * 2;

        foreach (myBoid other in boids)
        {
            float d = Vector3.Distance(other.location, location);

            if ((d > 0) && (d < desiredSeperation))
            {
                Vector3 diff = location - other.location;
                diff.Normalize();

                diff /= d;

                sum += diff;
                count++;
            }
        }

        if (count > 0)
        {
            sum /= count;

            sum *= maxSpeed;

            Vector3 steer = sum - velocity;
            steer = Vector3.ClampMagnitude(steer, maxForce);


            return steer;
        }
        return Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force);
    }
}