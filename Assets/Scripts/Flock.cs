using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab; // fish
    public FlockBehavior behavior; // behavior of fishies

    // list of all of our fishies
    List<FlockAgent> agents = new List<FlockAgent>();
    

    [Range(10, 1000)]
    public int numFishies = 250; // total number of fishies to ge generated
    [Range(1f, 100f)]
    public float minSpeed = 5f; // min speed of fish
    [Range(1f, 100f)]
    public float maxSpeed = 10f; // max speed of fish
    [Range(1f,10f)]
    public float  neighborRadius = 1.5f; // radius to detect neighbors
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    // variables to hold squares of other values to avoid recalculating
    float maxSpeedSqr;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    void Start()
    {
        // create all fishies and add them to our list of all fishies in this Flock
        for(int i = 0; i < numFishies; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * numFishies * 0.1f, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }

        // squared values just so we don't compute them over and over again in update
        maxSpeedSqr = maxSpeed * maxSpeed;
        squareAvoidanceRadius = neighborRadius * neighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;


    }

    void Update()
    {
        // goes through every fish, finds objects near it, and calculates next move
        foreach(FlockAgent agent in agents)
        {
            //get all transforms near the fish
            List<Transform> context = GetNearbyObjects(agent);

            // calculate next move based on behavior, there will be many but one behavior that encapsulates all of them
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= minSpeed;

            // if our vector magnitude is greater than our max speed, set the move to the limit we set
            if (move.sqrMagnitude > maxSpeedSqr)
            {
                move = move.normalized * maxSpeed;
            }

            // make our fishie move with the vector we created
            agent.Move(move);

        }
    }

    // returns list of transforms close to our fish
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        // gets all neighbors of our fish
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius); 

        // add neighbors to the list we will return
        foreach (Collider2D collider in contextColliders)
        {
            if (collider!= agent.AgentCollider)
            {
                context.Add(collider.transform);
            }
        }
        return context;
    }
}
