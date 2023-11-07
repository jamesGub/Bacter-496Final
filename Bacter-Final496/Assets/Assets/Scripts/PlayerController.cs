using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public int dashCharges = 3;
    public bool isDashing = false;
    private float dashCooldown = 5.0f;
    private float dashDuration = 0.2f;
    private float dashTimer = 0.0f;
    public bool dashUnlock = false;

    public MitosisGauge mitosisGauge;

   public void StartDashAbility()
    {
        if (dashUnlock && !isDashing && dashCharges > 0)
        {
            StartCoroutine(Dash());
        }
    }
  
    void Update() {

      if (isDashing) { 
        dashTimer += Time.deltaTime;

        if(dashTimer >= dashDuration) { 
          isDashing = false;
          dashTimer = 0.0f;  
        }
    }
    else if (dashCharges < 3 && dashTimer >= dashCooldown) { 
        dashCharges++;
        dashTimer = 0.0f;
    }

    MovePlayer(); 
    }

    IEnumerator Dash() { 
        isDashing = true;
        dashCharges--;

        Vector3 dashDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized; 
               for (float timer = 0; timer < dashDuration; timer += Time.deltaTime)
        {
            transform.position += dashDirection * moveSpeed * Time.deltaTime * 10f; 
            yield return null;
        }
    
    }
    void MovePlayer() { 
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

}




