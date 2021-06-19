using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// The MainMenuManager class handles all the variables and functions for each unique Main Menu instance.
/// </summary>
public class MainMenuManager : MonoBehaviour
{

    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    //public DataService ds = new DataService("KSG_DB.db");

    /// <summary>
    /// Text Output field to be displayed on the main menu
    /// </summary>
    public Text UserNameDisplay;

    /// <summary>
    /// Button to to be displayed on the main menu
    /// </summary>
    public Button StartSPGame;

    /// <summary>
    /// Button to be displayed on the main menu
    /// </summary>
    public Button LoadSPGame;

    /// <summary>
    /// On startup, checks if a user has an already existing player table entry. If they do exist, they will have the option to load a previous game
    /// , if not, the option will be unavalibe
    /// </summary>
    public void Start()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        if (lcPlayer == null)
        {
            UserManager.UserManagerInstance.LoadGame = false;
        }
        else if (lcPlayer != null)
        {
            UserManager.UserManagerInstance.LoadGame = true;
        }

        if (UserManager.UserManagerInstance.LoadGame == false)
        {
            LoadSPGame.interactable = false;
        }
        else if (UserManager.UserManagerInstance.LoadGame == true)
        {
            LoadSPGame.interactable = true;
        }
    }

    /// <summary>
    /// The main menu will update with the name of the currently logged in user
    /// </summary>
    public void Update()
    {
        UserNameDisplay.text = null;
        UserNameDisplay.text = "Logged In As: " + UserManager.UserManagerInstance.UserEmail;

    }

    /// <summary>
    /// Upon the start new game button being clicked. The usermanager instance will changed to signal a new game is being started
    /// </summary>
    public void NewGame()
    {

        UserManager.UserManagerInstance.NewGame = true;
        UserManager.UserManagerInstance.LoadGame = false;
    }

    /// <summary>
    /// Upon the load current game button being clicked. The usermanager instance will changed to signal a previous game is being loaded
    /// </summary>
    public void LoadGame()
    {
        UserManager.UserManagerInstance.LoadGame = true;
    }
}