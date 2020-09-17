using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
	[SerializeField]
	protected bool showDebugLines = true;
	public bool ShowDebugLines
	{
		get
		{
			return showDebugLines;
		}
	}

	protected Vector3 desiredVelocity;
	protected Vector3 steeringVelocity;

	/// <summary>
	/// Do steering behaviour code here. At the end of this the desiredVelocity and steeringVelocity variables should be set
	/// </summary>
	/// <param name="steeringAgent">The agent this component is acting on</param>
	/// <returns>The steeringVelocity should always be returned here</returns>
	public abstract Vector3 UpdateBehaviour(SteeringAgent steeringAgent);

	protected virtual void Start()
	{
		// Annoyingly this is needed for the enabled bool flag to work in Unity. A MonoBehaviour must now have one of the following
		// to activate this: Start(), Update(), FixedUpdate(), LateUpdate(), OnGUI(), OnDisable(), OnEnabled()
	}

	/// <summary>
	/// Draws debug info that is helpful to see what might be happening
	/// </summary>
	public virtual void DebugDraw()
	{
		Debug.DrawRay(transform.position, desiredVelocity, Color.red);
		Debug.DrawRay(transform.position, steeringVelocity, Color.blue);
	}
}