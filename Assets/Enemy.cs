using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    BoxCollider boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.inCombat = true;
            PlayerController.CombatStarted.Invoke(); 
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
