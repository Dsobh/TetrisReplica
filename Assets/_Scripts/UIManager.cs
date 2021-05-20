using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText, linesText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.SetText("0");
        linesText.SetText("0");
    }

}
