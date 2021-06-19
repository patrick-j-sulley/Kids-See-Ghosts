using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using UManager = UnityEngine.SceneManagement;



/// <summary>
/// The LoginManage class handles all the variables and functions for each login menu instance
/// </summary>
public class LoginManager : MonoBehaviour
{
    /// <summary>
    /// A Text Input Field on the Login Screen
    /// </summary>
    public InputField txtUserEmail;

    /// <summary>
    /// A Text Input Field on the Login Screen
    /// </summary>
    public InputField txtPassword;

    /// <summary>
    /// A Text Output Display Field on the Login Screen
    /// </summary>
    public Text txtOutput;

    /// <summary>
    /// A Button on the login screen
    /// </summary>
    public Button btnLogin;

    /// <summary>
    /// A Button on the login screen
    /// </summary>
    public Button btnSignUp;

    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    public DataService ds;
    /// <summary>
    /// A static instance of each unique login manager instance
    /// </summary>
    public static LoginManager LoginManagerInstance;

    /// <summary>
    /// The LoginScreenRunning bool variable determines whether or not a login screen is active
    /// </summary>
    private bool LoginScreenRunning;

    /// <summary>
    /// Keeps track of the the game model for a game instance
    /// </summary>
    public GameModel gameModel;

    /// <summary>
    /// An instance of the unique firebase authentication service manager
    /// </summary>
    public AuthManager authManager;

    /// <summary>
    /// Upon startup, checkings for existance of database
    /// </summary>
    private void Start()
    {

        if (DataServiceManager.ds != null && DataServiceManager.ds.DoesDBExist() == false)
        {
            DataServiceManager.ds.CreateDB();
        }
        
    }

    /// <summary>
    /// Login function that calls to the AuthManager's sign in Method with the specified variables. 
    /// </summary>
    public void Login()
    {

        authManager.SignIn(GoodResultSignIn, BadResultSignIn, txtUserEmail.text, txtPassword.text);

    }

    /// <summary>
    /// SignUp function that calls to the AuthManager's sign up method with the specified variables. 
    /// </summary>
    public void SignUp()
    {

        authManager.SignUp(GoodResultSignUp, BadResultSignUp, txtUserEmail.text, txtPassword.text);

    }

    /// <summary>
    /// Updates the output text display on the login screen with a message string variable
    /// </summary>
    /// <param name="message">
    /// A string variable containing the string to be displayed
    /// </param>
    private void UpdateStatus(string message)
    {
        txtOutput.text = message;
    }

    /// <summary>
    /// Handles the error message upon an unsuccessful sign up through the firebase authentication manager
    /// </summary>
    public void BadResultSignUp()
    {
        UpdateStatus("Sorry, There was an Error creating your Account." + authManager.theLastTask.Exception);
    }

    /// <summary>
    /// Handles the success message upon a successful sign up through the firebase authentication manager, as well as creating a local SQLite entry for the user
    /// locally in the user table, performing a local login, and changing the current scene to the main menu.
    /// </summary>
    public void GoodResultSignUp()
    {
        Firebase.Auth.FirebaseUser newUser = authManager.theLastTask.Result;
        UpdateStatus("Account Created, You are now loading into Kids See Ghosts...");

        User lcUser = new User
        {
            UserEmail = txtUserEmail.text,
            Password = txtPassword.text
        };

        DataServiceManager.ds.Connection.Insert(lcUser);
        UserManager.UserManagerInstance.UserEmail = txtUserEmail.text;
        string lcMainMenuScreen = "MainMenuScreen";
        UManager.SceneManager.LoadScene(lcMainMenuScreen);
    }

    /// <summary>
    /// Handles the error message upon an unsuccessful sign in through the firebase authentication manager
    /// </summary>
    public void BadResultSignIn()
    {
        UpdateStatus("Sorry, There was an Error signing into your Account" + authManager.theLastTask.Exception);
    }

    /// <summary>
    /// Handles the success message upon a successful sign in through the firebase authentication manager, as well as running a check for the users existance
    /// locally in the SQLite database. If the user exists locally, they'll just be signed in, if the user doesn't exist locally, a local SQLite entry will be
    /// created and they will be signed in directly after.
    /// </summary>
    public void GoodResultSignIn()
    {
        UpdateStatus("Sign in Sucessful, You are now loading into Kids See Ghosts...");

        bool lcUserExist = DataServiceManager.ds.DoesUserExist(txtUserEmail.text, txtPassword.text);

        if (lcUserExist == true)
        {
            UpdateStatus("lcUserExists has been used, and user does exist");
            UserManager.UserManagerInstance.UserEmail = txtUserEmail.text;
            UpdateStatus("UserManagerInstance has been used, and the UserEmail has been assigned");
            string lcMainMenuScreen = "MainMenuScreen";
            UManager.SceneManager.LoadScene(lcMainMenuScreen);
        }
        else
        {
            UpdateStatus("lcUserExists has been used, and user does not exist");
            User lcUser = new User
            {
                UserEmail = txtUserEmail.text,
                Password = txtPassword.text
            };

            DataServiceManager.ds.Connection.Insert(lcUser);
            UserManager.UserManagerInstance.UserEmail = txtUserEmail.text;
            UpdateStatus("UserManagerInstance has been used, and the UserEmail has been assigned");
            string lcMainMenuScreen = "MainMenuScreen";
            UManager.SceneManager.LoadScene(lcMainMenuScreen);
        }


    }
}