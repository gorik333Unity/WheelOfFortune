using UnityEngine;

namespace SaveModule
{
    public static class SaveSystem
    {
        private const string SCORE_KEY = "ScoreKey";
        private const string LAST_WIN_KEY = "LastWinKey";

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

        public static void SaveLastWinScore(float score)
        {
            PlayerPrefs.SetFloat(LAST_WIN_KEY, score);
            PlayerPrefs.Save();
        }

        public static float LoadLastWinScore()
        {
            if (PlayerPrefs.HasKey(LAST_WIN_KEY))
            {
                return PlayerPrefs.GetFloat(LAST_WIN_KEY);
            }

            return 0f;
        }
    }
}
