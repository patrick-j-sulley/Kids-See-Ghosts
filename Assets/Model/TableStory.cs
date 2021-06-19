using SQLite4Unity3d;

/// <summary>
/// The Story Class handles the variable that represents the Story table within the connected SQLite Database.
/// </summary>
public class Story
{

    /// <summary>
    /// The ID int variable records down the ID of each unique story entry
    /// </summary>
	[NotNull]
    public int ID { get; set; }

    /// <summary>
    /// The type string variable records down the type of the story entry
    /// </summary>
    [NotNull]
    public string Type { get; set; }

    /// <summary>
    /// The AreaName string variable records down the name of the area for the story entry
    /// </summary>
    [NotNull]
    public string AreaName { get; set; }

    /// <summary>
    ///  The description string variable records down the description to be displayed for the story entry.
    /// </summary>
    [NotNull]
    public string Description { get; set; }

    public override string ToString()
    {
        return string.Format("[Story: ID={0}, AreaName={1}, Description={2}", ID, AreaName, Description);
    }
}
