using UnityEngine;

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
            GameManager.Floor++;
            this.Invoke(UpdateFloor, LevelStartDelay);
            this.Invoke(HideLoadingImage, LevelStartDelay);
        }

        private void HideLoadingImage()
        {
            Destroy(_loadingImage);
        }

        public void GameOver()
        {
            Instantiate(LoadingCanvas);
            _loadingImage = GameObject.Find("LoadingImage");
            _loadingText = GameObject.Find("LoadingText").GetComponent<UnityEngine.UI.Text>();
            _loadingText.text = "YOU DIED";
            _loadingImage.SetActive(true);
            enabled = false;
        }

        private void UpdateFloor()
        {
            GameObject.FindGameObjectWithTag("HudManager").GetComponent<FloorDisplay>().UpdateFloor(GameManager.Floor);
        }
    }
}
