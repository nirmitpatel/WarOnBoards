using UnityEngine;

public class TankController : MonoBehaviour
{
//    public WheelCollider wheelFR;
//    public WheelCollider wheelFL;
//    public WheelCollider wheelRR;
//    public WheelCollider wheelRL;

	public WheelCollider wheelFront;
	public WheelCollider wheelMiddle;
	public WheelCollider wheelRear;

	public Transform wheelFrontTrans;
	public Transform wheelMiddleTrans;
	public Transform wheelRearTrans;

//    public Transform wheelFLTrans;
//    public Transform wheelFRTrans;
//    public Transform wheelRLTrans;
//    public Transform wheelRRTrans;
    public float maxTorque = 50;

    public float lowestSteerAtSpeed = 50;
    public float lowSpeedSteerAngle = 10;
    public float highSpeedSteerAngle = 1;

    public float currentSpeed;
    public float topSpeed = 150;
    public float maxReverseSpeed = 50;
    public float decelerationSpeed = 30;

    private bool braked = false;
    public float maxBrakeTorque = 100;

    private float localSidewaysFriction;
    private float localForwardFriction;
    private float slipSidewaysFriction;
    private float slipForwardFriction;

    private Vector3 temp;
    private Rigidbody tankBody;
    void Start()
    {
//        tankBody = GetComponent<Rigidbody>();
//       	temp = tankBody.centerOfMass;
//        temp.y = -0.9f;
//       	temp.z = 0.5f;
//       	tankBody.centerOfMass = temp;
        //SetValues();
    }
    /*void SetValues()
    {
        localForwardFriction = wheelFR.forwardFriction.stiffness;
        localSidewaysFriction = wheelFR.sidewaysFriction.stiffness;
        slipForwardFriction = 0.04f;
        slipSidewaysFriction = 0.02f;
    }*/
    void FixedUpdate()
    {
        ControlTank();
        HandBrake();
    }
    void Update()
    {
//        wheelFLTrans.Rotate(-wheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
//        wheelFRTrans.Rotate(-wheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
//        wheelRLTrans.Rotate(-wheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
//        wheelRRTrans.Rotate(-wheelRR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

		wheelFrontTrans.Rotate(0, wheelFront.rpm / 60 * 360 * Time.deltaTime, 0);
		wheelMiddleTrans.Rotate(0, wheelMiddle.rpm / 60 * 360 * Time.deltaTime, 0);
		wheelRearTrans.Rotate(0, wheelRear.rpm / 60 * 360 * Time.deltaTime, 0);

//        temp = wheelFLTrans.localEulerAngles;
//        temp.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
//        wheelFLTrans.localEulerAngles = temp;
//
//        temp = wheelFRTrans.localEulerAngles;
//        temp.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;
//        wheelFRTrans.localEulerAngles = temp;

        WheelPosition();
    }
    void ControlTank()
    {
//        currentSpeed = Mathf.Round(2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000);
		currentSpeed = Mathf.Round(2 * Mathf.PI * wheelFront.radius * wheelFront.rpm * 60 / 1000);
        if (currentSpeed < topSpeed && currentSpeed > -maxReverseSpeed && !braked)
        {
//            wheelFR.motorTorque = maxTorque * Input.GetAxis("Vertical");
//            wheelFL.motorTorque = maxTorque * Input.GetAxis("Vertical");
			wheelFront.motorTorque = maxTorque * Input.GetAxis("Vertical");
			wheelMiddle.motorTorque = maxTorque * Input.GetAxis("Vertical");
			wheelRear.motorTorque = maxTorque * Input.GetAxis("Vertical");
        }
        else
        {
//            wheelFR.motorTorque = 0;
//            wheelFL.motorTorque = 0;
			wheelFront.motorTorque = 0;
			wheelMiddle.motorTorque = 0;
			wheelRear.motorTorque = 0;
        }
        if (Input.GetButton("Vertical") == false)
        {
//            wheelFR.brakeTorque = decelerationSpeed;
//            wheelFL.brakeTorque = decelerationSpeed;
			wheelFront.brakeTorque = decelerationSpeed;
			wheelMiddle.brakeTorque = decelerationSpeed;
			wheelRear.brakeTorque = decelerationSpeed;
        }
        else
        {
//            wheelFR.brakeTorque = 0;
//            wheelFL.brakeTorque = 0;
			wheelFront.brakeTorque = 0;
			wheelMiddle.brakeTorque = 0;
			wheelRear.brakeTorque = 0;
        }
        float speedFactor = GetComponent<Rigidbody>().velocity.magnitude / lowestSteerAtSpeed;
        float currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
        currentSteerAngle *= Input.GetAxis("Horizontal");
//        wheelFL.steerAngle = currentSteerAngle;
//        wheelFR.steerAngle = currentSteerAngle;
		wheelFront.steerAngle = currentSteerAngle;
    }

