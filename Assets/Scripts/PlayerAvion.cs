using UnityEngine; // cao! :D

public class PlayerAvion : MonoBehaviour
{
    [Header("Control Target")]
    public Rigidbody2D AvionRB;
    public Transform AvionTrans;
    [Header("Throttle")]
    [Range(0f, 8f)] public float throttleIncrement;
    public float throttleMin;
    public float throttleMax;
    [Range(0f, 100f)] public float throttle;
    public float velocityFloat;
    [Range(0f, 15f)] public float maxSpeed;
    [Header("Steering")]
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
            AvionRB.AddTorque(turningForce * turningRestraint * 4);
        }

        if (Input.GetKey("d"))
        {
            AvionRB.AddTorque(turningForce * turningRestraint * -4);
        }

        turningRestraint = VelocityTurningRestraintCalculator(velocityFloat);

        if (Input.GetKey("w"))
        {
            throttle += throttleIncrement;
            throttle = throttle > throttleMax ? throttleMax : throttle;
        }

        if (Input.GetKey("s"))
        {
            throttle -= throttleIncrement;
            throttle = throttle < throttleMin ? throttleMin : throttle;
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
                isBraking = true;

                turningForce *= brakeTurningMultiplier;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (isBraking)
            {
                throttle = prevThrottle;
                isBraking = false;

                turningForce = turningForceDefault;
            }
        }

        AvionRB.AddForce(throttle * AvionTrans.right / 18, ForceMode2D.Force);

        AvionRB.velocity = Vector2.ClampMagnitude(AvionRB.velocity, maxSpeed);

        velocityFloat = CalculateVelocityFloat(AvionRB.velocityX, AvionRB.velocityY);

        velocityFloat = CalculateVelocityFloat(AvionRB.velocityX, AvionRB.velocityY);

    }

    float VelocityTurningRestraintCalculator(float velocity)
    {
        return 1 - Mathf.InverseLerp(0, maxSpeed * turningRestraintReduction, velocity);
    }

    public float CalculateVelocityFloat(float VelocityX, float VelocityY)
    {
        return Mathf.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY);
    }

}
