using UnityEngine; // cao! :D

public class PlayerAvion : MonoBehaviour
{
    [Header("Control Target")]
    public Rigidbody2D AvionRB;
    public Transform AvionTrans;
    [Header("Throttle")]
    [Range(-85f, 100f)] public float throttle;
    [Range(-85, 100)] public int throttleZeroMarker;
    [Range(0f, 8f)] public float throttleIncrement;
    public float throttleMin;
    public float throttleMax;
    public bool hasThrottleControl;
    public float velocityFloat;
    [Range(0f, 15f)] public float maxSpeed;
    [Range(0f, 100f)] public float enginePower;
    [Header("Steering")]
    [Range(-1f, 1f)]public float steering;
    public float turningForce;
    [Range(1.0f, 2f)] public float turningForceDefault;
    [Range(0f, 1f)] public float turningRestraint;
    [Range(1f, 10f)] public float turningRestraintReduction;
    [Range(0f, 10f)] public float brakeTurningMultiplier;
    [Header("Brakes")]
    public float prevThrottle;
    public bool isBraking = false;
    public float brakeThrottle;

    void Start()
    {
        AvionRB = this.GetComponent<Rigidbody2D>();
        AvionTrans = transform;
        throttle = 0;
        turningForce = turningForceDefault;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            steering = 1;
        }

        if (Input.GetKey("d"))
        {
            steering = -1;
        }

        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            steering = 0;
        }


        if (Input.GetKey("w") && hasThrottleControl)
        {
            throttle += throttleIncrement;
            throttle = throttle > throttleMax ? throttleMax : throttle;
        }
        else if (Input.GetKey("w"))
        {
            prevThrottle += throttleIncrement;
            prevThrottle = prevThrottle > throttleMax ? throttleMax : prevThrottle;
        }

        if (Input.GetKey("s") && hasThrottleControl)
        {
            throttle -= throttleIncrement;
            throttle = throttle < throttleMin ? throttleMin : throttle;
        }
        else if (Input.GetKey("s"))
        {
            prevThrottle -= throttleIncrement;
            prevThrottle = prevThrottle < throttleMin ? throttleMin : prevThrottle;
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
            if (!isBraking)
            {
                prevThrottle = throttle;
                throttle = brakeThrottle;
                hasThrottleControl = false;
                turningForce *= brakeTurningMultiplier;

                isBraking = true;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (isBraking)
            {
                throttle = prevThrottle;
                hasThrottleControl = true;
                turningForce = turningForceDefault;

                isBraking = false;
            }
        }

        AvionRB.AddForce(throttle /18 * AvionTrans.right * enginePower, ForceMode2D.Force); // main enjin

        AvionRB.velocity = Vector2.ClampMagnitude(AvionRB.velocity, maxSpeed); // max speed

        AvionRB.AddTorque(turningForce * turningRestraint * 4 * steering); //steering

        velocityFloat = CalculateVelocityFloat(AvionRB.velocityX, AvionRB.velocityY); // velocity float

        turningRestraint = TurningRestraintCalculator(velocityFloat);
    }

    float TurningRestraintCalculator(float velocity)
    {
        return 1 - Mathf.InverseLerp(0, maxSpeed * turningRestraintReduction, velocity);
    }

    public float CalculateVelocityFloat(float VelocityX, float VelocityY)
    {
        return Mathf.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY);
    }

}
