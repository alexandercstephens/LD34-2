using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoseScript : MonoBehaviour {
    public HingeJoint2D noseTip;
    public float noseTipRadius = 0.2f;
    public float noseRotSpeed = 5f;
    public LayerMask whatIsClimbable;
    public SpriteRenderer noseGlow;
    public int initialTrunkSegments;
    public GameObject trunkBase;
    public GameObject trunkSegment;
    public GameObject trunkTip;

    Rigidbody2D rb;

    Vector3 anchorPoint;
    Collider2D anchor;
    float noseLength = 0f;

    List<GameObject> trunkSegments;

    public bool isNosing;
    public bool isExtended;

    void Awake() {
        rb = trunkBase.GetComponent<Rigidbody2D>();
        noseTip.enabled = false;
        noseGlow.enabled = false;
        trunkSegments = new List<GameObject>();
    }

    void Start() {
        GrowNose(initialTrunkSegments);
    }

    void FixedUpdate() {
        float realAngle = transform.rotation.eulerAngles.z;
        if (isNosing)
        {
            Vector2 diff = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            
            float angleDiff = (realAngle - angle) % 360;
            if (angleDiff < 180)
            {
                rb.angularVelocity = -noseRotSpeed * angleDiff * noseLength;
            }
            else
            {
                rb.angularVelocity = noseRotSpeed * (360 - angleDiff) * noseLength;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            isExtended = true;

            Vector2 basePos2d = new Vector2(trunkBase.transform.position.x, trunkBase.transform.position.y);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diff = Vector2.ClampMagnitude(mousePos - basePos2d, noseLength);
            Vector2 finalPos = basePos2d + diff;
            //Vector2 finalPos = mousePos;

            if (!isNosing) {
                Collider2D overlap = Physics2D.OverlapCircle(finalPos, noseTipRadius, whatIsClimbable);
                if (overlap != null)
                {
                    isNosing = true;
                    noseTip.anchor = new Vector2(diff.magnitude, 0f);
                    noseTip.enabled = true;
                    anchorPoint = overlap.transform.InverseTransformPoint(finalPos);
                    anchor = overlap;
                }
            }
            
            if (isNosing) {
                  noseTip.connectedAnchor = anchor.transform.TransformPoint(anchorPoint);
                  finalPos = noseTip.connectedAnchor;
                  diff = finalPos - basePos2d;
            }

            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            float per = diff.magnitude / (float)trunkSegments.Count;
            for (var i = 0; i < trunkSegments.Count; i++)
            {
                trunkSegments[i].GetComponent<FixedJoint2D>().enabled = false;
                trunkSegments[i].transform.localPosition = new Vector3(i * per, 0f, 0f);
                trunkSegments[i].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            trunkTip.GetComponent<FixedJoint2D>().enabled = false;
            trunkTip.transform.localPosition = new Vector3(trunkSegments.Count * per, 0f, 0f);
            trunkTip.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            //if (!isNosing) {
                trunkBase.transform.rotation = Quaternion.Euler(trunkBase.transform.eulerAngles.x, trunkBase.transform.eulerAngles.y, angle);
            //}       
        } else // if (isNosing)
        {
            //if (isNosing)
            //{
            //    //var k = 0;
            //    //for (; k < trunkSegments.Count; k++) {
            //    //    if (trunkSegments[k].GetComponent<CircleCollider2D>().IsTouchingLayers(whatIsClimbable))
            //    //    {
            //    //        break;
            //    //    }
            //    //}
            //    //if (k < trunkSegments.Count)
            //    //{
            //    //    var goodPosition = trunkSegments[k - 1].transform.position;
            //    //    for (; k < trunkSegments.Count; k++)
            //    //    {
            //    //        trunkSegments[k].transform.position = goodPosition;
            //    //    }
            //    //}
            //}
            if (isExtended)
            {

                for (var i = 0; i < trunkSegments.Count; i++)
                {
                    trunkSegments[i].transform.position = trunkBase.transform.position;
                }
                trunkTip.transform.position = trunkBase.transform.position;
            }
            isExtended = false;
            isNosing = false;
            noseTip.enabled = false;
            for (var i = 0; i < trunkSegments.Count; i++) {
                trunkSegments[i].GetComponent<FixedJoint2D>().enabled = true;
            }
            trunkTip.GetComponent<FixedJoint2D>().enabled = true;            
        }
    }

    public void GrowNose(int n) {
        Transform topSegment = trunkTip.GetComponent<FixedJoint2D>().connectedBody.transform;
        for (var i = 0; i < n; i++)
        {
            noseLength += 0.055f;

            GameObject newSegment = (GameObject)Instantiate(trunkSegment, topSegment.position, topSegment.rotation);
            newSegment.transform.parent = transform;
            
            FixedJoint2D j = newSegment.GetComponent<FixedJoint2D>();
            j.connectedBody = topSegment.GetComponent<Rigidbody2D>();
            j.connectedAnchor = new Vector2(0.055f, 0f);
            j.anchor = new Vector2(0f, 0f);
            
            topSegment = newSegment.transform;
            trunkSegments.Add(newSegment);
        }
        trunkTip.transform.position = topSegment.position;
        trunkTip.transform.localPosition += new Vector3(0.055f, -0f, 0f);
        
        FixedJoint2D ttj = trunkTip.GetComponent<FixedJoint2D>();
        ttj.connectedBody = topSegment.GetComponent<Rigidbody2D>();
        ttj.connectedAnchor = new Vector2(0.055f, 0f);
    }
}
