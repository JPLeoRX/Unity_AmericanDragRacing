using UnityEngine;
using System;

[Serializable]
public class EngineTuning
{
	public TuningType type = TuningType.Level0Stock;

	public EngineTuning (TuningType type) {
		this.type = type;
	}

	public double GetPercentageIncrease() {
		if (type == TuningType.Level1StreetSpec)
			return 0.1;
		else if (type == TuningType.Level2TunerSpec)
			return 0.2;
		else if (type == TuningType.Level3SportsSpec)
			return 0.3;
		else if (type == TuningType.Level4RaceSpec)
			return 0.4;
		else if (type == TuningType.Level5DragSpec)
			return 0.5;
		else if (type == TuningType.Level6MagicSpec)
			return 0.8;
		else
			return 0;
	}
}