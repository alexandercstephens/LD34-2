using UnityEngine;
using System.Collections;

public class NoseScript : MonoBehaviour {
    public HingeJoint2D noseTip;
    public Collider2D noseTipCollider;
    public float noseRotSpeed = 5f;
    public LayerMask whatIsClimbable;
    public SpriteRenderer noseGlow;

    Rigidbody2D rb;
    SpriteRenderer sr;

    public bool isNosing;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        noseTip.enabled = false;
        noseGlow.enabled = false;
    }

    void FixedUpdate() {
        float realAngle = transform.rotation.eulerAngles.z;
        //if (!isNosing)
        //{
            Vector2 diff = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            float angleDiff = (realAngle - angle) % 360;
            if (angleDiff < 180)
            {
                rb.angularVelocity = -noseRotSpeed * angleDiff;
            }
            else
            {
                rb.angularVelocity = noseRotSpeed * (360 - angleDiff);
            }
        //}        

        sr.flipY = realAngle > 90 && realAngle < 270;        
    }

    void Update()
    {
        bool canClimb = noseTipCollider.IsTouchingLayers(whatIsClimbable);
        if (canClimb) {
            noseGlow.transform.position = noseTipCollider.transform.position;
            noseGlow.enabled = true;
        } else {
            noseGlow.enabled = false;
        }

        if (Input.GetMouseButton(0) && !isNosing && canClimb) {
            noseTip.enabled = true;
            noseTip.connectedAnchor = noseTipCollider.transform.position;
            isNosing = true;
        } else if (!Input.GetMouseButton(0) && isNosing) {
            noseTip.enabled = false;
            isNosing = false;
        }
    }
}
