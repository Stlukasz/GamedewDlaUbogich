using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    Inputs inputs;
    PoruszaniePostaci poruszaniePostaci;
    cameraManager CameraManager;

    public bool isInteracting;
    public bool isUsingRootMotion;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputs = GetComponent<Inputs>();
        poruszaniePostaci = GetComponent<PoruszaniePostaci>();
        CameraManager = FindObjectOfType<cameraManager>();
    }

    private void Update()
    {
        inputs.AllInputsHandler();
    }


    private void FixedUpdate()
    {
        poruszaniePostaci.AllMove();
    }

    private void LateUpdate()
    {
        CameraManager.HandleCamera();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");
        poruszaniePostaci.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", poruszaniePostaci.isOnGround);
    }

}
