using SQLite4Unity3d;

/// <summary>
/// The AreaItem Class handles the variable that represents the AreaItem table within the connected SQLite Database.
/// </summary>
public class AreaItem
{

    /// <summary>
    /// The ID int variable records the ID of the unique entry into the AreaItem Table
    /// </summary>
    [PrimaryKey, AutoIncrement, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The AreaName string variable records the name of the Area within the AreaItem instance
    /// </summary>
    [NotNull]
    public string AreaName { get; set; }

    /// <summary>
    /// The ItemID int variable records down the ID of the Item within the AreaItem instance
    /// </summary>
    [NotNull]
    public int ItemID { get; set; }

    /// <summary>
    /// The PlayerUserEmail string variable records down the UserEmail of the player involved with the AreaItem instance
    /// </summary>
    [NotNull]
    public string PlayerUserEmail { get; set; }

    public override string ToString()
    {
        return string.Format("[AreaItem: AreaName={0}, ItemID={1}", AreaName, ItemID);
    }
}
