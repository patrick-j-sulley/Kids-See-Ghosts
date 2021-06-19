using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UManager = UnityEngine.SceneManagement;


/// <summary>
/// The UserManager class handles all the variables and functions for each unique UserManager instance.
/// </summary>
public class UserManager : MonoBehaviour
{
    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    //public DataService ds = new DataService("KSG_DB.db");

    /// <summary>
    /// Handles each unique instanstiation of the User Manager
    /// </summary>
    public static UserManager UserManagerInstance = null;

    /// <summary>
    /// Records down the username of the logged in user
    /// </summary>
    private string _userEmail;

    /// <summary>
    /// Records whether or not a new game has been started
    /// </summary>
    private bool _newGame;

    /// <summary>
    /// Records whether or not a current game has been loaded
    /// </summary>
    private bool _loadGame;

    /// <summary>
    /// Upon being awoken, the Usermanage instance will stay awake throughout all scenes with the use of the included functions within.
    /// </summary>
    void Awake()
    {

        if (UserManagerInstance == null)
        {

            UserManagerInstance = this;

        }
        else if (UserManagerInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    /// <summary>
    /// The Getter and Setter for the UserEmail
    /// </summary>
    public string UserEmail
    {
        get
        {
            return _userEmail;
        }

        set
        {
            _userEmail = value;
        }
    }

    /// <summary>
    /// The Getter and Setter for the NewGame bool
    /// </summary>
    public bool NewGame
    {
        get
        {
            return _newGame;
        }

        set
        {
            _newGame = value;
        }
    }

    /// <summary>
    /// The Getter and Setter for the LoadGame bool
    /// </summary>
    public bool LoadGame
    {
        get
        {
            return _loadGame;
        }

        set
        {
            _loadGame = value;
        }
    }

}