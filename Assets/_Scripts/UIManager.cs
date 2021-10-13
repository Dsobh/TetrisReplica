using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TMP_Text scoreText, linesText, levelText;
    private int lineNumber = 0;
    private int level = 1;
    private int score = 0;
    private bool gameOver = false;

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
            }
        }
    }

    void OnEnable()
    {
        PieceController.OnLevelChange += HandleLevelChange;
        PieceController.OnNumberOfLinesChange += HandleLineChange;
        PieceController.OnGameOverTrigger += HandleGameOver;
    }

    void OnDisable()
    {
        PieceController.OnLevelChange -= HandleLevelChange;
        PieceController.OnNumberOfLinesChange -= HandleLineChange;
        PieceController.OnGameOverTrigger -= HandleGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.SetText("0");
        linesText.SetText("0");
        levelText.SetText("1");
    }

    void HandleLevelChange(int level)
    {
        this.level = level;
        levelText.SetText(this.level.ToString());
    }

    void HandleLineChange(int lineValue, int scoreValue)
    {
        score = scoreValue;
        lineNumber = lineValue;
        linesText.SetText(lineNumber.ToString());
        scoreText.SetText(score.ToString());
    }

    void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
        gameOver = true;
    }

}
