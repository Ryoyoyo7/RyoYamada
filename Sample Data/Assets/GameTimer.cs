using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timeText;
    public Text resultText;
    public GameObject startPanel;

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

        if (startPanel != null)
        {
            startPanel.SetActive(true);
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
        PrepareGame(10);
    }

    public void SelectNormal()
    {
        PrepareGame(20);
    }

    public void SelectHard()
    {
        PrepareGame(30);
    }

    private void PrepareGame(int score)
    {
        clearScore = score;
        time = timeLimit;
        countdown = countdownTime;

        isPlaying = false;      // まだゲーム中ではない
        isGameOver = false;
        isCountingDown = true;  // カウントダウン中

        if (counter != null)
        {
            counter.hitCount = 0;
        }

        if (startPanel != null)
        {
            startPanel.SetActive(false);
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

        if (resultText != null)
        {
            if (counter.hitCount >= clearScore)
            {
                resultText.text = "GAME CLEAR";
            }
            else
            {
                resultText.text = "GAME OVER";
            }

            resultText.gameObject.SetActive(true);
        }
    }
}