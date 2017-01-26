using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Events;

//controls most of the functions in the game
public class GameController: MonoBehaviour
{
    public GameObject attackingPlayer;
    public GameObject defendingPlayer;
    public GameObject gameInfoCannvas;
    public GameObject gameOverCanvas;
    public Text winningText;

    PlayerScript[] attackingCharacters;
    PlayerScript[] defendingCharacters;

    public GameObject movesDropdownCanvas;
    Dropdown[] dropdownList;
    string DropdownName;

    int currentMove;
    int currentLane;
    int dropdownIndex;



    void Awake()
    {
        //lists of the players characers in games, gotten from the gameobjects 
        attackingCharacters = attackingPlayer.GetComponentsInChildren<PlayerScript>();
        defendingCharacters = defendingPlayer.GetComponentsInChildren<PlayerScript>();
        //list of the dropdown objects
        dropdownList = movesDropdownCanvas.GetComponentsInChildren<Dropdown>();
        //set the gameover panel to not active
        gameOverCanvas.gameObject.SetActive(false);
        foreach(var item in dropdownList)
        {
            //set all dropdowns off
            item.interactable = false;
        }
        //hide the dropdown buttons without disabling them as this was causing them to break for some reason 
        foreach(var item in dropdownList)
        {
            item.transform.localRotation = Quaternion.Euler(90, 0 , 0);
        }
        
    }

