using System;
using UnityEngine;

/// <summary>
/// The GameModel Class handles all the variables, functions and game manipulation for each individual unique game instance.
/// </summary>
[Serializable]
public class GameModel
{
    /// <summary>
    /// The ds DataService Variable handles the instanstiation of the database connection.
    /// </summary>
    public static DataService ds;

    /// <summary>
    /// The DIRECTION enum variable handles the North, South, East & West directions to be used for movements made within the game
    /// </summary>
    public enum DIRECTION { North, South, East, West };

    /// <summary>
    /// The StoryVisitType string variable handles the recording of the current Visit Story Type for the area that the player is currently in
    /// within a game.
    /// </summary>
    private string _storyVisitType;

    /// <summary>
    /// The StoryDisplay string variable handles the recording of the Type of story that is to be displayed to a player.
    /// </summary>
    private string _storyDisplay;

    /// <summary>
    /// The AreaNull bool variable handles the recording of whether or not the player can move in the specified direction by recording whether 
    /// the area in that direction is null or not.
    /// </summary>
    private bool _areaNull;

    /// <summary>
    /// The GameModel instanstiation function handles the checking of whether a new or returning player is launching a game instance, as well as
    /// whether or not they're starting a new game or loading a previous game.
    /// </summary>
    public GameModel()
    {

        if (DataServiceManager.ds != null)
        {

            Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

            if (lcPlayer == null)
            {
                DataServiceManager.ds.CreateNewGame_NewPlayer();
            }
            else if (lcPlayer != null)
            {
                if (UserManager.UserManagerInstance.LoadGame == false)
                {
                    DataServiceManager.ds.CreateNewGame_OldPlayer(lcPlayer);
                }
                else if (UserManager.UserManagerInstance.LoadGame == true)
                {
                    AreaVisitCounter();
                    VisitStoryTypeCheck();
                    VisitStoryDisplay();
                }

            }

        }

    }

    /// <summary>
    /// The AreaVisitCounter function handles the recording of how many times a player has visited an area. It checks if the player has any
    /// entries for the specified area in the AreaVisit table, upon which if they don't have any, a new entry will be created. In the scenario 
    /// that the user has already visited the area, it just adds 1 to the number of times visited and updates the players entry in the AreaVisit
    /// table.
    /// </summary>
    public void AreaVisitCounter()
    {

        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        AreaVisit lcAreaVisit = DataServiceManager.ds.Connection.Table<AreaVisit>().Where(x => x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail & x.AreaName == lcPlayer.CurrentArea).FirstOrDefault();

        if (lcAreaVisit == null)
        {
            DataServiceManager.ds.FirstTimeAreaVisit(lcPlayer.CurrentArea);
        }
        else if (lcAreaVisit.Number >= 1)
        {

            lcAreaVisit.Number = lcAreaVisit.Number + 1;
            DataServiceManager.ds.Connection.Update(lcAreaVisit);

        }
    }

    /// <summary>
    /// The VisitStoryTypeCheck function handles the checking of what type of story that the StoryVisitType variable should be representing. If
    /// the user is visiting the area for the first time, this signifys a first visit story, otherwise, a return visit is signified.
    /// </summary>
    public void VisitStoryTypeCheck()
    {

        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        AreaVisit lcAreaVisit = DataServiceManager.ds.Connection.Table<AreaVisit>().Where(x => x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail & x.AreaName == lcPlayer.CurrentArea).FirstOrDefault();

        if (lcAreaVisit.Number == 1)
        {
            _storyVisitType = "FirstVisit";
        }
        else if (lcAreaVisit.Number > 1)
        {
            _storyVisitType = "ReturnVisit";

        }

    }

