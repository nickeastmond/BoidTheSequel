using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public float avoidanceDistance = 1.5f; // how far away fish will avoid the fish or obstacle

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if there are no neighbors return zero vector
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // filter in order to only have transforms from this flock or obstacles
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        Vector2 move = Vector2.zero;

        int nAvoid = 0;
        float distance; // distance  will hold how far away fish is from another object
        float itemRadius;  // item radius will hold colliders radius if it exists, which will be used make the avoidRadius larger if applicable
        float avoidanceRadius; // avoidance radius is the total distance fish must avoid being in.


        foreach (Transform item in filteredContext)
        {
            distance = Vector2.Distance(item.position, agent.transform.position);
            itemRadius = 0f;

            // check if the item has a collider, and if it does, use its size as the radius.
            Collider2D itemCollider = item.GetComponent<Collider2D>();
            if (itemCollider != null)
            {
                itemRadius = Mathf.Max(itemCollider.bounds.extents.x, itemCollider.bounds.extents.y);
            }

            // we will use this updated radius as the distance we will check with to decide if we need to abort
            avoidanceRadius = avoidanceDistance + itemRadius;

            // if we are inside the avoidance radius add the negative vector that will push fish away from object
            if (distance < avoidanceRadius)
            {
                nAvoid++;
                move += (Vector2)(agent.transform.position - item.position).normalized * avoidanceRadius;
            }
        }

        // avoids dividing by zero, finds average vector to escape objects it is trying to avoid
        if (nAvoid > 0)
        {
            move /= nAvoid;
        }

        return move;
    }
}
