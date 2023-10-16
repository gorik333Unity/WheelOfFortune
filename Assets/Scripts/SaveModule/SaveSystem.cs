using UnityEngine;

namespace SaveModule
{
    public static class SaveSystem
    {
        private const string SCORE_KEY = "ScoreKey";

        public static void SaveScore(float score)
        {
            PlayerPrefs.SetFloat(SCORE_KEY, score);
            PlayerPrefs.Save();
        }

        public static float LoadScore()
        {
            if (PlayerPrefs.HasKey(SCORE_KEY))
            {
                return PlayerPrefs.GetFloat(SCORE_KEY);
            }

            return 0f;
        }
    }
}
