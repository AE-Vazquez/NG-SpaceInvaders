using UnityEngine;

public class PlayerMovementController : MovementController
{
    void Update()
    {
        float newSpeed = 0;
        if(Input.GetKey(KeyCode.A))
        {
            newSpeed += m_baseSpeed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            newSpeed -= m_baseSpeed;
        }

        SetSpeed(newSpeed);
    }
}
