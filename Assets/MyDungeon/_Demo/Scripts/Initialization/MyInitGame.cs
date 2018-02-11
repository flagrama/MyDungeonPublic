﻿using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class MyInitGame : InitGame
    {
        public GameObject LoadingCanvas;
        private GameObject _loadingImage;
        private UnityEngine.UI.Text _loadingText;

        // Use this for initialization
        protected override void Start()
        {
            Instantiate(LoadingCanvas);
            _loadingImage = GameObject.Find("LoadingImage");
            _loadingText = GameObject.Find("LoadingText").GetComponent<UnityEngine.UI.Text>();
            _loadingImage.SetActive(true);
            base.Start();
            MyDungeon.GameManager.Floor++;
            Invoke("UpdateFloor", LevelStartDelay);
            Invoke("HideLoadingImage", LevelStartDelay);
        }

        private void HideLoadingImage()
        {
            Destroy(_loadingImage);
        }

        public void GameOver()
        {
            _loadingText.text = "YOU DIED";
            enabled = false;
        }

        private void UpdateFloor()
        {
            GameObject.FindGameObjectWithTag("HudManager").GetComponent<FloorDisplay>().UpdateFloor(MyDungeon.GameManager.Floor);
        }
    }
}