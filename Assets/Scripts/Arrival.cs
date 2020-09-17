using UnityEngine;

public class Arrival : SteeringBehaviour
{
    /// <summary>
    /// Controls how far from the target position should the agent start to slow down
    /// </summary>
	[SerializeField]
	protected float arrivalRadius = 200.0f;

	public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
	{
		// Implement me
		return steeringVelocity;
	}
}
