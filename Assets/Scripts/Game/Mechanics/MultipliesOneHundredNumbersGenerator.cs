using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Mechanics
{
    [System.Serializable]
    public class MultipliesOneHundredNumbersGenerator : INumbersGenerator
    {
        [SerializeField]
        private int _min;

        [SerializeField]
        private int _max;

        [SerializeField]
        private int _minIntervalBetweenNumbers;

        [SerializeField]
        private int _multiplier;

        [SerializeField, Header("if 100, number will be xx00, like 1000, 10000")]
        private int _multipliesNumber;

        public int[] GenerateNumbers(int count)
        {
            var numbers = GetAllPossibleNumbers();
            var result = GetShuffledCertainCountOfNumbers(numbers, count);

            return result;
        }

        private int[] GetAllPossibleNumbers()
        {
            var valuesPool = new List<int>();
            var count = _max / _minIntervalBetweenNumbers;

            for (int i = 0; i < count; i++)
            {
                valuesPool.Add((i + 1) * _multiplier);
            }

            return valuesPool.ToArray();
        }

        private int[] GetShuffledCertainCountOfNumbers(int[] numbers, int count)
        {
            var result = new List<int>();
            var shuffled = numbers.Shuffle().ToArray();

            for (int i = 0; i < count; i++)
            {
                result.Add(shuffled[i]);
            }

            return result.ToArray();
        }
    }
}
