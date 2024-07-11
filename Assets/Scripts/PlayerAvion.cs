using UnityEngine; // cao! :D

public class PlayerAvion : MonoBehaviour
{
    [Header("Control Target")]
    public Rigidbody2D AvionRB;
    public Transform AvionTrans;
    [Header("Throttle")]
    public float throttleIncrement = 1.7f;
    [Range(-500f, 0f)] public float throttleMin = 0f;
    [Range(0f, 500f)] public float throttleMax = 100f;
    [Range(0f, 100f)] public float throttle = 0;
    public float velocityFloat = 0f;
    [Range(0f, 10f)] public float maxSpeed = 4;
    [Header("Steering")]
    [Range(1.0f, 2f)] public float turningForce = 1.5f;
    public float turningRestraint = 0;  
    [Range(1f, 10f)] public float TR_Multiplier = 3f;
    [Header("Brakes")]
    public float prevThrottle;
    public bool isBraking = false;
    public float brakeThrottle = -85;

    void Start()
    {
        AvionRB = this.GetComponent<Rigidbody2D>();
        AvionTrans = transform;
        throttle = 0;
    }

    void FixedUpdate()
    {

        Debug.Log(Time.time);

        if (Input.GetKey("a"))
        {
            AvionRB.AddTorque(turningForce * turningRestraint * 4);
        }

        if (Input.GetKey("d"))
        {
            AvionRB.AddTorque(turningForce * turningRestraint * -4);
        }

        turningRestraint = VelocityTurningRestraintCalculator();

        if (Input.GetKey("w"))
        {
            throttle += throttleIncrement;
            throttle = throttle > throttleMax ? throttleMax : throttle;
            //Debug.Log("w" + throttle);
        }

        if (Input.GetKey("s"))
        {
            throttle -= throttleIncrement;
            throttle = throttle < throttleMin ? throttleMin : throttle;
            //Debug.Log("s" + throttle);
        }

        if (Input.GetKeyDown("e"))
        {
            throttle = throttleMax;
        }

        if (Input.GetKeyDown("q"))
        {
            throttle = throttleMin;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(!isBraking)
            {
                prevThrottle = throttle;
                throttle = brakeThrottle;
                isBraking = true;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if(isBraking)
            {
                throttle = prevThrottle;
                isBraking = false;
            }
        }

        AvionRB.AddForce(throttle * AvionTrans.right / 18, ForceMode2D.Force);

        AvionRB.velocity = Vector2.ClampMagnitude(AvionRB.velocity, maxSpeed);

        velocityFloat = CalculateVelocityFloat(AvionRB.velocityX, AvionRB.velocityY);

    }

    float VelocityTurningRestraintCalculator()
    {
        float velocity = CalculateVelocityFloat(AvionRB.velocityX, AvionRB.velocityY);

        return 1 - Mathf.InverseLerp(0, maxSpeed * TR_Multiplier, velocity);
    }

    public float CalculateVelocityFloat(float VelocityX, float VelocityY)
    {
        return Mathf.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY);
    }

}
