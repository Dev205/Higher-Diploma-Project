using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moves : MonoBehaviour {
    //a list of ints which refer to the choices in the dropdown menu
    public List<int> playerMoves;
    List<int> winList;
    List<int> loseList;
    List<int> drawList;

    void Start()
    {
        Debug.Log("In moves, start()");
        winList = new List<int>();
        playerMoves = new List<int>();
        drawList = new List<int>();
        loseList = new List<int>();
        for (int i = 1; i < 13; i++)
        {
            playerMoves.Add(i);
        }
        Debug.Log("PlayerMOves list length is: " + playerMoves.Count);

    }

    public int comparingMoves(int attackingMove, int defendingMove)
    {
        Debug.Log("In moves, comparing moves");
        int damage = 0;
        if(winList != null)
        {
            winList.Clear();
            drawList.Clear();
            loseList.Clear();
        }
        Debug.Log("Before switch, Attacking move is: " + attackingMove + "Defending move is " + defendingMove);
        switch (attackingMove)
        {
            case 1:
            case 2:
            case 3:
                //creates 3 lists with numbers in them to compare the incoming move numbers and gives the relevant damage
                drawList.AddRange(playerMoves.GetRange(0, 4));
                loseList.AddRange(playerMoves.GetRange(4, 4));
                winList.AddRange(playerMoves.GetRange(8, 4));
                Debug.Log("In case 3 switch");
                if (drawList.Contains(defendingMove))
                {
                    Debug.Log("Switch 1 draw");
                    damage = 0;
                }else if (loseList.Contains(defendingMove))
                {
                    Debug.Log("Switch 1 lose");
                    damage = 34;
                }else if (winList.Contains(defendingMove))
                {
                    Debug.Log("Switch 1 win");
                    damage = 0;
                }
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                drawList.AddRange(playerMoves.GetRange(4, 4));
                loseList.AddRange(playerMoves.GetRange(8, 4));
                winList.AddRange(playerMoves.GetRange(0, 4));
                if (drawList.Contains(defendingMove))
                {
                    damage = 0;
                }
                else if (loseList.Contains(defendingMove))
                {
                    damage = 34;
                }
                else if (winList.Contains(defendingMove))
                {
                    damage = 0;
                }
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                drawList.AddRange(playerMoves.GetRange(8, 4));
                loseList.AddRange(playerMoves.GetRange(0, 4));
                winList.AddRange(playerMoves.GetRange(4, 4));
                if (drawList.Contains(defendingMove))
                {
                    damage = 0;
                }
                else if (loseList.Contains(defendingMove))
                {
                    damage = 34;
                }
                else if (winList.Contains(defendingMove))
                {
                    damage = 0;
                }
                break;
        }
        //return damage to know is total health has been changed
        return damage;

    }
}
