using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform character;

    [SerializeField]
    private float playerVelocity = 0.0f;
    

    [SerializeField]
    float speed = 6;

    [SerializeField]
    float characterX;

    [SerializeField]
    float characterY;

    [SerializeField]
    float movementThresholdX = 3;

    [SerializeField]
    float movementThresholdY = 2;

    private Vector3 movementToPosition;

    [SerializeField]
    public float smoothTime = 1.5f;
    
    //camera variables
    private float cameraSizeInput;
    private float cameraSizeChanger;
    public float smoothTimeCameraSize = 0.5f;
    private Vector3 cameraVelocity = Vector3.zero;

    private float getCharacterPositionRelativeToCamera(float characterPos, float cameraPos)
    {
        if (characterPos > cameraPos)
            return characterPos - cameraPos;
        else
            return cameraPos - characterPos;
    }

    void Update()
    {
        //CharacterController controller = GetComponent<CharacterController>();
        //Camera camera = GetComponent<Camera>();


        ////Clamp's the velocity value coming from the player's rigidbody betweeen the given values
        //cameraSizeInput = Mathf.Clamp(Mathf.Abs(controller.velocity.x), 5.0F, 10.0F);
        ////Using Mathf.SmoothDamp smooth the value from the current camera size to the target size 
        //cameraSizeChanger = Mathf.SmoothDamp(camera.orthographicSize, cameraSizeInput, ref playerVelocity, smoothTimeCameraSize);
        ////set the camera's size
        //camera.orthographicSize = cameraSizeChanger;

        characterY = getCharacterPositionRelativeToCamera(character.transform.position.y, transform.position.y);
        characterX = getCharacterPositionRelativeToCamera(character.transform.position.x, transform.position.x);


        if (characterX >= movementThresholdX || characterY >= movementThresholdY)
        {
            movementToPosition = character.transform.position;
            movementToPosition.z = -10; //static z position;

            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            //transform.position = Vector3.MoveTowards(transform.position, movementToPosition, speed * Time.deltaTime); //probably need to do movement based on character speed. For now this is a set speed
            //transform.position = SmoothApproach(transform.position, movementToPosition, 0.5f);
            var smoothMovementVector = Vector3.SmoothDamp(transform.position, movementToPosition, ref cameraVelocity, smoothTime);


            transform.position = smoothMovementVector;
            //cameraTransform.position = cameraPos;
        }
    }
}