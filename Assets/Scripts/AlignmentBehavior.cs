using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if fish has no neighbors return vector of itself moving up (forward from fish perspective)
        if (context.Count == 0)
        {
            return agent.transform.up;
        }

        // filter in order to only have transforms from this flock
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        Vector2 move = Vector2.zero;

        // add all of the neighbors up (forward) vectors and average out
        foreach (Transform item in filteredContext)
        {
            move += (Vector2)item.transform.up;
        }
        move /= context.Count;

        return move;
    }
}
