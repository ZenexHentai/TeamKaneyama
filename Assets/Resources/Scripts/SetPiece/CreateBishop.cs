﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateBishop : MonoBehaviour {

    [SerializeField]
    GameObject bishopPieces;

    [SerializeField]
    int bishopPieceNum;

    [SerializeField]
    Text bishoptext;

    GameObject select_piece = null;
    // Use this for initialization
    void Start()
    {
        CreatePieces(6.25f, -2.0f);
        bishoptext.text = "×" + bishopPieceNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (bishopPieceNum != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
                {
                    if (hit.collider.gameObject.name == "Setbishop(Clone)")
                        select_piece = Instantiate(hit.collider.gameObject);
                }
            }
            if (select_piece)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 pos = Input.mousePosition;
                    pos.z = 0.8f;
                    select_piece.transform.position = Camera.main.ScreenToWorldPoint(pos);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
                    {
                        SaveInfomation buff = hit.collider.gameObject.GetComponent<SaveInfomation>();
                        if (buff.piece == null)
                        {
                            buff.piece = select_piece;
                            select_piece.transform.position = hit.collider.gameObject.transform.position + new Vector3(0, 0, -0.1f);
                            select_piece.layer = LayerMask.NameToLayer("Default");
                            //hit.collider.gameObject.GetComponent<SellDate>().on_pise = select_piece;
                            bishopPieceNum -= 1;
                            bishoptext.text = "×" + bishopPieceNum.ToString();
                        }
                        else
                        {
                            Destroy(select_piece);
                        }
                    }
                    else
                    {
                        Destroy(select_piece);
                    }
                    select_piece = null;
                }
            }
        }


    }

    public void CreatePieces(float x, float y)
    {
        GameObject obj = Instantiate(bishopPieces);
        Vector3 piecePos = new Vector3(x, y, 4.9f);
        obj.transform.position = piecePos;
    }
}
