using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman {

    public override bool[,] PossibleMove() {
        bool[,] r = new bool[8, 8];
        Chessman c;
        // This is because we need to move in the diagonal directions
        int i, j;

        // Top Left
        i = CurrentX;
        j = CurrentY;

        while (true) {
            i--;
            j++;

            if (i < 0 || j >= 8) {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null){
                r[i, j] = true;
            }
            else {
                if (c.isWhite != isWhite) {             // which means it is an enemy and we can take over this piece
                    r[i, j] = true;
                }
                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i++;
            j++;

            if (i >= 8 || j >= 8){
                break;
            }

            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {             // which means it is an enemy and we can take over this piece
                    r[i, j] = true;
                }
                break;
            }
        }

        // Down Left
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i--;
            j--;

            if (i < 0 || j < 0){
                break;
            }

            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {             // which means it is an enemy and we can take over this piece
                    r[i, j] = true;
                }
                break;
            }
        }

        // Down Right
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i++;
            j--;

            if (i >= 8 || j < 0){
                break;
            }

            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (c.isWhite != isWhite)
                {             // which means it is an enemy and we can take over this piece
                    r[i, j] = true;
                }
                break;
            }
        }

        return r;   
    }

}
