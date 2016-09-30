using UnityEngine;
using System;

[Serializable]
public class PlayerControls : BasicControls
{
	public Car playerCar;
	public double raceDistance;


	public PlayerControls (Car playerCar, double raceDistance)
	{
		this.playerCar = playerCar;
		this.raceDistance = raceDistance;

		playerCar.EnableCamera();
	}

	// Update is called with fixed time step
	public void FixedUpdate() 
	{
		if (!raceStarted)
		{
			this.PressShiftDown();
			this.ReleaseShiftUp();
			this.ApplyControlsToCar(playerCar);
		}

		// If the race is in progress
		if (raceStarted && ! raceEnded) 
		{
			// Check if finished
			if (playerCar.GetCurrentDistance() >= raceDistance)
				raceEnded = true;

			// Update controls
			this.ApplyControlsToCar(playerCar);

			// Update time
			this.UpdateRaceTime();
		}
	}

	public override void RaceStart() {
		raceStarted = true;
		raceEnded = false;
		this.ReleaseShiftDown();
		this.PressShiftUp();
		this.ApplyControlsToCar(playerCar);
		this.ReleaseShiftUp();
	}


}