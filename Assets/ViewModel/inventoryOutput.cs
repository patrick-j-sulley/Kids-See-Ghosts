using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The inventoryOutput class handles all the functions and variables related to displaying the players inventory contents on the inventory screen
/// </summary>
public class inventoryOutput : MonoBehaviour
{
    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    //public DataService ds = new DataService("KSG_DB.db");

    /// <summary>
    /// Output text to be displayed in inventory canvas in Game
    /// </summary>
    public Text output; //

    /// <summary>
    /// Constantly Updates the information that is being displayed on the inventory panel in the inventory interface screen.
    /// </summary>
    private void Update()
    {

        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        List<InventoryItem> lcInvList = new List<InventoryItem>();
        lcInvList = DataServiceManager.ds.RetrieveInventoryItemsList();

        if (lcInvList != null) //Checks if players inventory contains items
        {
            output.text = null;
            foreach (InventoryItem prInvItem in lcInvList) //Checks for each individual item contained in the players inventory
            {
                Item lcItem = DataServiceManager.ds.Connection.Table<Item>().Where(x => x.ID == prInvItem.ItemID).FirstOrDefault();

                output.text = output.text + lcItem.Name + "\n" + " Type:" + lcItem.Type + " Damage:" + lcItem.DamageBonus + " Health:" + lcItem.HealthBonus + "\n";
            }
        }
        else
        {
            output.text = "Your inventory is empty!"; //Message displayed to user if they have no items within their inventory currently
        }

    }

}