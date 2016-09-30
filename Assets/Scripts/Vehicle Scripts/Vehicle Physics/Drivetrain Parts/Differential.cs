using UnityEngine;
using System;

[Serializable]
public class Differential
{
	private double gearRatioFinalDrive;				// Gear ratio of final drive
	private double inputTorque;						// Input torque supplied from transmission
	private double outputTorque;					// Output torque sent to the wheels
	private double inputFeedbackRpm;				// Input RPM supplied by the wheels
	private double outputFeedbackRpm;				// Output RPM sent to the transmission
	private double torqueIncreaseRatioTuning;		// Increase in output torque from tuning



	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Differential (double gearRatioFinalDrive, DifferentialTuning differentialTuning, DifferentialGearsTuning differentialGearsTuning) {
		this.gearRatioFinalDrive = gearRatioFinalDrive + gearRatioFinalDrive * differentialGearsTuning.GetPercentageIncrease();
		this.torqueIncreaseRatioTuning = differentialTuning.GetPercentageIncrease();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Computation ------------------------------------------
	//---------------------------------------------------------------------------------------
	// Compute the output torque of the differential
	public void ComputeOutputTorque() {
		// Compute the output torque based on input torque and gear ratio
		this.outputTorque = inputTorque * gearRatioFinalDrive;

		// Compute the output torque based on the tuning installed
		this.outputTorque = outputTorque + outputTorque * torqueIncreaseRatioTuning;
	}

	// Compute the feedback rpm of this part
	public void ComputeOutputFeedbackRPM() {
		this.outputFeedbackRpm = inputFeedbackRpm * gearRatioFinalDrive;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//--------------------------- Public Setters/Getters ------------------------------------
	//---------------------------------------------------------------------------------------
	// Supply input torque to this part
	public void SetInputTorque(double torque) {
		this.inputTorque = torque;
	}

	// Obtain the output torque from this part
	public double GetOutputTorque() {
		return outputTorque;
	}

	// Supply input engine RPM to this part
	public void SetInputFeedbackRPM(double rpm) {
		this.inputFeedbackRpm = rpm;
	}

	// Obtain the feedback rpm from this part
	public double GetOutputFeedbackRPM() {
		return outputFeedbackRpm;
	}

	public double GetGearRatioFinalDrive() {
		return gearRatioFinalDrive;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}