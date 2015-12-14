using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trunk : MonoBehaviour {
    public GameObject trunkPiece;
    public int trunkPieces = 0;

    List<Rigidbody2D> pieceList;

    void Awake () {
        pieceList = new List<Rigidbody2D>();
        pieceList.Add(GetComponent<Rigidbody2D>());
    }
    
	void Start () {
	    for (var i = 0; i < trunkPieces; i++) {
            Rigidbody2D lastPiece = pieceList[pieceList.Count - 1];
            GameObject newPiece = (GameObject)Instantiate(trunkPiece, lastPiece.transform.position, lastPiece.transform.rotation);
            newPiece.transform.SetParent(transform);
            newPiece.transform.localScale = Vector3.one;

            Rigidbody2D rb = newPiece.GetComponent<Rigidbody2D>();
            FixedJoint2D hj = newPiece.GetComponent<FixedJoint2D>();

            hj.connectedBody = lastPiece;
            hj.connectedAnchor = new Vector2(0.035f, 0f);
            //hj.distance = 0f;
            pieceList.Add(rb);
        }
	}
	
	void Update () {
        pieceList[pieceList.Count - 1].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    //if (Input.GetMouseButton(0)) {
        //    Vector3 forwardPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    forwardPosition.z = 0;
        //    for (var i = pieceList.Count - 1; i >= 1; i--)
        //    {
        //        Vector3 direction = Vector3.ClampMagnitude(forwardPosition - pieceList[i].transform.position, 0.035f);
        //        pieceList[i].transform.position = pieceList[i].transform.position + direction;
        //        forwardPosition = pieceList[i].transform.position;
        //    }
        //}
	}
}
