using SQLite4Unity3d;

/// <summary>
/// The Enemy Class handles the variable that represents the Enemy table within the connected SQLite Database.
/// </summary>
public class Enemy
{

    /// <summary>
    /// The ID int variable records the ID of the unique enemy
    /// </summary>
	[PrimaryKey, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The Name string variable records down the name of the enemy
    /// </summary>
    [NotNull]
    public string Name { get; set; }

    /// <summary>
    /// The HpCurrent int variable records down the starting/current hp of an enemy 
    /// </summary>
    [NotNull]
    public int HpCurrent { get; set; }

    /// <summary>
    /// The HpMax int variable records down the maximum HP of an enemy
    /// </summary>
    [NotNull]
    public int HpMax { get; set; }

    /// <summary>
    /// The DamageAmount int variable records the damage amount of an enemy
    /// </summary>
    [NotNull]
    public int DamageAmount { get; set; }

    /// <summary>
    /// The CanEvade bool variable records down whether or not an enemy can be evaded
    /// </summary>
    [NotNull]
    public bool CanEvade { get; set; }

    /// <summary>
    /// The CanBefriend bool variable records down whether or not an enemy can be befriended
    /// </summary>
    [NotNull]
    public bool CanBefriend { get; set; }

    /// <summary>
    /// The XpRewardAmount int variable records down the amount of XP that is rewarded to a player upon an enemy being slain.
    /// </summary>
    [NotNull]
    public int XpRewardAmount { get; set; }

    public override string ToString()
    {
        return string.Format("[Enemy: ID={0}, Direction={1}, HpCurrent={2}, HpMax={3}, DamageAmount={4}, CanEvade={5}, CanBefriend={6}, XpRewardAmount={7}", ID, Name, HpCurrent, HpMax, DamageAmount, CanEvade, CanBefriend, XpRewardAmount);
    }
}
