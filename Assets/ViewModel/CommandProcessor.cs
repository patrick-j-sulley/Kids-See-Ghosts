using System;
using UnityEngine;

/// <summary>
/// The aDisplayer function handles the displaying of specified string values from the command processor to the specified text field in unity
/// </summary>
/// <param name="value">
/// The value string variable is passed to the aDisplayer function and contains the text to be displayed
/// </param>
public delegate void aDisplayer(String value);

/// <summary>
/// The CommandProcessor class handles all the functions and variables related to the processing of all in game commands
/// </summary>
public class CommandProcessor
{
    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    //public DataService ds = new DataService("KSG_DB.db");

    /// <summary>
    /// The CommandProcessor function handles the instanstiation of the Command Processor
    /// </summary>
    public CommandProcessor()
    {

    }

    /// <summary>
    /// The Parse function handles the retrieval, processing and output of all commands inputted through the in game text field.
    /// </summary>
    /// <param name="pCmdStr">
    /// The pCmdStr string variable contains the command text that is inputted through the in game text field
    /// </param>
    /// <param name="display">
    /// The display aDisplayed variable contains the call to display for the output of the command
    /// </param>
    public void Parse(String pCmdStr, aDisplayer display)
    {
        String strResult = "Command Not Recognised"; //Message sent to the console whenever a command isn't recognised.

        char[] charSeparators = new char[] { ' ' };
        pCmdStr = pCmdStr.ToLower();
        String[] parts = pCmdStr.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries); // The tokenisation of the command

