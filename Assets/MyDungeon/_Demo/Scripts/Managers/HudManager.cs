using UnityEngine;
using UnityEngine.UI;

namespace MyDungeon.Demo
{
    public class HudManager : MonoBehaviour
    {
        private const int MaxHealth = 1000;
        private const int BaseHealth = 50;
        public static HudManager Instance;
        private Text _floorText;
        private RectTransform _healthBar;
        private RectTransform _healthBarBackground;
        private RectTransform _healthBarForeground;
        private Text _healthText;
        private Text _levelText;
        private Text _messageLogText;
        private Text _xpText;
        public GameObject HudCanvas;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitUi();
        }

        public void InitUi()
        {
            HudCanvas = (GameObject) Instantiate(Resources.Load("hudCanvas"));

            Instance._messageLogText = GameObject.Find("Message Log").GetComponent<Text>();
            Instance._healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
            Instance._healthText = GameObject.Find("HealthText").GetComponent<Text>();
            Instance._healthBarForeground = GameObject.Find("HealthForeground").GetComponent<RectTransform>();
            Instance._healthBarBackground = GameObject.Find("HealthBackground").GetComponent<RectTransform>();
            Instance._floorText = GameObject.Find("FloorText").GetComponent<Text>();
            Instance._levelText = GameObject.Find("LevelText").GetComponent<Text>();
            Instance._xpText = GameObject.Find("ExperienceText").GetComponent<Text>();

            if (MenuManager.Instance.InMainMenu)
                HudCanvas.SetActive(false);
        }

        public void AddMessage(string message)
        {
            _messageLogText.text = message + "\n" + _messageLogText.text;
        }

        public void AddDebugMessage(string message)
        {
            _messageLogText.text = "<color='yellow'>DEBUG: " + message + "</color>" + "\n" + _messageLogText.text;
        }

        public void UpdateHealth(int curHealth, int maxHealth)
        {
            _healthText.text = string.Format("HP:{0,3}/{1,3}", curHealth, maxHealth);
            _healthBarForeground.sizeDelta =
                new Vector2(
                    curHealth / (float) maxHealth * Mathf.RoundToInt(
                        BaseHealth + _healthBar.rect.width * Mathf.Sin(maxHealth / (float) MaxHealth) / 2),
                    _healthBarForeground.sizeDelta.y);
            Instance._healthBarBackground.sizeDelta = new Vector2(
                Mathf.RoundToInt(BaseHealth + _healthBar.rect.width * Mathf.Sin(maxHealth / (float) MaxHealth) / 2),
                Instance._healthBarForeground.sizeDelta.y);
        }

        public void UpdateFloor(int floor)
        {
            _floorText.text = string.Format("{0,3}F", floor);
        }

        public void UpdateLevel(int level)
        {
            _levelText.text = string.Format("Lv{0,3}", level);
        }

        public void UpdateXp(int curXp, int nextXp)
        {
            _xpText.text = string.Format("{0,4}/{1,4}", curXp, nextXp);
        }
    }
}