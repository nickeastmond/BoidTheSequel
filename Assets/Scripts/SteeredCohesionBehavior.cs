using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // return zero vector if there are no neighbors
        if (context.Count == 0)
        {
            return Vector2.zero;

        }
          
        // add the positions of all neighbors
        Vector2 move = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            move += (Vector2)item.position;
        }

        // calculates average by dividing by the count
        move /= context.Count;

        // creates cohesion move
        move -= (Vector2)agent.transform.position;
        move = Vector2.SmoothDamp(agent.transform.up, move, ref currentVelocity, agentSmoothTime);

        return move;
    }
}