    void Start()
    {

    }
    public void startGame()
    {
        Debug.Log("In startGame");
        gameInfoCannvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        //reset the rotation of the dropdowns so they are visible
        foreach (var item in dropdownList)
        {
            item.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //turns on the first dropdown so that game can start
        dropdownList[0].interactable = true;
    }
    public void startRound()
    {

        //carries out the attack phase, to see who wins
        if (attackingCharacters[0].moveNumber != null && defendingCharacters[0].moveNumber != null)
        {
            attackPhase(0, 0);
        }else if(attackingCharacters[1].moveNumber != null && defendingCharacters[1].moveNumber != null)
        {
            attackPhase(1, 1);
        }else if(attackingCharacters[2].moveNumber != null && defendingCharacters[2].moveNumber != null)
        {
            attackPhase(2, 2);
        }
        checkForWin();

    }

    void attackPhase(int attackingCharacter, int defendingCharacter)
    {
        //compares the moves of the objects in the list, then sets them back to null for the next round
        Debug.Log("In attackphase before attacking method");
        attackingCharacters[attackingCharacter].attack(attackingCharacters[attackingCharacter].moveNumber.GetValueOrDefault(), defendingCharacters[defendingCharacter].moveNumber.GetValueOrDefault());
        defendingCharacters[defendingCharacter].attack(defendingCharacters[defendingCharacter].moveNumber.GetValueOrDefault(), attackingCharacters[attackingCharacter].moveNumber.GetValueOrDefault());
        attackingCharacters[attackingCharacter].moveNumber = null;
        defendingCharacters[defendingCharacter].moveNumber = null;
        //trying to get the moves to switch when there is a gap in the characters.
    }

    public void setDropdownActive(int index, string setting)
    {
        switch (setting.ToLower())
        {
            //sets a dropdown to interactable or not depending on the string
            case "hide":
                dropdownList[index].interactable = false;
                break;
            case "show":
                dropdownList[index].interactable = true;
                break;
        }
    }

    public void storeMove(int index)
    {
        currentMove = index;
        movePhase2(currentLane, dropdownIndex);
        
    }

    public void movePhase(int characterNumber, int dropdownIndex)
    {
        //stores the move selected from the dropdown in the object in the list.
        //then changes the active dropdown so the next person can select a move
        //attacking side
        //it checks if both characters are active, if one is not it will move oonto the next lane and check there
        //this is way too long. Will try to figure out a better way
        if(DropdownName == attackingCharacters[characterNumber].dropdownMenu.name)
        {
            attackingCharacters[characterNumber].moveNumber = currentMove;
            if (dropdownIndex == 0)
            {
                if (checkTurn(defendingCharacters, 0) && checkTurn(attackingCharacters, 0))
                {
                    changeSides(0, 3);
                }
                else if (checkTurn(defendingCharacters, 1) && checkTurn(attackingCharacters, 1))
                {
                    changeSides(0, 1);
                }
                else if (checkTurn(defendingCharacters, 2) && checkTurn(attackingCharacters, 2))
                {
                    changeSides(0, 2);
                }
                
            }else if (dropdownIndex == 1 )
            {
                if (checkTurn(defendingCharacters, 1)  && checkTurn(attackingCharacters, 1))
                {
                    changeSides(1, 4);
                }
                else if (checkTurn(defendingCharacters, 2) && checkTurn(attackingCharacters, 2))
                {
                    changeSides(1, 2);
                }
                else if (checkTurn(defendingCharacters, 0) && checkTurn(attackingCharacters, 0))
                {
                    changeSides(1, 0);
                }
                
            } else if (dropdownIndex == 2 )
            {
                if (checkTurn(defendingCharacters, 2) && checkTurn(attackingCharacters, 2))
                {
                    changeSides(2, 5);
                }
                else if (checkTurn(defendingCharacters, 0) && checkTurn(attackingCharacters, 0))
                {
                    changeSides(2, 0);
                }
                else if (checkTurn(defendingCharacters, 1) && checkTurn(attackingCharacters, 1))
                {
                    changeSides(2, 1);
                }
                
            }
        }else if(DropdownName == defendingCharacters[characterNumber].dropdownMenu.name)
        {//defending side
            defendingCharacters[characterNumber].moveNumber = currentMove;
            if(dropdownIndex == 3)
            {
                if (checkTurn(attackingCharacters, 1) && checkTurn(defendingCharacters, 1))
                {
                    changeSides(3, 1);
                }
                else if (checkTurn(attackingCharacters, 2) && checkTurn(defendingCharacters, 2))
                {
                    changeSides(3, 2);
                }
                else if (checkTurn(attackingCharacters, 0) && checkTurn(defendingCharacters, 0))
                {
                    changeSides(3, 0);
                }

                
            }else if(dropdownIndex == 4)
            {
                if (checkTurn(attackingCharacters, 2) && checkTurn(defendingCharacters, 2))
                {
                    changeSides(4, 2);
                }
                else if (checkTurn(attackingCharacters, 0) && checkTurn(defendingCharacters, 0))
                {
                    changeSides(4, 0);
                }
                else if (checkTurn(attackingCharacters, 1) && checkTurn(defendingCharacters, 1))
                {
                    changeSides(4, 1);
                }
                
            }else if (dropdownIndex == 5)
            {
                if(checkTurn(attackingCharacters, 0) && checkTurn(defendingCharacters, 0))
                {
                    changeSides(5, 0);
                }else if(checkTurn(attackingCharacters, 1) && checkTurn(defendingCharacters, 1))
                {
                    changeSides(5, 1);
                }else if(checkTurn(attackingCharacters, 2) && checkTurn(defendingCharacters, 2))
                {
                    changeSides(5, 2);
                }
            }
        }
        startRound();
    }

    void changeSides(int currentSide, int nextSide)
    {
        //switches side by changing the dropdown that is active
        setDropdownActive(currentSide, "hide");
        setDropdownActive(nextSide, "show");
    }

    public void storeDropdownName(string name)
    {
        //stores the dropdown name for use
        DropdownName = name;
        Debug.Log("Dropdown name is: " + DropdownName);
    }
    public void storeDropdownIndex(int index)
    {
        //stores the dropdown index for use
        dropdownIndex = index;
    }
    public void storeCurrentLane(int laneNumber)
    {
        //stores the current lane for use
        currentLane = laneNumber;
    }

    //checks to see if one side has no members with health left
    void checkForWin()
    {
        int attackersDefeated = 0;
        int defendersDefeated= 0;
        Debug.Log("In check for win");
        foreach (var item in defendingCharacters)
        {
            if (item.health.currentHealth == 0)
            {
                defendersDefeated += 1;
                item.gameObject.SetActive(false);
                
            }
        }
        //if there are 3 characters on a side with no health they lose
        if (defendersDefeated == 3|| defendersDefeated == 2 && attackersDefeated == 1)
        {
            Debug.Log("Bandits win");
            foreach (var item in dropdownList)
            {
                //set all dropdowns off
                item.interactable = false;
            }

            gameOverCanvas.SetActive(true);
            winningText.text = "Bandits Win";
        }

        foreach (var item in attackingCharacters)
        {
            if(item.health.currentHealth == 0)
            {
                attackersDefeated += 1;
                item.gameObject.SetActive(false);
            }
        }
        if (attackersDefeated == 3 || attackersDefeated == 2 && defendersDefeated == 1)
        {
            foreach (var item in dropdownList)
            {
                //set all dropdowns off
                item.interactable = false;
            }
            Debug.Log("Defenders win");
            gameOverCanvas.SetActive(true);
            winningText.text = "Knights Win";
        }
    }

    public void leaveGame()
    {
        Application.Quit();
    }

    public void restartGame()
    {
        //restarts the game be resetting health etc
        foreach(var item in attackingCharacters)
        {
            item.health.resetHealth();
            item.gameObject.SetActive(true);
            item.dropdownMenu.gameObject.SetActive(true);
        }
        foreach (var item in defendingCharacters)
        {
            item.health.resetHealth();
            item.gameObject.SetActive(true);
            item.dropdownMenu.gameObject.SetActive(true);

        }
        gameInfoCannvas.gameObject.SetActive(false);
        startGame();
    }
    void movePhase2(int characterNumber, int dropdownIndex)
    {
        //if both menus are active carry out turn
        if (attackingCharacters[characterNumber].dropdownMenu.IsActive() && defendingCharacters[characterNumber].dropdownMenu.IsActive())
        {
            movePhase(characterNumber, dropdownIndex);
        }
    }

    bool checkTurn(PlayerScript[]List, int lane)
    {
        //used to get true or false if a character is active
        if (List[lane].gameObject.activeSelf)
        {
            return true;
        }else
        {
            return false;
        }
    }
}




