using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform character;

    [SerializeField]
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    float speed = 6;

    [SerializeField]
    float characterX;

    [SerializeField]
    float characterY;

    [SerializeField]
    float movementThreshold = 3;
    
    private Vector3 movementToPosition;

    [SerializeField]
    public float smoothTime = 0.3f;

    private float getCharacterPositionRelativeToCamera(float characterPos, float cameraPos)
    {
        if (characterPos > cameraPos)
            return characterPos - cameraPos;
        else
            return cameraPos - characterPos;
    }

    void Update()
    {
        characterY = getCharacterPositionRelativeToCamera(character.transform.position.y, transform.position.y);
        characterX = getCharacterPositionRelativeToCamera(character.transform.position.x, transform.position.x);


        if (characterX >= movementThreshold || characterY >= movementThreshold)
        {
            movementToPosition = character.transform.position;
            movementToPosition.z = -10; //static z position;

            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            //transform.position = Vector3.MoveTowards(transform.position, movementToPosition, speed * Time.deltaTime); //probably need to do movement based on character speed. For now this is a set speed
            //transform.position = SmoothApproach(transform.position, movementToPosition, 0.5f);
            var smoothMovementVector = Vector3.SmoothDamp(transform.position, movementToPosition, ref velocity, smoothTime);


            transform.position = smoothMovementVector;
            //cameraTransform.position = cameraPos;
        }
    }
    //public float interpVelocity;
    //public float minDistance;
    //public float followDistance;
    //public GameObject target;
    //public Vector3 offset;
    //Vector3 targetPos;

    //[SerializeField] private Vector3 velocity = Vector3.zero;
    //public Transform character;

    //public float dampTime = 0.15f;
    
    
    //Camera playerCamera;
    //int i = 0;
    ////public Transform toFollow;
    ////Vector3 diff;
    //void Awake()
    //{
    //    playerCamera = GetComponent<Camera>();
    //    // diff = transform.position - toFollow.position;
    //}

    //void Start()
    //{

    //}

    //void Update()
    //{
    //    if (character)
    //    {
            
    //        var edgeBoundary = playerCamera.ScreenToWorldPoint(new Vector3(Screen.width * .8f, Screen.height * .5f, character.position.z));

    //        if (i % 60 == 0) {
    //            Debug.Log("Boundary box size");
    //            Debug.Log(edgeBoundary);
    //            Debug.Log("Target x");
    //            Debug.Log(character.transform.position.x);
    //            Debug.Log("Target y");
    //            Debug.Log(character.transform.position.y);
    //            Debug.Log("Target x");
    //            Debug.Log(Mathf.Abs(character.transform.position.x));
    //            Debug.Log("--------------------------");
    //            i = 1;
    //        }
            

    //        if (Mathf.Abs(character.transform.position.x) > edgeBoundary.x / 2)
    //        {
    //            Debug.Log("******HAPPENED******");
    //            Vector3 delta = character.position - playerCamera.ViewportToWorldPoint(new Vector3(transform.position.x - edgeBoundary.x, transform.position.y - edgeBoundary.y, edgeBoundary.z));
    //            Vector3 destination = transform.position + delta;
    //            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    //        }
    //        i++;
    //        //Vector3 point = playerCamera.WorldToViewportPoint(target.position);
    //        //Vector3 delta = target.position - playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
    //        //Vector3 destination = transform.position + delta;
    //        //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    //    }
    //}
}
