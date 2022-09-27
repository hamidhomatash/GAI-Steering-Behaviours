using UnityEngine;

public class Wander : SteeringBehaviour
{
	/// <summary>
	/// Controls how large the imaginary circle is
	/// NOTE: [SerializeField] exposes a C# variable to Unity's inspector without making it public. Useful for encapsulating code
	/// while still giving access to the Unity inspector
	/// </summary>
	[SerializeField]
    protected float circleRadius = 150.0f;

	/// <summary>
	/// Controls how far from the agent position should the centre of the circle be
	/// NOTE: [SerializeField] exposes a C# variable to Unity's inspector without making it public. Useful for encapsulating code
	/// while still giving access to the Unity inspector
	/// </summary>
	[SerializeField]
    protected float circleDistance = 250.0f;

	protected override void Start()
	{
		base.Start();

		// Use for initialisation
	}

	public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
	{
		// Implement me!
		return steeringVelocity;
	}

	public override void DebugDraw(SteeringAgent steeringAgent)
	{
		// Look at the arrival steering behaviour to see what you might want to draw and where to help with debugging

		base.DebugDraw(steeringAgent);
	}
}
