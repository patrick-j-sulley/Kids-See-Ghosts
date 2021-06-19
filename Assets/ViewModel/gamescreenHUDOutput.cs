using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The gamescreenHUDOutput Class handles all the variables and functions related to displaying information on the HUD within the gameplay interface
/// </summary>
public class gamescreenHUDOutput : MonoBehaviour
{
    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    //public DataService ds = new DataService("KSG_DB.db");

    /// <summary>
    /// Output text that displays on the gameplay screen
    /// </summary>
    public Text OnLoad_StoryOutput;

    /// <summary>
    /// Output text that displays on the gameplay screen
    /// </summary>
    public Text HPoutput;

    /// <summary>
    /// Output text that displays on the gameplay screen
    /// </summary>
    public Text XPoutput;

    /// <summary>
    /// Output text that displays on the gameplay screen
    /// </summary>
    public Text DMGoutput;

    /// <summary>
    /// Output text that displays on the gameplay screen
    /// </summary>
    public Text LVLoutput;

    /// <summary>
    /// Output text that displays on the gameplay screen
    /// </summary>
    public Text Areaoutput;

    /// <summary>
    /// Handles what is displayed upon the games startupt
    /// </summary>
    private void Start()
    {
        GameManager.instance.gameModel.VisitStoryTypeCheck();
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        Story lcStory = DataServiceManager.ds.Connection.Table<Story>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.Type == GameManager.instance.gameModel.StoryVisitType).FirstOrDefault();
        OnLoad_StoryOutput.text = lcStory.Description;

    }

    /// <summary>
    /// Constantly Updates the information that is being displayed on the HUD in the gameplay interface screen.
    /// </summary>
    private void Update()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        if (lcPlayer.HpCurrent >= 0) //Checks if players inventory contains items
        {
            HPoutput.text = null;
            HPoutput.text = lcPlayer.HpCurrent + "/" + lcPlayer.HpMax;
            XPoutput.text = null;
            XPoutput.text = XPoutput.text + lcPlayer.XpCurrent;
            DMGoutput.text = null;
            DMGoutput.text = DMGoutput.text + lcPlayer.DamageAmount;
            LVLoutput.text = null;
            LVLoutput.text = LVLoutput.text + lcPlayer.LvlCurrent;
            Areaoutput.text = null;
            Areaoutput.text = Areaoutput.text + "Current Area: " + lcPlayer.CurrentArea;
        }
        else
        {
            HPoutput.text = "DEAD";
            XPoutput.text = "DEAD";
            DMGoutput.text = "DEAD";
            LVLoutput.text = "DEAD";
            Areaoutput.text = "DEAD";
        }
    }
}