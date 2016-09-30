using UnityEngine;
using System;

[Serializable]
public class Engine 
{
	private double rpmIncreaseRate = 3000;		// Rate of increase in RPM when throttle is pressed in neutral gear
	private double rpmDecreaseRate = 3000;		// Rate of decrease in RPM when throttle is released in neutral gear
	private TorqueCurve torqueCurve;			// Engine torque curve
	private double minRPM;						// Minimum possible RPM
	private double maxRPM;						// Maximum possible RPM
	private double currentRPM;					// Current engine RPM
	private double outputTorque;				// Current engine torque
	private EngineTuning tuning;				// Tuning for this part



	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Engine (TorqueCurve torqueCurve, EngineTuning tuning) {
		this.torqueCurve = torqueCurve;
		this.minRPM = torqueCurve.GetMinRPM();
		this.maxRPM = torqueCurve.GetMaxRPM();
		this.tuning = tuning;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Computation ------------------------------------------
	//---------------------------------------------------------------------------------------
	// Compute the torque produced by the engine
	public void ComputeOutputTorque(double throttlePosition) {
		// Compute the output torque based on torque curve and throttle position
		this.outputTorque = torqueCurve.GetTorque(currentRPM) * throttlePosition;

		// Compute the output torque based on the tuning installed
		this.outputTorque = outputTorque + outputTorque * tuning.GetPercentageIncrease();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//------------------------------- Public Getters ----------------------------------------
	//---------------------------------------------------------------------------------------
	public TorqueCurve GetTorqueCurve() {
		return this.torqueCurve;
	}

	public double GetCurrentRPM() {
		return this.currentRPM;
	}

	public double GetOutputTorque() {
		return this.outputTorque;
	}

	public double GetMinRPM() {
		return this.minRPM;
	}

	public double GetMaxRPM() {
		return this.maxRPM;
	}

	public double GetRPMIncreaseRate() {
		return this.rpmIncreaseRate;
	}

	public double GetRPMDecreaseRate() {
		return this.rpmDecreaseRate;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//------------------------------- Public Setters ----------------------------------------
	//---------------------------------------------------------------------------------------
	public void SetTorqueCurve(TorqueCurve value) {
		this.torqueCurve = value; this.minRPM = value.GetMinRPM(); this.maxRPM = value.GetMaxRPM();
	}

	public void SetRPMIncreaseRate(double value) {
		this.rpmIncreaseRate = value;
	}

	public void SetRPMDecreaseRate(double value) {
		this.rpmDecreaseRate = value;
	}

	public void SetInputFeedbackRPM(double rpm, double throttlePosition) {
		// If the car is in neutral
		if (rpm < 0) {
			// If the throttle is pressed
			if (throttlePosition > 0)
				// Increase the RPM
				rpm = currentRPM + rpmIncreaseRate * Time.fixedDeltaTime;
			// If the throttle is released
			else
				// Decrease the RPM
				rpm = currentRPM - rpmDecreaseRate * Time.fixedDeltaTime;
		}

		// Clamp the RPM between min and max allowed values
		this.currentRPM = Mathf.Clamp((float) rpm, (float) minRPM, (float) maxRPM);
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}