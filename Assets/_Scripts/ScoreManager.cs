using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int linesCounter = 0;
    public int comboLines = 0;
    public int levelCounter = 1;
    public int score = 0;

    public void IncreaseLevel()
    {
        levelCounter += 1;
    }

    public void IncreaseLine()
    {
        linesCounter += 1;
    }

    public void IncreaseCombo()
    {
        comboLines += 1;
    }

    public void ResetCombo()
    {
        comboLines = 0;
    }

    public void calcCombo()
    {
        switch(comboLines)
        {
            case 1:
                score += 40;
                break;
            case 2:
                score += 100;
                break;
            case 3:
                score += 300;
                break;
            case 4:
                score += 1200;
                break;
            default:
                break;
        }
    }
}
