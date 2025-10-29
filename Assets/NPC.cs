using System;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool inRange;

    private TextMeshProUGUI Interact; 
    private PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Interact = GameObject.Find("InteractText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        player = other.gameObject.GetComponent<PlayerController>();
        if (!player.inShop)
        {
            Interact.text = "Press E to Interact";    
        }
        else
        {
            Interact.text = "";
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Interact.text = "";
        
    }

   
}


