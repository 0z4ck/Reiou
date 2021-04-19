using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uma : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        Chessman c;
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

        if (isWhite)
        {

            //Up
            if (CurrentY != 8)
            {
                c = BoradManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX, CurrentY + 1] = true;
            }

            //Down
            if (CurrentY != 0)
            {
                c = BoradManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX, CurrentY - 1] = true;
            }

            // Middle Left
            if (CurrentX != 0)
            {
                c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY];
                if (c == null)
                    r[CurrentX - 1, CurrentY] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX - 1, CurrentY] = true;
            }

            // Middle Right
            if (CurrentX != 8)
            {
                c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                if (c == null)
                    r[CurrentX + 1, CurrentY] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX + 1, CurrentY] = true;
            }
        }

        else
        {

            //Teki Up
            if (CurrentY != 8)
            {
                c = BoradManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX, CurrentY + 1] = true;
            }

            //Teki Down
            if (CurrentY != 0)
            {
                c = BoradManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX, CurrentY - 1] = true;
            }

            // Middle Left
            if (CurrentX != 0)
            {
                c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY];
                if (c == null)
                    r[CurrentX - 1, CurrentY] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX - 1, CurrentY] = true;
            }

            // Middle Right
            if (CurrentX != 8)
            {
                c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                if (c == null)
                    r[CurrentX + 1, CurrentY] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX + 1, CurrentY] = true;
            }
        }

        return r;
    }
}