    void WheelPosition()
    {
        RaycastHit hit;
        Vector3 wheelPosition;
//        if (Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, out hit, wheelFL.radius + wheelFL.suspensionDistance))
//        {
//            wheelPosition = hit.point + wheelFL.transform.up * wheelFL.radius;
//        }
//        else
//        {
//            wheelPosition = wheelFL.transform.position - wheelFL.transform.up * wheelFL.suspensionDistance;
//        }
//        wheelFLTrans.position = wheelPosition;
//
//        if (Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, out hit, wheelFR.radius + wheelFR.suspensionDistance))
//        {
//            wheelPosition = hit.point + wheelFR.transform.up * wheelFR.radius;
//        }
//        else
//        {
//            wheelPosition = wheelFR.transform.position - wheelFR.transform.up * wheelFR.suspensionDistance;
//        }
//        wheelFRTrans.position = wheelPosition;

		if (Physics.Raycast(wheelFront.transform.position, -wheelFront.transform.up, out hit, wheelFront.radius + wheelFront.suspensionDistance))
		{
			wheelPosition = hit.point + wheelFront.transform.up * wheelFront.radius;
		}
		else
		{
			wheelPosition = wheelFront.transform.position - wheelFront.transform.up * wheelFront.suspensionDistance;
		}
		wheelFrontTrans.position = wheelPosition;

//        if (Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up, out hit, wheelRL.radius + wheelRL.suspensionDistance))
//        {
//            wheelPosition = hit.point + wheelRL.transform.up * wheelRL.radius;
//        }
//        else
//        {
//            wheelPosition = wheelRL.transform.position - wheelRL.transform.up * wheelRL.suspensionDistance;
//        }
//        wheelRLTrans.position = wheelPosition;
//
//        if (Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up, out hit, wheelRR.radius + wheelRR.suspensionDistance))
//        {
//            wheelPosition = hit.point + wheelRR.transform.up * wheelRR.radius;
//        }
//        else
//        {
//            wheelPosition = wheelRR.transform.position - wheelRR.transform.up * wheelRR.suspensionDistance;
//        }
//        wheelRRTrans.position = wheelPosition;
		// middle
//		if (Physics.Raycast(wheelMiddle.transform.position, -wheelMiddle.transform.up, out hit, wheelMiddle.radius + wheelMiddle.suspensionDistance))
//		{
//			wheelPosition = hit.point + wheelMiddle.transform.up * wheelMiddle.radius;
//		}
//		else
//		{
//			wheelPosition = wheelMiddle.transform.position - wheelMiddle.transform.up * wheelMiddle.suspensionDistance;
//		}
//		wheelMiddleTrans.position = wheelPosition;

		if (Physics.Raycast(wheelRear.transform.position, -wheelRear.transform.up, out hit, wheelRear.radius + wheelRear.suspensionDistance))
		{
			wheelPosition = hit.point + wheelRear.transform.up * wheelRear.radius;
		}
		else
		{
			wheelPosition = wheelRear.transform.position - wheelRear.transform.up * wheelRear.suspensionDistance;
		}
		wheelRearTrans.position = wheelPosition;
    }

    void HandBrake()
    {
        if (Input.GetButton("Jump"))
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
        if (braked)
        {
//            wheelFR.brakeTorque = maxBrakeTorque;
//            wheelFL.brakeTorque = maxBrakeTorque;
//            wheelFR.motorTorque = 0;
//            wheelFL.motorTorque = 0;
			wheelFront.brakeTorque = maxBrakeTorque;
			wheelFront.motorTorque = 0;
            /*if (GetComponent<Rigidbody>().velocity.magnitude > 1)
            {
                SetSlip(slipForwardFriction, slipSidewaysFriction);
            }
            else
            {
                SetSlip(1, 1);
            }*/
        }
        else
        {
//            wheelFR.brakeTorque = 0;
//            wheelFL.brakeTorque = 0;
			wheelFront.brakeTorque = 0;
            //SetSlip(localForwardFriction, localSidewaysFriction);
        }
    }
    /*void SetSlip(float currentForwardFriction, float currentSidewaysFriction)
    {
        WheelFrictionCurve tempo;
        tempo = wheelFR.forwardFriction;
        tempo.stiffness = currentForwardFriction;
        wheelFR.forwardFriction = tempo;

        tempo = wheelFL.forwardFriction;
        tempo.stiffness = currentForwardFriction;
        wheelFL.forwardFriction = tempo;

        tempo = wheelRR.forwardFriction;
        tempo.stiffness = currentForwardFriction;
        wheelRR.forwardFriction = tempo;

        tempo = wheelRL.forwardFriction;
        tempo.stiffness = currentForwardFriction;
        wheelRL.forwardFriction = tempo;
        
        tempo = wheelFR.sidewaysFriction;
        tempo.stiffness = currentSidewaysFriction;
        wheelFR.sidewaysFriction = tempo;

        tempo = wheelFL.sidewaysFriction;
        tempo.stiffness = currentSidewaysFriction;
        wheelFL.sidewaysFriction = tempo;
        
        tempo = wheelRR.sidewaysFriction;
        tempo.stiffness = currentSidewaysFriction;
        wheelRR.sidewaysFriction = tempo;

        tempo = wheelRL.sidewaysFriction;
        tempo.stiffness = currentSidewaysFriction;
        wheelRL.sidewaysFriction = tempo;
        
    }*/
}