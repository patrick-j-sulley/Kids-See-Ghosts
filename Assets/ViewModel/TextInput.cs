using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// The TextInput class handles all of the collecting and retrieving of text put into the in game input text field.
/// </summary>
public class TextInput : MonoBehaviour
{
    /// <summary>
    /// Keeps track of the players inputted text
    /// </summary>
    private InputField input; //

    /// <summary>
    /// /Keeps track of the players submitted event
    /// </summary>
    private InputField.SubmitEvent se; //

    /// <summary>
    /// Keeps track of the players on change event
    /// </summary>
    private InputField.OnChangeEvent ce; //

    /// <summary>
    /// Keeps track of output to be displayed back to the user
    /// </summary>
    public Text output; //

    /// <summary>
    /// Keeps track of the threshold of shake detection for the acceleration sensor detection
    /// </summary>
    public float ShakeDetectionThreshold;

    /// <summary>
    /// Keeps track of the minimum shake interval for the acceleration sensor detection
    /// </summary>
    public float MinShakeInterval;

    /// <summary>
    /// Keeps track of the threshold of the square shake detection for the acceleration sensor detection
    /// </summary>
    private float sqrShakeDetectionThreshold;

    /// <summary>
    /// Keeps track of the recorded time since the last shake detected for the acceleration sensor detection
    /// </summary>
    private float timeSinceLastShake;

    /// <summary>
    /// Calls for display to be updated with output
    /// </summary>
    /// <param name="aStr"></param>
    public void TextUpdate(string aStr) //
    {
        output.text = aStr;
    }

    /// <summary>
    /// On startup, the text field will be required to peform the following functions
    /// </summary>
    private void Start()
    {

        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);

        input = this.GetComponent<InputField>(); //Declares input field
        se = new InputField.SubmitEvent(); //Declares submit event
        se.AddListener(SubmitInput); //Declares submit event

        input.onEndEdit = se; //Calls for submitted event on end edit

    }

    /// <summary>
    /// While active, check for device shakes detected for the acceleration sensor detection. Every time a shake is detected, initiate the 
    /// currently inputted command.
    /// </summary>
    private void Update()
    {
        
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold 
            && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            string currentText = output.text;  //Takes the current output and keeps track of it temporarily 

            CommandProcessor aCmd = new CommandProcessor(); //Calls for new instance of command processor

            aCmd.Parse(input.text, TextUpdate); //Calls for command split function

            input.text = ""; //Calls for input text field to be cleared

            input.ActivateInputField(); //Calls for input text field to be activated again.

            timeSinceLastShake = Time.unscaledTime;
        }

    }

    /// <summary>
    /// The submit input function handles the variables and function calls upon the user submitting into the text field.
    /// </summary>
    /// <param name="arg0"></param>
    private void SubmitInput(string arg0) //Handles the input being submitted
    {
        string currentText = output.text;  //Takes the current output and keeps track of it temporarily 

        CommandProcessor aCmd = new CommandProcessor(); //Calls for new instance of command processor

        aCmd.Parse(arg0, TextUpdate); //Calls for command split function

        input.text = ""; //Calls for input text field to be cleared

        input.ActivateInputField(); //Calls for input text field to be activated again.
    }

    /// <summary>
    /// Handles the changing of the input in the input field.
    /// </summary>
    /// <param name="arg0"></param>
    private void ChangeInput(string arg0)
    {
        Debug.Log(arg0); //Sends text input to be noted in game console
    }
}