using SQLite4Unity3d;
using UnityEngine;
using System.Linq;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
/// <summary>
/// The DataService Class handles all connections made to and from the connected Database.
/// </summary>
public class DataService
{

    private SQLiteConnection _connection;
    /// <summary>
    /// The DataService instanstiation function handles the determining of the location of the SQLite database file depending on the device the application is on,
    /// which it does by using the DatabaseName string variable as well as a dbPath variable.
    /// </summary>
    /// 
    /// <param name="DatabaseName">
    /// The DatabaseName string variable that contains the name of the SQLite.DB file, this is passed in when a new DataSerivce instance is instanstiated
    /// </param>
    /// 
    public DataService(string DatabaseName)
    {

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

    }
    /// <summary>
    /// The DoesDBExist Bool is to determine whether there is already a pre-existing connected SQLite database file connected or not. It does
    /// this by checking whether or not any rows are returned when it runs a query to print out a list of rows from the Area Table. If it is 
    /// unable to do so, this means that the connected DB does not exist and a false is returned.
    /// </summary>
    /// <returns>True = DB does exist, False = DB doesn't exist.</returns>
    public bool DoesDBExist()
    {
        try
        {   ///lcRow is an int variable that returns the row count of the Area Table
            int lcRows = _connection.Table<Area>().ToList<Area>().Count;
            if (lcRows >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///A Try Catch that catches an SQLiteException error to prevent the application crashing.
        catch (SQLiteException)
        {
            return false;
        }
    }
    /// <summary>
    /// The CreateDB Function handles all of the base Table creations, as well as the base game insert statements (Areas, Stories, Items & Enemies).
    /// A base game table is one that does not have any of its data modified by any in game actions.
    /// </summary>
    public void CreateDB()
    {

        ///Drop & Creation of the User Table
        _connection.DropTable<User>();
        _connection.CreateTable<User>();

        ///Drop & Creation of the Area Table
        _connection.DropTable<Area>();
        _connection.CreateTable<Area>();

        ///Drop & Creation of the AreaEnemy Table
        _connection.DropTable<AreaEnemy>();
        _connection.CreateTable<AreaEnemy>();

        ///Drop & Creation of the AreaItem Table
        _connection.DropTable<AreaItem>();
        _connection.CreateTable<AreaItem>();

        ///Drop & Creation of the AreaVisit Table
        _connection.DropTable<AreaVisit>();
        _connection.CreateTable<AreaVisit>();

        ///Drop & Creation of the Enemy Table
        _connection.DropTable<Enemy>();
        _connection.CreateTable<Enemy>();

        ///Drop & Creation of the EnemyItem Table
        _connection.DropTable<EnemyItem>();
        _connection.CreateTable<EnemyItem>();

        ///Drop & Creation of the InventoryItem Table
        _connection.DropTable<InventoryItem>();
        _connection.CreateTable<InventoryItem>();

        ///Drop & Creation of the Item Table
        _connection.DropTable<Item>();
        _connection.CreateTable<Item>();

        ///Drop & Creation of the Player Table
        _connection.DropTable<Player>();
        _connection.CreateTable<Player>();

        ///Drop & Creation of the Story Table
        _connection.DropTable<Story>();
        _connection.CreateTable<Story>();

        ///Insert All Areas
        _connection.InsertAll(new[]{

            new Area{
                AreaName = "Thou Sand Beach",
                HasEnemy = false,
                HasItem = false,
                North = "The Heartless Hills",
                South = "The Jay Sea",
                East = null,
                West = "The Roc-A-Fella Rocks"

            },

            new Area{
                AreaName = "The Jay Sea",
                HasEnemy = false,
                HasItem = false,
                North = "Thou Sand Beach",
                South = null,
                East = null,
                West = null
            },

            new Area{
                AreaName = "The Heartless Hills",
                HasEnemy = false,
                HasItem = true,
                North = "The New Workout Plain",
                South = "Thou Sand Beach",
                East = "Chi Town",
                West = "Franks Track"
            },

            new Area{
                AreaName = "The Roc-A-Fella Rocks",
                HasEnemy = false,
                HasItem = true,
                North = "Franks Track",
                South = null,
                East = "Thou Sand Beach",
                West = null
            },

            new Area{
                AreaName = "Franks Track",
                HasEnemy = false,
                HasItem = false,
                North = "The Beach of Pablo",
                South = "The Roc-A-Fella Rocks",
                East = "The Heartless Hills",
                West = null
            },

            new Area{
                AreaName = "Chi Town",
                HasEnemy = false,
                HasItem = true,
                North = "Ultralight Streams",
                South = null,
                East = "Impregnate Bridge",
                West = "The Heartless Hills"
            },

            new Area{
                AreaName = "The New Workout Plain",
                HasEnemy = false,
                HasItem = false,
                North = "Blood Leaf Valley",
                South = "The Heartless Hills",
                East = "Jesus Walks",
                West = "The Beach of Pablo"
            },

            new Area{
                AreaName = "Impregnate Bridge",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = "The No-Bodi-Fukinwitma Cliffs",
                West = "Chi Town"
            },

            new Area{
                AreaName = "The No-Bodi-Fukinwitma Cliffs",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = "The On Site",
                West = "Impregnate Bridge"
            },

            new Area{
                AreaName = "The On Site",
                HasEnemy = false,
                HasItem = false,
                North = "Church In The Wild",
                South = null,
                East = "All Bay",
                West = "The No-Bodi-Fukinwitma Cliffs"
            },

            new Area{
                AreaName = "All Bay",
                HasEnemy = false,
                HasItem = false,
                North = "Say-You Well",
                South = null,
                East = "Bittersweet Poe-Sea",
                West = "The On Site"
            },

            new Area{
                AreaName = "Bittersweet Poe-Sea",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = null,
                West = "All Bay"
            },

            new Area{
                AreaName = "Say-You Well",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = "All Bay",
                East = null,
                West = "Church In The Wild"
            },

            new Area{
                AreaName = "Church In The Wild",
                HasEnemy = false,
                HasItem = true,
                North = null,
                South = "The On Site",
                East = "Say-You Well",
                West = "Hol-Mai River"
            },

            new Area{
                AreaName = "Hol-Mai River",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = "Church In The Wild",
                West = "Gold Digger Mines"
            },

            new Area{
                AreaName = "Gold Digger Mines",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = "Hol-Mai River",
                West = "Ultralight Streams"
            },

            new Area{
                AreaName = "Ultralight Streams",
                HasEnemy = false,
                HasItem = false,
                North = "Jesus Walks",
                South = "Chi Town",
                East = "Gold Digger Mines",
                West = "The New Workout Plain"
            },

            new Area{
                AreaName = "Jesus Walks",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = "Ultralight Streams",
                East = "Blame Lane",
                West = "Blood Leaf Valley"
            },

            new Area{
                AreaName = "Blame Lane",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = null,
                West = "Jesus Walks"
            },

            new Area{
                AreaName = "Blood Leaf Valley",
                HasEnemy = false,
                HasItem = false,
                North = "Guilt Pits",
                South = "The New Workout Plain",
                East = "Jesus Walks",
                West = null
            },

            new Area{
                AreaName = "The Beach of Pablo",
                HasEnemy = false,
                HasItem = false,
                North = "N.M.P.I.L Bay",
                South = "Franks Track",
                East = "The New Workout Plain",
                West = "X-T-Sea"
            },

            new Area{
                AreaName = "X-T-Sea",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = null,
                East = "The Beach of Pablo",
                West = null
            },

            new Area{
                AreaName = "N.M.P.I.L Bay",
                HasEnemy = false,
                HasItem = false,
                North = null,
                South = "The Beach of Pablo",
                East = "Don-Lemme-Getinmai Zone",
                West = null
            },

            new Area{
                AreaName = "Don-Lemme-Getinmai Zone",
                HasEnemy = false,
                HasItem = true,
                North = null,
                South = null,
                East = null,
                West = "N.M.P.I.L Bay"
            },

            new Area{
                AreaName = "Guilt Pits",
                HasEnemy = false,
                HasItem = false,
                North = "Ghost Town",
                South = "Blood Leaf Valley",
                East = null,
                West = null
            },

             new Area{
                AreaName = "Ghost Town",
                HasEnemy = true,
                HasItem = false,
                North = null,
                South = "Guilt Pits",
                East = null,
                West = null
            },




        });

        ///Insert All Stories
        _connection.InsertAll(new[]{

            new Story{
                ID = 1,
                Type = "FirstVisit",
                AreaName = "Thou Sand Beach",
                Description = "Welcome, To Kids See Ghosts. You have awoken in the 4th dimension on Thou Sand Beach, Pick your first move based on the map by typing in 'Go', followed up by either North, South, West or East. If an Area has an Enemy, type 'Attack Enemy' to hit an attack on them. If an Area has an Item, type 'Pick Up' to add it to your inventory. And finally,This Ghost Boy 3000 will alert you of your whereabouts and current state throughout your journey. Good Luck!"

            },

            new Story{
                ID = 2,
                Type = "ReturnVisit",
                AreaName = "Thou Sand Beach",
                Description = "You returned to your starting location, Thou Sand Beach. There's still nothing but sand here."

            },

            new Story{
                ID = 3,
                Type = "FirstVisit",
                AreaName = "The Jay Sea",
                Description = "You stumble into the water that appears to be the Jay-Sea. However, you don't see anywhere close out in the distance so you doubt your abilty to get out of here by swimming."

            },

            new Story{
                ID = 4,
                Type = "ReturnVisit",
                AreaName = "The Jay Sea",
                Description = "You have returned to the waters of The Jay Sea, still just as wet and nothing but water in the distance as far as the eye can see."

            },

            new Story{
                ID = 5,
                Type = "FirstVisit",
                AreaName = "The Heartless Hills",
                Description = "You have made your way up north to The Heartless Hills, you notice a note on the ground, pick it up?"

            },

            new Story{
                ID = 6,
                Type = "ReturnVisit",
                AreaName = "The Heartless Hills",
                Description = "You have returned to The Heartless Hills, still carries the heartbreaking aura that it carried on your first visit."

            },

            new Story{
                ID = 7,
                Type = "Item",
                AreaName = "The Heartless Hills",
                Description = "The Note Reads: ''iN tHe NiGhT i HeAr Em TaLK, tHe CoLdEsT sToRy EvEr ToLd. SoMeWhErE fAr AlOnG tHiS RoAd hE lOsT hIs SoUl, He LoSt HiS sOuL tO a WoMaN sO hEaRtLeSs''." + "You put the note in your inventory and decide upon your next movement."

            },

            new Story{
                ID = 8,
                Type = "FirstVisit",
                AreaName = "The Roc-A-Fella Rocks",
                Description = "You have arrived at The Roc-A-Fella Rocks. Perhaps some rock searching might be of use?"

            },

            new Story{
                ID = 9,
                Type = "ReturnVisit",
                AreaName = "The Roc-A-Fella Rocks",
                Description = "You have returned to the Rock-A-Fella Rocks, not much to find here as you already searched through the rocks."

            },

            new Story{
                ID = 10,
                Type = "Item",
                AreaName = "The Roc-A-Fella Rocks",
                Description = "You have picked up the Diamond From Sierra Leone, You throw it up in the air after feeling its vibe but luckily catch it on the way back down. You add it to your inventory before deciding on your next move."

            },

            new Story{
                ID = 11,
                Type = "FirstVisit",
                AreaName = "Franks Track",
                Description = "You have arrived at the entry point to Franks Track. You see a sign that reads: 'Up North: The Beach of Pablo, Down South: Roc-A-Fella Rocks.' Which direction do you choose to go in?"

            },

            new Story{
                ID = 12,
                Type = "ReturnVisit",
                AreaName = "Franks Track",
                Description = "You have returned to the entry point to Franks Track. Still remains is the sign that reads: 'Up North: The Beach of Pablo, Down South: Roc-A-Fella Rocks.' Which direction do you choose to go in?"

            },

            new Story{
                ID = 13,
                Type = "FirstVisit",
                AreaName = "Chi Town",
                Description = "You have arrived at Chi Town, a small little abandoned town that appears to be empty and carries an awfully haunted atmosphere. Choose your next move."

            },

            new Story{
                ID = 14,
                Type = "ReturnVisit",
                AreaName = "Chi Town",
                Description = "You have returned to Chi Town, a Homecoming of sorts as you think about it now and then."

            },

            new Story{
                ID = 15,
                Type = "Item",
                AreaName = "Chi Town",
                Description = "You notice a baseball cap with a '3' on it on the ground and pick it up. Chance's Hat has been added to your inventory. You then decide on your next move"

            },

            new Story{
                ID = 16,
                Type = "FirstVisit",
                AreaName = "The New Workout Plain",
                Description = "You have arrived at The New Workout Plain. You see a valley out in the far far distance that looks to be quite the potential journey. Do you wish to keep moving forward North?"

            },

            new Story{
                ID = 17,
                Type = "ReturnVisit",
                AreaName = "The New Workout Plain",
                Description = "You have returned to The New Workout Plain, maybe you've done everything there is to do here, maybe you can work it out? work it out?"

            },

            new Story{
                ID = 18,
                Type = "FirstVisit",
                AreaName = "Impregnate Bridge",
                Description = "You have arrived at the Impregnate Bridge. Odd name aside, according to your map, up east past this crossing are the No-Bodi-Fukinwitma Cliffs. Do you wish to keep moving forward?"

            },

            new Story{
                ID = 19,
                Type = "ReturnVisit",
                AreaName = "Impregnate Bridge",
                Description = "You have once again stumbled upon the Impregnate Bridge. Cover up next time perhaps?"

            },

            new Story{
                ID = 20,
                Type = "FirstVisit",
                AreaName = "The No-Bodi-Fukinwitma Cliffs",
                Description = "You have arrived at the No-Bodi-Fukinwitma Cliffs. Watch your step as you make your next move, or it may all fall down."

            },

            new Story{
                ID = 21,
                Type = "ReturnVisit",
                AreaName = "The No-Bodi-Fukinwitma Cliffs",
                Description = "You have returned to the No-Bodi-Fukinwitma Cliffs. Just like the first time, all you see are Cliffs, Cliffs, Cliffs, Cliffs, Cliffs."

            },

            new Story{
                ID = 22,
                Type = "FirstVisit",
                AreaName = "The On Site",
                Description = "You have arrived on site at the, well, 'on site'. Up North is the Church in the Wild, while further East is All Bay. What is your next move?"

            },

            new Story{
                ID = 23,
                Type = "ReturnVisit",
                AreaName = "The On Site",
                Description = "You have returned on site, at the, well, 'on site'. Much like your first visit, you're still getting this thing shaking like parkinsons.'"

            },

            new Story{
                ID = 24,
                Type = "FirstVisit",
                AreaName = "All Bay",
                Description = "You have arrived at All Bay, apart from the nice view, a path up north to the See-You Well, and an entry point to the Bittersweet Poe-Sea further east, you notice a sign that reads the following: ''South, South, South Side! All Bay.'' This odd sign confuses you before you decide your next move."

            },

            new Story{
                ID = 25,
                Type = "ReturnVisit",
                AreaName = "All Bay",
                Description = "You have returned to All Bay, even on a ReturnVisit, the aura of this place makes you feel like you've been balling all day."

            },

            new Story{
                ID = 26,
                Type = "FirstVisit",
                AreaName = "Bittersweet Poe-Sea",
                Description = "Upon getting your feet wet, you notice that out in the distance you are still unable to spot any land out in the distance. The idea of swimming out this way has got you thinking to yourself ''This is gonna be the the death of me.'' Upon which you decide that this is not the way to go."

            },

            new Story{
                ID = 27,
                Type = "ReturnVisit",
                AreaName = "Bittersweet Poe-Sea",
                Description = "Once again you stumble into the waters of the Bittersweet Poe-Sea, once again you think to yourself 'I don't want you, I don't need you. I love you and hate you at the very same time.'"

            },

            new Story{
                ID = 28,
                Type = "FirstVisit",
                AreaName = "Say-You Well",
                Description = "You have arrived at the Say-You Well, At this stage in your journey, you'll already contemplating taking off your cool and losing control. However you feel that quitting now would be a bad move, so you snap out of it and deciding upon your next move."

            },

            new Story{
                ID = 29,
                Type = "ReturnVisit",
                AreaName = "Say-You Well",
                Description = "You have returned to the Say-You Well, once again you are contemplating taking of your cool and losing control, but once again you are able to snap back out of it."

            },

            new Story{
                ID = 30,
                Type = "FirstVisit",
                AreaName = "Church In The Wild",
                Description = "You have arrived at the Church in The Wild, and notice a man approaching you. He says ''Human beings in a mob, What's a mob to a king? What's a king to a god? What's a god to a non-believer, who don't believe in anything? Will he make it out alive? Alright, alright, no church in the wild...Son, I am Father Ocean, you have a long journey ahead of you, you may need this more than I.''"

            },

            new Story{
                ID = 31,
                Type = "ReturnVisit",
                AreaName = "Church In The Wild",
                Description = "You have returned to the Church in The Wild, seaking help. However, Father Ocean appears to be nowhere in sight and there seem to be no signs of him still being present. Scary."

            },

            new Story{
                ID = 32,
                Type = "Item",
                AreaName = "Church In The Wild",
                Description = "Father Ocean has handed you his scepter, of which you have added into your inventory. He then tells you: ''This land, It’s something that the pastor don’t preach, It’s something that a teacher can’t teach. I wish you luck in your journeys ahead, son.'' You thank him and decide upon your next move."

            },

            new Story{
                ID = 33,
                Type = "FirstVisit",
                AreaName = "Hol-Mai River",
                Description = "You have arrived at the Hol-Mai river and proceed to rest under a tree to catch up on some much needed rest. After awakening again, you think to yourself ''Bitch, I'm back out my coma'' and proceed to make your next move."

            },

            new Story{
                ID = 34,
                Type = "ReturnVisit",
                AreaName = "Hol-Mai River",
                Description = "You have returned to Hol-Mai river, and once again, you locate the tree in which you took a nap under the first time you were here. And once again, upon waking up, you think to yourself ''Bitch, I'm back out my coma''"

            },

            new Story{
                ID = 35,
                Type = "FirstVisit",
                AreaName = "Gold Digger Mines",
                Description = "You have arrived at the entry point to the Gold Digger Mines after what has felt like 18 years, 18 years. However, the entry has been locked and sealed, a triflin' end indeed. You then say to yourself ''I gotta leave, get down, go head, get down.'' before deciding on your next move."

            },

            new Story{
                ID = 36,
                Type = "ReturnVisit",
                AreaName = "Gold Digger Mines",
                Description = "You have returned to the entry point of the Gold Digger mines, of which is still locked from outside access. Still remains in the area is a Broke Digger, that upon being fixed could be a Gold Digger."

            },

            new Story{
                ID = 37,
                Type = "FirstVisit",
                AreaName = "Ultralight Streams",
                Description = "You have arrived at the Ultralight Streams, this is a god dream, this is everything. The aura surround you in this area has you feeling blessed and feeling faithful in your journey ahead."

            },

            new Story{
                ID = 38,
                Type = "ReturnVisit",
                AreaName = "Ultralight Streams", //CONTINUE MAKING STORY TABLE INSERTS
                Description = "You have returned to the Ultralight Streams, once again, THIS IS AN ULTRALIGHT STREAM! THIS IS A GOD DREAM! THIS IS EVERYTHING!"

            },

            new Story{
                ID = 39,
                Type = "FirstVisit",
                AreaName = "Jesus Walks",
                Description = "You have arrived at the entry point for Jesus Walks. Unfortunately the sign containing path directions for the walks are mostly faded and you can only decipher out the location names. Those being Blame Lane, Blood Leaf Valley and TBD. Thought you are not religious, you get down to your knees and say to yourself ''God, show me the way because the Devil's tryna break me down. The only thing that I pray is that my feet don't fail me now.'' You then get back up onto your feet and decide on which direction you're going to test your faith in."

            },

            new Story{
                ID = 40,
                Type = "ReturnVisit",
                AreaName = "Jesus Walks",
                Description = "You have returned to the entry point for Jesus Walks. Unfortunately the sign containing path directions for the walks are still faded and you can still only decipher out the location names. Those being Blame Lane, Blood Leaf Valley and TBD. Thought you are not religious, you get down to your knees and say to yourself ''God, show me the way because the Devil's tryna break me down. The only thing that I pray is that my feet don't fail me now.'' You then get back up onto your feet and decide on which direction you're going to test your faith in."

            },

            new Story{
                ID = 41,
                Type = "FirstVisit",
                AreaName = "Blame Lane",
                Description = "You have arrived at the Blame Lane. You say to yourself: ''Let's walk the Blame Lane, I love you more, Let's walk the Blame Lane, for sure.''. Confused as to why you said this, you quickly come to your senses and decide upon your next move."

            },

            new Story{
                ID = 42,
                Type = "ReturnVisit",
                AreaName = "Blame Lane",
                Description = "Once again, you have returned to the Blame Lane. Once again you say to yourself: ''Let's walk the Blame Lane, I love you more, Let's walk the Blame Lane, for sure.''."

            },

            new Story{
                ID = 43,
                Type = "FirstVisit",
                AreaName = "Blood Leaf Valley",
                Description = "You have arrived at Blood Leaf Valley, and immediately notice its haunted aura. Strange fruit hanging from the poplar trees, with blood on the leaves. This setting strikes an impulse of fear into you. You think to yourself ''I know there ain't anything wrong with me, something strange is happening.''. You muster up the required courage and prepare yourself for your next movement."

            },

            new Story{
                ID = 44,
                Type = "ReturnVisit",
                AreaName = "Blood Leaf Valley",
                Description = "You have returned to the Blood Leaf Valley, and once again are haunted by the Strange fruit hanging from the poplar trees, with blood on the leaves."

            },

            new Story{
                ID = 45,
                Type = "FirstVisit",
                AreaName = "The Beach of Pablo",
                Description = "You have arrived at The Beach of Pablo, it's somewhat of a mess, but it's a nice mess with good variety and some real quality to it. You enjoy the scenary for a bit before deciding on your next movement."

            },

            new Story{
                ID = 46,
                Type = "ReturnVisit",
                AreaName = "The Beach of Pablo",
                Description = "You've returned to The Beach of Pablo. Upon your ReturnVisit to the area, the beach has grown more on you than your first visit to the area. You've come to appreciate it as a whole and despite the mess, you enjoy the variety of themes it offers."

            },

            new Story{
                ID = 47,
                Type = "FirstVisit",
                AreaName = "X-T-Sea",
                Description = "You dip your feet into the water of X-T-Sea, upon which you start to get more of them sick thoughts inside your head. With this unholy feeling inside, as well as the fact that there is no swimmable distance that is viewable from this location, you figure that it would be best to move towards another area."

            },

            new Story{
                ID = 48,
                Type = "ReturnVisit",
                AreaName = "X-T-Sea",
                Description = "Once again you dip your feet into the water of X-T-Sea, once again you think sick thoughts and see nothing but water out in the distance."

            },

            new Story{
                ID = 49,
                Type = "FirstVisit",
                AreaName = "N.M.P.I.L Bay",
                Description = "You've arrived at N.M.P.I.L Bay. Confused about the name, you notice a sign that reads: ''No More Parties in This Bay, Please Baby No More Parties in This Bay.''. This sign has left you even more confused. You decide to not look further into it and make your next move."

            },

            new Story{
                ID = 50,
                Type = "ReturnVisit",
                AreaName = "N.M.P.I.L Bay",
                Description = "You have returned to N.M.P.I.L Bay, once again you choose to listen to the sign and not throw any parties within the area like it asks you kindly."

            },

            new Story{
                ID = 51,
                Type = "FirstVisit",
                AreaName = "Don-Lemme-Getinmai Zone",
                Description = "You have arrived in the Don-Lemme-Getinmai Zone. In the zone you notice a gigantic golden royal looking chair with a sign next to it reading: ''You are now watching the throne.'' Apart from your confusion as to what this means, you notice next to, what you can only assume is 'The Throne', is a an unlocked golden chest. Do you dare to look inside?"

            },

            new Story{
                ID = 52,
                Type = "ReturnVisit",
                AreaName = "Don-Lemme-Getinmai Zone",
                Description = "You have returned to the Don-Lemme-Getinmai Zone, Don-Lemme-Getinmai Zone, Don-Lemme-Getinmai Zone, Don-Lemme-Getinmai Zone. You are now watching the throne, You're definitely in your zone, zone, zone, zone. "

            },

            new Story{
                ID = 53,
                Type = "Item",
                AreaName = "Don-Lemme-Getinmai Zone",
                Description = "Inside this chest is what appears to be a golden crossbow, with the word ''Bow-Tis'' encrusted on it. Upon picking up the bow, you start to feel photoshoot fresh, looking like wealth, you're about to call the paparazi on yourself. You add Bow-Tis to your inventory and prepare to move back towards your original path."

            },

            new Story{
                ID = 54,
                Type = "FirstVisit",
                AreaName = "Guilt Pits",
                Description = "You have arrived at the Guilt Pits. You hear a continuous sound of''All dem a gwaan dem a dem a dem a gwaan'' coming from all the pits surround you, and see the words ''If you loved me so much, why'd you let me go?'' written all over the surrounding ground around you. Needless to say, this area has got you feeling petrified so you are quick to make your next movement."

            },

            new Story{
                ID = 55,
                Type = "ReturnVisit",
                AreaName = "Guilt Pits",
                Description = "You have returned to the Guilt Pits. Still remains the hauting sound of ''All dem a gwaan dem a dem a dem a gwaan'' and the words ''If you loved me so much, why'd you let me go?'' written all over the surrounding ground around you. Needless to say, this place still gives you the creeps."

            },

            new Story{
                ID = 56,
                Type = "FirstVisit",
                AreaName = "Ghost Town",
                Description = "Here you are, one of the locations that you were the most concerned about approaching. The eerily titled ''Ghost Town''. What you have enecounted is as expected, a series of empty buildings, however, you notice a glowing figure out in the distance. She turns to you and screams''I let it all go, of everything that I know, yeah, Of everything that I know, yeah, And nothing hurts anymore, I feel kinda free''...You have Encountered...Gh0st70 Shake."

            },

            new Story{
                ID = 57,
                Type = "ReturnVisit",
                AreaName = "Ghost Town",
                Description = "You have returned to the eerie Ghost Town. Still remains the aftermath of your battle with Gh0st70 Shake. Still remains the feel of feeling kind of freeeeee..."

            },

            new Story{
                ID = 58,
                Type = "Enemy",
                AreaName = "Ghost Town",
                Description = "You have slain Gh0st70 Shake, during her final breaths, she continues to repeatedly scream out the phrase ''I FEEEEEEEEL KINDA FREEEEEEEEEEEEEEEEEEEeeeeeee....'' You notice that upon her demise, she has dropped an item on the ground."

            },

            new Story{
                ID = 59,
                Type = "Item",
                AreaName = "Ghost Town",
                Description = "After slaying Gh0st70 Shake, you have added her dropped Hand of Stove into your inventory, nothing hurts anymore and you feel kind of free. It's time to move onwards"

            },

        });

        ///Insert All Items
        _connection.InsertAll(new[]{

             new Item{
                ID = 1,
                Name = "Heartless Note",
                Type = "Misc",
                DamageBonus = 0,
                HealthBonus = 0

            },

             new Item{
                ID = 2,
                Name = "Diamond From Sierra Leone",
                Type = "Weapon",
                DamageBonus = 10,
                HealthBonus = 5

            },

             new Item{
                ID = 3,
                Name = "Chance's Hat",
                Type = "Head",
                DamageBonus = 10,
                HealthBonus = 25

            },

             new Item{
                ID = 4,
                Name = "Frank's Scepter",
                Type = "Weapon",
                DamageBonus = 35,
                HealthBonus = 10

            },

             new Item{
                ID = 5,
                Name = "Bow-Tis",
                Type = "Weapon",
                DamageBonus = 40,
                HealthBonus = 20

            },

             new Item{
                ID = 6,
                Name = "Hand of Stove",
                Type = "Weapon",
                DamageBonus = 50,
                HealthBonus = 30

            },




        });

        ///Insert All Enemies
        _connection.InsertAll(new[]{

             new Enemy{
                ID = 1,
                Name = "Gh0st70 Shake",
                HpCurrent = 100,
                HpMax = 100,
                DamageAmount = 15,
                CanEvade = false,
                CanBefriend = false,
                XpRewardAmount = 100
             }


        });

    }
    /// <summary>
    /// The CreateNewGame_NewPlayer Function handles the inserting into the Player table with the details of a new player, as well as inserting all
    /// the required game specific data into their appropriate tables (EnemyItems, AreaEnemies, AreaItems, AreaVisit.)
    /// </summary>
    public void CreateNewGame_NewPlayer()
    {
        ///Insert New Player
        _connection.InsertAll(new[]{
            new Player{
                UserEmail = UserManager.UserManagerInstance.UserEmail,
                HpCurrent = 100,
                HpMax = 100,
                DamageAmount = 50,
                XpCurrent = 0,
                LvlCurrent = 1,
                LvlUpDamageBonus = 10,
                LvlUpHealthBonus = 10,
                DistanceTravelled = 0.0,
                EnemiesSlain = 0,
                Points = 0,
                CurrentArea = "Thou Sand Beach"
            }
            });

        ///Insert All EnemyItems
        _connection.InsertAll(new[]{

             new EnemyItem{
                ItemID = 6,
                EnemyID = 1,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail
             }


        });

        ///Insert All AreaEnemies
        _connection.InsertAll(new[]{

             new AreaEnemy{
                EnemyID = 1,
                AreaName = "Ghost Town",
                HpCurrent = 100,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail
             }


        });

        ///Insert All AreaItems
        _connection.InsertAll(new[]{

             new AreaItem{
                AreaName = "The Heartless Hills",
                ItemID = 1,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "The Roc-A-Fella Rocks",
                ItemID = 2,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "Chi Town",
                ItemID = 3,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "Church In The Wild",
                ItemID = 4,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "Don-Lemme-Getinmai Zone",
                ItemID = 5,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },



        });

        ///Insert All AreaVisit
        _connection.InsertAll(new[]{

             new AreaVisit{
                Number = 1,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail,
                AreaName = "Thou Sand Beach"
             }


        });
    }
    /// <summary>
    /// The CreateNewGame_OldPlayer handles the updating of the data pre-existing row within the Player Table by setting all values back to the
    /// start game defaults. It also deletes any rows within the InventoryItem, EnemyItem, AreaEnemy and AreaVisit tables that contain the players
    /// username, and inserts new default values to replace them.
    /// </summary>
    /// <param name="prPlayer">
    /// The Player variable tied to the currently logged in user that is passed into the function to have its values set to default.
    /// </param>
    public void CreateNewGame_OldPlayer(Player prPlayer)
    {
        prPlayer.HpCurrent = 100;
        prPlayer.HpMax = 100;
        prPlayer.DamageAmount = 10;
        prPlayer.XpCurrent = 0;
        prPlayer.LvlCurrent = 1;
        prPlayer.LvlUpDamageBonus = 10;
        prPlayer.LvlUpHealthBonus = 10;
        prPlayer.DistanceTravelled = 0.0;
        prPlayer.EnemiesSlain = 0;
        prPlayer.Points = 0;
        prPlayer.CurrentArea = "Thou Sand Beach";

        ///lcInvList List variable that collects a list of every InventoryItem Row that contains the Player's UserEmail
        List<InventoryItem> lcInvList = new List<InventoryItem>();
        lcInvList = _connection.Table<InventoryItem>().Where(x => x.PlayerUserEmail == prPlayer.UserEmail).ToList();

        ///Deletes every row within the InventoryItem that contains the Player's UserEmail
        foreach (InventoryItem prInvItem in lcInvList)
        {
            _connection.Delete(prInvItem);
        }

        ///lcInvList List variable that collects a list of every EnemyItem Row that contains the Player's UserEmail
        List<EnemyItem> lcEneItemList = new List<EnemyItem>();
        lcEneItemList = _connection.Table<EnemyItem>().Where(x => x.PlayerUserEmail == prPlayer.UserEmail).ToList();

        ///Deletes every row within the EnemyItem that contains the Player's UserEmail
        foreach (EnemyItem prEneItem in lcEneItemList)
        {
            _connection.Delete(prEneItem);

        }

        ///Insert All EnemyItems
        _connection.InsertAll(new[]{

             new EnemyItem{
                ItemID = 6,
                EnemyID = 1,
                PlayerUserEmail = prPlayer.UserEmail
             }


        });

        ///lcInvList List variable that collects a list of every AreaItem Row that contains the Player's UserEmail
        List<AreaItem> lcAreaItemList = new List<AreaItem>();
        lcAreaItemList = _connection.Table<AreaItem>().Where(x => x.PlayerUserEmail == prPlayer.UserEmail).ToList();

        ///Deletes every row within the AreaItem that contains the Player's UserEmail
        foreach (AreaItem prAreaItem in lcAreaItemList)
        {
            _connection.Delete(prAreaItem);

        }

        ///Insert All AreaItems
        _connection.InsertAll(new[]{

             new AreaItem{
                AreaName = "The Heartless Hills",
                ItemID = 1,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "The Roc-A-Fella Rocks",
                ItemID = 2,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "Chi Town",
                ItemID = 3,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "Church In The Wild",
                ItemID = 4,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },

             new AreaItem{
                AreaName = "Don-Lemme-Getinmai Zone",
                ItemID = 5,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail

            },



        });

        ///lcInvList List variable that collects a list of every AreaEnemy Row that contains the Player's UserEmail
        List<AreaEnemy> lcAreaEneList = new List<AreaEnemy>();
        lcAreaEneList = _connection.Table<AreaEnemy>().Where(x => x.PlayerUserEmail == prPlayer.UserEmail).ToList();

        ///Deletes every row within the AreaEnemy that contains the Player's UserEmail
        foreach (AreaEnemy prAreaEne in lcAreaEneList)
        {
            _connection.Delete(prAreaEne);

        }

        ///Insert All AreaEnemies
        _connection.InsertAll(new[]{

             new AreaEnemy{
                EnemyID = 1,
                AreaName = "Ghost Town",
                HpCurrent = 100,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail
             }


        });

        ///lcInvList List variable that collects a list of every AreaVisit Row that contains the Player's UserEmail
        List<AreaVisit> lcAreaVisitList = new List<AreaVisit>();
        lcAreaVisitList = _connection.Table<AreaVisit>().Where(x => x.PlayerUserEmail == prPlayer.UserEmail).ToList();

        ///Deletes every row within the AreaVisit that contains the Player's UserEmail
        foreach (AreaVisit prAreaVisit in lcAreaVisitList)
        {
            _connection.Delete(prAreaVisit);

        }

        ///Insert All AreaVisit
        _connection.InsertAll(new[]{

             new AreaVisit{
                Number = 1,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail,
                AreaName = "Thou Sand Beach"
             }


        });

        ///Update Player row with Players UserEmail with default values.
        _connection.Update(prPlayer);
    }

    /// <summary>
    /// The FirstTimeAreaVisit function handles the insertion into the AreaVisit table for the first time that a player visits an area.
    /// </summary>
    /// <param name="lcCurrentArea">
    /// The lcCurrentArea string that is passed into the function contains the name of the area that the player is currently in so that the
    /// insert statement records down the location that they are currently in.
    /// </param>
    public void FirstTimeAreaVisit(string lcCurrentArea)
    {
        _connection.InsertAll(new[]{
            new AreaVisit{
                Number = 1,
                PlayerUserEmail = UserManager.UserManagerInstance.UserEmail,
                AreaName = lcCurrentArea
            }
            });
    }

    /// <summary>
    /// The PickUpItem function handles the insertion into the InventoryItem table everytime a player picks up an item in game and adds it to their
    /// inventory.
    /// </summary>
    /// <param name="lcItemID">
    /// The lcItemID int is passed into the function to act as a foreign key to the Item table within the new row.
    /// </param>
    /// <param name="lcPlayerName">
    /// The lcPlayer string is password into the function to act as a foreign key to the Player table within the new row.
    /// </param>
    public void PickUpItem(int lcItemID, string lcPlayerName)
    {
        _connection.InsertAll(new[]{
            new InventoryItem{
                ItemID = lcItemID,
                PlayerUserEmail = lcPlayerName
            }
            });
    }

    /// <summary>
    /// The RetrieveInventoryItemsList List handles the collecting and returning of a list that contains every item in a specific players inventory.
    /// This is done by collecting a list of every entry within the InventoryItem table that contains a Player's UserEmail.
    /// </summary>
    /// <returns>
    /// A List containing every row in the InventoryItem table that contains a Player's UserEmail.
    /// </returns>
    public List<InventoryItem> RetrieveInventoryItemsList()
    {
        List<InventoryItem> lcInv = _connection.Table<InventoryItem>().Where(x => x.PlayerUserEmail == UserManager.UserManagerInstance.UserEmail).ToList();
        return lcInv;
    }

    /// <summary>
    /// The Getter & Setter for the SQLiteConnection Variable.
    /// </summary>
    public SQLiteConnection Connection
    {
        get
        {
            return _connection;
        }

        set
        {
            _connection = value;
        }
    }

    public bool DoesUserExist(string prUserEmail, string prUserPassword)
    {
        try
        {   ///lcRow is an int variable that returns the row count of the Area Table
            User lcUserExist = _connection.Table<User>().Where(x => x.UserEmail == prUserEmail && x.Password == prUserPassword).FirstOrDefault();
            if (lcUserExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///A Try Catch that catches an SQLiteException error to prevent the application crashing.
        catch (SQLiteException)
        {
            return false;
        }
    }




}
