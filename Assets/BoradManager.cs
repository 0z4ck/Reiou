using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class BoradManager : MonoBehaviour
{
    private PhotonView photonView;
    public static BoradManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedChessman;

    public GameObject komadai;

    public GameObject komadaiteki;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;
    private int oldSelectionX = -1;
    private int oldSelectionY = -1;

    private int xHoji = -1;
    private int yHoji = -1;

    private int koma_id = -1;

    private float selectionXShousuu = -1;
    private float selectionYShousuu = -1;

    public List<GameObject> chessmanPrefabs;
    public GameObject textPrefab;
    public GameObject canvas;

    private List<GameObject> activeChessman;

    //private List<Chessman> whiteKomadai = new List<Chessman>();
    private Chessman[] whiteKomadai = new Chessman[28];

    //private List<Chessman> blackKomadai = new List<Chessman>();
    private Chessman[] blackKomadai = new Chessman[28];

    private List<GameObject> nariSentaku = new List<GameObject>();
   
    private Quaternion orientation = Quaternion.Euler(90, 0, 0);

    public bool isWhiteTurn = true;
    private bool isNariSelection=false;

    private GameObject[] maisuuTexts = new GameObject[28];

    private int[] maisuuInt = new int[28];


    [PunRPC]
    void ChatMessage(string a, string b)
    {
        Debug.Log(string.Format("ChatMessage {0} {1}", a, b));
    }

    private void Start()
    {
        Instance = this;
        SpawnAllChessmans ();
    }
    private void Update()

    //カーソルを動かしただけで実行される
    {
        UpdateSelection();
        DrawChessboard();


        if (Input.GetMouseButtonDown(0))

        //マウスクリックされた
        {
            

            
            if ((selectionX >= 0 && selectionY >= 0) || (selectionYShousuu < -0.3 && selectionYShousuu > -1.1) || (selectionYShousuu<10.1&&selectionYShousuu>9.3))
            //盤面内でクリックがあった 又は　自分の駒台でクリックがあった　又は　敵の駒台でクリックがあった
            {
                if (selectedChessman == null && !isNariSelection)
                {
                    //何も選択されていない状態でのクリックなのでその位置の駒を選択状態にする
                    //Select the chessman
                    SelectChessman(selectionXShousuu, selectionYShousuu, selectionX, selectionY);

                }
                else
                {
                    
                    
                    if (isNariSelection)
                    {
                        //dumpBoard(Chessmans);
                        //成り駒選択画面が表示されている状態でのクリック

                        if (selectionXShousuu > 4.5)
                        //盤面の右半分がクリックされた
                        //MoveChessMan関数の下部の指し手確定のコード
                        {
                            foreach (GameObject go in nariSentaku)
                                Destroy(go);
                            isNariSelection = false;
                            if (allowedMoves[xHoji, yHoji])
                            {

                                commitMove(xHoji, yHoji);
                                BoardHighlights.Instance.Hidehighlight();
                                selectedChessman = null;
                                /*
                                Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
                                selectedChessman.transform.position = GetTileCenter(xHoji, yHoji);
                                selectedChessman.SetPosition(xHoji, yHoji);
                                Chessmans[xHoji, yHoji] = selectedChessman;
                                isWhiteTurn = !isWhiteTurn;
                                */
                            }
                        }
                        else
                        //盤面の左半分がクリックされた
                        //activeChessmanから成る前の駒を削除し、成り駒のGameObjectを生成する
                        {
                            
                            foreach (GameObject go in nariSentaku)
                                Destroy(go);
                            isNariSelection = false;
                            if (allowedMoves[xHoji, yHoji])
                            {

                                /*
                                activeChessman.Remove(selectedChessman.gameObject);
                                Destroy(selectedChessman.gameObject);
                                SpawnChessman(koma_id, xHoji, yHoji);
                                commitMove関数内で移動元の座標としてselectedChessman.CurrentYなどを参照しているため
                                わざと移動前の座標に成りごまをスポーンする
                                */

                                SpawnChessman(koma_id, selectedChessman.CurrentX, selectedChessman.CurrentY);
                                Destroy(selectedChessman.gameObject);

                                selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                                
                                //dumpBoard(Chessmans);
                                commitMove(xHoji, yHoji);

                                BoardHighlights.Instance.Hidehighlight();
                                selectedChessman = null;
                                /*
                                Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
                                selectedChessman.transform.position = GetTileCenter(xHoji, yHoji);
                                selectedChessman.SetPosition(xHoji, yHoji);
                                Chessmans[xHoji, yHoji] = selectedChessman;
                                isWhiteTurn = !isWhiteTurn;
                                */
                            }
                        }
                    }
                    else
                    {
                        if (selectionYShousuu > 0 && selectionYShousuu < 9)
                        {
                            //駒が選択されていて移動先の升がクリックされた状態
                            // Move the chessman
                            MoveChessman(selectionX, selectionY);
                            PhotonView photonView = PhotonView.Get(this);
                            photonView.RPC("ChatMessage", RpcTarget.All, "jup", "and jup.");
                            //dumpBoard(Chessmans);
                        }
                    }
                }
            }
            oldSelectionX = selectionX;
            oldSelectionY = selectionY;
        }
    }

    private void SelectChessman(float xf, float yf, int x,int y)
    {
        //Debug.Log(yf);
        if((yf < -0.3 && yf > -1.1))
            //自分の駒台クリック
        {
            if (!isWhiteTurn) return;
            int rounded_x = Mathf.RoundToInt(xf);

            if (rounded_x >= 1 && rounded_x <= 8)
                koma_id = 8 - rounded_x;

            if (maisuuInt[koma_id] >= 1)
            {
                selectedChessman = whiteKomadai[koma_id] ;
                allowedMoves = selectedChessman.PossibleMove();
                BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
            }
            return;
        }
        if ((yf < 10.1 && yf > 9.3))
        //相手の駒台クリック
        {
            if (isWhiteTurn) return;
            int rounded_x = Mathf.RoundToInt(xf);

            if (rounded_x >= 1 && rounded_x <= 8)
                koma_id = 7 + rounded_x;

            if (maisuuInt[koma_id] >= 1)
            {
                selectedChessman = blackKomadai[koma_id];
                allowedMoves = selectedChessman.PossibleMove();
                BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
            }
            return;
        }
        //Chessmansは駒のGameObjectが格納された9x9の二次元配列
        //クリックされた座標に駒がないとき
        //クリックをなかったことにする
        if (Chessmans[x, y] == null)
            return;

        //手番じゃない方の駒がクリックされた時
        //クリックをなかったことにする
        if (Chessmans[x, y].isWhite != isWhiteTurn)
            return;
        
        //ここよくわからん
       
        bool hasAtleastOneMove = false;
        
        //

        allowedMoves = Chessmans[x, y].PossibleMove ();
        //ここも
        //その駒が動けないなら選択した状態にしない
        //クリックをなかったことにする
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (allowedMoves[i, j])
                    hasAtleastOneMove = true;
        if (!hasAtleastOneMove)
            return;
            
        //

        //選択中の駒にその位置の駒を代入
        selectedChessman = Chessmans[x, y];

        //動ける升をハイライトする
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        /*Debug.Log(selectedChessman.CurrentY);
        Debug.Log(y);*/
        if (allowedMoves[x, y])
        {
            if (selectedChessman.CurrentY != -1)
            {
                //ここまで

                //飛車の成りに関して
                if (selectedChessman.GetType() == typeof(hisya))
                {
                    if (isWhiteTurn && (y >= 6 || oldSelectionY >= 6))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[1], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[16], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 16;
                        xHoji = x;
                        yHoji = y;
                        return;
                    }
                    if (!isWhiteTurn && (y <= 2 || oldSelectionY <= 2))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[9], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[22], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 22;
                        xHoji = x;
                        yHoji = y;
                        return;
                    }
                }
                //角の成りに関して
                if (selectedChessman.GetType() == typeof(kaku))
                {
                    if (isWhiteTurn && (y >= 6 || oldSelectionY >= 6))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[2], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[17], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 17;
                        xHoji = x;
                        yHoji = y;
                        return;
                    }
                    if (!isWhiteTurn && (y <= 2 || oldSelectionY <= 2))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[10], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[23], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 23;
                        xHoji = x;
                        yHoji = y;
                        return;
                    }
                }
                //銀の成りに関して
                if (selectedChessman.GetType() == typeof(gin))
                {
                    if (isWhiteTurn && (y >= 6 || oldSelectionY >= 6))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[4], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[18], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 18;
                        xHoji = x;
                        yHoji = y;
                        return;
                    }
                    if (!isWhiteTurn && (y <= 2 || oldSelectionY <= 2))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[12], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[24], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 24;
                        xHoji = x;
                        yHoji = y;
                        return;
                    }
                }
                //歩の成りこみに関して
                if (selectedChessman.GetType() == typeof(hu))
                {
                    if (isWhiteTurn && (y == 6 || y == 7))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[7], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[21], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 21;
                        xHoji = x;
                        yHoji = y;
                        //dumpBoard(Chessmans);
                        return;

                    }
                    if (isWhiteTurn && y == 8)
                    {
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(21, selectedChessman.CurrentX, selectedChessman.CurrentY);
                        selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                    }
                    if (!isWhiteTurn && (y == 1 || y == 2))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[15], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[27], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 27;
                        xHoji = x;
                        yHoji = y;
                        return;

                    }
                    else if (!isWhiteTurn && y == 0)
                    {
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(27, selectedChessman.CurrentX, selectedChessman.CurrentY);
                        selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                    }
                }
                //ここまで

                //こっから香が9段目、1段目に行ったら自動的に成香に成るように
                if (selectedChessman.GetType() == typeof(kyou))
                {
                    if (isWhiteTurn && (y == 6 || y == 7))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[6], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[20], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 20;
                        xHoji = x;
                        yHoji = y;
                        return;

                    }


                    if (isWhiteTurn && y == 8)
                    {
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(20, selectedChessman.CurrentX, selectedChessman.CurrentY);
                        selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                    }

                    if (!isWhiteTurn && (y == 1 || y == 2))
                    {
                        GameObject go = Instantiate(chessmanPrefabs[14], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[26], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 26;
                        xHoji = x;
                        yHoji = y;
                        return;

                    }
                    else if (!isWhiteTurn && y == 0)
                    {
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(26, selectedChessman.CurrentX, selectedChessman.CurrentY);
                        selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                    }
                }
                //ここまで

                //こっから桂が8,9段目、1,2段目に行ったら自動的に成桂に成るように
                if (selectedChessman.GetType() == typeof(kei))
                {
                    if (isWhiteTurn && y == 6)
                    {
                        GameObject go = Instantiate(chessmanPrefabs[5], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[19], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 19;
                        xHoji = x;
                        yHoji = y;
                        return;

                    }
                    if (y == 8 || y == 7)
                    {
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(19, selectedChessman.CurrentX, selectedChessman.CurrentY);
                        selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                    }
                    //ダサいやり方で改善の余地あり

                    if (!isWhiteTurn && y == 2)
                    {
                        GameObject go = Instantiate(chessmanPrefabs[13], GetTileRightEdge(selectionX, selectionY), orientation) as GameObject;
                        GameObject go2 = Instantiate(chessmanPrefabs[25], GetTileLeftEdge(selectionX, selectionY), orientation) as GameObject;
                        go.transform.SetParent(transform);
                        go2.transform.SetParent(transform);
                        nariSentaku.Add(go);
                        nariSentaku.Add(go2);
                        isNariSelection = true;
                        koma_id = 25;
                        xHoji = x;
                        yHoji = y;
                        return;

                    }

                    else if (!isWhiteTurn && (y == 0 || y == 1))
                    {
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(25, selectedChessman.CurrentX, selectedChessman.CurrentY);
                        selectedChessman = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY];
                    }

                }
            }
            //ここまで
            commitMove(x, y);

        }
        BoardHighlights.Instance.Hidehighlight();
        selectedChessman = null;
    }
    private void commitMove(int x, int y)
    {
        //ここから駒を消すとこ
        Chessman c = Chessmans[x, y];
        
        if (c != null && c.isWhite != isWhiteTurn)
        {
            //Capture a piece

            //王様について
            //If it is the king
            if (c.GetType() == typeof(ou))
            {
                EndGame();
                return;
            }

            activeChessman.Remove(c.gameObject);
            Destroy(c.gameObject);
        }
        //移動元をnullにする

        if (selectedChessman.CurrentY != -1)
        {
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;

            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
        }
        else
        {
            maisuuInt[koma_id]--;

            if (maisuuInt[koma_id] != 0)
            {
                if (isWhiteTurn)
                {
                    GameObject motigoma = Instantiate(chessmanPrefabs[koma_id], selectedChessman.transform.position, orientation) as GameObject;
                    whiteKomadai[koma_id] = motigoma.GetComponent<Chessman>();
                    whiteKomadai[koma_id].SetPosition(-1, -1);
                }
                else
                {
                    GameObject motigoma = Instantiate(chessmanPrefabs[koma_id], selectedChessman.transform.position, orientation) as GameObject;
                    blackKomadai[koma_id] = motigoma.GetComponent<Chessman>();
                    blackKomadai[koma_id].SetPosition(-1, -1);
                }
            }
            if (maisuuInt[koma_id] <= 1)
            {
                Destroy(maisuuTexts[koma_id]);
            }
            else
            {
                maisuuTexts[koma_id].GetComponent<Text>().text = maisuuInt[koma_id].ToString();
            }
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
        }

        //成り選択画面の後Chessmans[x, y] がnullになっている ※解決済み
        //Debug.Log(Chessmans[x, y] != null);
        if (Chessmans[x, y] != null)
            {
            int koma_index;
                Chessman[] playerKomadai;
                GameObject komadaiObject;
                Vector3 komadaip;
                Vector3 maisuPos;


            if (isWhiteTurn)
            {
                koma_index = -1;
                if (Chessmans[x, y].GetType() == typeof(hisya)) koma_index = 1;
                if (Chessmans[x, y].GetType() == typeof(kaku)) koma_index = 2;
                if (Chessmans[x, y].GetType() == typeof(kin)) koma_index = 3;
                if (Chessmans[x, y].GetType() == typeof(gin)) koma_index = 4;
                if (Chessmans[x, y].GetType() == typeof(kei)) koma_index = 5;
                if (Chessmans[x, y].GetType() == typeof(kyou)) koma_index = 6;
                if (Chessmans[x, y].GetType() == typeof(hu)) koma_index = 7;
                if (Chessmans[x, y].GetType() == typeof(Ryuu)) koma_index = 1;
                if (Chessmans[x, y].GetType() == typeof(Uma)) koma_index = 2;
                if (Chessmans[x, y].GetType() == typeof(NariGin)) koma_index = 4;
                if (Chessmans[x, y].GetType() == typeof(NariKei)) koma_index = 5;
                if (Chessmans[x, y].GetType() == typeof(NariKyou)) koma_index = 6;
                if (Chessmans[x, y].GetType() == typeof(To)) koma_index = 7;
                playerKomadai = whiteKomadai;
                komadaiObject = komadai;
                
                komadaip = new Vector3(1.0f, 0f, -0.7f) + Vector3.right * (7 - koma_index);
                maisuPos = komadaip + new Vector3(0.2f, 0f, 0.5f);

            }
            else
            {
                koma_index = -1;
                if (Chessmans[x, y].GetType() == typeof(hisya)) koma_index = 9;
                if (Chessmans[x, y].GetType() == typeof(kaku)) koma_index = 10;
                if (Chessmans[x, y].GetType() == typeof(kin)) koma_index = 11;
                if (Chessmans[x, y].GetType() == typeof(gin)) koma_index = 12;
                if (Chessmans[x, y].GetType() == typeof(kei)) koma_index = 13;
                if (Chessmans[x, y].GetType() == typeof(kyou)) koma_index = 14;
                if (Chessmans[x, y].GetType() == typeof(hu)) koma_index = 15;
                if (Chessmans[x, y].GetType() == typeof(Ryuu)) koma_index = 9;
                if (Chessmans[x, y].GetType() == typeof(Uma)) koma_index = 10;
                if (Chessmans[x, y].GetType() == typeof(NariGin)) koma_index = 12;
                if (Chessmans[x, y].GetType() == typeof(NariKei)) koma_index = 13;
                if (Chessmans[x, y].GetType() == typeof(NariKyou)) koma_index = 14;
                if (Chessmans[x, y].GetType() == typeof(To)) koma_index = 15;
                playerKomadai = blackKomadai;
                komadaiObject = komadaiteki;
                komadaip = new Vector3(1.0f, 0f, 9.7f) + Vector3.right * (koma_index-8);
                maisuPos = komadaip - new Vector3(0.2f, 0f, 0.5f);
            }
            
                {
                        //playerKomadai[koma_index] = Chessmans[x, y];
                        maisuuInt[koma_index]++;

                        int maisuu = 0;

                        /*foreach (Chessman koma in playerKomadai)
                        {
                            if (koma.GetType() == Chessmans[x, y].GetType())
                                maisuu++;
                        }*/
                        maisuu = maisuuInt[koma_index];

                        if (maisuu == 1)
                        {
                            //Vector3 komadaip = komadaiObject.transform.position + Vector3.left * 1.2857f * koma_index * 0.5f + Vector3.right;
                            GameObject motigoma = Instantiate(chessmanPrefabs[koma_index], komadaip, orientation) as GameObject;
                    playerKomadai[koma_index] = motigoma.GetComponent<Chessman>();
                    playerKomadai[koma_index].SetPosition(-1, -1);
                        }
                        else if (maisuu == 2)
                        {
                            GameObject text2 = GameObject.Find("Text");


                    //Vector3 maisuPos = new Vector3(1.2f, 0f, -0.2f) + Vector3.right * (7 - koma_index);
                    //Vector3 maisuPos = new Vector3(6.2f, 0f, -0.2f) + Vector3.right * 1.857f * (koma_index - 7) * 5f;

                    Vector3 maisuSize = new Vector3(0.0015f, 0.0015f, 0.0015f);
                            GameObject text = Instantiate(textPrefab, maisuPos, text2.transform.rotation) as GameObject;
                            text.transform.localScale = maisuSize;
                            maisuuTexts[koma_index] = text;
                            text.transform.SetParent(canvas.transform);
                            Text texttext = text.GetComponent<Text>();
                            texttext.text = maisuu.ToString();
                            if (!isWhiteTurn) text.transform.Rotate(new Vector3(180,180,0));
                        }
                        else
                        {
                            maisuuTexts[koma_index].GetComponent<Text>().text = maisuu.ToString();
                        }

                    }
                 }

            

            Chessmans[x, y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
     
    }


    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        /*
        selectionXShousuu = hit.point.x;
        selectionYShousuu = hit.point.y;
        Debug.Log(selectionXShousuu);
        Debug.Log(selectionYShousuu);*/

        if (Physics.Raycast(Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            //Debug.Log(LayerMask.GetMask("ChessPlane"));
            //カーソルの位置（浮動小数点）のアップデート
            //少数点以下切り捨てでselectionX,Yはint型
            //Debug.Log(selectionXShousuu);
            //Debug.Log(selectionYShousuu);
            selectionX = (int)hit.point.x;
            selectionXShousuu = hit.point.x;
            selectionYShousuu = hit.point.z;
            selectionY = ((int)(hit.point.z+1))-1;
        }
        else
            {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessman(int index,int x,int y)
    {
        //GameObjectを生成して駒を出現させる
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        go.transform.SetParent(transform);

        //駒を9x9二次元配列Chessmansに挿入
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        //盤面上に存在する駒の配列に追加する
        activeChessman.Add(go);
    }

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[9, 9];

        //Spawn the zibun team!

        //ou
        SpawnChessman(0,4, 0);

        //hisya
        SpawnChessman(1,7, 1);

        //kaku
        SpawnChessman(2,1, 1);

        //kin
        SpawnChessman(3,3, 0);
        SpawnChessman(3,5, 0);

        //gin
        SpawnChessman(4,2, 0);
        SpawnChessman(4,6, 0);

        //kei
        SpawnChessman(5,1, 0);
        SpawnChessman(5,7, 0);

        //kyou
        SpawnChessman(6,0, 0);
        SpawnChessman(6,8, 0);

        //hu
        for (int i = 0; i < 9; i++)
            SpawnChessman(7,i, 2);

        //Spawn the teki team!

        //ou
        SpawnChessman(8,4, 8);

        //hisya
        SpawnChessman(9,1, 7);

        //kaku
        SpawnChessman(10,7, 7);

        //kin
        SpawnChessman(11,3, 8);
        SpawnChessman(11,5, 8);

        //gin
        SpawnChessman(12,2, 8);
        SpawnChessman(12,6, 8);

        //kei
        SpawnChessman(13,1, 8);
        SpawnChessman(13,7, 8);

        //kyou
        SpawnChessman(14,0, 8);
        SpawnChessman(14,8, 8);

        //hu
        for (int i = 0; i < 9; i++)
            SpawnChessman(15,i, 6);

    }

    private Vector3 GetTileCenter(int x,int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
    private Vector3 GetTileRightEdge(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x+1);
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
    private Vector3 GetTileLeftEdge(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x);
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 9;
        Vector3 heigthLine = Vector3.forward * 9;

        for (int i = 0; i <= 9; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 9; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heigthLine);
            }
        }

        // Draw the selection
        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(
               Vector3.forward * (selectionY + 1 )+ Vector3.right * selectionX,
               Vector3.forward * selectionY + Vector3.right * (selectionX + 1));

        }
    }

    private void dumpBoard(Chessman[,] Chessmans)
    {
        System.String msg = "";
        for (int j = 0; j < Chessmans.GetLength(1); j++)
        {
            msg += "\n";
            for (int i = 0; i < Chessmans.GetLength(0); i++)
            {
                msg +=  Chessmans[i,j]==null ? "(null)" : Chessmans[i, j].ToString();
                
            }
        }
        Debug.Log(msg);
    }

    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("あなたが勝ちました！！");
        else
            Debug.Log("相手の勝ちです…");

        foreach (GameObject go in activeChessman)
            Destroy(go);
        isWhiteTurn = true;
        BoardHighlights.Instance.Hidehighlight();
        SpawnAllChessmans();
    }
}

