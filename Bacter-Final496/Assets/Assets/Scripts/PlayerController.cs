using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int dashCharges = 3;
    public bool isDashing = false;
    protected float dashCooldown = 3.0f;
    private float dashDuration = 0.2f;
    private float dashTimer = 0.0f;
    public bool dashUnlock = false;
    private bool canMove = true;
    public TMP_Text dashChargesText;

    public HealthSystem healthSystem;

    public MitosisGauge mitosisGauge;

    public float collisionDamage = 20f;

   public void StartDashAbility()
    {
        if (dashUnlock && !isDashing && dashCharges > 0)
        {
            StartCoroutine(Dash());
        }
    }
  
    void Update() {

        if (canMove) {

        if (Input.GetKeyDown(KeyCode.LeftShift)) { 
        StartDashAbility(); 
        }    
        
        if (isDashing) { 
            dashTimer += Time.deltaTime;

            if(dashTimer >= dashDuration) { 
                isDashing = false;
                dashTimer = 0.0f;  
            } 
        }
        
            
        else {
                if (dashCharges < 3 && dashTimer >= dashCooldown) {
                dashCharges++;
                dashTimer = 0.0f;
                UpdateDashChargesUI();
        }
            
        dashTimer += Time.deltaTime;

        MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.R) && mitosisGauge != null) { 
            mitosisGauge.ApplyRegenAbility();
        }    

    }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
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

    void UpdateDashChargesUI() {
        if (dashChargesText != null)
        {
            dashChargesText.text = "Dash Charges: " + dashCharges;
        }
    }
    void MovePlayer() { 
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
         
         if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f){
            healthSystem.PlayerStartedMoving();
        }
    }

    public void EnablePlayerMovement() { 
        canMove = true;
    }

    public void DisablePlayerMovement() { 
        canMove = false; 
    }

    public float DamageOther()
    {
        return collisionDamage;
    }

}





