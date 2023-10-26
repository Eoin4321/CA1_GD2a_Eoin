using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    //Runs this when object collides with something
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checks to see if what it collides with has the player tag
        if (other.CompareTag("Player")) 
        {
            //Changes the scene to the victory scene.
            SceneManager.LoadScene("Victory");
        }
    }
}
