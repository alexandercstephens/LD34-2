using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trunk : MonoBehaviour {
    public GameObject trunkPiece;
    public int trunkPieces = 0;

    List<GameObject> pieceList;

    void Awake () {
        pieceList = new List<GameObject>();
        pieceList.Add(this.gameObject);
    }
    
	void Start () {
	    for (var i = 0; i < trunkPieces; i++) {
            GameObject lastPiece = pieceList[pieceList.Count - 1];
            GameObject newPiece = (GameObject)Instantiate(trunkPiece, lastPiece.transform.position, lastPiece.transform.rotation);
            newPiece.transform.SetParent(transform);
            newPiece.transform.localScale = Vector3.one;

            Rigidbody2D rb = newPiece.GetComponent<Rigidbody2D>();
            FixedJoint2D hj = newPiece.GetComponent<FixedJoint2D>();
            Collider2D col = newPiece.GetComponent<Collider2D>();

            hj.connectedBody = lastPiece.GetComponent<Rigidbody2D>();
            hj.connectedAnchor = new Vector2(0.035f, 0f);
            for (var j = 0; j < pieceList.Count; j++) {
                Physics2D.IgnoreCollision(pieceList[j].GetComponent<Collider2D>(), col);
            }
            //hj.distance = 0f;
            pieceList.Add(newPiece);
        }
	}
	
	void Update () {
        
    }

    void FixedUpdate () {
        if (Input.GetMouseButton(0))
        {
            for (var i = 1; i < pieceList.Count; i++) {
                pieceList[i].GetComponent<PolygonCollider2D>().enabled = false;
                pieceList[i].GetComponent<FixedJoint2D>().enabled = false;
                pieceList[i].GetComponent<Rigidbody2D>().isKinematic = true;
            }

            Vector3 basePosition = pieceList[0].transform.position;
            Vector3 forwardPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            forwardPosition.z = 0;
            forwardPosition = basePosition + Vector3.ClampMagnitude(forwardPosition - basePosition, pieceList.Count * 0.035f);
            for (var i = 0; i < pieceList.Count; i++) {
                pieceList[i].transform.position = Vector3.Lerp(basePosition, forwardPosition, i / (float)pieceList.Count);
            }
        } else {
            for (var i = 1; i < pieceList.Count; i++) {
                pieceList[i].GetComponent<PolygonCollider2D>().enabled = true;
                pieceList[i].GetComponent<FixedJoint2D>().enabled = true;
                pieceList[i].GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
