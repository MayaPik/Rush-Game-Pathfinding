using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] float buildDelay = 0.5f;

    // void Start() {
    //     StartCoroutine(Build());
    // }
    public bool CreateTower(Tower tower, Vector3 position) 
    {

        Bank bank = FindObjectOfType<Bank>();
        if (bank && bank.CurrentBalance >= cost) {
                    position.y = 0;

            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        } else {
            return false;
            }
        }

    IEnumerator Build() {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);   
            foreach (Transform gradnchild in child)
            {
                gradnchild.gameObject.SetActive(false);   
            }
        }
      
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);   
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform gradnchild in child)
            {
                gradnchild.gameObject.SetActive(true);   
            }
        }
    }
}
