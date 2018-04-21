using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman {
    public override bool[,] PossibleMove(){
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        // Right        
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
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

        // Top Left
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i--;
            j++;

            if (i < 0 || j >= 8)
            {
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

        // Top Right
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i++;
            j++;

            if (i >= 8 || j >= 8)
            {
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

            if (i < 0 || j < 0)
            {
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

            if (i >= 8 || j < 0)
            {
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
