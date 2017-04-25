using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform tank;
    public float distance = 6.4f;
    public float height = 1.4f;
    public float rotationDamping = 3.0f;
    public float heightDamping = 2.0f;
    public float zoomRatio = 0.5f;
    public float defaultFOV = 60;

    private Vector3 rotationVector;
    void Start()
    {

    }
    void LateUpdate()
    {
		if (tank != null) 
		{
			float wantedAngle = rotationVector.y;
			float wantedHeight = tank.position.y + height;
			float myAngle = transform.eulerAngles.y;
			float myHeight = transform.position.y;
			myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime); //this will lerp the angle to required angle per frame
			Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0); //returns a quaternion with the euler angle of value myAngle
			myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime); //this will lerp the height per frame
			transform.position = tank.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			//next three lines will change the height of the camera which is lerping using Mathf.Lerp().
			//You cannot do this directly by manipulating transform.position.y
			Vector3 temp = transform.position;
			temp.y = myHeight;
			transform.position = temp;
			transform.LookAt(tank); //this line will look at the tank by manipulating the rotation values of the camera transform
		}
        
    }
    void FixedUpdate()
    {
		if (tank != null) 
		{
			Vector3 localVelocity = tank.InverseTransformDirection(tank.GetComponent<Rigidbody>().velocity);
			if (localVelocity.z < -0.5)
			{
				rotationVector.y = tank.eulerAngles.y + 180;
			}
			else
			{
				rotationVector.y = tank.eulerAngles.y;
			}
			float acceleration = tank.GetComponent<Rigidbody>().velocity.magnitude;
			Camera.main.fieldOfView = defaultFOV + acceleration * zoomRatio;
		}
        
    }
}