using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gin : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];

        if (isWhite)
        {
            if (CurrentY == -1)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        if (BoradManager.Instance.Chessmans[i, j] == null)
                            r[i, j] = true;

            }
            else
            {

                Chessman c;



                /*
                // Top Side
                i = CurrentX - 1;
                j = CurrentY + 1;
                if (CurrentY != 8)
                {

                    Debug.Log(CurrentX);
                    for (int k = 0; k < 3; k++)
                    {

                        if (i >= 0 || i < 9)
                        {
                            c = BoradManager.Instance.Chessmans[i, j];
                            if (c == null)
                                r[i, j] = true;
                            else if (isWhite != c.isWhite)
                                r[i, j] = true;
                        }

                        i++;
                    }
                }
                */

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

                /*
                // Down Side
                i = CurrentX - 1;
                j = CurrentY - 1;
                if (CurrentY != 0)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i >= 0 || i < 9)
                        {
                            c = BoradManager.Instance.Chessmans[i, j];
                            if (c == null)
                                r[i, j] = true;
                            else if (isWhite != c.isWhite)
                                r[i, j] = true;
                        }

                        i++;
                    }
                }
                */

                /*
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
                */
            }
        }

        else
        {
            if (CurrentY == -1)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        if (BoradManager.Instance.Chessmans[i, j] == null)
                            r[i, j] = true;

            }
            else
            {

                Chessman c;



                /*
                // Top Side
                i = CurrentX - 1;
                j = CurrentY + 1;
                if (CurrentY != 8)
                {

                    Debug.Log(CurrentX);
                    for (int k = 0; k < 3; k++)
                    {

                        if (i >= 0 || i < 9)
                        {
                            c = BoradManager.Instance.Chessmans[i, j];
                            if (c == null)
                                r[i, j] = true;
                            else if (isWhite != c.isWhite)
                                r[i, j] = true;
                        }

                        i++;
                    }
                }
                */

                // Teki Up Left
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
                if (CurrentY != 0)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                    if (c == null)
                        r[CurrentX, CurrentY - 1] = true;
                    else if (isWhite != c.isWhite)
                        r[CurrentX, CurrentY - 1] = true;
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
        }



        return r;
    }
}
