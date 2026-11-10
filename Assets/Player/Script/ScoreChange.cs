using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreChange : MonoBehaviour
{

    [SerializeField] private TextMeshPro ScoreStr;

    private int ScoreInt = 0;
    private float LastVelocity = 0;
    private Rigidbody2D PlayerRB;

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        ScoreStr.text = "0";
    }

    void Update()
    {

        if (LastVelocity > 0 && PlayerRB.linearVelocity.x < 0 || LastVelocity < 0 && PlayerRB.linearVelocity.x > 0)
        {
            ScoreInt++;
            ScoreStr.text = "" + ScoreInt;
        }

        LastVelocity = PlayerRB.linearVelocity.x;
    }
}
