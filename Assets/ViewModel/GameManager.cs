using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UManager = UnityEngine.SceneManagement;


/// <summary>
/// The GameManager class handles all the variables and functions for each unique game instance.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    ///Records what is to be displayed upon loading a new game
    /// </summary>
    public Text OnLoadDisplay;

    /// <summary>
    /// Keeps track of an instance of a game
    /// </summary>
    public static GameManager instance; //

    /// <summary>
    /// Keeps track of whether or not a game instance is currently running
    /// </summary>
    private bool gameRunning; //

    /// <summary>
    /// Keeps track of the the game model for a game instance
    /// </summary>
    public GameModel gameModel; //

    /// <summary>
    /// Keeps track of the currently active canvase
    /// </summary>
    public Canvas activeCanvas; //

    /// <summary>
    /// Keeps track of all the canvases
    /// </summary>
    public Dictionary<string, Canvas> canvases; //

    /// <summary>
    /// Declares the currently active canvas that the user can view/interact with
    /// </summary>
    /// <param name="pName">
    /// Collects the name of the passed through canvas
    /// </param>
    public void setActiveCanvas(string pName) //
    {
        if (canvases.ContainsKey(pName)) //Checks if a canvas contains a specified canvas name.
        {

            foreach (Canvas acanvas in canvases.Values)  // Set all to not active;
            {
                acanvas.gameObject.SetActive(false);
            }
            activeCanvas = canvases[pName];
            Debug.Log("I am the active one " + pName);
            activeCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("I can not find " + pName + " to make active."); //Message sent to the console informing that the requested canvas name wasn't avalible
        }
    }

    /// <summary>
    /// Declare Current Unity Scene
    /// </summary>
    /// <returns>Returns name of the current scene</returns>
    public string currentUScene() //
    {
        return UManager.SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Change Current Unity Scene
    /// </summary>
    /// <param name="pSceneName">
    /// Records name of the new scene to load</param>
    public void changeUScene(string pSceneName) //
    {
        UManager.SceneManager.LoadScene(pSceneName);
    }

    /// <summary>
    /// Declars Game Instance is Running
    /// </summary>
    private void Awake()
    {
        if (instance == null) //
        {
            instance = this;
            gameRunning = true;
            Debug.Log("I am the one");
            gameModel = new GameModel();
            canvases = new Dictionary<string, Canvas>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Returns status of game running
    /// </summary>
    /// <returns>Whether game is running or not</returns>
    public bool IsGameRunning() 
    {
        return gameRunning;
    }
}