    /// <summary>
    /// The VisitStoryDisplay function handles the displaying of the story text based on the players current area and visit type. It does this by
    /// retrieving the description from the Story table that contains the correct area and visit type
    /// </summary>
    public void VisitStoryDisplay()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        Story lcStory = DataServiceManager.ds.Connection.Table<Story>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.Type == _storyVisitType).FirstOrDefault();
        _storyDisplay = lcStory.Description;



    }

    /// <summary>
    /// The Go function handles the collection of the direction that the user inputted after their Go command that they put into the text field
    /// within the game.
    /// </summary>
    /// <param name="pDirection">
    /// The pDirection variable is collected by the Go function and then passed to the Move function.
    /// </param>
    public void Go(DIRECTION pDirection)
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        Move(pDirection);

    }

    /// <summary>
    /// The Move function handles the collection and usage of the direction variable. It takes the direction variable and checks for whether or not
    /// the direction leads into a valid area for the player. If an area is valid, it will update the players current area, otherwise, it will set
    /// the AreaNull variable to true
    /// </summary>
    /// <param name="pDirection">
    /// The pDirection variable is collected by the Move function and used to check for the area that is in that direction for the player.
    /// </param>
    public void Move(GameModel.DIRECTION pDirection)
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        Area lcCurrentArea = DataServiceManager.ds.Connection.Table<Area>().Where(x => x.AreaName == lcPlayer.CurrentArea).FirstOrDefault();


        switch (pDirection)
        {
            case GameModel.DIRECTION.North:

                if (lcCurrentArea.North != null)
                {
                    _areaNull = false;
                    lcPlayer.CurrentArea = lcCurrentArea.North;
                    DataServiceManager.ds.Connection.Update(lcPlayer);
                }
                else
                {
                    _areaNull = true;
                }
                break;

            case GameModel.DIRECTION.South:
                if (lcCurrentArea.South != null)
                {
                    _areaNull = false;
                    lcPlayer.CurrentArea = lcCurrentArea.South;
                    DataServiceManager.ds.Connection.Update(lcPlayer);
                }
                else
                {
                    _areaNull = true;
                }
                break;

            case GameModel.DIRECTION.East:
                if (lcCurrentArea.East != null)
                {
                    _areaNull = false;
                    lcPlayer.CurrentArea = lcCurrentArea.East;
                    DataServiceManager.ds.Connection.Update(lcPlayer);
                }
                else
                {
                    _areaNull = true;
                }
                break;

            case GameModel.DIRECTION.West:
                if (lcCurrentArea.West != null)
                {
                    _areaNull = false;
                    lcPlayer.CurrentArea = lcCurrentArea.West;
                    DataServiceManager.ds.Connection.Update(lcPlayer);
                }
                else
                {
                    _areaNull = true;
                }
                break;

        }
    }

    /// <summary>
    /// The Pickup Function handles the collection of data for the player picking up an item for the area that they are currently within. 
    /// It collects a passed through Item variable and passes the ItemID and Player UserEmail to the Pick Up item function in the Dataservice class.
    /// </summary>
    /// <param name="lcAreaItem">
    /// The lcAreaItem AreaItem variable is collected by the Pickup function to then be sent to the PickUpItem function in the Dataservice class.
    /// </param>
    public void Pickup(AreaItem lcAreaItem)
    {
        DataServiceManager.ds.PickUpItem(lcAreaItem.ItemID, lcAreaItem.PlayerUserEmail);
    }

    /// <summary>
    /// The RemoveItemFromArea function handles the collection of data for the removal on an item from an area. It collects a passed through 
    /// AreaItem variable and calls for it to be deleted from the connected database.
    /// </summary>
    /// <param name="lcAreaItem">
    /// The lcAreaItem AreaItem variable is collected by the RemoveItemFromArea function to then be deleted from the connected database.
    /// </param>
    public void RemoveItemFromArea(AreaItem lcAreaItem)
    {
        DataServiceManager.ds.Connection.Delete(lcAreaItem);
    }

    /// <summary>
    /// The PointsFromPickup function handles the distribution of points to a Player upon them picking up an item. It specifys the current player
    /// adds 10 points to their overall score and calls to the database to update their points column from their row.
    /// </summary>
    public void PointsFromPickup()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        lcPlayer.Points = lcPlayer.Points + 10;
        DataServiceManager.ds.Connection.Update(lcPlayer);
    }

    /// <summary>
    /// The PickUpStoryDisplay function handles the displaying of the story text based on the item that the player picked up and the area that they
    /// picked it up from. It does this by changing the StoryVisitType to Item and collecting the description from the specified story row.
    /// </summary>
    public void PickUpStoryDisplay()
    {
        _storyVisitType = "Item";
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        Story lcStory = DataServiceManager.ds.Connection.Table<Story>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.Type == _storyVisitType).FirstOrDefault();
        _storyDisplay = lcStory.Description;

    }

    /// <summary>
    /// The AttackEnemy function handles the attacking of an enemy by a player within the game. It does this by collecting the specified enemy from
    /// the area that the player is currently within, and subtracting the players current health based on what the enemies attack damage is. In turn
    /// the enemies area instance will have its current health removed with the same formula the other way round. Both specified rows are updated
    /// within the connected database.
    /// </summary>
    public void AttackEnemy()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        AreaEnemy lcAreaEnemy = DataServiceManager.ds.Connection.Table<AreaEnemy>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        Enemy lcEnemy = DataServiceManager.ds.Connection.Table<Enemy>().Where(x => x.ID == lcAreaEnemy.EnemyID).FirstOrDefault();

        lcAreaEnemy.HpCurrent -= lcPlayer.DamageAmount;
        lcPlayer.HpCurrent -= lcEnemy.DamageAmount;

        DataServiceManager.ds.Connection.Update(lcAreaEnemy);
        DataServiceManager.ds.Connection.Update(lcPlayer);

    }

    /// <summary>
    /// The AddItemStats function collects a passed through area item and collects the HP and Damage Bonus stats attachted to it, and adds them to
    /// the players current stats. After adding the numbers up, the players specified row within the connected database will be updated
    /// </summary>
    /// <param name="lcAreaItem">
    /// The lcAreaItem AreaItem variable is collected by the AddItemStats function to retrieve the bonuses to be added to the players stats.
    /// </param>
    public void AddItemStats(AreaItem lcAreaItem)
    {
        Item lcItem = DataServiceManager.ds.Connection.Table<Item>().Where(x => x.ID == lcAreaItem.ItemID).FirstOrDefault();
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        lcPlayer.HpMax += lcItem.HealthBonus;
        lcPlayer.HpCurrent = lcPlayer.HpMax;
        lcPlayer.DamageAmount += lcItem.DamageBonus;

        DataServiceManager.ds.Connection.Update(lcPlayer);
    }

    /// <summary>
    /// The EnemyDropItem function handles the dropping of an item from an enemy that has just been defeated, into the current area that a player is in.
    /// It does this by collecting the Enemy Item from the specified enemy, adding it as a new entry into the AreaItem table, and then deleteing it from
    /// the specified enemy item row within the database
    /// </summary>
    public void EnemyDropItem()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        AreaEnemy lcAreaEnemy = DataServiceManager.ds.Connection.Table<AreaEnemy>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.PlayerUserEmail == lcPlayer.UserEmail).FirstOrDefault();
        Enemy lcEnemy = DataServiceManager.ds.Connection.Table<Enemy>().Where(x => x.ID == lcAreaEnemy.EnemyID).FirstOrDefault();
        EnemyItem lcEnemyItem = DataServiceManager.ds.Connection.Table<EnemyItem>().Where(x => x.EnemyID == lcAreaEnemy.EnemyID & x.PlayerUserEmail == lcPlayer.UserEmail).FirstOrDefault();
        Item lcBaseItem = DataServiceManager.ds.Connection.Table<Item>().Where(x => x.ID == lcEnemyItem.ItemID).FirstOrDefault();

        DataServiceManager.ds.Connection.InsertAll(new[]{
            new AreaItem{
                AreaName = lcPlayer.CurrentArea,
                ItemID = lcEnemyItem.ItemID,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail
            }
            });

        DataServiceManager.ds.Connection.Delete(lcEnemyItem);

    }

    /// <summary>
    /// The RemoveEnemyFromArea function handles the removal of a recently slain enemy from an area. It does this by collecting the specified Area
    /// Enemy from an Area and calling to the connected database for it to be removed.
    /// </summary>
    public void RemoveEnemyFromArea()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        AreaEnemy lcAreaEnemy = DataServiceManager.ds.Connection.Table<AreaEnemy>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.PlayerUserEmail == lcPlayer.UserEmail).FirstOrDefault();

        DataServiceManager.ds.Connection.Delete(lcAreaEnemy);

    }

    /// <summary>
    /// The PlayerEnemySlain_Points_andEnemiesSlain_Update function handles the distribution of Points and addition to the Enemies Slain counter
    /// for each player. It does this by collecting the specified player, adding to their points and enemies slain counter, and calling to the
    /// connected database to update the specified player row.
    /// </summary>
    public void PlayerEnemySlain_Points_andEnemiesSlain_Update()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        lcPlayer.EnemiesSlain += 1;
        lcPlayer.Points += 100;
        DataServiceManager.ds.Connection.Update(lcPlayer);

    }

    /// <summary>
    /// The PlayerTravelDistance_XP_andPoints_Update function handles the distribution of addition to the travel distance counter, XP counter, and
    /// points counter for each player. It does this by collecting the specified player, adding to their distance travelled, points and XP, and then
    /// calling to the connected database to update the specified player row.
    /// </summary>
    public void PlayerTravelDistance_XP_andPoints_Update()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        lcPlayer.DistanceTravelled += 1.00;
        lcPlayer.Points += 1;
        lcPlayer.XpCurrent += 1;
        DataServiceManager.ds.Connection.Update(lcPlayer);

    }

    /// <summary>
    /// The PlayerEnemySlainXPReward function handles the distribution of XP to be rewarded to a player upon slaying an enemy. It does this by
    /// collecting the Player and AreaEnemy, adding to the players XP counter the amount of XP reward that the enemy has, and then calling to the
    /// connected database to update the specified player row.
    /// </summary>
    public void PlayerEnemySlainXPReward()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        AreaEnemy lcAreaEnemy = DataServiceManager.ds.Connection.Table<AreaEnemy>().Where(x => x.AreaName == lcPlayer.CurrentArea & x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();
        Enemy lcEnemy = DataServiceManager.ds.Connection.Table<Enemy>().Where(x => x.ID == lcAreaEnemy.EnemyID).FirstOrDefault();

        lcPlayer.XpCurrent += lcEnemy.XpRewardAmount;
        DataServiceManager.ds.Connection.Update(lcPlayer);
    }

    /// <summary>
    /// The PlayerXPCheck function handles the checking of a Players current XP amount to determine whether they are eligible to level up or not.
    /// It does this by collecting the player, checking their XP amount to see if it's 100 or more. Upon which if it is, will take away 100 XP,
    /// Add 1 to the players level counter, call for an update of the player table in the connected database, and then call for the
    /// PlayerLvlUpBounses function.
    /// </summary>
    public void PlayerXPCheck()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        if (lcPlayer.XpCurrent >= 100)
        {
            lcPlayer.XpCurrent -= 100;
            lcPlayer.LvlCurrent += 1;
            DataServiceManager.ds.Connection.Update(lcPlayer);
            PlayerLvlUpBonuses();
        }
    }

    /// <summary>
    /// The PlayerLvlUpBonuses Function handles the distribution of level up bonuses that a player will receive upon levelling up. This is done by
    /// collecting the player, adding to their HP the level up health bonus, setting their current hp to max hp, adding their Damage to the level
    /// up damage bonus, doubling their level up bonus amount and then calling for the specified player row in the database to be updated.
    /// </summary>
    public void PlayerLvlUpBonuses()
    {
        Player lcPlayer = DataServiceManager.ds.Connection.Table<Player>().Where(x => x.UserEmail == UserManager.UserManagerInstance.UserEmail).FirstOrDefault();

        lcPlayer.HpMax += lcPlayer.LvlUpHealthBonus;
        lcPlayer.HpCurrent = lcPlayer.HpMax;
        lcPlayer.DamageAmount += lcPlayer.LvlUpDamageBonus;

        lcPlayer.LvlUpHealthBonus = lcPlayer.LvlUpHealthBonus * 2;
        lcPlayer.LvlUpDamageBonus = lcPlayer.LvlUpDamageBonus * 2;

        DataServiceManager.ds.Connection.Update(lcPlayer);
    }

    /// <summary>
    /// The Getter and Setter for the StoryVisitType variable
    /// </summary>
    public string StoryVisitType
    {
        get
        {
            return _storyVisitType;
        }

        set
        {
            _storyVisitType = value;
        }
    }

    /// <summary>
    /// The Getter and Setter for the StoryDisplay variable
    /// </summary>
    public string StoryDisplay
    {
        get
        {
            return _storyDisplay;
        }

        set
        {
            _storyDisplay = value;
        }
    }

    /// <summary>
    /// The Getter and Setter for the AreaNull variable
    /// </summary>
    public bool AreaNull
    {
        get
        {
            return _areaNull;
        }

        set
        {
            _areaNull = value;
        }
    }

}