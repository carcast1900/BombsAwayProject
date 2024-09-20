using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private int points;
    public TextMesh text;

    // Use this for initialization
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Checks to see if the object is not a bomb, as if a bomb is destroyed errors occur
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Throwables")
        {
            //Checks to see if the object is wood to give 50 points or concrete to give 100 points
            if (col.gameObject.tag == "Wood")
            {
                points += 50;
            }
            else
            {
                points += 100;
            }
            Destroy(col.gameObject);
        }
        UpdatePoints();
    }

    //updates the text for the points to the current value of the points variable
    public void UpdatePoints()
    {
        text.text = points.ToString();
    }
}
    
