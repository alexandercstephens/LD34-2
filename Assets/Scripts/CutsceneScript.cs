﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour {
    public Text billyText;
    public Text playerText;
    public PlayerScript player;
    public Text steveText;

    void Start() {
        billyText.text = "His name is Billy.";
        Invoke("F0", 2f);
    }

    void F0() {
        billyText.text = "He has Pinocchio Syndrome.";
        Invoke("F01", 1.5f);
        Invoke("F1", 3f);
    }
    void F01() {
        playerText.text = "It's a real medical condition!";
        Invoke("F02", 2f);
    }
    void F02() {
        playerText.text = "";
    }

    void F1() {
        billyText.text = "Every time he lies to someone to whom he's never lied before, his nose grows longer.";
        Invoke("F2", 3f);
    }

    void F2() {
        billyText.text = "He's lived a pretty honest life so far.";
        Invoke("F3", 3f);
    }

    void F3() {
        billyText.color = Color.red;
        billyText.text = "But you control him now.";
        Invoke("F4", 3f);
    }
    
    void F4() {
        billyText.text = "";
        playerText.text = "Nice to meet you, Player1!";
        player.GrowNose();
        Invoke("F5", 3f);
    }

    void F5() {
        playerText.text = "";
    }

    //void F4() {
    //    billyText.text = "";
    //    steveText.text = "His name is Steve.";
    //    Invoke("F5", 3f);
    //}
    //
    //void F5() {
    //    steveText.text = "He is Billy's best friend.";
    //    Invoke("F6", 3f);
    //}
    //
    //void F6() {
    //    steveText.text = "His cat is stuck in a tree, and no one can reach her.";
    //    Invoke("F66", 3f);
    //}
    //
    //void F66() {
    //    playerText.text = "Nice to meet you, Player1!";
    //    player.GrowNose();
    //}

    // Update is called once per frame
    void Update () {
	
	}
}
