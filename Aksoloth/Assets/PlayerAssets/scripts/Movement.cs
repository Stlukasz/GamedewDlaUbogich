using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private CharacterController controller;
    private float speed = 5;
    public float speed2;
    public float rotationSpeed;
    public float baseSpeed;
    public float runSpeed;
    public float jumpForce;
    private Vector3 inputVector;
    private bool inAir = false;
    private float rotX;
    private float rotY;

    float waitTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {

        waitTime = waitTime + Time.deltaTime;
       
        MoveControls();  
        
    }


    private IEnumerator WaitForAnimation()
    {
        
        yield return new WaitForSeconds(0.6f);
        animator.SetLayerWeight(0, 1);
        
        animator.SetLayerWeight(1, 0.8f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(1, 0.6f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(1, 0.4f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(1, 0.2f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(1, 0f);
        

    }



    private IEnumerator WaitForLanding()
    {
          
        animator.SetLayerWeight(2, 0.8f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(2, 0.6f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(2, 0.4f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(2, 0.2f);
        yield return new WaitForSeconds(0.02f);
        animator.SetLayerWeight(2, 0f);       

    }

    public void MoveControls()
    {
        //this.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        //poruszanie się do przodu,tył i po bokach

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float VelocityY = rb.velocity.y;

        Vector3 vector;      



        if (Input.GetKey(KeyCode.W))
        {

            vector = transform.forward * speed;        
            animator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vector = transform.forward * y;
            vector = vector.normalized * speed2;
            animator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            vector = transform.right * x;
            vector = vector.normalized * speed2;
            animator.SetBool("Walk", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            vector = transform.right * x;
            vector = vector.normalized * speed2;
            animator.SetBool("Walk", true);
        }
        else
        {
            vector = transform.forward * y + transform.right * x;
            animator.SetBool("Walk", false);
            //rb.velocity = transform.forward *0;
        }

        vector.y = VelocityY;
        rb.velocity = vector;






        if (Input.GetKeyDown(KeyCode.Space) && inAir == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);          
            animator.SetBool("jump", true);        
            animator.SetLayerWeight(2, 1);
           
        }
        

        if (Input.GetKey(KeyCode.LeftShift) && animator.GetBool("Walk") == true)
        {
            speed = runSpeed;
            animator.SetBool("Run", true);
        }
        else
        {
            speed = baseSpeed;
            animator.SetBool("Run", false);
        }



        //Wyciąganie Broni
        if (Input.GetKeyDown(KeyCode.Mouse2) && animator.GetBool("Fight") == false && waitTime > 0.8f)
        {
            animator.SetBool("Fight", true);
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
            waitTime = 0;

        }
        else if (animator.GetBool("Fight") == true && Input.GetKeyDown(KeyCode.Mouse2) && waitTime > 0.8f)
        {
            animator.SetBool("Fight", false);
            StartCoroutine("WaitForAnimation");
            waitTime = 0;

        }

        //Obrót postaci
         rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, rotationSpeed * Input.GetAxis("Mouse X"), 0f));

       // rb.angularVelocity = Vector3.zero; // <-------------------
       // rotY = Input.GetAxis("Mouse X") * rotationSpeed;
       // rotX = Input.GetAxis("Mouse Y") * rotationSpeed;
        //rb.AddTorque(transform.right * rotX + Vector3.up * rotY);


    }


    void OnTriggerEnter(Collider other)
    {
        inAir = false;
        Debug.Log("ground");
       
        animator.SetBool("jump", false);      
        StartCoroutine("WaitForLanding");
       
    }

    void OnTriggerExit(Collider other)
    {
        inAir = true;
        Debug.Log("air");
    }
   
}
