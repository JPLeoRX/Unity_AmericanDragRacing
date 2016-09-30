using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Race 
{
	public double raceDistance;
	public Text display;
	public Car playerCar;
	public Car opponentCar;
	public int opponentRpmTolerance;

	private PlayerControls player;
	private OpponentControls opponent;

	private bool playerWon = false;
	private bool opponentWon = false;


	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Race (double raceDistance, Car playerCar, Car opponentCar, int rpmTolerance, Text raceResults)
	{
		this.raceDistance = raceDistance;
		this.playerCar = playerCar;
		this.opponentCar = opponentCar;
		this.opponentRpmTolerance = rpmTolerance;
		this.display = raceResults;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	


	//---------------------------------------------------------------------------------------
	//------------------------------------- Game --------------------------------------------
	//---------------------------------------------------------------------------------------
	// Use this for initialization
	public void Start () 
	{
		// Create new controls
		player = new PlayerControls(playerCar, raceDistance);
		opponent = new OpponentControls(opponentCar, raceDistance, opponentRpmTolerance);

		// Hide displau
		this.DisplayDisable();
	}
	
	// Update is called once per frame
	public void Update () 
	{
	
	}

	// Update is called with fixed time step
	public void FixedUpdate() 
	{
		// Update the controls
		player.FixedUpdate();
		opponent.FixedUpdate();

		// If the player has finished
		if (player.raceEnded)
		{
			// Stop the game
			Time.timeScale = 0;

			// Decide who won
			if (opponent.raceTime < player.raceTime && opponent.raceEnded)
				opponentWon = true;
			else
				playerWon = true;

			// Display the results
			this.DisplayShowResults();
		}
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//------------------------------- Display Helpers ---------------------------------------
	//---------------------------------------------------------------------------------------
	private void DisplayShowResults() {
		this.DisplayEnable();

		if (playerWon)
			this.DisplayShow1();
		else
			this.DisplayShow2();
	}

	private void DisplayDisable() {
		display.enabled = false;
	}

	private void DisplayEnable() {
		display.enabled = true;
	}

	private void DisplayShow1() {
		display.text = "1st!";
	}

	private void DisplayShow2() {
		display.text = "2nd!";
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//------------------------------ Control Listeners --------------------------------------
	//---------------------------------------------------------------------------------------
	public void RaceStart() {
		player.RaceStart();
		opponent.RaceStart();
	}

	public void PlayerPressGas() {
		player.PressGas();
	}

	public void PlayerReleaseGas() {
		player.ReleaseGas();
	}

	public void PlayerPressShiftUp() {
		player.PressShiftUp();
	}

	public void PlayerReleaseShiftUp() {
		player.ReleaseShiftUp();
	}

	public void PlayerPressShiftDown() {
		player.PressShiftDown();
	}

	public void PlayerReleaseShiftDown() {
		player.ReleaseShiftDown();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Public Getters ---------------------------------------
	//---------------------------------------------------------------------------------------
	public bool GetPlayerWon() {
		return playerWon;
	}

	public bool GetOpponentWon() {
		return opponentWon;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}