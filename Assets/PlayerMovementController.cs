using UnityEngine;

public class PlayerMovementController : MovementController
{
    void Update()
    {
        float newSpeed = 0;
        if(Input.GetKey(KeyCode.A))
        {
            newSpeed += m_moveSpeed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            newSpeed -= m_moveSpeed;
        }

        SetSpeed(newSpeed);
    }
}
