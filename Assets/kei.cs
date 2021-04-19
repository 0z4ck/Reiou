using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kei : Chessman
{
    public override bool[,] PossibleMove()
    {

        //こっから歩を参照

        bool[,] r = new bool[9, 9];

        // zibun team move
        if (isWhite)
        {
            //UpLeft
                KeiMove(CurrentX - 1, CurrentY + 2, ref r);
            //UpRight
                KeiMove(CurrentX + 1, CurrentY + 2, ref r);

        }
        else
        {

            //DownLeft
                KeiMove(CurrentX - 1, CurrentY - 2, ref r);
            //DownRight
                KeiMove(CurrentX + 1, CurrentY - 2, ref r);

            return r;
        }
        return r;
    }

    public void KeiMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            c = BoradManager.Instance.Chessmans[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }
}
