using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] int scoreToUi;
    [SerializeField] TextMeshProUGUI text;
    Bank bank;

    void Start() {
        bank = FindObjectOfType<Bank>();
    }

    void Update() {
       scoreToUi = bank.CurrentBalance;
       text.SetText(scoreToUi.ToString());
    }
}
