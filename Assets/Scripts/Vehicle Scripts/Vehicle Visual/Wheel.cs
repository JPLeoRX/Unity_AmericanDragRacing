using UnityEngine;
using System;

[Serializable]
public class Wheel
{
	private MeshRenderer wheelMesh;							// The mesh of this wheel
	public double wheelRadius;								// The radius of this whee;
	private ParticleSystem wheelSmoke;						// The smoke particle system (if available)
	private double burnoutMaxWheelRPM = 100;				// Maximum burnout wheel RPM
	private double burnoutMaxSmokeEmissionRate = 250;		// Maximum burnout smoke emission rate

	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Wheel(MeshRenderer wheelMesh) {
		// Save wheel mesh
		this.wheelMesh = wheelMesh;

		// Compute wheel radius
		this.wheelRadius = wheelMesh.bounds.size.y / 2.0;
	}

	public Wheel (MeshRenderer wheelMesh, ParticleSystem wheelSmoke) {
		// Save wheel smoke
		this.wheelSmoke = wheelSmoke;

		// Save wheel mesh
		this.wheelMesh = wheelMesh;

		// Compute wheel radius
		this.wheelRadius = wheelMesh.bounds.size.y / 2.0;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//---------------------------- Normal Wheel Rotation ------------------------------------
	//---------------------------------------------------------------------------------------
	private double wheelRPM;				// Current wheel RPM
	private Vector3 wheelCenter;			// Current center of the wheel
	private float rotationAngle;			// Current rotation angle
	private Vector3 rotationAxis;			// Current rotation axis

	public void Spin(double vehicleVelocity) 
	{
		// Compute wheel RPM
		if (vehicleVelocity < 100f / 3.6f)
			wheelRPM = (60 * vehicleVelocity) / (2 * wheelRadius * Mathf.PI);
		else
			wheelRPM = (60 * 100f / 3.6f) / (2 * wheelRadius * Mathf.PI);

		// Compute wheel center
		wheelCenter = wheelMesh.bounds.center;

		// Compute rotation angle
		rotationAngle = (float) wheelRPM / 60.0f * 360.0f * Time.deltaTime;

		// Compute rotation axis
		rotationAxis = wheelMesh.transform.right;

		// Apply rotation
		wheelMesh.transform.RotateAround(wheelCenter, rotationAxis, -rotationAngle);
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Wheel Burnout ----------------------------------------
	//---------------------------------------------------------------------------------------
	public void Burnout(double engineRPM, double engineMaxRPM, double engineMinRPM) 
	{
		// If the smoke particle system exists
		if (wheelSmoke != null)
		{
			// Compute wheel RPM
			wheelRPM = (float) ((engineRPM - engineMinRPM) / engineMaxRPM * burnoutMaxWheelRPM);

			// Compute wheel center
			wheelCenter = wheelMesh.bounds.center;

			// Compute rotation angle
			rotationAngle = (float) wheelRPM / 60.0f * 360.0f * Time.deltaTime;

			// Compute rotation axis
			rotationAxis = wheelMesh.transform.right;

			// Apply rotation
			wheelMesh.transform.RotateAround(wheelCenter, rotationAxis, -rotationAngle);

			// Apply smoke
			wheelSmoke.emissionRate = (float) ((engineRPM - engineMinRPM) / engineMaxRPM * burnoutMaxSmokeEmissionRate);
		}

	}

	public void BurnoutStop() 
	{
		// If the smoke particle system exists
		if (wheelSmoke != null)
		{
			// Turn off smoke
			wheelSmoke.emissionRate = 0;
		}
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}