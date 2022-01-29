using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreKeeping : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private bool isEnabled;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text topScoreText;

    private const string TOP_SCORE = "topScore";


    // Start is called before the first frame update
    void Start()
    {
        PlayerEvents.OnPlayerDeath += OnPlayerDeath;
        Reset();
        StartCoroutine(UpdateScore());
    }

    private void Disable()
    {
        isEnabled = false;
    }

    private void Reset()
    {
        score = 0;
        topScoreText.text = PlayerPrefs.GetInt(TOP_SCORE, 0).ToString();
    }

    private void OnPlayerDeath()
    {
        Disable();
        SaveTopScore();
    }

    private IEnumerator UpdateScore()
    {
        while (isEnabled)
        {
            score++;
            scoreText.text = score.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    private void SaveTopScore()
    {
        int topScore = PlayerPrefs.GetInt(TOP_SCORE, 0);
        if (score > topScore)
        {
            topScoreText.text = score.ToString();
            PlayerPrefs.SetInt(TOP_SCORE, score);
            PlayerPrefs.Save();
        }
    }
}
