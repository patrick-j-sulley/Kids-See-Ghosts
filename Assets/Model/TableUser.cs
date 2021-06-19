using SQLite4Unity3d;

/// <summary>
/// The User Class handles the variable that represents the User table within the connected SQLite Database. 
/// </summary>
public class User
{

    /// <summary>
    /// The UserEmail string variable records down the username for each unique user
    /// </summary>
	[PrimaryKey, NotNull]
    public string UserEmail { get; set; }

    /// <summary>
    /// The password string variable records down the password for the user.
    /// </summary>
    [NotNull]
    public string Password { get; set; }

    public override string ToString()
    {
        return string.Format("[User: UserEmail={0}, Password={1}", UserEmail, Password);
    }
}
