using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors; // list of all behaviors in this behavior
    public float[] weights; // list of scalars or intensities we will use for behaviors. 

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // need to have identical length lists else we print an error
        if (weights.Length == behaviors.Length)
        {
            // move will be the vector holding the final value for the movement of the fish
            Vector2 move = Vector2.zero;

            // for every behavior, calculate a vector, and add it to move
            for (int i = 0; i < behaviors.Length; i++)
            {
                // get vector 
                Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock);

                // scale it by the weight
                partialMove *= weights[i];

                // add vector if applicable
                if (partialMove != Vector2.zero)
                {
                    if (partialMove.sqrMagnitude > weights[i] * weights[i])
                    {
                        partialMove.Normalize();
                        partialMove *= weights[i];
                    }
                    move += partialMove;
                }
            }
            return move;
        } else
        {
            Debug.Log("ERROR: NEED TO HAVE SAME AMOUNT OF BEHAVIORS AS WEIGHTS", this);
            return Vector2.zero;
        } 
    }
}
