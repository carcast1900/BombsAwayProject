using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    //The variable used to keep track of time
    private float timer;
    //Variable for 3D text
    private TextMesh t;
    public GameObject bomb;
    

	// Use this for initialization
	void Start () {
        t = GetComponent<TextMesh>();
        timer = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {

        //Checks to see if the bomb was destroyed, not blown up and deletes text
        if (bomb == null && t.text != "0.0")
        {
            Destroy(gameObject);
            Destroy(this);
        }

        else
        {
            //Moves the text to the bomb
            transform.position = Vector3.MoveTowards(transform.position, bomb.transform.position, 100);

            //Decreases the timer if it is not zero
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;

                //Sets the timer to only use 2 decimal points
                t.text = timer.ToString("n1");
            }

            //Sets the timer text to 0.0 instead of the negative number after and detonates the bomb
            else
            {
                t.text = "0.0";
                bomb.GetComponent<Throwable>().ApplyExplosion();
                Destroy(gameObject);
            }
        }
	}
}
