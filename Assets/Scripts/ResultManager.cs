using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        // Ambil skor dari PlayerPrefs
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        
        // Tampilkan skor
        scoreText.text = "Score: " + finalScore.ToString();
    }
}
