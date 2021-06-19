using UnityEngine;

/// <summary>
/// The LoadOnClickCanvas class handles the functions and variables related to loading of a new canvase upon clicking an in game button
/// </summary>
public class LoadOnClickCanvas : MonoBehaviour
{
    /// <summary>
    /// The ChangeCanvas function handles the changing of the current canvas to the passed in and new specified canvas
    /// </summary>
    /// <param name="pCanvas">
    /// The pCanvase string variable handles the name of the specified canvas
    /// </param>
    public void ChangeCanvas(Canvas pCanvas)
    {
        pCanvas.gameObject.SetActive(true); //Sets specific canvas as the active canvas for the user to view

        Canvas[] canvases = gameObject.GetComponentsInChildren<Canvas>(); //Gets all the child components contained within a canvas

        foreach (Canvas aCvn in canvases) //Sets all other canvases as inactive
        {
            if (aCvn.name != pCanvas.name)
            {
                aCvn.gameObject.SetActive(false);
            }
        }
    }
}