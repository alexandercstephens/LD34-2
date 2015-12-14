using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    public Vector2 Smoothing, Margin;

    public BoxCollider2D Bounds;

    public bool IsFollowing { get; set; }

    private Vector3 _min, _max;

    int i = 1;

    public void Start()
    {
        IsFollowing = true;
        Smoothing = new Vector2(3, 3);
        Margin = new Vector2(1, 0.5f);
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
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

        var camera = GetComponent<Camera>();
        var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

        //todo: make the stupid bounding box work, it interferes with the player's movement currently
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, _min.y + camera.orthographicSize, _max.y - camera.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}