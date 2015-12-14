using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class npcScript : MonoBehaviour {
    public string playerDialogue;
    public string[] responseDialogues;
    public float lieRadius = 1.2f;
    public Text playerText;
    public NoseScript nose;
    public Transform player;
    public Text spaceInstructions;
    public Text npctext;

    bool hasBeenLiedTo = false;
    int responseIndex = 0;
    bool isSpaceEnabled = false;
    
	void Awake () {
        spaceInstructions.enabled = false;
	}
	
	void Update () {
        if (!hasBeenLiedTo && (player.position - transform.position).sqrMagnitude < lieRadius * lieRadius) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasBeenLiedTo = true;
                playerText.text = playerDialogue;
                InvokeRepeating("GiveThanks", 1.5f, 3f);
                Invoke("CancelPlayerText", 1.5f);
                nose.GrowNose(15);
            }
            spaceInstructions.enabled = true;
            isSpaceEnabled = true;
        } else if (isSpaceEnabled)
        {
            spaceInstructions.enabled = false;
            isSpaceEnabled = false;
        }
	}

    void CancelPlayerText () {
        playerText.text = "";
    }

    void GiveThanks() {
        if (responseIndex >= responseDialogues.Length) {
            npctext.text = "";
            CancelInvoke();
        } else {
            npctext.text = responseDialogues[responseIndex];
            responseIndex += 1;
        }   
    }

    void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere(transform.position, lieRadius);
    }
}
