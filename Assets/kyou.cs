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
            if (CurrentY == -1)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 8; j++)
                        if (BoradManager.Instance.Chessmans[i, j] == null)
                            r[i, j] = true;

            }
            else
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
        }
        else
        {
            if (CurrentY == -1)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 1; j < 9; j++)
                        if (BoradManager.Instance.Chessmans[i, j] == null)
                            r[i, j] = true;

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
        }

        return r;
    }
}
