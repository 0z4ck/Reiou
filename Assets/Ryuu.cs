using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryuu : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        Chessman c;
        int i;

        // Right
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

        if (isWhite)
        {

            // Up Left
            if (CurrentY != 8)
            {
                if (CurrentX != 0)
                {
                    //
                    c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                    if (c == null)
                    {
                        r[CurrentX - 1, CurrentY + 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            // Up Right
            if (CurrentY != 8)
            {
                if (CurrentX != 8)

                {
                    c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                    if (c == null)
                    {
                        r[CurrentX + 1, CurrentY + 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Down Left
            if (CurrentY != 0)
            {
                if (CurrentX != 0)
                {
                    //
                    c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                    if (c == null)
                    {
                        r[CurrentX - 1, CurrentY - 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            // Down Right
            if (CurrentY != 0)
            {
                if (CurrentX != 8)

                {
                    c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                    if (c == null)
                    {
                        r[CurrentX + 1, CurrentY - 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

        }

        else
        {

            // Up Left
            if (CurrentY != 8)
            {
                if (CurrentX != 0)
                {
                    //
                    c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                    if (c == null)
                    {
                        r[CurrentX - 1, CurrentY + 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            // Up Right
            if (CurrentY != 8)
            {
                if (CurrentX != 8)

                {
                    c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                    if (c == null)
                    {
                        r[CurrentX + 1, CurrentY + 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Down Left
            if (CurrentY != 0)
            {
                if (CurrentX != 0)
                {
                    //
                    c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                    if (c == null)
                    {
                        r[CurrentX - 1, CurrentY - 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            // Down Right
            if (CurrentY != 0)
            {
                if (CurrentX != 8)

                {
                    c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                    if (c == null)
                    {
                        r[CurrentX + 1, CurrentY - 1] = true;
                    }
                    else if (isWhite != c.isWhite)
                        r[CurrentX + 1, CurrentY - 1] = true;
                }
            }
        }

        return r;
    }
}