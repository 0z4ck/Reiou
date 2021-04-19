using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kyou : Chessman
{
    //こっから飛車を参照
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        if (isWhite)
        {

            Chessman c;
            int i;
            // Zibun Up
            i = CurrentY;
            while (true)
            {
                i++;
                if (i >= 9)
                    break;

                c = BoradManager.Instance.Chessmans[CurrentX, i];
                if (c == null)
                    r[CurrentX, i] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[CurrentX, i] = true;

                    break;
                }
            }
        }
        else
        {
            Chessman c;
            int i;
            // Teki Up
            i = CurrentY;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                c = BoradManager.Instance.Chessmans[CurrentX, i];
                if (c == null)
                    r[CurrentX, i] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[CurrentX, i] = true;

                    break;
                }
            }
        }

        return r;
    }
}
