using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/MouseAttraction")]
public class MouseAttractionBehavior : FilteredFlockBehavior
{
    public float attractionStrength = 1f; // Strength of the attraction force.

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // check if the mouse is within camera bounds
        if (IsMouseInScreen())
        {
            // get mouse coodinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get direction fish are attracted to and magnitude
            Vector2 attractionDirection = (mousePosition - (Vector2)agent.transform.position);
            float distanceToMouse = attractionDirection.magnitude;

            // calculate the attraction force which take into account distance
            float attractionForceMagnitude = attractionStrength / (distanceToMouse + 1f);

            // nromalize and scale
            Vector2 attractionForce = attractionDirection.normalized * attractionForceMagnitude;

            // Return the attraction force as the desired move.
            return attractionForce;
        }

        return Vector2.zero;
    }

    private bool IsMouseInScreen()
    {
        Vector3 mousePosition = Input.mousePosition;
        return mousePosition.x >= 0f && mousePosition.x <= Screen.width &&
               mousePosition.y >= 0f && mousePosition.y <= Screen.height;
    }
}
