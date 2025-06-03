using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TMP_Text scoreText;

    public void Start()
    {
        Events.AddScore += AddScore;
        AddScore(0);
    }

    private void AddScore(int amount)
    {
        score += amount;
        scoreText.SetText(score.ToString());
    }
}
