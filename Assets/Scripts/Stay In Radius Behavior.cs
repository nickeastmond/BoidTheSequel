using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector2 center; // where the center of the circle lies
    public float radius = 15f; // how the limiting circle is 

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // calculate how far away we are from the center of the circle
        Vector2 centerOffset = center - (Vector2)agent.transform.position;

        // ratio of how close we are to the edge of the circle
        float ratio = centerOffset.magnitude / radius;

        // if we are within 90 percent of the circle don't change movement of the fish, but if we are close to it return vector that will return the fish to the safe area
        if (ratio > 0.9f)
        {
            return centerOffset * ratio * ratio;
        } else
        {
            return Vector2.zero;
        }
    }
}
