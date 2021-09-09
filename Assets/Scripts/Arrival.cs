using UnityEngine;

public class Arrival : SteeringBehaviour
{
	/// <summary>
	/// Controls how far from the target position should the agent start to slow down
	/// NOTE: [SerializeField] exposes a C# variable to Unity's inspector without making it public. Useful for encapsulating code
	/// while still giving access to the Unity inspector
	/// </summary>
	[SerializeField]
	protected float arrivalRadius = 200.0f;

	public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
	{
		// Implement Me!
		return steeringVelocity;
	}
}
