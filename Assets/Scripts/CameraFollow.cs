using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    public Vector2 Smoothing, Margin;
    
    public bool IsFollowing { get; set; }

    public void Start()
    {
        IsFollowing = true;
        Smoothing = new Vector2(3, 3);
        Margin = new Vector2(1, 0.5f);
    }

    public void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            if (Mathf.Abs(x - Player.position.x) > Margin.x)
            {
                x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
            }

            if (Mathf.Abs(y - Player.position.y) > Margin.y)
            {
                y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
            }
        }

        transform.position = new Vector3(x, y, transform.position.z);
    }
}