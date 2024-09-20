using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Throwable : MonoBehaviour {
    private float power = 15.0f;
    private float radius = 5.0f;
    private float upForce = 1.0f;
    private Vector3 _throwVelocity;
    private Vector3 _previousPosition;
    private bool held;
    public GameObject text;
    public GameObject splash;

	// Use this for initialization
	void Start () {
        held = false;
	}
	
	// Update is called once per frame
    //This is from the Google codelab for throwable objects
	void Update ()
    {
        //velocity is based on the previous position
        Vector3 frameVelocity = (transform.position - _previousPosition) / Time.deltaTime;

        const int samples = 3;
        //average the velocity calculate over the last number of frames
        _throwVelocity = _throwVelocity * (samples - 1) / samples + frameVelocity / samples;

        //update previous position
        _previousPosition = transform.position;

	}

    //Allows for holding objects with the pointer
    public void Hold()
    {
        if (!held)
        {
            //gets the Transform component of the pointer
            Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;

            //set the GameObject's parent to the pointer
            transform.SetParent(pointerTransform, false);

            //position in its view
            transform.localPosition = new Vector3(0, 0, 2);

            //prevent people from grabbing the bomb again
            held = true;

            //activate countdown
            text.GetComponent<Countdown>().enabled = true;

            
        }
    }

    //Allows for releasing objects
    public void Release()
    {
        //set the parent to the world
        transform.SetParent(null, true);

        //get the rigidbody physics component
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //reset veolcity
        rigidbody.velocity = Vector3.zero;

        //enable physics
        rigidbody.isKinematic=false;
        rigidbody.useGravity = true;

        // throw the object when releasing while held
        rigidbody.AddForce(_throwVelocity, ForceMode.VelocityChange);
    }

    //Adds the explosion force to the objects in a radius of 5
    public void ApplyExplosion()
    {
        //position of the bomb
        Vector3 explodePos = GetComponent<Transform>().transform.position;

        //particle affect of the explosion
        Instantiate(splash, transform.position, transform.rotation);

        //check for things in radius and then add them into an array
        Collider[] colliders = Physics.OverlapSphere(explodePos, radius);
        foreach (Collider hit in colliders)
        {
            //Check each item in the array if it has a rigidbody and apply an explosion if it does
            Rigidbody ri = hit.GetComponent<Rigidbody>();
            if (ri != null)
            {
                ri.AddExplosionForce(power, explodePos, radius, upForce, ForceMode.Impulse);
            }
        }

        //destroys bomb
        Destroy(gameObject);
    }
}
