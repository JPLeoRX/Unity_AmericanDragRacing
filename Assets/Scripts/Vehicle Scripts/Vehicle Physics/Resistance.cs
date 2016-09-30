using UnityEngine;
using System;

[Serializable]
public class Resistance
{
	private static double airDensity = 1.225;
	private static double vehicleFrontalAreaCoefficient = 0.85;
	private static double tresholdVelocity = 0.0001;
	
	public double dragCoefficient;
	public double vehicleWidth;
	public double vehicleHeight;
	public double dragForce;

	public double vehicleMass;
	public double rollingFrictionCoefficient;
	public double rollingFrictionForce;



	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Resistance (double dragCoefficient, double vehicleWidth, double vehicleHeight, double vehicleMass, BodyTuning bodyTuning, TiresTuning tiresTuning) {
		this.dragCoefficient = dragCoefficient;
		this.vehicleWidth = vehicleWidth;
		this.vehicleHeight = vehicleHeight;
		this.vehicleMass = vehicleMass - vehicleMass * bodyTuning.GetPercentageDecrease();
		this.rollingFrictionCoefficient = tiresTuning.GetRollingFrictionCoefficient();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Computation ------------------------------------------
	//---------------------------------------------------------------------------------------
	// Compute the drag force
	public void ComputeDragForce(double vehicleVelocity) {
		if (vehicleVelocity > tresholdVelocity)
			this.dragForce = - 0.5 * dragCoefficient * airDensity * vehicleVelocity * vehicleVelocity * (vehicleFrontalAreaCoefficient * vehicleWidth * vehicleHeight);
		else
			this.dragForce = 0;
	}

	// Compute the rolling friction force
	public void ComputeRollingFrictionForce(double vehicleVelocity, double movingForce) {
		if (vehicleVelocity > tresholdVelocity)
			this.rollingFrictionForce = - rollingFrictionCoefficient * vehicleMass * 9.8;
		else
			this.rollingFrictionForce = 0;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//--------------------------- Public Setters/Getters ------------------------------------
	//---------------------------------------------------------------------------------------
	public double GetDragForce() {
		return dragForce;
	}

	public double GetRollingFrictionForce() {
		return rollingFrictionForce;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}