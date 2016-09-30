using UnityEngine;
using System;

[Serializable]
public class Car : MonoBehaviour 
{
	/************************************************ Specified Values ****************************************************************/
	/**********************************************************************************************************************************/
	public Transform vehicleRoot;
	public MeshRenderer bodyMesh;
	public MeshRenderer wheelMeshLF;
	public MeshRenderer wheelMeshLR;
	public MeshRenderer wheelMeshRF;
	public MeshRenderer wheelMeshRR;
	public Camera vehicleCamera;
	public ParticleSystem[] tireParticleSystems;
	public float vehicleDragCoefficient = 0.5f;
	public float vehicleMass = 1500;
	public TorqueCurve engineTorqueCurve = new TorqueCurve(new double[]{1000, 3000, 4250, 5500}, new double[]{200, 373, 300, 200});
	public double[] drivetrainGearRatios = new double[]{2.84, 1.55, 1.00, 0.70};
	public double drivetrainGearRatioFinalDrive = 2.73;

	public EngineTuning engineTuning = new EngineTuning(TuningType.Level0Stock);
	public TransmissionTuning transmissionTuning = new TransmissionTuning(TuningType.Level0Stock);
	public TransmissionGearsTuning transmissionGearsTuning = new TransmissionGearsTuning(GearsTuningType.Stock);
	public DifferentialTuning differentialTuning = new DifferentialTuning(TuningType.Level0Stock);
	public DifferentialGearsTuning differentialGearsTuning = new DifferentialGearsTuning(GearsTuningType.Stock);
	public BodyTuning bodyTuning = new BodyTuning(TuningType.Level0Stock);
	public TiresTuning tiresTuning = new TiresTuning(TuningType.Level0Stock);
	/**********************************************************************************************************************************/



	/************************************************* Computed Values ****************************************************************/
	/**********************************************************************************************************************************/
	private float vehicleLength;
	private float vehicleWidth;
	private float vehicleHeight;
	private float vehicleWheelRadius;
	/**********************************************************************************************************************************/



	/************************************************* Vehicle Physics ****************************************************************/
	/**********************************************************************************************************************************/
	private Drivetrain drivetrain;
	private Resistance resistance;
	private double a;						// Acceleration
	private double dv;						// Delta velocity
	private double v;						// Velocity
	private double ds;						// Delta path
	/**********************************************************************************************************************************/



	/************************************************* Vehicle Visual *****************************************************************/
	/**********************************************************************************************************************************/
	private Wheel wheelLF;
	private Wheel wheelLR;
	private Wheel wheelRF;
	private Wheel wheelRR;
	/**********************************************************************************************************************************/



	/************************************************* Vehicle Controls ***************************************************************/
	/**********************************************************************************************************************************/
	private double controlsThrottle;
	private double controlsShiftUp;
	private double controlsShiftDown;
	/**********************************************************************************************************************************/



	/************************************************* Vehicle Readings ***************************************************************/
	/**********************************************************************************************************************************/
	private int currentRPM;
	private int currentSpeedKMH;
	private int currentGearActual;
	private int currentGearReading;
	private double currentDistance;
	/**********************************************************************************************************************************/



	// Use this for initialization
	void Start () {
		// Compute vehicle dimensions
		vehicleLength = bodyMesh.bounds.size.z;
		vehicleWidth = bodyMesh.bounds.size.x;
		vehicleHeight = bodyMesh.bounds.size.y;

		// Compute wheel radius
		vehicleWheelRadius = wheelMeshLF.bounds.size.y / 2.0f;

		// Create drivetrain and resistance
		drivetrain = new Drivetrain(engineTorqueCurve, drivetrainGearRatios, drivetrainGearRatioFinalDrive, engineTuning, transmissionTuning, transmissionGearsTuning, differentialTuning, differentialGearsTuning);
		resistance  = new Resistance(vehicleDragCoefficient, vehicleWidth, vehicleHeight, vehicleMass, bodyTuning, tiresTuning);

		// Create wheels
		wheelLF = new Wheel(wheelMeshLF); 
		wheelLR = new Wheel(wheelMeshLR, tireParticleSystems[0]); 
		wheelRF = new Wheel(wheelMeshRF); 
		wheelRR = new Wheel(wheelMeshRR, tireParticleSystems[1]);
	}
	
