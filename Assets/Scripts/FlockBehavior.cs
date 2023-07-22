using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    // abstract function all behaviors should have implemented
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
