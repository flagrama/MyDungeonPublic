using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
    
    public static HudManager instance = null;
    public GameObject hudCanvas;

    Text messageLogText;
    Text healthText;
    private RectTransform healthBar;
    private RectTransform healthBarForeground;
    private RectTransform healthBarBackground;
    private Text floorText;
    private Text levelText;
    private Text xpText;

    private const int MAX_HEALTH = 1000;
    private const int BASE_HEALTH = 50;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitUi();
    }

    public void InitUi()
    {
        hudCanvas = (GameObject)Instantiate(Resources.Load("hudCanvas"));

        instance.messageLogText = GameObject.Find("Message Log").GetComponent<Text>();
        instance.healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
        instance.healthText = GameObject.Find("HealthText").GetComponent<Text>();
        instance.healthBarForeground = GameObject.Find("HealthForeground").GetComponent<RectTransform>();
        instance.healthBarBackground = GameObject.Find("HealthBackground").GetComponent<RectTransform>();
        instance.floorText = GameObject.Find("FloorText").GetComponent<Text>();
        instance.levelText = GameObject.Find("LevelText").GetComponent<Text>();
        instance.xpText = GameObject.Find("ExperienceText").GetComponent<Text>();

        if (MenuManager.instance.inMainMenu)
            hudCanvas.SetActive(false);
    }

    public void AddMessage(string message)
    {
        messageLogText.text = message + "\n" + messageLogText.text;
    }

    public void AddDebugMessage(string message)
    {
        messageLogText.text = "<color='yellow'>DEBUG: " + message + "</color>" + "\n" + messageLogText.text;
    }

    public void UpdateHealth(int curHealth, int maxHealth)
    {
        healthText.text = String.Format("HP:{0,3}/{1,3}", curHealth, maxHealth);
        healthBarForeground.sizeDelta = new Vector2((float)curHealth / (float)maxHealth * (Mathf.RoundToInt(BASE_HEALTH + (healthBar.rect.width * Mathf.Sin(maxHealth / (float)MAX_HEALTH)) / 2)), healthBarForeground.sizeDelta.y);
        instance.healthBarBackground.sizeDelta = new Vector2(Mathf.RoundToInt(BASE_HEALTH + (healthBar.rect.width * Mathf.Sin(maxHealth / (float)MAX_HEALTH)) / 2), instance.healthBarForeground.sizeDelta.y);
    }

    public void UpdateFloor(int floor)
    {
        floorText.text = String.Format("{0,3}F", floor);
    }

    public void UpdateLevel(int level)
    {
        levelText.text = String.Format("Lv{0,3}", level);
    }

    public void UpdateXp(int curXp, int nextXp)
    {
        xpText.text = String.Format("{0,4}/{1,4}", curXp, nextXp);
    }
}
