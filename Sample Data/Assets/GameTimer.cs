using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timeText;
    public float timeLimit = 30.0f;

    public static bool isGameOver = false;

    private float time;

    void Start()
    {
        time = timeLimit;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            isGameOver = true;
        }

        timeText.text = "Time : " + time.ToString("F1");
    }
}