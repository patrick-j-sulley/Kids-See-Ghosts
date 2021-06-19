using SQLite4Unity3d;

/// <summary>
/// The InventoryItem Class handles the variable that represents the InventoryItem table within the connected SQLite Database.
/// </summary>
public class InventoryItem
{

    /// <summary>
    /// The ID int variable records the ID of the unique InventoryItem entry into the InventoryItem table
    /// </summary>
    [PrimaryKey, AutoIncrement, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The ItemID int variable records down the ID of the Item involved in an InventoryItem instance
    /// </summary>
    [NotNull]
    public int ItemID { get; set; }

    /// <summary>
    /// The PlayerUserEmail string variable records down the username of the player connected to an InventoryItem instance.
    /// </summary>
    [NotNull]
    public string PlayerUserEmail { get; set; }

    public override string ToString()
    {
        return string.Format("[InventoryItem: ItemID={0}, PlayerUserEmail={1}", ItemID, PlayerUserEmail);
    }
}
