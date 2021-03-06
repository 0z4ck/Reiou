using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaku : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        Chessman c;
        if (CurrentY == -1)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (BoradManager.Instance.Chessmans[i, j] == null)
                        r[i, j] = true;

        }
        else
        {
            int i, j;

        // Top Left
        i = CurrentX;
        j = CurrentY;
        
            while (true)
            {
                i--;
                j++;
                if (i < 0 || j >= 9)
                    break;
                c = BoradManager.Instance.Chessmans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;

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
                if (i >= 9 || j >= 9)
                    break;
                c = BoradManager.Instance.Chessmans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;

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
                    break;
                c = BoradManager.Instance.Chessmans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;

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
                if (i >= 9 || j < 0)
                    break;
                c = BoradManager.Instance.Chessmans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;

                    break;
                }
            }
        }

        return r;
    }
}
