using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pushable : MonoBehaviour
{
    private Scene scene;
    //current points
    private static int b;
    //goal points
    private static int g;

    // Use this for initialization
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Loads the current scene again
    public void restartCurrentScene()
    {
        SceneManager.LoadScene(scene.name);
    }

    //Checks to see if the goal for the level has been met and then loads level 1
    public void SceneX(int x)
    {
        b = int.Parse(GameObject.Find("Point").GetComponent<TextMesh>().text);
        g = int.Parse(GameObject.Find("Goal").GetComponent<TextMesh>().text);
        if (b>=g)
        {
            SceneManager.LoadScene("Level "+x);
        }
    }

    //Loads the welcome screen after level 8
    public void Welcome()
    {
        b = int.Parse(GameObject.Find("Point").GetComponent<TextMesh>().text);
        g = int.Parse(GameObject.Find("Goal").GetComponent<TextMesh>().text);
        if (b >= g)
        {
            SceneManager.LoadScene("Welcome Screen");
        }
    }


    //Starts level 1
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
}