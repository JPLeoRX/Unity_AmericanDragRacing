using UnityEngine;
using System;

[Serializable]
public class OpponentControls : BasicControls
{
	public Car opponentCar;
	public double raceDistance;
	public int rpmTolerance = 500;




	public OpponentControls (Car opponentCar, double raceDistance, int rpmTolerance)
	{
		this.opponentCar = opponentCar;
		this.raceDistance = raceDistance;
		this.rpmTolerance = rpmTolerance;

		opponentCar.DisableCamera();
	}

	// Update is called with fixed time step
	public void FixedUpdate() 
	{
		// If the race is in progress
		if (raceStarted && !raceEnded)
		{
			// Check if finished
			if (opponentCar.GetCurrentDistance() >= raceDistance)
				raceEnded = true;
			
			// Release the shifter
			this.ReleaseShiftDown(); this.ReleaseShiftUp();

			// Press the throttle
			this.PressGas();

			// Shift up when needed
			if (opponentCar.GetRedlineRPM() - rpmTolerance < opponentCar.GetCurrentRPM() && opponentCar.GetCurrentRPM() < opponentCar.GetRedlineRPM())
				this.PressShiftUp();

			// Update controls
			this.ApplyControlsToCar(opponentCar);

			// Update time
			this.UpdateRaceTime();
		}
	}

	public override void RaceStart() {
		raceStarted = true;
		raceEnded = false;
	}
}