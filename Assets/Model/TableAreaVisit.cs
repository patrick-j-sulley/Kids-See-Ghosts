using SQLite4Unity3d;

/// <summary>
/// The AreaVisit Class handles the variable that represents the AreaVisit table within the connected SQLite Database.
/// </summary>
public class AreaVisit
{

    /// <summary>
    /// The ID int variable records the ID of the unique entry into the AreaVisit Table
    /// </summary>
    [AutoIncrement, PrimaryKey, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The Number int variable records down the current number of visits within an AreaVisit instance
    /// </summary>
    [NotNull]
    public int Number { get; set; }

    /// <summary>
    /// The PlayerUserEmail string variable records down the username of the player involved within an AreaVisit instance
    /// </summary>
    [NotNull]
    public string PlayerUserEmail { get; set; }

    /// <summary>
    /// The AreaName string variable records down the name of the area involved within an AreaVisit instance
    /// </summary>
    [NotNull]
    public string AreaName { get; set; }

    public override string ToString()
    {
        return string.Format("[AreaVisit: ID={0}, PlayerUserEmail={1}, AreaName={2}", Number, PlayerUserEmail, AreaName);
    }
}
