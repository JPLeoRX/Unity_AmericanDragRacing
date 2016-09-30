using UnityEngine;
using System;

[Serializable]
public class TorqueCurve
{
	public double[] RPM;				// RPM values
	public double[] torque;				// Torque values

	private double currentRPM;			// Current RPM
	private double currentTorque;		// Current torque


	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public TorqueCurve(double[] x, double[] y) {
		this.RPM = x;
		this.torque = y;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Computation ------------------------------------------
	//---------------------------------------------------------------------------------------
	private void ComputeTorque() {
		// For each linear region of the curve
		for (int index = 0; index < RPM.Length - 1; index++) {
			// Obtain X's and Y's
			double x1 = RPM[index];
			double x2 = RPM[index + 1];
			double y1 = torque[index];
			double y2 = torque[index + 1];

			// If current X value falls into this region
			if (x1 < currentRPM && currentRPM < x2) {
				// Compute current Y using linear interpolation
				currentTorque = ((y2 - y1) / (x2 - x1)) * (currentRPM - x1) + y1;

				// Exit the method
				return;
			}

			// If current X is the left border value, replace current Y directly
			else if (currentRPM == x1) {
				currentTorque = y1;
				return;
			}

			// If current X is the right border value, replace current Y directly
			else if (currentRPM == x2) {
				currentTorque = y2;
				return;
			}
		}
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//--------------------------- Public Setters/Getters ------------------------------------
	//---------------------------------------------------------------------------------------
	public void SetRPM(double x) {
		this.currentRPM = x;
	}

	public double GetTorque() {
		return currentTorque;
	}

	public double GetTorque(double RPM) {
		this.SetRPM(RPM);
		this.ComputeTorque();
		return this.GetTorque();
	}

	public double GetMinRPM() {
		return RPM[0];
	}

	public double GetMaxRPM() {
		return RPM[RPM.Length - 1];
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}