	// Update is called once per frame
	void Update () {

		if (this.GetCurrentGearActual() == 0 && this.GetCurrentSpeedKMH() == 0)
		{
			wheelLR.Burnout(this.GetCurrentRPM(), this.GetRedlineRPM(), this.GetMinRPM());
			wheelRR.Burnout(this.GetCurrentRPM(), this.GetRedlineRPM(), this.GetMinRPM());

			this.currentGearReading = 1;
		}

		else
		{
			wheelLR.BurnoutStop();
			wheelRR.BurnoutStop();

			// Rotate the wheels
			wheelLF.Spin(v); 
			wheelLR.Spin(v); 
			wheelRF.Spin(v); 
			wheelRR.Spin(v);

			this.currentGearReading = currentGearActual;
		}
	}

	// Update is called with fixed time step
	void FixedUpdate() {
		drivetrain.ComputeVehicleMovingForce((float) v, vehicleWheelRadius, controlsThrottle, controlsShiftUp, controlsShiftDown);
		resistance.ComputeDragForce(v);
		resistance.ComputeRollingFrictionForce(v, drivetrain.GetVehicleMovingForce());

		a = (drivetrain.GetVehicleMovingForce() + resistance.GetDragForce() + resistance.GetRollingFrictionForce()) / vehicleMass;
		dv = a * Time.fixedDeltaTime;

		v = v + dv;
		v = Mathf.Clamp((float) v, 0, (float) drivetrain.GetMaxVelocityAtCurrentGear(vehicleWheelRadius));
		ds = v * Time.fixedDeltaTime;

		float x = vehicleRoot.position.x;
		float y = vehicleRoot.position.y;
		float z = vehicleRoot.position.z;

		vehicleRoot.position = new Vector3(x, y, (float) (z - ds));

		currentRPM = (int) drivetrain.GetCurrentRpm();
		currentSpeedKMH = (int) Mathf.Round((float) (v * 3.6));
		currentGearActual = drivetrain.GetCurrentGear();
		currentDistance += ds;
	}



	//---------------------------------------------------------------------------------------
	//------------------------------ Vehicle Controls ---------------------------------------
	//---------------------------------------------------------------------------------------
	public void SetControlsThrottle(double value) {
		this.controlsThrottle = value;
	}

	public void SetControlsShiftUp(double value) {
		this.controlsShiftUp = value;
	}

	public void SetControlsShiftDown(double value) {
		this.controlsShiftDown = value;
	}

	public void DisableCamera() {
		this.vehicleCamera.enabled = false;
	}

	public void EnableCamera() {
		this.vehicleCamera.enabled = true;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//------------------------------ Vehicle Readings ---------------------------------------
	//---------------------------------------------------------------------------------------
	public int GetCurrentRPM() {
		return currentRPM;
	}

	public int GetRedlineRPM() {
		return (int) drivetrain.GetMaxRPM();
	}

	public int GetMinRPM() {
		return (int) drivetrain.GetMinRPM();
	}

	public int GetCurrentSpeedKMH() {
		return currentSpeedKMH;
	}

	public int GetCurrentGearActual() {
		return currentGearActual;
	}

	public int GetCurrentGearReading() {
		return currentGearReading;
	}

	public double GetCurrentDistance() {
		return currentDistance;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------

	public void positionAtGroundLevel(MeshRenderer ground, bool allightLeft, bool allightRight, double deltaWidth) {
		float x = 0, y = 0, z = 0;
		z = (float) (ground.bounds.center.z + vehicleLength / 2.0f);
		y = (float) (vehicleRoot.transform.position.y - wheelMeshLF.bounds.min.y);

		if (allightLeft)
			x = (float)  (ground.bounds.center.x + bodyMesh.bounds.size.x / 2.0 + deltaWidth);
		if (allightRight)
			x = (float)  (ground.bounds.center.x - bodyMesh.bounds.size.x / 2.0 - deltaWidth);

		this.vehicleRoot.transform.position = new Vector3(x, y, z);
	}


}