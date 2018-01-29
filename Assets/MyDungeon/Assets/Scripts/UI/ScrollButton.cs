using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour
{
    private string Name;
    private int Index;
    public Text ButtonText;
    public ScrollView ScrollView;

    public void SetNameAndIndex(string name, int i)
    {
        Index = i;
        Name = name;
        ButtonText.text = name;
    }

    public void Button_Click()
    {
        ScrollView.ButtonClicked(Name, Index);
    }
}
