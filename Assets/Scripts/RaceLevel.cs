using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaceLevel : MonoBehaviour 
{
	public double raceDistance;

	public Text displayCountdown;
	public Text displayRaceResults;
	public Cluster cluster;

	public Car playerCar;
	public Car opponentCar;
	public int opponentRpmTolerance;

	public MeshRenderer startingGround;








	private Race race;
	private Countdown countdown;



	//---------------------------------------------------------------------------------------
	//------------------------------------- Game --------------------------------------------
	//---------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () {
		this.race = new Race(raceDistance, playerCar, opponentCar, opponentRpmTolerance, displayRaceResults);
		this.countdown = new Countdown(displayCountdown, race);
		this.cluster.SetCar(playerCar);

		this.playerCar.positionAtGroundLevel(startingGround, true, false, 0.2);
		this.opponentCar.positionAtGroundLevel(startingGround, false, true, 0.2);

		race.Start();
		countdown.Start();
	}
	
	// Update is called once per frame
	void Update () {
		race.Update();
		countdown.Update();
	}
		
	// Update is called with fixed time step
	void FixedUpdate() {
		race.FixedUpdate();
		countdown.FixedUpdate();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//------------------------------ Control Listeners --------------------------------------
	//---------------------------------------------------------------------------------------
	public void PlayerPressGas() {
		race.PlayerPressGas();
	}

	public void PlayerReleaseGas() {
		race.PlayerReleaseGas();
	}

	public void PlayerPressShiftUp() {
		race.PlayerPressShiftUp();
	}

	public void PlayerReleaseShiftUp() {
		race.PlayerReleaseShiftUp();
	}

	public void PlayerPressShiftDown() {
		if (playerCar.GetCurrentGearActual() != 1)
			race.PlayerPressShiftDown();
	}

	public void PlayerReleaseShiftDown() {
		race.PlayerReleaseShiftDown();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}
