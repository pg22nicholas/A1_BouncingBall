using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathBall : MonoBehaviour
{

    [SerializeField] [Range(0, 1)] private float Bounciness = .2f;      // Bounciness of the ball (0-1) inclusive   
    [SerializeField] private float Gravity = -9.81f;                    // Gravity coefficient
    [SerializeField] private Transform GroundTransform;                 // The ground object the ball can collide with

    private float TimeInCurrentDirection = 0f;                          // Time ball has been falling / going up
    private float VelocityStart = 0f;                                   // Velocity start, Negatity for downward starting velocty, positive for upwards starting velocity
    private bool bBallStopped = false;                                  // If the ball has stopped moving

    // Start is called before the first frame update
    void Start()
    {
        // Ground must be initialized
        if (GroundTransform == null)
            throw new System.Exception("GroundTransform not initialzied");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(VelocityStart);
        if (!bBallStopped)
        {
            MoveBall();

            if (IsCollidedWithGround())
            {
                // TODO: Check if speed is small, then stop the ball

                // Initial velocity upwards of ball after hitting the ground
                
                VelocityStart = CalcVelocity() * -1 * Bounciness;
                TimeInCurrentDirection = 0;
                MoveBall();
            }
        }
        else
        {
            // Check is keyboard space selected to bounce the ball
        }
    }

    void MoveBall()
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;

        TimeInCurrentDirection += Time.deltaTime;
        float MoveAmount = CalcVelocity() * Time.deltaTime;
        //Debug.Log(velocity);
        gameObject.transform.position = new Vector3(x, y + MoveAmount, z);
    }

    float CalcVelocity()
    {
        return (Gravity * TimeInCurrentDirection + VelocityStart);
    }

    bool IsCollidedWithGround()
    {
        return (gameObject.transform.position.y - gameObject.transform.localScale.y / 2) < GroundTransform.position.y;
    }
}
