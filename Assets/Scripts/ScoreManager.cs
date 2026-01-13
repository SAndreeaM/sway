using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private float startY;
    private int score;

    void Start()
    {
        if(player != null)
        {
            startY = player.position.y; 
        }
    }

    void Update()
    {
        if(player == null) return;

        // Calculate score based on the distance traveled downward
        float distance = startY - player.position.y;
        score = Mathf.Max(0, Mathf.FloorToInt(distance));
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}