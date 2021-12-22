using System;
using System.Linq;

namespace Extensions
{
    public static class RandomExtensions
	{
		public static bool CheckSuccess(this Random random, float chancePercentage)
		{
			float attemptChance = random.NextPositiveFloat();
			return attemptChance >= 0 && attemptChance <= chancePercentage;
		}

		public static int NextFavoringMinors(this Random randomGen, int[] crescentNumbers)
		{
			int sum = crescentNumbers.Sum();

			float[] probabilities = crescentNumbers
				.Reverse()
				.Select(n => (float)n / sum)
				.ToArray();

			for (int i = 0; i < probabilities.Length; i++)
			{
				if (i > 0)
				{
					probabilities[i] += probabilities[i - 1];
				}
			}

			return randomGen.NextFromOrderedProbabilities(probabilities);
		}

		public static int NextFromOrderedProbabilities(this Random randomGen, float[] orderedProbabilities)
		{
			float randomNumber = randomGen.NextPositiveFloat();

			for (int i = 0; i < orderedProbabilities.Length; i++)
			{
				if (i == 0 && randomNumber < orderedProbabilities[i] ||
					i == orderedProbabilities.Length - 1 && randomNumber > orderedProbabilities[i - 1] &&
					randomNumber <= orderedProbabilities[i] ||
					i > 0 && randomNumber >= orderedProbabilities[i - 1] && randomNumber < orderedProbabilities[i])
				{
					return i;
				}
			}

			return 0;
		}

		public static int NextPureDistributed(this Random randomGen, int[] numbers, float skipChancePerIndex, bool decrementalSkip = false, float decrementRate = 0.5f)
		{
			for (int i = 0; i < numbers.Length; i++)
			{
				if (!randomGen.CheckSuccess(skipChancePerIndex))
				{
					return i;
				}

				if (decrementalSkip)
				{
					skipChancePerIndex *= decrementRate;
				}
			}

			return 0;
		}

		public static int NextWhileSkipping(this Random randomGen, int[] crescentNumbers, float bonusChance = 0)
		{
			int sum = crescentNumbers.Sum();
			float[] chances = crescentNumbers.Select(number => (float)number / sum).ToArray();

			for (int i = 0; i < crescentNumbers.Length; i++)
			{
				if (!randomGen.CheckSuccess(chances[i] + bonusChance))
				{
					return i;
				}
			}

			return 0;
		}

		public static float NextFloat(this Random randomGen)
		{
			return (float)randomGen.NextDouble();
		}

		public static float NextFloat(this Random randomGen, float min, float max)
		{
			return randomGen.NextFloat() * (max - min) + min;
		}

		public static float NextPositiveFloat(this Random randomGen)
		{
			return (float)randomGen.NextDouble();
		}
	}
}