using SQLite4Unity3d;

/// <summary>
/// The EnemyItem Class handles the variable that represents the EnemyItem table within the connected SQLite Database.
/// </summary>
public class EnemyItem
{

    /// <summary>
    /// The ID int variable records the ID of the unique EnemyItem entry into the EnemyItem table
    /// </summary>
    [PrimaryKey, AutoIncrement, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The ItemID int variable records down the ID of the Item involved in an EnemyItem instance
    /// </summary>
    [NotNull]
    public int ItemID { get; set; }

    /// <summary>
    /// The EnemyID int variable records down the ID of the Enemy involved in an EnemyItem instance
    /// </summary>
    [NotNull]
    public int EnemyID { get; set; }

    /// <summary>
    /// The PlayerUserEmail string variable records down the username of the player connected to an EnemyItem instance.
    /// </summary>
    [NotNull]
    public string PlayerUserEmail { get; set; }

    public override string ToString()
    {
        return string.Format("[EnemyItem: ItemID={0}, EnemyID={1}, PlayerUserEmail={2}", ItemID, EnemyID, PlayerUserEmail);
    }
}
