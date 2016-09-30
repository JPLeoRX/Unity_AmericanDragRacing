using UnityEngine;
using System;
using UnityEngine.UI;

public class Cluster : MonoBehaviour
{
	// The car to which this cluster is attached
	private Car car;

	// Texts
	public Text textRPM;
	public Text textSpeed;
	public Text textGear;

	// Images
	public Image barSample;
	public Image backgroundPanel;

	// Resulting array of bars
	private Image[] bars;

	// Colors
	private Color32 colorNormal = new Color32(253, 253, 253, 255);
	private Color32 colorRedline = new Color32(230, 0, 0, 255);
	private Color32 colorCoveredNormal = new Color32(69, 255, 255, 255);
	private Color32 colorCoveredRedline = new Color32(255, 90, 0, 255);

	// Image data
	private int barWidth;
	private int barHeight;
	private int backgroundPanelWidth;
	private int backgroundPanelHeight;
	private int barsStartX;						// Starting X coordinate, where first bar must be drawn

	// Gaps data
	private const int gapBetweenBars = 2;				// Gap between bars
	private const int gapBorder = 5;					// Gap between first/last bars and border of the background

	// Tachometer data
	private int maxRPM;
	private int spacingRPM = 500;
	private int numberOfBars;
	private int numberOfRedlineBars;



	//---------------------------------------------------------------------------------------
	//------------------------------------- Game --------------------------------------------
	//---------------------------------------------------------------------------------------
	// Use this for initialization
	void Start ()
	{
		this.InitializeTachometerData();
		this.InitializeImageData();
		this.InitializeBars();
	}

	// Update is called once per frame
	void Update()
	{
		this.UpdateBars();
		this.UpdateText();
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
		


	//---------------------------------------------------------------------------------------
	//------------------------------- Initialization ----------------------------------------
	//---------------------------------------------------------------------------------------
	public void InitializeTachometerData()
	{
		// Compute max rpm
		this.maxRPM = car.GetRedlineRPM() + spacingRPM;

		// Compute number of bars
		this.numberOfBars = maxRPM / spacingRPM;

		// Compute number of redline bars
		if (maxRPM <= 6000)
			this.numberOfRedlineBars = 2;
		else if (maxRPM <= 8000)
			this.numberOfRedlineBars = 3;
		else
			this.numberOfRedlineBars = 4;
	}

	public void InitializeImageData() 
	{
		// Store background panel's data
		this.backgroundPanelWidth = (int) backgroundPanel.rectTransform.sizeDelta.x;
		this.backgroundPanelHeight = (int) backgroundPanel.rectTransform.sizeDelta.y;

		// Compute bar width, save bar height
		this.barWidth = (int) ((backgroundPanelWidth - gapBorder * 2) / this.numberOfBars) - gapBetweenBars;
		this.barHeight = (int) barSample.rectTransform.sizeDelta.y;

		// Compute bars starting position
		this.barsStartX = (int) (backgroundPanel.rectTransform.anchoredPosition.x - backgroundPanelWidth / 2 + barWidth / 2 + gapBorder);
	}

	public void InitializeBars() 
	{
		// Create new array
		this.bars = new Image[this.numberOfBars];

		// For each bar
		for (int i = 0; i < this.numberOfBars; i++)
		{
			// Clone the bar
			Image tempBar = (Image) UnityEngine.Object.Instantiate(barSample);

			// Set correct parent
			tempBar.rectTransform.parent = barSample.rectTransform.parent;

			// Set correct size
			tempBar.rectTransform.sizeDelta = new Vector2(barWidth, barHeight);

			// Compute coordinates
			int tempBarX = (int) (barsStartX + i * (barWidth + gapBetweenBars));
			int tempBarY = (int) tempBar.rectTransform.anchoredPosition.y;

			// Set correct coordinates
			tempBar.rectTransform.anchoredPosition = new Vector2(tempBarX, tempBarY);

			// Save bar in array
			this.bars[i] = tempBar;
		}

		// Disable sample bar
		barSample.enabled = false;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------


	//---------------------------------------------------------------------------------------
	//---------------------------------- Updates --------------------------------------------
	//---------------------------------------------------------------------------------------
	private void UpdateBars() 
	{
		// Compute covered bars end index
		int coveredEndIndex = car.GetCurrentRPM() / spacingRPM;

		// Compute redline bars start index
		int redlineStartIndex = numberOfBars - numberOfRedlineBars;

		// For all bars
		for (int barIndex = 0; barIndex < numberOfBars; barIndex++)
		{
			// If this bar is covered, but not before redline
			if (barIndex < coveredEndIndex && barIndex < redlineStartIndex)
			{
				this.bars[barIndex].color = colorCoveredNormal;
				this.textRPM.color = colorCoveredNormal;
			}

			// If this bar is not covered, and is not in redline
			else if (barIndex < redlineStartIndex)
			{
				this.bars[barIndex].color = colorNormal;
			}

			// If this bar is covered, and it is in the redline
			else if (barIndex < coveredEndIndex && barIndex >= redlineStartIndex)
			{
				this.bars[barIndex].color = colorCoveredRedline;
				this.textRPM.color = colorCoveredRedline;
			}

			// If this bar is redline
			else
			{
				this.bars[barIndex].color = colorRedline;
			}
		}
	}

	private void UpdateText() 
	{
		// Update gear text
		textGear.text = "" + car.GetCurrentGearReading() + "";

		// Update speed text
		textSpeed.text = "" + car.GetCurrentSpeedKMH() + "";

		// Update RPM text
		if (car.GetCurrentRPM() % 10 == 0)
			textRPM.text = "" + car.GetCurrentRPM() + "";
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------



	//---------------------------------------------------------------------------------------
	//-------------------------------- Public Setters ---------------------------------------
	//---------------------------------------------------------------------------------------
	public void SetCar(Car car)
	{
		this.car = car;
	}
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------
}