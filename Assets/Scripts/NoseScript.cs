using UnityEngine;
using System.Collections;

public class NoseScript : MonoBehaviour {
    public HingeJoint2D noseTip;
    public float noseTipRadius = 0.2f;
    public float noseRotSpeed = 5f;
    public LayerMask whatIsClimbable;
    public SpriteRenderer noseGlow;
    public int initialTrunkSegments;
    public Transform trunkSegmentBase;
    public GameObject trunkSegment;
    public GameObject trunkTip;

    Rigidbody2D rb;

    Vector3 anchorPoint;
    Collider2D anchor;

    public bool isNosing;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        noseTip.enabled = false;
        noseGlow.enabled = false;
    }

    void Start() {
        Transform topSegment = trunkSegmentBase;
        for (var i = 0; i < initialTrunkSegments; i++) {
            noseTip.anchor += new Vector2(0.055f, 0f);

            GameObject newSegment = (GameObject)Instantiate(trunkSegment, topSegment.position, topSegment.rotation);
            newSegment.transform.parent = topSegment;
            newSegment.transform.localPosition += new Vector3(0.0003f, -0.0534f, 0f);
            topSegment = newSegment.transform;
        }
        GameObject tip = (GameObject)Instantiate(trunkTip, topSegment.position, topSegment.rotation);
        tip.transform.parent = topSegment;
        tip.transform.localPosition += new Vector3(-0.00259996f, -0.05260085f, 0f);
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
    }

    void Update()
    {
        Vector3 noseTipPosition = transform.TransformPoint(noseTip.anchor);
        Collider2D overlap = Physics2D.OverlapCircle(noseTipPosition, noseTipRadius, whatIsClimbable);
        bool canClimb = overlap != null;

        if (Input.GetMouseButton(0) && !isNosing && canClimb) {
            noseTip.enabled = true;
            anchorPoint = overlap.transform.InverseTransformPoint(noseTipPosition);
            anchor = overlap;
            isNosing = true;
        } else if (!Input.GetMouseButton(0) && isNosing) {
            noseTip.enabled = false;
            isNosing = false;
        }
        if (canClimb) {
            noseGlow.transform.position = noseTipPosition;
            if (isNosing) {
                noseGlow.color = Color.white;
            } else {
                noseGlow.color = Color.green;
            }
            noseGlow.enabled = true;
        } else {
            noseGlow.enabled = false;
        }

        if (isNosing) {
            noseTip.connectedAnchor = anchor.transform.TransformPoint(anchorPoint);
        }
    }

    //void OnDrawGizmos() {
    //    Gizmos.DrawSphere(transform.TransformPoint(noseTip.anchor), noseTipRadius);
    //}
}
