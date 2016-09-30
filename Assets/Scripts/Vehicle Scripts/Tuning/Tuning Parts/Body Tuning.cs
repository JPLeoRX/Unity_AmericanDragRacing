using UnityEngine;
using System;

[Serializable]
public class BodyTuning 
{
	public TuningType type = TuningType.Level0Stock;

	public BodyTuning(TuningType type) {
		this.type = type;
	}

	public double GetPercentageDecrease() {
		if (type == TuningType.Level1StreetSpec)
			return 0.05;
		else if (type == TuningType.Level2TunerSpec)
			return 0.10;
		else if (type == TuningType.Level3SportsSpec)
			return 0.15;
		else if (type == TuningType.Level4RaceSpec)
			return 0.20;
		else if (type == TuningType.Level5DragSpec)
			return 0.30;
		else if (type == TuningType.Level6MagicSpec)
			return 0.50;
		else
			return 0;
	}
}
