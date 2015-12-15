using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreeInside : MonoBehaviour {
    public GameObject treeInside;
    public GameObject mushroomLadder;
    public Transform player;
    public Text enterText;

    Collider2D trigger;

    void Awake()
    {
        trigger = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (trigger.OverlapPoint(player.position)) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!treeInside.activeSelf)
                {
                    treeInside.SetActive(true);
                    mushroomLadder.SetActive(false);
                }
                else
                {
                    treeInside.SetActive(false);
                    mushroomLadder.SetActive(true);
                }
            }            

            enterText.enabled = true;
            if (!treeInside.activeSelf)
            {
                enterText.text = "Press Space to Enter";
            } else
            {
                enterText.text = "Press Space to Exit";
            }
        } else
        {
            enterText.enabled = false;
        }
    }
}
