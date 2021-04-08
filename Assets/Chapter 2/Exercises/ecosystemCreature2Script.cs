using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecosystemCreature2Script : MonoBehaviour
{
    List<myMover2> movers = new List<myMover2>(); // Now we have multiple Movers!
    myAttractor a;
    private Vector3 wind = new Vector3(0.05f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        int numberOfMovers = 10;
        for (int i = 0; i < numberOfMovers; i++)
        {
            Vector2 randomLocation = new Vector2(Random.Range(-7f, 7f), Random.Range(-7f, 7f));
            Vector2 randomVelocity = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            myMover2 m = new myMover2(Random.Range(0.2f, 1f), randomVelocity, randomLocation); //Each Mover is initialized randomly.
            movers.Add(m);
        }
        a = new myAttractor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (myMover2 m in movers)
        {
            m.body.AddForce(wind, ForceMode.Impulse);

            Rigidbody body = m.body;
            Vector2 force = a.Attract(body); // Apply the attraction from the Attractor on each Mover object

            m.ApplyForce(force);
            m.Update();
        }
    }
}

public class myAttractor
{
    public float mass;
    private Vector2 location;
    private float G;
    public Rigidbody body;
    private GameObject attractor;
    private float radius;

    public myAttractor()
    {
        attractor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.Destroy(attractor.GetComponent<SphereCollider>());
        Renderer renderer = attractor.GetComponent<Renderer>();
        body = attractor.AddComponent<Rigidbody>();
        body.position = Vector2.zero;

        // Generate a radius
        radius = 1;

        // Place our mover at the specified spawn position relative
        // to the bottom of the sphere
        attractor.transform.position = body.position;

        // The default diameter of the sphere is one unit
        // This means we have to multiple the radius by two when scaling it up
        attractor.transform.localScale = 2 * radius * Vector3.one;

        // We need to calculate the mass of the sphere.
        // Assuming the sphere is of even density throughout,
        // the mass will be proportional to the volume.
        body.mass = (4f / 3f) * Mathf.PI * radius * radius * radius;
        body.useGravity = false;
        body.isKinematic = true;

        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.red;

        G = 9.8f;
    }

    public Vector2 Attract(Rigidbody m)
    {
        Vector2 force = body.position - m.position;
        float distance = force.magnitude;

        // Remember we need to constrain the distance so that our circle doesn't spin out of control
        distance = Mathf.Clamp(distance, 3f, 15f);

        force.Normalize();
        float strength = (G * body.mass * m.mass) / (distance * distance);
        force *= strength * 2;
        return force;
    }
}

public class myMover2
{
    // The basic properties of a mover class
    public Transform transform;
    public Rigidbody body;

    private Vector2 minimumPos, maximumPos;

    private GameObject mover;

    public myMover2(float randomMass, Vector2 initialVelocity, Vector2 initialPosition)
    {
        mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.Destroy(mover.GetComponent<SphereCollider>());
        transform = mover.transform;
        mover.AddComponent<Rigidbody>();
        body = mover.GetComponent<Rigidbody>();
        body.useGravity = false;
        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        mover.transform.localScale = new Vector3(randomMass, randomMass, randomMass);

        body.mass = 1;
        body.position = initialPosition; // Default location
        body.velocity = initialVelocity; // The extra velocity makes the mover orbit
        findWindowLimits();





    }

    public void ApplyForce(Vector2 force)
    {
        body.AddForce(force, ForceMode.Force);
    }

    public void Update()
    {
        CheckEdges();
    }

    public void CheckEdges()
    {
        Vector2 velocity = body.velocity;
        if (transform.position.x > maximumPos.x || transform.position.x < minimumPos.x)
        {
            velocity.x *= -1 * Time.deltaTime;
        }
        if (transform.position.y > maximumPos.y || transform.position.y < minimumPos.y)
        {
            velocity.y *= -1 * Time.deltaTime;
        }
        body.velocity = velocity;
    }

    private void findWindowLimits()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 10;
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}