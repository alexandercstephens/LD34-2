using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class npcScript : MonoBehaviour {
    public string playerDialogue;
    public string[] responseDialogues;
    public LayerMask playerLayer;
    public Text playerText;

    Collider2D col;
    Text text;
    bool hasBeenLiedTo = false;
    int responseIndex = 0;
    
	void Awake () {
        col = GetComponent<Collider2D>();
        text = GetComponentInChildren<Text>();
	}
	
	void Update () {
        if (!hasBeenLiedTo && col.IsTouchingLayers(playerLayer)) {
            hasBeenLiedTo = true;
            playerText.text = playerDialogue;
            InvokeRepeating("GiveThanks", 1.5f, 3f);
            Invoke("CancelPlayerText", 1.5f);
        }
	}

    void CancelPlayerText () {
        playerText.text = "";
    }

    void GiveThanks() {
        if (responseIndex >= responseDialogues.Length) {
            text.text = "";
            CancelInvoke();
        } else {
            text.text = responseDialogues[responseIndex];
            responseIndex += 1;
        }   
    }
}
