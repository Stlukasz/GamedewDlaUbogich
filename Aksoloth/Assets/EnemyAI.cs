using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator animator;

    public Transform Player;
    public float viewRange;
    public float atackRange;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.position, this.transform.position)< viewRange)
        {
            Vector3 kieronek = Player.position - this.transform.position;
            kieronek.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(kieronek), 0.1f);

            animator.SetBool("run", true);
            if (kieronek.magnitude > atackRange)
            {
                this.transform.Translate(0, 0, speed);
                animator.SetBool("atack",true);
            }
            else
            {
                animator.SetBool("atack", false);
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}
