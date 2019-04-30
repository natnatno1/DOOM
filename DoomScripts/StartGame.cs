using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Define what happens when the player clicks on this game object
    private void OnMouseDown()
    {
        // Load the "SampleScene"
        SceneManager.LoadScene("SampleScene");
    }
}
