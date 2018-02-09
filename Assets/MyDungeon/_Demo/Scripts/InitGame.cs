using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class InitGame : MonoBehaviour
    {
        public float LevelStartDelay = 2f;
        public GameObject LoadingCanvas;
        private GameObject _loadingImage;
        private UnityEngine.UI.Text _loadingText;

        private void Start()
        {
            Instantiate(LoadingCanvas);
            _loadingImage = GameObject.Find("LoadingImage");
            _loadingText = GameObject.Find("LoadingText").GetComponent<UnityEngine.UI.Text>();
            _loadingImage.SetActive(true);
            GameManager.Instance.Floor++;
            Invoke("UpdateFloor", LevelStartDelay);
            Invoke("GenerateBoard", LevelStartDelay);
            Invoke("HideLoadingImage", LevelStartDelay);
        }

        private void GenerateBoard()
        {
            GetComponent<GridGenerator>().GenerateBoard();
        }

        private void HideLoadingImage()
        {
            Destroy(_loadingImage);
        }

        private void UpdateFloor()
        {
            Camera.main.GetComponent<FloorDisplay>().UpdateFloor(GameManager.Instance.Floor);
        }

        public void GameOver()
        {
            _loadingText.text = "YOU DIED";
            enabled = false;
        }
    }
}
