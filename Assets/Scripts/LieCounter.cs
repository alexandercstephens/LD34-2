using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LieCounter : MonoBehaviour {

    Text text;
    int liesLeft = 4;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	    if (liesLeft < 4)
        {
            text.text = "Lies Left: " + liesLeft;
        }
	}

    public void Lie ()
    {
        liesLeft -= 1;
        if (liesLeft == 0)
        {
            Invoke("LoadEnd", 5f);
        }
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
