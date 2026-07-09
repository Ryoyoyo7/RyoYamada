using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timeText;
    public Text resultText;
    public Text resultDetailText;
    public GameObject startPanel;
    public GameObject retryButton;
    private string difficultyName = "Easy";

    public float timeLimit = 30.0f;
    public float countdownTime = 3.0f;

    public static bool isPlaying = false;
    public static bool isGameOver = false;

    private float time;
    private float countdown;
    private int clearScore = 10;
    private bool isCountingDown = false;

    private Counter counter;
    private TargetGenerator targetGenerator;

    void Start()
    {
        time = timeLimit;
        countdown = countdownTime;

        isPlaying = false;
        isGameOver = false;
        isCountingDown = false;

        counter = GameObject.Find("GameDirector").GetComponent<Counter>();
        targetGenerator = GameObject.Find("TargetGenerator").GetComponent<TargetGenerator>();

        timeText.text = "Time : " + timeLimit.ToString("F1");

        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }

        if (resultDetailText != null)
        {
            resultDetailText.gameObject.SetActive(false);
        }

        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        if (retryButton != null)
        {
            retryButton.SetActive(false);
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            Countdown();
            return;
        }

        if (!isPlaying || isGameOver)
        {
            return;
        }

        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            FinishGame();
        }

        timeText.text = "Time : " + time.ToString("F1");
    }

    public void SelectEasy()
    {
        PrepareGame(10, "Easy");
    }

    public void SelectNormal()
    {
        PrepareGame(20, "Normal");
    }

    public void SelectHard()
    {
        PrepareGame(30, "Hard");
    }

    private void PrepareGame(int score, string difficulty)
    {
        clearScore = score;
        difficultyName = difficulty;

        time = timeLimit;
        countdown = countdownTime;

        isPlaying = false;      // まだゲーム中ではない
        isGameOver = false;
        isCountingDown = true;  // カウントダウン中

        if (counter != null)
        {
            counter.ResetRecord();
        }

        if (resultDetailText != null)
        {
            resultDetailText.gameObject.SetActive(false);
        }

        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        if (retryButton != null)
        {
            retryButton.SetActive(false);
        }

        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "3";
        }

        timeText.text = "Time : " + time.ToString("F1");
    }

    private void Countdown()
    {
        countdown -= Time.deltaTime;

        if (countdown > 2)
        {
            resultText.text = "3";
        }
        else if (countdown > 1)
        {
            resultText.text = "2";
        }
        else if (countdown > 0)
        {
            resultText.text = "1";
        }
        else
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        isCountingDown = false;
        isPlaying = true;       // ここで初めてゲーム開始
        isGameOver = false;

        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }

        if (targetGenerator != null)
        {
            targetGenerator.StartTargetGenerate(); // ここで初めてTarget生成
        }
    }

    private void FinishGame()
    {
        isPlaying = false;
        isGameOver = true;

        int score = counter.hitCount;
        string result;

        if (score >= clearScore)
        {
            result = "GAME CLEAR";
        }
        else
        {
            result = "GAME OVER";
        }

        string highScoreKey = "HighScore_" + difficultyName;
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }

        float accuracy = 0.0f;

        if (counter.shotCount > 0)
        {
            accuracy = (float)(counter.targetHitCount + counter.bonusHitCount) / counter.shotCount * 100.0f;
        }

        if (resultText != null)
        {
            resultText.text = result;
            resultText.gameObject.SetActive(true);
        }

        if (resultDetailText != null)
        {
            resultDetailText.text =
                "Difficulty : " + difficultyName + "\n" +
                "Score : " + score + " / " + clearScore + "\n" +
                "Shot : " + counter.shotCount + "\n" +
                "Target Hit : " + counter.targetHitCount + "\n" +
                "Bonus Hit : " + counter.bonusHitCount + " / " + " 3 " + "\n" +
                "Obstacle Hit : " + counter.obstacleHitCount + "\n" +
                "Accuracy : " + accuracy.ToString("F1") + "%\n" +
                "High Score : " + highScore;

            resultDetailText.gameObject.SetActive(true);
        }

        if (retryButton != null)
        {
            retryButton.SetActive(true);
        }
    }

    public void RetryGame()
    {
        time = timeLimit;
        countdown = countdownTime;

        isPlaying = false;
        isGameOver = false;
        isCountingDown = false;

        if (counter != null)
        {
            counter.ResetRecord();
        }

        ClearObjects();

        timeText.text = "Time : " + timeLimit.ToString("F1");

        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }

        if (retryButton != null)
        {
            retryButton.SetActive(false);
        }

        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        if (resultDetailText != null)
        {
            resultDetailText.gameObject.SetActive(false);
        }
    }

    private void ClearObjects()
    {
        TargetController[] targets = FindObjectsOfType<TargetController>();
        foreach (TargetController target in targets)
        {
            Destroy(target.gameObject);
        }

        BulletController[] bullets = FindObjectsOfType<BulletController>();
        foreach (BulletController bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}