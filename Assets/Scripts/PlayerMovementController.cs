using UnityEngine;

public class PlayerMovementController : MovementController
{
    void Update()
    {
        if (GameManager.GameState != GameManager.GameStates.Started)
        {
            return;
        }
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
