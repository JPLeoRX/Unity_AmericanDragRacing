using UnityEngine;
using System;

[Serializable]
public class Transmission
{
	private int numberOfGears;
	private double[] gearRatios;
	private int currentGear = 1;
	private double torqueIncreaseRatioTuning;

	private double engineRpm;
	private double inputTorque;
	private double outputTorque;
	private double inputFeedbackRpm;
	private double outputFeedbackRpm;
	private double timePassedSinceLastShift;
	private double shiftDelayTime = 0.3;



	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Transmission (double[] gearRatios, TransmissionTuning transmissiontuning, TransmissionGearsTuning gearboxTuning) {
		this.numberOfGears = gearRatios.Length;
		this.torqueIncreaseRatioTuning = transmissiontuning.GetPercentageIncrease();
		this.gearRatios = new double[numberOfGears];
		for (int i = 0; i < numberOfGears; i++)
			this.gearRatios[i] = gearRatios[i] + gearRatios[i] * gearboxTuning.GetPercentageIncrease();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Computation ------------------------------------------
	//---------------------------------------------------------------------------------------
	// Compute the output torque of the gearbox
	public void ComputeOutputTorque() {
		if (currentGear == 0)
			this.outputTorque = 0;

		else {
			// Compute the output torque based on input torque and gear ratio
			this.outputTorque = inputTorque * gearRatios[currentGear - 1];

			// Compute the output torque based on the tuning installed
			this.outputTorque = outputTorque + outputTorque * torqueIncreaseRatioTuning;
		}
	}

	// Compute the output engine RPM of the gearbox
	public void ComputeOutputFeedbackRpm() {
		if (currentGear == 0)
			this.outputFeedbackRpm = -1;
		else
			this.outputFeedbackRpm = inputFeedbackRpm * gearRatios[currentGear - 1];
	}

	// Compute the shifting if possible
	public void ComputeShifting(double shiftUp, double shiftDown) {
		if (shiftDown > 0 && timePassedSinceLastShift > shiftDelayTime)
			this.ShiftDown();
			
		else if (shiftUp > 0 && timePassedSinceLastShift > shiftDelayTime)
			this.ShiftUp();

		else
			timePassedSinceLastShift += Time.fixedDeltaTime;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//----------------------------------- Shifting ------------------------------------------
	//---------------------------------------------------------------------------------------
	private void ShiftUp() {
		if (0 <= currentGear && currentGear < numberOfGears) {
			currentGear++;
			timePassedSinceLastShift = 0;
		}
	}
		
	private void ShiftDown() {
		if (0 < currentGear && currentGear <= numberOfGears) {
			currentGear--;
			timePassedSinceLastShift = 0;
		}
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
	public void SetInputFeedbackRPM(double feedbackRpm) {
		this.inputFeedbackRpm = feedbackRpm;
	}

	// Obtain the output engine RPM from this part
	public double GetOutputFeedbackRPM() {
		return outputFeedbackRpm;
	}

	// Get current gear
	public int GetCurrentGear() {
		return currentGear;
	}

	// Get current gear ratio
	public double GetCurrentGearRation() {
		if (currentGear == 0)
			return 0;
		else
			return gearRatios[currentGear - 1];
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}