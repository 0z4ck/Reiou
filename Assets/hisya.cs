using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hisya : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        Chessman c;
        int i;

        // Right
        if (CurrentY == -1)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (BoradManager.Instance.Chessmans[i, j] == null)
                        r[i, j] = true;

        }
        else
        {
            i = CurrentX;
            while (true)
            {
                i++;
                if (i >= 9)
                    break;

                c = BoradManager.Instance.Chessmans[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[i, CurrentY] = true;

                    break;
                }
            }

            // Left
            i = CurrentX;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                c = BoradManager.Instance.Chessmans[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[i, CurrentY] = true;

                    break;
                }
            }

            // Up
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

            // Down
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
