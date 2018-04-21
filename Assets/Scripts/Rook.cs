using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman {
    
    public override bool[,] PossibleMove(){
        bool[,] r = new bool[8,8];

        Chessman c;
        int i;

        // Right        
        i = CurrentX;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else {              // either its our piece or opponents one
                if (c.isWhite != isWhite) {
                    r[i, CurrentY] = true;
                }
                break;          // anyways we are going to do a break in both case as in the second case its blocking our way
            }
        }

        // Left     
        i = CurrentX;               // We are going to reset our i for obvious reasons
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {              // either its our piece or opponents one
                if (c.isWhite != isWhite)
                {
                    r[i, CurrentY] = true;
                }
                break;          // anyways we are going to do a break in both case as in the second case its blocking our way
            }
        }

        // Up     
        i = CurrentY;               // We are going to reset our i for obvious reasons
        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            } 
            else
            {              // either its our piece or opponents one
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;          // anyways we are going to do a break in both case as in the second case its blocking our way
            }
        }

        // Down     
        i = CurrentY;               // We are going to reset our i for obvious reasons
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {              // either its our piece or opponents one
                if (c.isWhite != isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;          // anyways we are going to do a break in both case as in the second case its blocking our way
            }
        }

        return r;
    }

}
