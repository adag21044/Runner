using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's score.
/// Implements the Command Pattern for score updates.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    //[SerializeField] private Text scoreText; // UI element to display the score
    private int currentScore = 0;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"ScoreManager: Score updated. Current score: {currentScore}");
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        /*if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
        else
        {
            Debug.LogWarning("ScoreManager: ScoreText reference is missing!");
        }*/
    }
}
