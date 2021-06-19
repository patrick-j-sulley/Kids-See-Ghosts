using UnityEngine;

/// <summary>
/// The identifyCanvas class handles the indentification of canvases within the unity engine.
/// </summary>
public class identifyCanvas : MonoBehaviour
{
    /// <summary>
    /// On start up, it initiates the functions related to indetifying canvases.
    /// </summary>
    private void Start()
    {
        Canvas[] lcCanvases = gameObject.GetComponents<Canvas>(); //Keeps track of all the cavases
        Canvas lcCanvas = lcCanvases[0];
        string lcName = lcCanvas.name;

        // 
        if (GameManager.instance != null) //Checks to see if the GameManager currently has a running instance
                                          
        {
            GameManager.instance.canvases.Add(lcName, lcCanvas);
            Debug.Log("I added a canvas " + lcName); //Message sent to the console to declare which specific canvas has been added
        }
        else
        {
            Debug.Log("Canvas " + lcName + " not added"); //Message sent to the console to declare which specific canvas has not been added
        }
    }
}