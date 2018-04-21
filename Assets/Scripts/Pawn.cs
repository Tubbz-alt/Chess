using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman {

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Chessman c, c2;
        int[] e = BoardManager.Instance.EnPassantMove;

        // White team move
        if (isWhite)
        {
            // Left and Right are from the users perspective
            // Diagonal Left
            if (CurrentX != 0 && CurrentY != 7) {

                if(e[0] == CurrentX - 1 && e[1] == CurrentY + 1){
                    r[CurrentX - 1, CurrentY + 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];        // This also illustrates the usecase of setting up the Instance as the class variable for accessing that object's parameters(without the use of inspector) by using the class name. Remember this technique!
                if (c != null && !c.isWhite) {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }
            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 7) {

                if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite) {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Middle
            if(CurrentY != 7){
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                // The only way to move forward is if there is nobody in front of us
                if (c == null) {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }
            // Middle on first move
            if (CurrentY == 1) {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                // The only way to the special move is we are on the row first and there are two empty spaces ahead of us
                if (c == null && c2 == null) {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
        }
        else
        {
            // Left and Right are from the users perspective
            // Diagonal Left
            if (CurrentX != 0 && CurrentY != 0)
            {

                if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];        // This also illustrates the usecase of setting up the Instance as the class variable for accessing that object's parameters(without the use of inspector) by using the class name. Remember this technique!
                if (c != null && c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }
            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 0)
            {

                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                // The only way to move forward is if there is nobody in front of us
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }
            // Middle on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                // The only way to the special move is we are on the row first and there are two empty spaces ahead of us
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }

        }

        return r;
    }

}
