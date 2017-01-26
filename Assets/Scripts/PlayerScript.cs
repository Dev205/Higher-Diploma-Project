using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour {
    //the script attached to the bandit or knight game object
    public Dropdown dropdownMenu;
    public Moves moves;
    public int? moveNumber;
    public HealthScript health;
    


    void Awake()
    {
        //gets the moves script attatched to the game object
        moves = GetComponentInChildren<Moves>();
        health = GetComponentInChildren<HealthScript>();
        
        
    }
    //attack method which uses the moves script
    public void attack(int attackingMove, int defendingMove)
    {
        Debug.Log("In playerscript attack");
        var damageDealt = moves.comparingMoves(attackingMove, defendingMove);
        if(health != null)
        {
            Debug.Log("Before damage dealt");
            health.takeDamage(damageDealt);
        }

    }
}
