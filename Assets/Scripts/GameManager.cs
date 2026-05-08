using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Interfaces;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("スコア管理")]
    [SerializeField] private int score = 0;
    [SerializeField] private TMP_Text scoreText;

    [Header("制限時間")]
    [SerializeField] private float timeLimit = 30f; // 制限時間
    [SerializeField] private TMP_Text timetext;
    private float timer;
    private bool isGameOver = false;

    [Header("スタートUI")]
    [SerializeField] private TextMeshProUGUI readytext;
    [SerializeField] private TextMeshProUGUI gotext;

    [Header("PlayerHP")]
    [SerializeField] private TextMeshProUGUI hptext;

    [Header("Player")]
    [SerializeField] private Player player;

    private Coroutine startRoutine;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        InitGame();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void InitGame()
    {
        // 現在のシーンをチェック
        if (SceneManager.GetActiveScene().name != "UnityProject0501") return; 

        timer = timeLimit;
        isGameOver = false;

        UpdateScoreUI();
        UpdateTimeUI();
        UpdateHPUI();

        // UI初期化
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (timetext != null) timetext.gameObject.SetActive(false);
        if (readytext != null) readytext.gameObject.SetActive(false);
        if (gotext != null) gotext.gameObject.SetActive(false);
        if (hptext != null) hptext.gameObject.SetActive(false);

        // 既存コルーチンがあれば停止
        if(startRoutine != null)StopCoroutine(startRoutine);
        startRoutine = StartCoroutine(StartGameRoutine());
    }

    private void Update()
    {
        if(isGameOver)return;

        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            timer = 0;
            GameOver();
        }
        UpdateTimeUI() ;
    }

    // ==== READY/GO 演出 ====
    // 順番に出現させる
    // Ready→Go→Score、Timer
    private IEnumerator StartGameRoutine()
    {
        // 時間停止
        Time.timeScale = 0f;

        if (FadeManager.instance != null)
        {
            while (!FadeManager.instance.IsFadeComplete)
            {
                yield return null;
            }
        }
        
        // ReadyText出現
        if(readytext != null)
        {
            readytext.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            readytext.gameObject.SetActive(false);
        }

        // GoText出現
        if (gotext != null)
        {
            gotext.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            gotext.gameObject.SetActive(false);
        }

        // ScoreTextとTimeText出現
        if (scoreText != null) scoreText.gameObject.SetActive(true);
        if (timetext != null) timetext.gameObject.SetActive(true);
        if (hptext != null) hptext.gameObject.SetActive(true);

        // ゲーム開始
        Time.timeScale = 1f;
    }

    // ==== スコア関連 ====
    public void AddScore(int amont)
    {
       // ScoreManagerにスコアを送る
        ScoreManager.instance.AddScore(amont);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score:" + ScoreManager.instance.Score;
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI() ;

        if (ScoreManager.instance != null) ScoreManager.instance.ResetScore();
    }

    // PlayerHP UI更新
    public void UpdateHPUI()
    {
        if(hptext !=  null && player != null)
        {
            hptext.text = "HP:" + player.Health;
        }
    }

    // ==== 時間表示 ====
    private void UpdateTimeUI()
    {
        if(timetext != null)
        {
            timetext.text = "Time:" + timer.ToString("F1");
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("ゲームオーバー");
        FadeManager.instance.FadeToScene("EndScene");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "UnityProject0501")
        {
            InitGame();
        }
        else
        {
            if (scoreText != null) scoreText.gameObject.SetActive(false);
            if (timetext != null) timetext.gameObject.SetActive(false);
            if (readytext != null) readytext.gameObject.SetActive(false);
            if (gotext != null) gotext.gameObject.SetActive(false);
            if (hptext != null) hptext.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private IEnumerator DelayStart()
    {
        yield return null;
        StartCoroutine(StartGameRoutine());
    }
}
