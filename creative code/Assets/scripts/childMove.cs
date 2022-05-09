using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class childMove : MonoBehaviour
{
    public int my_moveLenght;
    public int my_speed;
    public GameObject center;
    public Vector3 normalDeplacement;
    public Material couleur1;
    public Material couleur2;
    [SerializeField] private Vector3 targetPosition;
    private int storedSpeed;
    private bool finalDestination;

// Start is called before the first frame update
    void Start()
    {
        storedSpeed = my_speed;
        my_speed = 0;
        // rotate toward center modulo random
        transform.LookAt(center.transform.position);
        transform.Rotate(0,Random.Range(-40f,40f),0,Space.Self);
        targetPosition = transform.position + transform.forward * my_moveLenght;
        normalDeplacement = targetPosition - transform.position;
    }

// Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, my_speed * Time.deltaTime);
        if (transform.position == targetPosition && !finalDestination)
        {
            transform.Rotate(0,Random.Range(-20f,20f),0,Space.Self);
            targetPosition = transform.position + transform.forward * my_moveLenght;
        }
        
    }

    public void OnEventStart()
    {
        my_speed = storedSpeed;
        Debug.Log("event receive" + my_speed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision" + other);
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("tag");
            targetPosition = other.transform.position;
            this.GetComponent<Collider>().enabled = false;
            finalDestination = true;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (finalDestination || transform.position.x < other.transform.position.x )
            {
                targetPosition = transform.position;
                //this.GetComponent<Collider>().enabled = false;
                var render =gameObject.GetComponents<MeshRenderer>();
                render[0].material = couleur1;
                finalDestination = true;
                //tag = "";
            }
            else
            {
                var render =gameObject.GetComponents<MeshRenderer>();
                render[0].material = couleur2;
                childMove otherMove = other.gameObject.GetComponent<childMove>();
                Vector3 cross = otherMove.normalDeplacement + normalDeplacement;
                Debug.Log("position "+ transform.position+ " other position " + other.transform.position +
                          " deplacement " + normalDeplacement + " norme " + normalDeplacement.normalized +
                          " deplacement other " + otherMove.normalDeplacement + " norme " + otherMove.normalDeplacement.normalized +
                          " cross " + cross);
                
                transform.LookAt(transform.position + cross);
                targetPosition = transform.position + cross * my_moveLenght;
            }
            Debug.Log("tag");
            //targetPosition = other.transform.position;
        }
    }
}