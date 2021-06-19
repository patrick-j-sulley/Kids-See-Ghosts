using SQLite4Unity3d;

/// <summary>
/// The Item Class handles the variable that represents the Item table within the connected SQLite Database.
/// </summary>
public class Item
{

    /// <summary>
    /// The ID int variable records the ID of the unique Item
    /// </summary>
	[PrimaryKey, NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The Name string variable records down the items name
    /// </summary>
    [NotNull]
    public string Name { get; set; }

    /// <summary>
    /// The type string variable records down the items type
    /// </summary>
    [NotNull]
    public string Type { get; set; }

    /// <summary>
    /// The damage bonus byte variable records down the items damage bonus amount
    /// </summary>
    public byte DamageBonus { get; set; }

    /// <summary>
    /// The health bonus byte variable records down the items health bonus amount
    /// </summary>
    public byte HealthBonus { get; set; }


    public override string ToString()
    {
        return string.Format("[Item: ID={0}, Direction={1}, AreaOrigin={2}, AreaDestination={3}, HealthBonus={4}", ID, Name, Type, DamageBonus, HealthBonus);
    }
}
