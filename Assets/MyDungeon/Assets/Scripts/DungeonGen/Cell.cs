using UnityEngine;
using System.Collections;

public class Cell
{
    public int xPos;
    public int yPos;
    public bool used;

    public void SetupCell(int column, int row)
    {
        xPos = column;
        yPos = row;
        used = false;
    }
}
