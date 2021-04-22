using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hu : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[9, 9];
        Chessman c;
        //Chessman c2;

        // zibun team move
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
                //Zibun Up
                if (CurrentY != 8)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                    if (c == null)
                        r[CurrentX, CurrentY + 1] = true;
                    else if (isWhite != c.isWhite)
                        r[CurrentX, CurrentY + 1] = true;
                }
                //Diagonal Left
                /*
                if (CurrentX != 0 && CurrentY != 8)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX - 1, CurrentY + 1] = true;
                }

                //Diagonal Right
                if (CurrentX != 8 && CurrentY != 8)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX + 1, CurrentY + 1] = true;
                }

                //Middle
                if(CurrentY != 8)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                    if (c == null)
                        r[CurrentX, CurrentY + 1] = true;
                }

                //改善の余地あり
                //前に駒が来た時に取れるように書かれてあるがわざわざ別に書く必要なさそう

                if (CurrentX != 8 && CurrentY != 8)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX, CurrentY + 1] = true;
                } 
                */


                //Middle on first move
                /*
                if (CurrentY == 2)
                    //歩が三段目にいるとき
                {
                    c = BoradManager.Instance.Chessmans [CurrentX, CurrentY + 1];
                    c2 = BoradManager.Instance.Chessmans [CurrentX, CurrentY + 2];
                    if (c == null & c2 == null)
                        r[CurrentX, CurrentY + 2] = true;
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
                // Teki Up
                if (CurrentY != 0)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                    if (c == null)
                        r[CurrentX, CurrentY - 1] = true;
                    else if (isWhite != c.isWhite)
                        r[CurrentX, CurrentY - 1] = true;
                }
                //Diagonal Left
                /*
                if (CurrentX != 0 && CurrentY != 0)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX - 1, CurrentY - 1] = true;
                }

                //Diagonal Right
                if (CurrentX != 8 && CurrentY != 0)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX + 1, CurrentY - 1] = true;
                }

                //Middle
                if (CurrentY != 0)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                    if (c == null)
                        r[CurrentX, CurrentY - 1] = true;
                }
                //改善の余地あり
                //なぜかこっちは駒が取れない件について


                if (CurrentX != 0 && CurrentY != 0)
                {
                    c = BoradManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX, CurrentY - 1] = true;
                }
                */


                //Middle on first move
                /*
                if (CurrentY == 6)
                    //歩が七段目にいるとき
                {
                    c = BoradManager.Instance.Chessmans [CurrentX, CurrentY - 1];
                    c2 = BoradManager.Instance.Chessmans [CurrentX, CurrentY - 2];
                    if (c == null & c2 == null)
                        r[CurrentX, CurrentY - 2] = true;
                }
                */
            }
        }

        return r;

    }
}
