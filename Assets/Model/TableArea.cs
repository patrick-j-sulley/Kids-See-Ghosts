using SQLite4Unity3d;

/// <summary>
/// The Area Class handles the variable that represents the Area table within the connected SQLite Database.
/// </summary>
public class Area
{

    /// <summary>
    /// The AreaName string variable records the name of the Area
    /// </summary>
	[PrimaryKey, NotNull]
    public string AreaName { get; set; }

    /// <summary>
    /// The HasEnemy Bool variable records whether an area has an enemy or not
    /// </summary>
    [NotNull]
    public bool HasEnemy { get; set; }

    /// <summary>
    /// The HasItem variable records whether an area has an Item or not
    /// </summary>
    [NotNull]
    public bool HasItem { get; set; }

    /// <summary>
    /// The North string variable records the name of the Area within the North direction of the Area
    /// </summary>
    public string North { get; set; }

    /// <summary>
    /// The South string variable records the name of the Area within the North direction of the Area
    /// </summary>
    public string South { get; set; }

    /// <summary>
    /// The West string variable records the name of the Area within the North direction of the Area
    /// </summary>
    public string West { get; set; }

    /// <summary>
    /// The East string variable records the name of the Area within the North direction of the Area
    /// </summary>
    public string East { get; set; }


    public override string ToString()
    {
        return string.Format("[Area: Direction={0}, HasEnemy={1}, HasItem={2}, North={3}, South={4}, West={5}, East={6}", AreaName, HasEnemy, HasItem, North, South, West, East);
    }
}
