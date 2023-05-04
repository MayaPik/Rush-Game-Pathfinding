using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance {get {return currentBalance;}}
   
   void Awake() {
    currentBalance = startingBalance;
   }
   public void Deposit(int amount) {

        currentBalance += Mathf.Abs(amount); //absoluate
   }
    public void Withdraw(int amount) {

        currentBalance -= Mathf.Abs(amount); //absoluate

        if (currentBalance < 0) {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
   }
}
