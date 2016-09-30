using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Countdown 
{
	public Text display;
	public Race race;

	// Colors
	private Color colorUsual = new Color32(255, 90, 0, 255);
	private Color colorGo = new Color32(0, 219, 0, 255);

	// End of the countdown indicator
	private bool finished = false;

	// Times
	private float timeDisplayDigits = 1.0f;
	private float timeBetweenDigits = 0.5f;
	private float startTime;
	private float showReadyStartTime;
	private float showReadyEndTime;
	private float show3StartTime;
	private float show3EndTime;
	private float show2StartTime;
	private float show2EndTime;
	private float show1StartTime;
	private float show1EndTime;
	private float showGoStartTime;
	private float showGoEndTime;



	//---------------------------------------------------------------------------------------
	//-------------------------------- Constructors -----------------------------------------
	//---------------------------------------------------------------------------------------
	public Countdown (Text display, Race race)
	{
		this.display = display;
		this.race = race;
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
		this.InitializeTimes();
	}

	// Update is called once per frame
	public void Update () 
	{

	}

	// Update is called with fixed time step
	public void FixedUpdate() 
	{
		// If the countdown has not ended yet
		if (!finished)
		{
			// Save the current time
			float currentTime = Time.time;

			if (currentTime < showReadyStartTime)
				this.ClearText();

			else if (showReadyStartTime < currentTime && currentTime < showReadyEndTime)
				this.ShowReady();

			else if (showReadyEndTime < currentTime && currentTime < show3StartTime)
				this.ClearText();

			else if (show3StartTime < currentTime && currentTime < show3EndTime)
				this.Show3();

			else if (show3EndTime < currentTime && currentTime < show2StartTime)
				this.ClearText();

			else if (show2StartTime < currentTime && currentTime < show2EndTime)
				this.Show2();

			else if (show2EndTime < currentTime && currentTime < show1StartTime)
				this.ClearText();

			else if (show1StartTime < currentTime && currentTime < show1EndTime)
				this.Show1();

			else if (show1EndTime < currentTime && currentTime < showGoStartTime)
				this.ClearText();

			else if (showGoStartTime < currentTime && currentTime < showGoEndTime)
				this.ShowGo();

			else if (currentTime > showGoEndTime)
				this.EndCountdown();
		}
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//----------------------------------- Helpers -------------------------------------------
	//---------------------------------------------------------------------------------------
	private void InitializeTimes() {
		// Save start time
		startTime = Time.time;

		showReadyStartTime = startTime + timeBetweenDigits;
		showReadyEndTime = showReadyStartTime + timeDisplayDigits;

		show3StartTime = showReadyEndTime + timeBetweenDigits;
		show3EndTime = show3StartTime + timeDisplayDigits;

		show2StartTime = show3EndTime + timeBetweenDigits;
		show2EndTime = show2StartTime + timeDisplayDigits;

		show1StartTime = show2EndTime + timeBetweenDigits;
		show1EndTime = show1StartTime + timeDisplayDigits;

		showGoStartTime = show1EndTime + timeBetweenDigits;
		showGoEndTime = showGoStartTime + timeDisplayDigits;
	}
		
	private void ShowReady() {
		display.color = colorUsual;
		display.text = "Get Ready!";
	}

	private void Show3() {
		display.color = colorUsual;
		display.text = "3";
	}

	private void Show2() {
		display.color = colorUsual;
		display.text = "2";
	}

	private void Show1() {
		display.color = colorUsual;
		display.text = "1";
	}

	private void ShowGo() {
		display.color = colorGo;
		display.text = "Go!";
	}

	private void ClearText() {
		display.color = colorUsual;
		display.text = "";
	}

	private void EndCountdown() {
		this.ClearText();
		finished = true;
		this.race.RaceStart();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}