using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The inventoryStatsOutput class handles all the functions and variables related to displaying the players stats on the inventory screen
/// </summary>
public class inventoryStatsOutput : MonoBehaviour
{
    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    //public DataService ds = new DataService("KSG_DB.db");

    /// <summary>
    /// Output text to be displayed in inventory canvas in Game
    /// </summary>
    public Text HPoutput; //

    /// <summary>
    /// Output text to be displayed in inventory canvas in Game
    /// </summary>
    public Text TimePlayedoutput; //

    /// <summary>
    /// Output text to be displayed in inventory canvas in Game
    /// </summary>
    public Text DistanceTravoutput; //

    /// <summary>
    /// Output text to be displayed in inventory canvas in Game
    /// </summary>
    public Text EnemiesSlainoutput; //

    /// <summary>
    /// Output text to be displayed in inventory canvas in Game
    /// </summary>
    public Text Pointsoutput; //

    /// <summary>
    /// Constantly Updates the information that is being displayed on the player stats in the inventory interface screen.
    /// </summary>
    private void Update()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        if (lcPlayer.HpCurrent != 0) //Checks if the players health is above 0
        {
            HPoutput.text = null;
            HPoutput.text = "Current Health: " + HPoutput.text + lcPlayer.HpCurrent + "/" + lcPlayer.HpMax;
            DistanceTravoutput.text = null;
            DistanceTravoutput.text = DistanceTravoutput.text + "Distance Travelled: " + lcPlayer.DistanceTravelled + " KM";
            EnemiesSlainoutput.text = null;
            EnemiesSlainoutput.text = EnemiesSlainoutput.text + "Enemies Slain: " + lcPlayer.EnemiesSlain;
            Pointsoutput.text = null;
            Pointsoutput.text = Pointsoutput.text + lcPlayer.Points;
        }
        else
        {
            HPoutput.text = "DEAD";
            DistanceTravoutput.text = "DEAD";
            EnemiesSlainoutput.text = "DEAD";
            Pointsoutput.text = "DEAD";
        }
    }
}