using UnityEngine;


/// <summary>
/// The SetActiveCanvas handles the functions related to the setting of the active canvas in the game
/// </summary>
public class SetActiveCanvas : MonoBehaviour
{
    /// <summary>
    /// On startup, it will run the required canvas functions.
    /// </summary>
    private void Start()
    {
        Canvas[] lcCanvases = gameObject.GetComponents<Canvas>();
        Canvas lcCanvas = lcCanvases[0];
        string lcName = lcCanvas.name;
        if (GameManager.instance != null)
            GameManager.instance.setActiveCanvas(lcName); // Sets Active Canvas to specified canvas
    }
}