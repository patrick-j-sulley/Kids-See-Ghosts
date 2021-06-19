using SQLite4Unity3d;

/// <summary>
/// The AreaEnemy Class handles the variable that represents the AreaEnemy table within the connected SQLite Database.
/// </summary>
public class AreaEnemy
{

    /// <summary>
    /// The ID int variable records the ID of the unique entry into the AreaEnemy Table
    /// </summary>
    [PrimaryKey, AutoIncrement, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The EnemyID int variable records the ID of the Enemy within the AreaEnemy instance
    /// </summary>
    [NotNull]
    public int EnemyID { get; set; }

    /// <summary>
    /// The AreaName string variable records the name of the Area within the AreaEnemy instance
    /// </summary>
    [NotNull]
    public string AreaName { get; set; }

    /// <summary>
    /// The HpCurrent int variable records down the current HP of the AreaEnemy instance
    /// </summary>
    [NotNull]
    public int HpCurrent { get; set; }

    /// <summary>
    /// The PlayerUserEmail string variable records down the Player that is involved with the AreaEnemy instance.
    /// </summary>
    [NotNull]
    public string PlayerUserEmail { get; set; }

    public override string ToString()
    {
        return string.Format("[AreaEnemy: EnemyID={0}, AreaName={1}, PlayerUserEmail={2}", EnemyID, AreaName, PlayerUserEmail);
    }
}
