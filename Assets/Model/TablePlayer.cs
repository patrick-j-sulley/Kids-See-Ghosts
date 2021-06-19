using SQLite4Unity3d;

/// <summary>
/// The Player Class handles the variable that represents the Player table within the connected SQLite Database.
/// </summary>
public class Player
{

    /// <summary>
    /// The UserEmail string variable records the UserEmail of each unique player
    /// </summary>
	[PrimaryKey, NotNull]
    public string UserEmail { get; set; }

    /// <summary>
    /// The HpCurrent int variable records down the current HP amount for the player.
    /// </summary>
    [NotNull]
    public int HpCurrent { get; set; }

    /// <summary>
    /// The HpMax int variable records down the max HP ammount for the player
    /// </summary>
    [NotNull]
    public int HpMax { get; set; }

    /// <summary>
    /// The DamageAmount int variable records down the damage amount for the player
    /// </summary>
    [NotNull]
    public int DamageAmount { get; set; }

    /// <summary>
    /// The XpCurrent int variable records down the current XP amount for the player
    /// </summary>
    [NotNull]
    public int XpCurrent { get; set; }

    /// <summary>
    /// The LvlCurrent int variable records down the current level for the player
    /// </summary>
    [NotNull]
    public int LvlCurrent { get; set; }

    /// <summary>
    /// The LvlUpDamageBonus int variable records down the level up damage bonus amount for the player
    /// </summary>
    [NotNull]
    public int LvlUpDamageBonus { get; set; }

    /// <summary>
    /// The LvlUpHealthBonus int variable records down the level up health bonus amount for the player
    /// </summary>
    [NotNull]
    public int LvlUpHealthBonus { get; set; }

    /// <summary>
    /// The DistanceTravelled double variable records down the distance travelled amount for the player
    /// </summary>
    [NotNull]
    public double DistanceTravelled { get; set; }

    /// <summary>
    /// The EnemiesSlain int variable records downt the enemies slain amount for the player
    /// </summary>
    [NotNull]
    public int EnemiesSlain { get; set; }

    /// <summary>
    /// The Points int variable records down the points amount for the player
    /// </summary>
    [NotNull]
    public int Points { get; set; }

    /// <summary>
    /// The CurrentArea string variable records down the name of the current area for the player.
    /// </summary>
    [NotNull]
    public string CurrentArea { get; set; }



    public override string ToString()
    {
        return string.Format("[Player: AreaName={0}, HpCurrent={1}, HpMax={2}, DamageAmount={3}, XpCurrent={4}, LvlCurrent={5}, LvlUpDamageBonus={6}, DistanceTravelled={7}, EnemiesSlain={8}, Points={9}, CurrentArea={9}", UserEmail, HpCurrent, HpMax, DamageAmount, XpCurrent, LvlCurrent, LvlUpDamageBonus, DistanceTravelled, EnemiesSlain, Points, CurrentArea);
    }
}
