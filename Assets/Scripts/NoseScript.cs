using UnityEngine;
using System.Collections;

public class NoseScript : MonoBehaviour {
    public HingeJoint2D noseTip;
    public float noseTipRadius = 0.2f;
    public float noseRotSpeed = 5f;
    public LayerMask whatIsClimbable;
    public SpriteRenderer noseGlow;

    Rigidbody2D rb;
    SpriteRenderer sr;

    Vector3 anchorPoint;
    Collider2D anchor;

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
        Vector3 noseTipPosition = transform.TransformPoint(noseTip.anchor);
        Collider2D overlap = Physics2D.OverlapCircle(noseTipPosition, noseTipRadius, whatIsClimbable);
        bool canClimb = overlap != null;
        if (canClimb) {
            noseGlow.transform.position = noseTipPosition;
            noseGlow.enabled = true;
        } else {
            noseGlow.enabled = false;
        }

        if (Input.GetMouseButton(0) && !isNosing && canClimb) {
            noseTip.enabled = true;
            anchorPoint = overlap.transform.InverseTransformPoint(noseTipPosition);
            anchor = overlap;
            isNosing = true;
        } else if (!Input.GetMouseButton(0) && isNosing) {
            noseTip.enabled = false;
            isNosing = false;
        }

        if (isNosing) {
            noseTip.connectedAnchor = anchor.transform.TransformPoint(anchorPoint);
        }
    }

    //void OnDrawGizmos() {
    //    Gizmos.DrawSphere(transform.TransformPoint(noseTip.anchor), noseTipRadius);
    //}
}
