using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman {

    public override bool[,] PossibleMove() {
        bool[,] r = new bool[8, 8];

        // UpLeft
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);

        // UpRight
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);

        // RightUp
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);

        // LeftUp
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);

        // BottomLeft
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);

        // BottomRight
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);

        // LeftBottom
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        // RightBottom
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);
        return r;
    }

    public void KnightMove(int x, int z, ref bool[,] r) {
        Chessman c;
        if (x >= 0 && x < 8 && z >= 0 && z < 8) {
            c = BoardManager.Instance.Chessmans[x, z];
            if(c == null){
                r[x, z] = true;
            }
            else if(isWhite != c.isWhite){
                r[x, z] = true;
            }
        }
    }

}
