using UnityEngine;
using System.Collections;

public class LevelDisplay : MonoBehaviour
{
    private UnityEngine.UI.Text _levelText;

    // Use this for initialization
    void Start()
    {
        _levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<UnityEngine.UI.Text>();
    }

    public void UpdateLevel(int level)
    {
        _levelText.text = string.Format("Lv{0,3}", level);
    }
}
