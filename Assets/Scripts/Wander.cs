using UnityEngine;

public class Wander : SteeringBehaviour
{
    /// <summary>
    /// Controls how large the imaginary circle is
    /// </summary>
    [SerializeField]
    protected float circleRadius = 100.0f;

    /// <summary>
    /// Controls how far from the agent position should the centre of the circle be
    /// </summary>
    [SerializeField]
    protected float circleDistance = 50.0f;

	public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
	{
        // Implement me
		return steeringVelocity;
	}
}