        // The processing of the tokens
        if (parts.Length > 1)
        {
            switch (parts[0])
            {
                case "pick": // Pick command identifier
                    Player lcPlayerPick = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

                    if (parts[1] == "up") // Up command extension identifier
                    {
                        AreaItem lcAreaItem = DataServiceManager.ds.Connection.Table<AreaItem>().Where(x => x.AreaName == lcPlayerPick.CurrentArea & x.PlayerUserEmail == lcPlayerPick.UserEmail).FirstOrDefault();

                        Debug.Log("Pick Up Command Received"); //Message sent to the console whenever a pick up command is successfully received.
                        if (lcAreaItem != null) //Check to determine if an area has an item to pick up.
                        {
                            GameManager.instance.gameModel.Pickup(lcAreaItem); //Call for the pickup item function
                            GameManager.instance.gameModel.AddItemStats(lcAreaItem);//Call for addition of item stats function
                            GameManager.instance.gameModel.RemoveItemFromArea(lcAreaItem); //Call for the remove item from area function
                            GameManager.instance.gameModel.PointsFromPickup();//Call for addition of points from pickup function
                            GameManager.instance.gameModel.PickUpStoryDisplay();//Call for display of pickup story
                            strResult = GameManager.instance.gameModel.StoryDisplay; //Story Result for whenever there is an item to pick up in the current area.
                        }
                        else
                        {
                            strResult = "There is nothing to pick up"; //Story Result for whenever there isn't an item to pick up in the current area.
                        }

                        if (parts.Length == 3)
                        {
                            String param = parts[2];
                        }
                    }

                    else
                    {
                        Debug.Log("Command Not Recognised"); //Message sent to the console whenever a command isn't recognised.
                        strResult = "Command Not Recognised";
                    }
                    display(strResult);
                    break;

                case "go": // Go command identifier

                    Player lcPlayerGo = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

                    switch (parts[1])
                    {
                        case "north": // North command extension identifier
                            Debug.Log("Go North Direction Received"); //Message sent to the console whenever a Go North command is successfully received.
                            GameManager.instance.gameModel.Go(GameModel.DIRECTION.North); //Calls for the Go North Direction Function
                            if (GameManager.instance.gameModel.AreaNull == false)
                            {
                                GameManager.instance.gameModel.AreaVisitCounter();//Calls for area visit counter function
                                GameManager.instance.gameModel.VisitStoryTypeCheck();//Calls for Visit Story Type check function
                                GameManager.instance.gameModel.VisitStoryDisplay();//Calls for Visit Story Display function
                                strResult = GameManager.instance.gameModel.StoryDisplay; //Story Result for the Area in the North direction from current area
                                GameManager.instance.gameModel.PlayerTravelDistance_XP_andPoints_Update();//Calls for Player Travel Distance, XP and Points update function
                                GameManager.instance.gameModel.PlayerXPCheck();//Calls for Player XP check function
                            }
                            else
                            {
                                strResult = "You are unable to move in that direction.";
                            }
                            break;

                        case "south": // South command extension identifier
                            Debug.Log("Go South Direction Received"); //Message sent to the console whenever a Go South command is successfully received.
                            GameManager.instance.gameModel.Go(GameModel.DIRECTION.South); //Calls for the Go South Direction Function
                            if (GameManager.instance.gameModel.AreaNull == false)
                            {
                                GameManager.instance.gameModel.AreaVisitCounter();//Calls for area visit counter function
                                GameManager.instance.gameModel.VisitStoryTypeCheck();//Calls for Visit Story Type check function
                                GameManager.instance.gameModel.VisitStoryDisplay();//Calls for Visit Story Display function
                                strResult = GameManager.instance.gameModel.StoryDisplay; //Story Result for the Area in the North direction from current area
                                GameManager.instance.gameModel.PlayerTravelDistance_XP_andPoints_Update();//Calls for Player Travel Distance, XP and Points update function
                                GameManager.instance.gameModel.PlayerXPCheck();//Calls for Player XP check function
                            }
                            else
                            {
                                strResult = "You are unable to move in that direction.";
                            }
                            break;

                        case "east": // East command extension identifier
                            Debug.Log("Go East Direction Received"); //Message sent to the console whenever a Go East command is successfully received.
                            GameManager.instance.gameModel.Go(GameModel.DIRECTION.East); //Calls for the Go East Direction Function
                            if (GameManager.instance.gameModel.AreaNull == false)
                            {
                                GameManager.instance.gameModel.AreaVisitCounter();//Calls for area visit counter function
                                GameManager.instance.gameModel.VisitStoryTypeCheck();//Calls for Visit Story Type check function
                                GameManager.instance.gameModel.VisitStoryDisplay();//Calls for Visit Story Display function
                                strResult = GameManager.instance.gameModel.StoryDisplay; //Story Result for the Area in the North direction from current area
                                GameManager.instance.gameModel.PlayerTravelDistance_XP_andPoints_Update();//Calls for Player Travel Distance, XP and Points update function
                                GameManager.instance.gameModel.PlayerXPCheck();//Calls for Player XP check function
                            }
                            else
                            {
                                strResult = "You are unable to move in that direction.";
                            }
                            break;

                        case "west": // West command extension identifier
                            Debug.Log("Go West Direction Received"); //Message sent to the console whenever a Go West command is successfully received.
                            GameManager.instance.gameModel.Go(GameModel.DIRECTION.West); //Calls for the Go West Direction Function
                            if (GameManager.instance.gameModel.AreaNull == false)
                            {
                                GameManager.instance.gameModel.AreaVisitCounter();//Calls for area visit counter function
                                GameManager.instance.gameModel.VisitStoryTypeCheck();//Calls for Visit Story Type check function
                                GameManager.instance.gameModel.VisitStoryDisplay();//Calls for Visit Story Display function
                                strResult = GameManager.instance.gameModel.StoryDisplay; //Story Result for the Area in the North direction from current area
                                GameManager.instance.gameModel.PlayerTravelDistance_XP_andPoints_Update();//Calls for Player Travel Distance, XP and Points update function
                                GameManager.instance.gameModel.PlayerXPCheck();//Calls for Player XP check function
                            }
                            else
                            {
                                strResult = "You are unable to move in that direction.";
                            }
                            break;

                        default:
                            Debug.Log("Unable to move in that direction."); //Message sent to the console whenever an invalid direction command is received
                            strResult = "You are unable to move in that direction."; //Story Result for whenever an invalid direction command is received
                            break;
                    }

                    display(strResult); //Display Story Result to User
                    break;


                case "attack": // attack command identifier

                    Player lcPlayerEnemy = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
                    AreaEnemy lcAreaEnemy = DataServiceManager.ds.Connection.Table<AreaEnemy>().Where(x => x.AreaName == lcPlayerEnemy.CurrentArea & x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

                    if (parts[1] == "enemy") // enemy command extension identifier
                    {
                        Debug.Log("Attack Command Received"); //Message sent to the console whenever an attack command is successfully received.

                        if (lcAreaEnemy != null) //Check to determine if an area has an enemy to attack.
                        {
                            Enemy lcEnemy = DataServiceManager.ds.Connection.Table<Enemy>().Where(x => x.ID == lcAreaEnemy.EnemyID).FirstOrDefault();

                            GameManager.instance.gameModel.AttackEnemy();//Call for attack enemy function

                            AreaEnemy lcAreaEnemy_PostAttack = DataServiceManager.ds.Connection.Table<AreaEnemy>().Where(x => x.AreaName == lcPlayerEnemy.CurrentArea & x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

                            strResult = "Fighting: " + lcEnemy.Name + "\n" + "Health Remaining: " + lcAreaEnemy_PostAttack.HpCurrent + " / " + lcEnemy.HpMax;//Display in game battle with enemy

                            if (lcAreaEnemy_PostAttack.HpCurrent <= 0)
                            {
                                Story lcEnemyStory = DataServiceManager.ds.Connection.Table<Story>().Where(x => x.AreaName == lcPlayerEnemy.CurrentArea & x.Type == "Enemy").FirstOrDefault();
                                strResult = lcEnemyStory.Description;//Display enemy slain story result
                                GameManager.instance.gameModel.PlayerEnemySlainXPReward();//Call for player enemy slain XP reward
                                GameManager.instance.gameModel.PlayerXPCheck();//Call for player XP check function
                                GameManager.instance.gameModel.EnemyDropItem();//Call for enemy drop item function
                                GameManager.instance.gameModel.RemoveEnemyFromArea();//Call for remove enemy from area function
                                GameManager.instance.gameModel.PlayerEnemySlain_Points_andEnemiesSlain_Update();//call for Player enemy slain points and enemies slain update function
                                GameManager.instance.gameModel.AreaVisitCounter();//Call for areavisitcounter function

                            }
                        }
                        else if (lcAreaEnemy == null)
                        {
                            strResult = "There are no Enemies to Attack"; //Story Result for whenever there isn't an enemy to attack in the current area.
                        }
                    }
                    display(strResult);
                    break;

                default:
                    Debug.Log("Command Not Recognised"); //Message sent to the console whenever a command isn't recognised.
                    strResult = "Command Not Recognised"; //Message sent to the console whenever a command isn't recognised.
                    display(strResult);
                    break;
            }
        }
        else
        {
            Debug.Log("Command Not Recognised"); //Message sent to the console whenever a command isn't recognised.
            strResult = "Command Not Recognised"; //Message sent to the console whenever a command isn't recognised.
            display(strResult);
        }
    }
}