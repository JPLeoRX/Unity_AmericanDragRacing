using UnityEngine;
using System;

[Serializable]
public class TransmissionTuning
{
	public TuningType type = TuningType.Level0Stock;

	public TransmissionTuning (TuningType type) {
		this.type = type;
	}

	public double GetPercentageIncrease() {
		if (type == TuningType.Level1StreetSpec)
			return -0.10;
		else if (type == TuningType.Level2TunerSpec)
			return -0.08;
		else if (type == TuningType.Level3SportsSpec)
			return -0.04;
		else if (type == TuningType.Level4RaceSpec)
			return -0.01;
		else if (type == TuningType.Level5DragSpec)
			return 0.00;
		else if (type == TuningType.Level6MagicSpec)
			return 0.15;
		else
			return -0.12;
	}
}