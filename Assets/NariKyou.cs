using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NariKyou : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        if (isWhite)
        {

            Chessman c;

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

            Chessman c;

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

            // Teki Up Left
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

            // Teki Up Right
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
