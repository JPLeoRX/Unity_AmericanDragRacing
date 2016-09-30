using UnityEngine;
using System;

[Serializable]
public class Drivetrain
{
	public Engine engine;
	public Transmission transmission;
	public Differential differential;

	private double vehicleMovingForce;



	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Drivetrain (TorqueCurve torqueCurve, double[] gearRatios, double gearRatioFinalDrive, 
		EngineTuning engineTuning, 
		TransmissionTuning transmissionTuning, TransmissionGearsTuning transmissionGearsTuning, 
		DifferentialTuning differentialTuning, DifferentialGearsTuning differentialGearsTuning) 
	{
		this.engine = new Engine(torqueCurve, engineTuning);
		this.transmission = new Transmission(gearRatios, transmissionTuning, transmissionGearsTuning);
		this.differential = new Differential(gearRatioFinalDrive, differentialTuning, differentialGearsTuning);
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Computation ------------------------------------------
	//---------------------------------------------------------------------------------------
	// Compute the moving force
	public void ComputeVehicleMovingForce(float vehicleVelocity, float wheelRadius, double controlsThrottle, double controlsShiftUp, double controlsShiftDown) {
		/* 
		 * RPM cycle
		 */
		/**********************************************************************************/
		// Compute RPM of the wheels and pass it to the differential
		double wheelsRpm = (60 * vehicleVelocity) / (2 * wheelRadius * Mathf.PI);
		differential.SetInputFeedbackRPM(wheelsRpm);

		// Compute RPM of the differential and pass it to the transmission
		differential.ComputeOutputFeedbackRPM();
		transmission.SetInputFeedbackRPM(differential.GetOutputFeedbackRPM());

		// Compute RPM of the transmission and pass it to the engine
		transmission.ComputeOutputFeedbackRpm(); transmission.ComputeShifting(controlsShiftUp, controlsShiftDown);
		engine.SetInputFeedbackRPM(transmission.GetOutputFeedbackRPM(), controlsThrottle);
		/**********************************************************************************/



		/* 
		 * Torque cycle
		 */
		/**********************************************************************************/
		// Compute engine torque and pass it to the transmission
		engine.ComputeOutputTorque(controlsThrottle);
		transmission.SetInputTorque(engine.GetOutputTorque());

		// Compute transmission torque and pass it to the differential
		transmission.ComputeOutputTorque();
		differential.SetInputTorque(transmission.GetOutputTorque());

		// Compute differential torque and save it as a moving force
		differential.ComputeOutputTorque();
		this.vehicleMovingForce = differential.GetOutputTorque() / wheelRadius;
		/**********************************************************************************/
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//--------------------------- Public Setters/Getters ------------------------------------
	//---------------------------------------------------------------------------------------
	public double GetVehicleMovingForce() {
		return vehicleMovingForce;
	}

	public double GetCurrentRpm() {
		return engine.GetCurrentRPM();
	}

	public double GetMaxRPM() {
		return engine.GetMaxRPM();
	}

	public double GetMinRPM() {
		return engine.GetMinRPM();
	}

	public int GetCurrentGear() {
		return transmission.GetCurrentGear();
	}

	public double GetMaxVelocityAtCurrentGear(float wheelRadius) {
		return engine.GetMaxRPM() / transmission.GetCurrentGearRation() / differential.GetGearRatioFinalDrive() * 2.0 * Mathf.PI * wheelRadius / 60.0;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}