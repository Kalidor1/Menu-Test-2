using System;
using System.Collections;
using Extensions;
using TMPro;
using UnityEditor;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [Header("Game States")]
    [SerializeField]
    public int points = 0;
    [SerializeField] private int level = 0;
    [SerializeField] private int maxLevel = 5;
    [SerializeField] private GameState gameState = GameState.Starting;
    [SerializeField] private bool isPaused;


    [Header("UI")][SerializeField] private GameObject gameUi;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI diedText;
    [SerializeField] private TextMeshProUGUI finishedText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private Image fadeImage;


    [Header("Prefabs and Stuff")]
    public GameObject[] levels;
    public GameState GameState => gameState;
    private GameState _prePauseState = GameState.Starting;

    [Header("Health UI")]
    public Health playerHealth;
    public Image[] healthImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Inventory")]
    public Inventory inventory;
    public bool atAltar;
    public TextMeshProUGUI inventoryText;

    [Header("Spawner")]
    public SpawnerType spawnerActive = SpawnerType.Enemy;

    [Header("Day/Night Cycle")]
    public float dayLength = 1f;
    public float nightLength = 1f;
    public TextMeshProUGUI dayNightText;
    public bool canEnter = false;
    public bool isInHouse = false;

    [Header("Public Stats")]
    public float playerSpeed = 5f;
    public float playerInvincibilityTime = 1f;
    public float attackSpeed = 0.5f;
    public float attackRange = 1f;
    public float pitchForkSize = 1f;
    public bool isBanana = false;

    [Header("What ever")]
    public int karma = 0;
    // Easter egg where you can sac yourself
    public int karmaEasterEgg = 1;
    public GameObject Player;
    public GameObject Spawn;
    public int sacrificed = 0;
    public bool canSacrifice = true;
    public int maxSacrificed = 3;

    private void Awake()
    {
        if (!ThisIsTheSingletonInstance())
        {
            return;
        }

        inventory = GetComponent<Inventory>();
        playerHealth = GetComponent<Health>();
    }

    private void Start()
    {
        AudioController.Instance.PlayDefaultMusic();

        gameState = GameState.Starting;

        UpdateUI();

        _prePauseState = gameState;
        SetPause(false);
        gameState = GameState.Playing;

        StartCoroutine(DayNightCycle());
    }

    IEnumerator DayNightCycle()
    {
        while (true)
        {
            // Remove all enemies during day
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }

            spawnerActive = SpawnerType.Item;
            canEnter = true;
            for (int i = 8; i <= 20; i++)
            {
                dayNightText.text = $"{GetTimeText(i)} (Day {Hub.Instance.daysSurvived})";
                yield return new WaitForSeconds(dayLength);
            }

            //move player to spawn
            if (isInHouse) Player.transform.position = Spawn.transform.position;
            canEnter = false;
            spawnerActive = SpawnerType.Enemy;
            for (int i = 21; i <= 31; i++)
            {
                dayNightText.text = $"{GetTimeText(i)} (Night {Hub.Instance.daysSurvived})";
                yield return new WaitForSeconds(nightLength);
            }

            Hub.Instance.daysSurvived++;
            sacrificed = 0;
            dayLength *= 0.9f;
            nightLength *= 1.1f;
        }
    }

    private string GetTimeText(int time)
    {
        var hour = time % 24;
        if (hour < 10)
        {
            return $"0{hour}:00";
        }
        else
        {
            return $"{hour}:00";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }

        if (isPaused)
        {
            return;
        }

        RenderPlayerState();



        canSacrifice = sacrificed < maxSacrificed;
    }

    private void RenderPlayerState()
    {
        if (playerHealth != null)
        {
            // Render health
            var health = playerHealth.CurrentHealth;
            var maxHealth = playerHealth.MaxHealth;

            for (int i = 0; i < healthImages.Length; i++)
            {
                if (i < health)
                {
                    healthImages[i].sprite = fullHeart;
                }
                else
                {
                    healthImages[i].sprite = emptyHeart;
                }

                if (i < maxHealth)
                {
                    healthImages[i].enabled = true;
                }
                else
                {
                    healthImages[i].enabled = false;
                }
            }
        }

        if (atAltar && inventoryText != null)
        {
            inventoryText.text = "";
            return;
        }

        if (inventory != null && inventoryText != null)
        {
            // Render inventory
            var items = inventory.items;
            var text = "";
            foreach (var item in items)
            {
                text += $"{item.Name}\n";
            }

            inventoryText.text = text;
        }

    }

    //  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ PUBLIC  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void PauseGame() => SetPause(true);
    public void ContinueGame() => SetPause(false);
    public void Test() => Debug.Log("Test");

    public void PlayerHealthReachedZero()
    {
        gameState = GameState.PlayerDied;
        SceneController.Instance.LoadScene("GameOver");
        UpdateUI();
    }

    public void Finished()
    {
        gameState = GameState.Finished;
        Debug.Log("Finished");
        UpdateUI();
    }

    //  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ PRIVATE  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void SetPause(bool paused)
    {
        if (paused)
        {
            _prePauseState = gameState;
            gameState = GameState.Paused;
        }
        else
        {
            gameState = _prePauseState;
        }

        isPaused = paused;

        if (gameUi != null)
        {
            gameUi.SetActive(!paused);
        }

        if (pauseUi != null)
        {
            pauseUi.SetActive(paused);
        }

        // Stopping time depends on your game! Turn-based games maybe don't need this
        Time.timeScale = paused ? 0 : 1;

        // Whatever else there is to do...
        // Deactivate other UI, etc.
    }

    private void ChangeLevel()
    {
        StartCoroutine(ChangeLevelCoroutine());
    }

    private IEnumerator ChangeLevelCoroutine()
    {
        gameState = GameState.Changing;

        if (level < maxLevel)
        {
            if (level != 0)
            {
                fadeImage.Fade(true);
            }

            level++;
            UpdateUI();


            yield return new WaitForSeconds(0.5f);

            if (level != 1)
            {
                fadeImage.Fade(false);
            }

            gameState = GameState.Playing;
            UpdateUI();
        }
        else
        {
            Finished();
        }
    }

    public void ApplyStats(InventoryItem item)
    {
        switch (item.Property)
        {
            case "playerSpeed":
                playerSpeed += item.Value;
                break;
            case "attackSpeed":
                attackSpeed += item.Value;
                break;
            case "attackRange":
                attackRange += item.Value;
                break;
            case "pitchForkSize":
                pitchForkSize += item.Value;
                break;
            case "banana":
                isBanana = true;
                break;
        }
    }

    public void UpdateUI()
    {

        switch (GameState)
        {
            case GameState.PlayerDied:
                diedText.gameObject.SetActive(true);
                finishedText.gameObject.SetActive(false);
                restartButton.SetActive(true);
                break;
            case GameState.Finished:
                diedText.gameObject.SetActive(false);
                finishedText.gameObject.SetActive(true);
                restartButton.SetActive(true);
                break;
            case GameState.Starting:
            case GameState.Playing:
            case GameState.Paused:
            case GameState.Changing:
            default:
                if (diedText?.gameObject != null) diedText.gameObject.SetActive(false);
                if (finishedText?.gameObject != null) finishedText.gameObject.SetActive(false);
                if (restartButton != null) restartButton.SetActive(false);
                break;
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(GameController))]
    public class GameControlTestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var gct = target as GameController;

            if (gct == null)
                return;

            if (!Application.isPlaying)
                return;

            if (GUILayout.Button("Restart"))
            {
                SceneController.Instance.RestartScene();
            }

            if (GUILayout.Button("Next Level"))
            {
                SceneController.Instance.LoadScene("PrototypeScene");
            }
        }
    }
#endif
}