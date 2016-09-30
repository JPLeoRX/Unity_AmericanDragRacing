using UnityEngine;
using System;

[Serializable]
public class BasicControls 
{
	public int GAS;
	public int SHIFT_UP;
	public int SHIFT_DOWN;

	public bool raceStarted;
	public bool raceEnded;
	public double raceTime;

	public void ApplyControlsToCar(Car car) {
		car.SetControlsThrottle(GAS);
		car.SetControlsShiftUp(SHIFT_UP);
		car.SetControlsShiftDown(SHIFT_DOWN);
	}

	public void UpdateRaceTime() {
		raceTime += Time.fixedDeltaTime;
	}

	public void PressGas() {
		GAS = 1;
	}

	public void ReleaseGas() {
		GAS = 0;
	}

	public void PressShiftUp() {
		SHIFT_UP = 1;
	}

	public void ReleaseShiftUp() {
		SHIFT_UP = 0;
	}

	public void PressShiftDown() {
		SHIFT_DOWN = 1;
	}

	public void ReleaseShiftDown() {
		SHIFT_DOWN = 0;
	}

	public virtual void RaceStart() {
		raceStarted = true;
	}

	public virtual void RaceEnd() {
		raceEnded = true;
	}
}
