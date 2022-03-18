using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathBall : MonoBehaviour
{

    [SerializeField] [Range(0, 1)] private float Bounciness = .2f;      // Bounciness of the ball (0-1) inclusive   
    [SerializeField] private float Gravity = -9.81f;                    // Gravity coefficient
    [SerializeField] private Transform GroundTransform;                 // The ground object the ball can collide with

    private float CurrVelocity = 0f;                                    // Current velocity of the ball
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
        // If the ball still moving, then update its movement
        if (!bBallStopped)
        {
            if (MoveBall())
            {   
                // Get the new velocity direction 
                CurrVelocity = CurrVelocity * -1 * Bounciness;

                Debug.Log(CurrVelocity);

                // if not enough velocity, then stop ball movement
                if (CurrVelocity < 0.01f)
                    bBallStopped = true;
            }
        }
        // Ball stopped, wait from keyboard input
        else
        {
            // TODO:
        }

        CalcNewCurrentVelocity();
    }

    // Move the ball, return true if it collided with the ground
    bool MoveBall()
    {
        // amount to move ball
        float MoveAmount = CurrVelocity * Time.deltaTime;

        // Set new ball position
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x, 
            gameObject.transform.position.y + MoveAmount, 
            gameObject.transform.position.z);

        // Prevent the ball from going through the ground
        bool bCollided = IsCollidedWithGround();
        if (bCollided)
        {
            gameObject.transform.position = new Vector3(
            gameObject.transform.position.x, 
            GroundTransform.position.y + gameObject.transform.localScale.y / 2, 
            gameObject.transform.position.z);
        }
        return bCollided;
    }

    // Set the new velocity of the ball
    float CalcNewCurrentVelocity()
    {
        return CurrVelocity = (Gravity * Time.deltaTime + CurrVelocity);
    }

    // Check if the ball has collided with the ground
    bool IsCollidedWithGround()
    {
        return (gameObject.transform.position.y - gameObject.transform.localScale.y / 2) < GroundTransform.position.y;
    }
}
