using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
	[SerializeField]
	protected float maxSpeed = 400.0f;

	/// <summary>
	/// Returns the maximum speed the agent can have
	/// </summary>
	public float MaxSpeed
	{
		get
		{
			return maxSpeed;
		}
	}

	[SerializeField]
	protected float maxSteering = 10.0f;

	/// <summary>
	/// Returns the maximum steering amount that can be applied
	/// </summary>
	public float MaxSteering
	{
		get
		{
			return maxSteering;
		}
	}

	private List<SteeringBehaviour> steeringBehvaiours = new List<SteeringBehaviour>();

	/// <summary>
	/// Returns the current velocity of the Agent
	/// </summary>
	public Vector3 CurrentVelocity
	{
		get;
		protected set;
	}
	
	/// <summary>
	/// Called once per frame
	/// </summary>
	private void Update()
	{
		CooperativeArbitration();
		UpdatePosition();
		UpdateDirection();
	}

	/// <summary>
	/// This is responsible for how to deal with multiple behaviours and selecting which ones to use. Please see this link for some decent descriptions of below:
	/// https://alastaira.wordpress.com/2013/03/13/methods-for-combining-autonomous-steering-behaviours/
	/// Remember some options for choosing are:
	/// 1 Finite state machines which can be part of the steering behaviours or not (Not the best approach but quick)
	/// 2 Weighted Truncated Sum
	/// 3 Prioritised Weighted Truncated Sum
	/// 4 Prioritised Dithering
	/// 5 Context Behaviours: https://andrewfray.wordpress.com/2013/03/26/context-behaviours-know-how-to-share/
	/// 6 Any other approach you come up with
	/// </summary>
	protected virtual void CooperativeArbitration()
	{
		bool showDebugLines = false;
		Vector3 steeringVelocity = Vector3.zero;
		
		GetComponents<SteeringBehaviour>(steeringBehvaiours);
		foreach (SteeringBehaviour currentBehaviour in steeringBehvaiours)
		{
			if(currentBehaviour.enabled)
			{
				steeringVelocity += currentBehaviour.UpdateBehaviour(this);
				showDebugLines |= currentBehaviour.ShowDebugLines;
			}
		}

		// Debug lines in scene view
		if (showDebugLines)
		{
			Debug.DrawRay(transform.position, CurrentVelocity, Color.green);
			foreach (SteeringBehaviour currentBehaviour in steeringBehvaiours)
			{
				if (currentBehaviour.enabled)
				{
					currentBehaviour.DebugDraw();
				}
			}
		}

		// Set final velocity
		CurrentVelocity += LimitSteering(steeringVelocity, MaxSteering);
		CurrentVelocity = LimitVelocity(CurrentVelocity, maxSpeed);
	}

	/// <summary>
	/// Updates the position of the GAmeObject via Teleportation. In Craig Reynolds architecture this would the Locomotion layer
	/// </summary>
	protected virtual void UpdatePosition()
	{
		transform.position += CurrentVelocity * Time.deltaTime;

		// The code below is just to wrap the screen for the agent like in Asteroids for example
		Vector3 position = transform.position;
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(position);

		while(viewportPosition.x < 0.0f)
		{
			viewportPosition.x += 1.0f;
		}
		while (viewportPosition.x > 1.0f)
		{
			viewportPosition.x -= 1.0f;
		}
		while (viewportPosition.y < 0.0f)
		{
			viewportPosition.y += 1.0f;
		}
		while (viewportPosition.y > 1.0f)
		{
			viewportPosition.y -= 1.0f;
		}

		position = Camera.main.ViewportToWorldPoint(viewportPosition);
		position.z = 0.0f;
		transform.position = position;
	}

	/// <summary>
	/// Sets the direction of the triangle to the direction it is moving in to give the illusion it is turning. Trying taking out the function
	/// call in Update() to see what happens
	/// </summary>
	protected virtual void UpdateDirection()
	{
		// Don't set the direction if no direction
		if (CurrentVelocity.sqrMagnitude > 0.0f)
		{
			transform.up = Vector3.Normalize(new Vector3(CurrentVelocity.x, CurrentVelocity.y, 0.0f));
		}
	}

	#region Static Helper Functions
	/// <summary>
	/// Limits the velocity vector to the maxSpeed
	/// </summary>
	/// <param name="velocity">Velocity to limit</param>
	/// <param name="maxSpeed">Amount to limit to</param>
	/// <returns>New Vector that has been limited</returns>
	static public Vector3 LimitVelocity(Vector3 velocity, float maxSpeed)
	{
		// This limits the velocity to max speed. sqrMagnitude is used rather than magnitude as in magnitude a square root must be computed which is a slow operation.
		// By using sqrMagnitude and comparing with maxSpeed squared we can get around using the expensive square root operation.
		if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
		{
			velocity.Normalize();
			velocity *= maxSpeed;
		}
		return velocity;
	}

	/// <summary>
	/// Limits the steering vector to the maxSteering
	/// </summary>
	/// <param name="steeringVelocity">Steering velocity to limit</param>
	/// <param name="maxSteering">Amount to limit to</param>
	/// <returns>New Vector that has been limited</returns>
	static public Vector3 LimitSteering(Vector3 steeringVelocity, float maxSteering)
	{
		// This limits the velocity to max steering. sqrMagnitude is used rather than magnitude as in magnitude a square root must be computed which is a slow operation.
		// By using sqrMagnitude and comparing with maxSteering squared we can get around using the expensive square root operation.
		if (steeringVelocity.sqrMagnitude > maxSteering * maxSteering)
		{
			steeringVelocity.Normalize();
			steeringVelocity *= maxSteering;
		}
		return steeringVelocity;
	}

	/// <summary>
	/// Returns the mouse position in 2d space
	/// </summary>
	/// <returns>The mouse position in 2d space</returns>
	static public Vector3 GetMousePosition()
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector3(temp.x, temp.y, 0.0f);
	}
	#endregion
}
