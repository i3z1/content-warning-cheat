using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using Zorro.Core;
using Zorro.Core.CLI;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using System.Linq;
using UnityEngine.PlayerLoop;
using System.Runtime.InteropServices;
using Steamworks;
using CurvedUI;
using System.ComponentModel;
using DefaultNamespace.Artifacts;
namespace ExampleAssembly
{
    public class Cheat : MonoBehaviour
    {
        public PhotonView photonView;
        private int mainWID = 1024;
        private Rect mainWRect = new Rect(5f, 5f, 300f, 150f);
        private bool godmode;
        private bool infBattery;
        private bool noRagDoll;
        private bool drawMenu = true;
        private bool stamina = false;
        private bool oxygen = false;
        public PlayerVisor visor;
        private bool superJump;
        private bool rainbowVisor;
        public bool blinkingFace;
        private bool greenScreenSpam;
        private string userInputText = ""; // Initialize an empty string to hold user input
        public SteamAPICall_t hAPICall;

        private float lastCacheTime = Time.time + 5f;
        private float lastItemCache = Time.time + 1f;
        public static Player[] players;
        public static BombItem[] bombs;
        public static PlayerController controller;
        public static Monster[] monsters;
        public static ItemInstance[] droppedItems;
        public static Spectate spectate;
        public static PlayerHandler playerHandler;
        private int selectedItemIndex = -1;
        private Vector2 scrollPosition;

        private string[] enemyNames = new string[] { "AnglerMimic", "Angler", "BarnacleBall", "BigSlap", "Bombs", "Dog", "Ear", "EyeGuy", "Flicker", "Ghost", "Jello", "Knifo", "Larva", "MimicInfiltrator", "Mouthe", "Slurper", "Snatcho", "Spider", "Toolkit_Fan", "Toolkit_Hammer", "Toolkit_Iron", "Toolkit_Vaccum", "Toolkit_Wisk", "Weeping", "Zombe" };
        private int selectedEnemyIndex = -1;
        private bool isEnemyDropdownVisible = false;
        private Vector2 enemyScrollPosition;
        private string enemyButtonText = "Select Enemy";
        private bool isDropdownVisible = false;
        private string buttonText = "Select Item";
        // public static Car[] vehicles;S
        public Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();
        public string currentKeyToRebind = null;
        public KeyCode waitingForKey1 = KeyCode.None;
        private bool waitingForKey = false; // Flag to indicate we're waiting for a key press.
        private int tabSelected = 0;
        private GUIStyle boxStyle, buttonStyle, labelStyle, toggleStyle, scrollViewStyle;
        public Transform playerTransform;
        public float followSpeed = 5f;
        public Vector3 offset = new Vector3(0, 2, 0); // Adjust as needed

        public static string selectedPlayerName = "";
        public static bool dropdownOpenPlayer = false;
        public static Vector2 scrollPositionPlayer = Vector2.zero;
        public static List<Player> PlayerControllers = new List<Player>();  // List to hold player controllers
        public static Player selectedPlayer;  // Declare as static if it needs to be accessed globally

        public static BombItem bombeee;  // This should be set somewhere in your code.



        void LoadKeyBinds()
        {
            keybinds.Clear(); // Clear existing keybinds to reload them

            // Default keybinds if none are saved
            AddKeyBind("Toggle Menu", KeyCode.N);
            AddKeyBind("Ragdoll Players", KeyCode.R);
            AddKeyBind("superRagdoll", KeyCode.Z);
            AddKeyBind("Goo All Players", KeyCode.G);
            AddKeyBind("Web All Players", KeyCode.V);
            AddKeyBind("Revive ALL", KeyCode.H);
            AddKeyBind("cantmoveall", KeyCode.X);
            AddKeyBind("Spawn Enemy", KeyCode.P);
            AddKeyBind("Kill enemys", KeyCode.L);
            AddKeyBind("Force Players to Bed", KeyCode.B);
            AddKeyBind("give item", KeyCode.I);
            AddKeyBind("bomb all players", KeyCode.O);

            // Add more keybinds as needed
        }

        void AddKeyBind(string action, KeyCode defaultKey)
        {
            KeyCode loadedKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(action, defaultKey.ToString()));
            keybinds[action] = loadedKey;
        }


        public void Keyhandler()
        {
            LoadKeyBinds();
            if (waitingForKey)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (currentKeyToRebind != null)
                        {
                            keybinds[currentKeyToRebind] = keyCode; // Update the keybind.
                            PlayerPrefs.SetString(currentKeyToRebind, keyCode.ToString()); // Save the new keybind.
                            waitingForKey = false; // Reset the flag.
                            currentKeyToRebind = null; // Clear the current action waiting for rebind.
                        }
                        break;
                    }
                }
            }
            if (!Input.anyKey || !Input.anyKeyDown)
            {
                return;
            }

            if (Input.GetKeyDown(keybinds["Toggle Menu"]))
            {
                drawMenu = !drawMenu;
            }

            if (Input.GetKeyDown(keybinds["bomb all players"]))
            {
                SpawnItem("Bomb");
                StartCoroutine(SpawnAndFollowBombs());
            }

            // Define the coroutine for spawning and setting bombs to follow



            if (Input.GetKeyDown(keybinds["Web All Players"]))
            {
                if (Cheat.players.Length > 0)
                {
                    foreach (Player player in Cheat.players)
                    {
                        WebTroll(player, false);
                    }
                }
            }

            if (Input.GetKeyDown(keybinds["Goo All Players"]))
            {
                geo_all();
            }

            if (Input.GetKeyDown(keybinds["Revive ALL"]))
            {
                Revive_ALL();
            }


            if (Input.GetKeyDown(keybinds["Ragdoll Players"]))
            {
                Ragdall();
            }
            if (Input.GetKeyDown(keybinds["cantmoveall"]))
            {
                CantMove();
            }

            if (Input.GetKeyDown(keybinds["Spawn Enemy"]))
            {
                MonsterSpawner.SpawnMonster(enemyNames[selectedEnemyIndex]);

            }
            if (Input.GetKeyDown(keybinds["Kill enemys"]))
            {

                BotHandler.instance.DestroyAll();
            }

            if (Input.GetKeyDown(keybinds["Force Players to Bed"]))
            {
                Bed[] beds = FindObjectsOfType<Bed>();
                Player[] players = Cheat.players;


                for (int i = 0; i < beds.Length; i++)
                {
                    beds[i].RequestSleep(players[i]);
                }
            }

            if (Input.GetKeyDown(keybinds["give item"]))
            {
                EquipItem(SingletonAsset<ItemDatabase>.Instance.lastLoadedItems[this.selectedItemIndex]);
  

            }
            if (Input.GetKeyDown(keybinds["superRagdoll"]))
            {
                SuperRagdoll();
            }

            if (Input.GetKeyDown(KeyCode.Space) && Player.localPlayer != null && superJump)
            {
                Player.localPlayer.refs.view.RPC("RPCA_Jump", RpcTarget.All, Array.Empty<object>());
            }

        }



        void SpawnItem(string itemName, Player targetPlayer = null)
        {
            // Find the item in the item database
            var item = ItemDatabase.Instance.lastLoadedItems.FirstOrDefault(i => i.name == itemName);

            if (item != null)
            {
                // Check if a specific target player is provided
                if (targetPlayer != null)
                {
                    // Spawn item only for the target player
                    Vector3 spawnPos = targetPlayer.HeadPosition();
                    Player.localPlayer.RequestCreatePickup(item, new ItemInstanceData(Guid.NewGuid()), spawnPos, Quaternion.identity);
                }
                else
                {
                    // Iterate over all players to spawn the item at each player's position
                    foreach (var player in Cheat.players)
                    {
                        if (player != null && player != Player.localPlayer)
                        {
                            Vector3 spawnPos = player.HeadPosition();
                            Player.localPlayer.RequestCreatePickup(item, new ItemInstanceData(Guid.NewGuid()), spawnPos, Quaternion.identity);
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Item named '{itemName}' not found in the database.");
            }
        }



        void CantMove(Player targetPlayer = null)
        {
            if (targetPlayer != null)
            {
                // Apply the effect only to the specified target player
                ApplyMovementRestriction(targetPlayer);
            }
            else
            {
                // Apply the effect to all players except the local player
                foreach (Player player in Cheat.players)
                {
                    if (player != Player.localPlayer)  // Optionally skip local player
                    {
                        ApplyMovementRestriction(player);
                    }
                }
            }
        }
        private void ApplyMovementRestriction(Player player)
        {
            MethodInfo methodInfo = typeof(Player).GetMethod("CallTakeDamageAndAddForceAndFall", BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo != null)
            {
                // If you want to kill all also change the 0f to a lethal amount like 99999f
                methodInfo.Invoke(player, new object[] { 0f, new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f), 5f), 500f });
            }
        }


        void Ragdall()
        {
            if (Cheat.players.Length > 0)
            {
                foreach (Player player in Cheat.players)
                {
                    if (player == Player.localPlayer)
                        continue;
                    MethodInfo methodInfo = typeof(Player).GetMethod("CallTakeDamageAndAddForceAndFall", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(player, new object[] { 0f, new Vector3(UnityEngine.Random.Range(-15, 15f), UnityEngine.Random.Range(-15f, 15f), 5f), 2.5f });
                    }
                }
            }
        }

        ///------------------------------------------------------------------------------------
        void SuperRagdoll(Player targetPlayer = null)
        {
            if (targetPlayer != null)
            {
                // Apply the effect only to the specified target player
                ApplyRagdollEffect(targetPlayer);
            }
            else
            {
                // Apply the effect to all players except the local player
                foreach (Player player in Cheat.players)
                {
                    if (player == Player.localPlayer) // Optionally skip local player
                        continue;
                    ApplyRagdollEffect(player);
                }
            }
        }

        private void ApplyRagdollEffect(Player player)
        {
            MethodInfo methodInfo = typeof(Player).GetMethod("CallTakeDamageAndAddForceAndFall", BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo != null)
            {
                // Set a high Y-axis value to simulate 'flying' into the sky.
                Vector3 forceVector = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(10f, 30f), UnityEngine.Random.Range(-10f, 10f));
                methodInfo.Invoke(player, new object[] { 0f, forceVector, 2.5f });
                
            }
        }
        ///------------------------------------------------------------------------------------------

        void Revive_ALL()
        {
            if (Cheat.players.Length > 0)
            {
                foreach (Player player in Cheat.players)
                {
                    if (player == Player.localPlayer)
                        continue;
                    if (player != null)
                    {
                        player.CallRevive();
                        player.data.health = 100f;
                    }
                }
            }
        }
        void geo_all()
        {
            var items = SingletonAsset<ItemDatabase>.Instance.lastLoadedItems;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == "GooBall")
                {
                    Item GooItem = items[i];
                    ItemGooBall Goo = GooItem.itemObject.GetComponent<ItemGooBall>();

                    if (Cheat.players.Length > 0)
                    {
                        foreach (Player player in Cheat.players)
                        {
                            GooTroll(Goo, player, false);
                        }
                    }
                    break;
                }
            }
        }

        void killall()
        {
            if (Cheat.players.Length > 0)
            {
                foreach (Player player in Cheat.players)
                {
                    if (player == Player.localPlayer)
                        continue;
                    MethodInfo methodInfo = typeof(Player).GetMethod("CallTakeDamageAndAddForceAndFall", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(player, new object[] { 99999f, new Vector3(UnityEngine.Random.Range(-0, 0f), UnityEngine.Random.Range(-0f, 0f), 5f), 2.5f });
                    }
                }
            }
        }

        void TriggerBlackScreenAndBreakLobby()
        {
            foreach (PhotonGameLobbyHandler handler in FindObjectsOfType<PhotonGameLobbyHandler>())
            {
                handler.photonView.RPC("RPC_StartTransition", RpcTarget.Others, Array.Empty<object>());
                RetrievableResourceSingleton<TransitionHandler>.Instance.TransitionToBlack(0f, delegate
                {
                    if (!PhotonNetwork.InRoom)
                    {
                        return;
                    }
                    VerboseDebug.Log("Returning To Surface!");
                    RetrievableSingleton<PersistentObjectsHolder>.Instance.FindPersistantObjects();
                    PhotonNetwork.LoadLevel("SurfaceScene");
                }, 0f);
            }
        }


        private void GooTroll(ItemGooBall gooItem, Player player, bool gooMonsters)
        {
            if (player != null && player != Player.localPlayer)
            {
                if (gooMonsters && !player.ai)
                    return;

                if (!gooMonsters && player.ai)
                    return;

                PhotonNetwork.Instantiate(gooItem.explodedGoopPref.name, player.HeadPosition(), Quaternion.identity, 0, null);
            }
        }

        private void WebTroll(Player player, bool gooMonsters)
        {
            if (player != null && player != Player.localPlayer)
            {
                if (gooMonsters && !player.ai)
                    return;

                if (!gooMonsters && player.ai)
                    return;
                PhotonNetwork.Instantiate("Web", player.HeadPosition(), Quaternion.identity, 0, null);
            }
        }
        private void EquipItem(Item item)
        {
            Vector3 debugItemSpawnPos = MainCamera.instance.GetDebugItemSpawnPos();
            Player.localPlayer.RequestCreatePickup(item, new ItemInstanceData(Guid.NewGuid()), debugItemSpawnPos, Quaternion.identity);
        }

            
        
        public void Update()
        {
            Keyhandler();

            if (Player.localPlayer != null)
            {
                if (blinkingFace)
                {
                    Face.OnUpdate();
                }
                if (rainbowVisor)
                {
                    Face.rainbow();
                }
                if (stamina)
                {
                    Player.localPlayer.data.currentStamina = 100;
                    Player.localPlayer.data.staminaDepleated = false;
                }
                if (oxygen)
                {
                    {
                        Player.localPlayer.data.remainingOxygen = 500;
                    }
                }
                if (godmode)
                {
                    Player.localPlayer.data.health = 100f;
                }
                if (infBattery)
                {
                    PlayerInventory playerInventory;
                    Player.localPlayer.TryGetInventory(out playerInventory);
                    if (playerInventory != null)
                    {
                        foreach (InventorySlot inventorySlot in playerInventory.slots)
                        {
                            BatteryEntry batteryEntry;
                            if (inventorySlot.ItemInSlot.item != null && inventorySlot.ItemInSlot.data.TryGetEntry<BatteryEntry>(out batteryEntry) && batteryEntry.m_maxCharge > batteryEntry.m_charge)
                            {
                                batteryEntry.AddCharge(1000);
                            }
                        }
                    }
                }
                if (greenScreenSpam)
                {
                    foreach (ProjectorMachine machine in FindObjectsOfType<ProjectorMachine>())
                    {
                        machine.PressMore();
                    }
                }
                if (noRagDoll)
                {
                    Player.localPlayer.data.fallTime = 0f;
                }
            }

            if (Time.time >= lastCacheTime)
            {
                lastCacheTime = Time.time + 3f;

                players = FindObjectsOfType<Player>();

                //controller = base.GetComponent<PlayerController>();
                // vehicles = FindObjectsOfType<Car>();

                ESP.mainCam = Camera.main;
            }

            if (Time.time >= lastItemCache)
            {
                lastItemCache = Time.time + 1f;

                droppedItems = FindObjectsOfType<ItemInstance>();
                bombs = FindObjectsOfType<BombItem>();
            }
        }


        public void OnGUI()
        {
            if (drawMenu )
            {
                mainWRect = GUILayout.Window(mainWID, mainWRect, MainWindow, "Main", boxStyle);

            }
        }


        private void MainWindow(int id)
        {


            Color backgroundColor = new Color(0.1f, 0.1f, 0.1f, 1.0f); // Dark black for the background

            // Define the main window color
            Color mainWindowColor = backgroundColor; // Dark black for the background


            Color buttonColor = new Color(0.2f, 0.2f, 0.6f, 1.0f); // Dark blue for buttons
            Color buttonHoverColor = new Color(0.25f, 0.25f, 0.75f, 1.0f); // Slightly lighter blue for button hover
            Color textColor = Color.white; // White text for better readability

            // Box Style
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = textColor;
            boxStyle.normal.background = MakeTex(2, 2, backgroundColor); // Dark black background for boxes
            boxStyle.fontSize = 12;
            boxStyle.padding = new RectOffset(10, 10, 10, 10); // Padding inside the box

            // Button Style
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = textColor;
            buttonStyle.normal.background = MakeTex(2, 2, buttonColor); // Dark blue for buttons
            buttonStyle.hover.background = MakeTex(2, 2, buttonHoverColor); // Lighter blue on hover
            buttonStyle.fontSize = 12;
            buttonStyle.padding = new RectOffset(10, 10, 5, 5); // Padding for buttons

            // Label Style
            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.normal.textColor = textColor; // White text
            labelStyle.fontSize = 12;

            // Toggle Style
            Color toggleBackgroundColor = new Color(0.2f, 0.2f, 0.2f, 1.0f); // Dark grey for toggle background
            Color toggleBorderColor = new Color(0.2f, 0.2f, 0.6f, 1.0f); // Dark blue for toggle border
            Color toggleTextColor = Color.white; // White text for better readability

            // Toggle Style
            toggleStyle = new GUIStyle(GUI.skin.toggle);
            toggleStyle.normal.textColor = toggleTextColor;
            toggleStyle.onNormal.textColor = toggleTextColor;
            toggleStyle.hover.textColor = toggleTextColor;
            toggleStyle.onHover.textColor = toggleTextColor;
            toggleStyle.focused.textColor = toggleTextColor;
            toggleStyle.onFocused.textColor = toggleTextColor;
            toggleStyle.active.textColor = toggleTextColor;
            toggleStyle.onActive.textColor = toggleTextColor;

            toggleStyle.normal.background = MakeTex(2, 2, toggleBackgroundColor);
            toggleStyle.onNormal.background = MakeTex(2, 2, toggleBorderColor);
            toggleStyle.hover.background = MakeTex(2, 2, toggleBackgroundColor);
            toggleStyle.onHover.background = MakeTex(2, 2, toggleBorderColor);
            toggleStyle.focused.background = MakeTex(2, 2, toggleBackgroundColor);
            toggleStyle.onFocused.background = MakeTex(2, 2, toggleBorderColor);
            toggleStyle.active.background = MakeTex(2, 2, toggleBackgroundColor);
            toggleStyle.onActive.background = MakeTex(2, 2, toggleBorderColor);

            toggleStyle.border = new RectOffset(1, 1, 1, 1); // Add a border if you want
            toggleStyle.margin = new RectOffset(4, 4, 4, 4);
            toggleStyle.padding = new RectOffset(4, 4, 4, 4);
            toggleStyle.fontSize = 12;
            toggleStyle.alignment = TextAnchor.MiddleCenter; // Center text both horizontally and vertically




            GUILayout.BeginHorizontal(boxStyle);//Tab Selector
            {
                if (GUILayout.Button("Self Tab", buttonStyle))
                {
                    tabSelected = 0;
                }
                if (GUILayout.Button("Lobby Tab", buttonStyle))
                {
                    tabSelected = 1;
                }
                if (GUILayout.Button("Troll Tab", buttonStyle))
                {
                    tabSelected = 2;
                }
                if (GUILayout.Button("Spawn Tab", buttonStyle))
                {
                    tabSelected = 3;
                }
                if (GUILayout.Button("Keybinds", buttonStyle)) // Added a button for the Keybinds tab
                {
                    tabSelected = 4; // Assuming 4 is the index for Keybinds
                }
                if (GUILayout.Button("Players", buttonStyle)) // Added a button for the Keybinds tab
                {
                    tabSelected = 5; // Assuming 4 is the index for Keybinds
                }
            }
            GUILayout.EndHorizontal();

            if (tabSelected == 0) //Self Tab
            {
                GUILayout.BeginVertical("ESP", boxStyle);
                {
                    GUILayout.Space(20f);
                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        GUILayout.Space(60f);

                        ESP.playerBox = GUILayout.Toggle(ESP.playerBox, "Player Box", toggleStyle , GUILayout.Width(100), GUILayout.Height(21));
                        ESP.monsterBox = GUILayout.Toggle(ESP.monsterBox, "Monster Boxes", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        ESP.playerName = GUILayout.Toggle(ESP.playerName, "Names", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                    }
                    GUILayout.EndHorizontal();
                    // Horizontal group for Crosshair and Items toggles
                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        GUILayout.Space(60f);

                        ESP.crosshair = GUILayout.Toggle(ESP.crosshair, "Crosshair", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        ESP.item = GUILayout.Toggle(ESP.item, "Items", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        ESP.Chams = GUILayout.Toggle(ESP.Chams, "Chams", toggleStyle, GUILayout.Width(100), GUILayout.Height(21) );
                    }
                    GUILayout.EndHorizontal();

                    // Horizontal group for Player Box, Chams button, Monster Boxes, and Player Name toggles

                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical("Self Toggles", boxStyle);
                {
                    GUILayout.Space(20f);
                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        GUILayout.Space(60f);
                        stamina = GUILayout.Toggle(stamina, "Inf. Stamina", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        oxygen = GUILayout.Toggle(oxygen, "Inf Oxygen", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        godmode = GUILayout.Toggle(godmode, "God Mode", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        GUILayout.Space(60f);
                        superJump = GUILayout.Toggle(superJump, "Inf. Jump", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        rainbowVisor = GUILayout.Toggle(rainbowVisor, "Rainbow Face", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        blinkingFace = GUILayout.Toggle(blinkingFace, "Blinking Face", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        GUILayout.Space(60f);
                        infBattery = GUILayout.Toggle(infBattery, "Inf. Battery", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        noRagDoll = GUILayout.Toggle(noRagDoll, "No Ragdoll", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
                        GUILayout.Label("", labelStyle);


                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
                GUILayout.BeginHorizontal(boxStyle);
                {
                    if (GUILayout.Button("Revive Self", buttonStyle))
                    {
                        Player.localPlayer.CallRevive();
                        Player.localPlayer.data.health = 100f;
                    }
                    if (GUILayout.Button("Ragdoll Self", buttonStyle))
                    {
                        Player.localPlayer.RPCA_TakeDamageAndAddForce(0f, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(1f, 10f)), 3f);
                    }
                    if (GUILayout.Button("Spawn Player", buttonStyle))
                    {
                        MonsterSpawner.SpawnMonster("Player");
                    }
                    if (GUILayout.Button("kill Self", buttonStyle))
                    {
                        Player.localPlayer.Die();
                        Player.localPlayer.data.health = 0f;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(boxStyle);
                {
                    if (GUILayout.Button("Open Console", buttonStyle))
                    {
                        foreach (DebugUIHandler item in FindObjectsOfType<DebugUIHandler>())
                        {
                            item.Show();
                        }
                    }

                    if (GUILayout.Button("Close Console", buttonStyle))
                    {
                        foreach (DebugUIHandler item in FindObjectsOfType<DebugUIHandler>())
                        {
                            item.Hide();
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }//Self Tab
            if (tabSelected == 1)
            {
                GUILayout.BeginVertical("Host Only", boxStyle);
                {
                    GUILayout.Space(20f);

                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        if (GUILayout.Button("Add $10K", buttonStyle))
                        {
                            SurfaceNetworkHandler.RoomStats.AddMoney(10000);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical("Offhost", boxStyle);
                {
                    GUILayout.Space(20f);
                    GUILayout.BeginHorizontal(boxStyle);
                    {
                        if (GUILayout.Button("Revive All Players", buttonStyle))
                        {
                            Revive_ALL();
                        }
                       
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical("Custom Face Settings", GUI.skin.box);
                {
                    GUILayout.Space(20f);
                    GUILayout.Label("Enter Face Text");
                    GUILayout.Space(20f);
                    userInputText = GUILayout.TextField(userInputText, 25); // Allow user to input text, with a max length of 25 characters

                    if (GUILayout.Button("Apply Face Text", buttonStyle))
                    {
                        // Check if Player.localPlayer is not null before attempting to update face settings
                        if (Player.localPlayer != null)
                        {
                            float hue = PlayerPrefs.GetFloat("VisorColor", 0);
                            int colorIndex = PlayerPrefs.GetInt("FaceColorIndex", 0);
                            float faceRotation = PlayerPrefs.GetFloat("FaceRotation", 0);
                            float faceSize = PlayerPrefs.GetFloat("FaceSize", 1);

                            // Use userInputText for the currentFace parameter
                            Player.localPlayer.refs.visor.SetAllFaceSettings(hue, colorIndex, userInputText, faceRotation, faceSize);
                        }
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.BeginHorizontal(GUI.skin.box);
                // Button to load the Surface Scene
                if (GUILayout.Button("Load Surface Scene", buttonStyle))
                {
                    PhotonNetwork.LoadLevel("SurfaceScene");
                }

                // Button to load the Factory Scene
                if (GUILayout.Button("Load Factory Scene", buttonStyle))
                {
                    PhotonNetwork.LoadLevel("FactoryScene");
                }

                // Button to load the Harbour Scene
                if (GUILayout.Button("Load Harbour Scene", buttonStyle))
                {
                    PhotonNetwork.LoadLevel("harbourScene");
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginVertical(GUI.skin.box);

                if (GUILayout.Button("Request Lobby List", buttonStyle))
                {
                    hAPICall = SteamMatchmaking.RequestLobbyList();
                    Debug.Log("Requested Lobby List");
                }

                if (GUILayout.Button("Random Join", buttonStyle))
                {
                    MainMenuHandler.Instance.JoinRandom();

                }

                GUILayout.EndVertical();






            }//Lobby Tab
            if (tabSelected == 2)
            {
                GUILayout.BeginHorizontal(boxStyle);
                {
                    if (GUILayout.Button("Goo All Players", buttonStyle))
                    {
                        geo_all();
                    }
                    if (GUILayout.Button("Goo All Monsters", buttonStyle))
                    {
                        var items = SingletonAsset<ItemDatabase>.Instance.lastLoadedItems;
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i].name == "GooBall")
                            {
                                Item GooItem = items[i];
                                ItemGooBall test = GooItem.itemObject.GetComponent<ItemGooBall>();

                                if (Cheat.players.Length > 0)
                                {
                                    foreach (Player player in Cheat.players)
                                    {
                                        GooTroll(test, player, true);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(boxStyle);
                {
                    if (GUILayout.Button("Web All Players", buttonStyle))
                    {
                        if (Cheat.players.Length > 0)
                        {
                            foreach (Player player in Cheat.players)
                            {
                                WebTroll(player, false);
                            }
                        }
                    }
                    if (GUILayout.Button("Web All Monsters", buttonStyle))
                    {
                        if (Cheat.players.Length > 0)
                        {
                            foreach (Player player in Cheat.players)
                            {
                                WebTroll(player, true);
                            }
                        }
                    }
                }
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal(boxStyle);
                {
                    if (GUILayout.Button("Ragdoll Players", buttonStyle))
                    {
                        Ragdall();
                    }
                    if (GUILayout.Button("all can't move", buttonStyle))
                    {
                        CantMove();
                    }
                    if (GUILayout.Button("kill all", buttonStyle))
                    {
                        killall();
                    }
                    if (GUILayout.Button("open/close door", buttonStyle))
                    {
                        foreach (DivingBellDoorButton BellDoore in FindObjectsOfType<DivingBellDoorButton>())
                        {
                            BellDoore.Interact(Player.localPlayer);
                        }
                    }
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Spawn Drone", buttonStyle))
                {
                    foreach (ShopHandler shop in FindObjectsOfType<ShopHandler>())
                    {
                        var fieldInfo = typeof(ShopHandler).GetField("m_PhotonView", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fieldInfo != null)
                        {
                            PhotonView m_PhotonView = (PhotonView)fieldInfo.GetValue(shop);

                            // Assuming you know the range of your item IDs
                            int maxItemId = 0x35; // Example maximum, adjust according to your actual max item ID
                            List<byte> itemIDsList = new List<byte>();

                            // Generate item IDs dynamically from 1 to maxItemId
                            for (byte i = 1; i <= maxItemId; i++)
                            {
                                itemIDsList.Add(i);
                            }

                            // Convert the list to an array since RPC methods expect an array
                            byte[] itemIDs = itemIDsList.ToArray();
                            

                            m_PhotonView.RPC("RPCA_SpawnDrone", RpcTarget.All, new object[] { itemIDs });
                            
                            
                        }
                    }
                }


                // Button to trigger the black screen / break lobby
                if (GUILayout.Button("Black Screen", buttonStyle))
                {
                    TriggerBlackScreenAndBreakLobby();
                }


                if (GUILayout.Button("Force Players to Bed", buttonStyle))
                {
                    Bed[] beds = FindObjectsOfType<Bed>();
                    Player[] players = Cheat.players;
                  

                    for (int i = 0; i < beds.Length; i++)
                    {
                        beds[i].RequestSleep(players[i]);
                    }
                }
                greenScreenSpam = GUILayout.Toggle(greenScreenSpam, "Projector Spam", toggleStyle, GUILayout.Width(100), GUILayout.Height(21));
            }
            if (tabSelected == 3)
            {
                GUILayout.BeginVertical("Item Spawner", boxStyle);
                {
                    GUILayout.Space(20f);
                    if (GUILayout.Button(buttonText, buttonStyle))
                    {
                        isDropdownVisible = !isDropdownVisible;
                    }

                    // This block only appears after clicking the button, acting as the dropdown.
                    if (isDropdownVisible)
                    {
                        // Set a proper height for the dropdown area
                        scrollPosition = GUILayout.BeginScrollView(scrollPosition, buttonStyle);

                        for (int i = 0; i < SingletonAsset<ItemDatabase>.Instance.lastLoadedItems.Count; i++)
                        {
                            if (GUILayout.Button(SingletonAsset<ItemDatabase>.Instance.lastLoadedItems[i].name , buttonStyle))
                            {
                                this.selectedItemIndex = i;
                                buttonText = SingletonAsset<ItemDatabase>.Instance.lastLoadedItems[i].name; // Update the button text
                                isDropdownVisible = false;
                            }
                        }

                        GUILayout.EndScrollView();
                    }

                    if (this.selectedItemIndex != -1 && GUILayout.Button("Give Item"/*, GUILayout.Width(200), GUILayout.Height(20)*/, buttonStyle))
                    {
                        
                        EquipItem(SingletonAsset<ItemDatabase>.Instance.lastLoadedItems[this.selectedItemIndex]);
                        //this.selectedItemIndex = -1;
                    }
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical("Enemy Spawner", boxStyle);
                {
                    GUILayout.Space(20f);

                    if (GUILayout.Button(enemyButtonText/*, GUILayout.Width(200), GUILayout.Height(20)*/, buttonStyle))
                    {
                        isEnemyDropdownVisible = !isEnemyDropdownVisible;
                    }

                    // This block only appears after clicking the button, acting as the dropdown.
                    if (isEnemyDropdownVisible)
                    {
                        // Set a proper height for the dropdown area
                        enemyScrollPosition = GUILayout.BeginScrollView(enemyScrollPosition, buttonStyle);

                        for (int i = 0; i < enemyNames.Length; i++)
                        {
                            if (GUILayout.Button(enemyNames[i] , buttonStyle))
                            {
                                selectedEnemyIndex = i;
                                enemyButtonText = enemyNames[i]; // Update the button text
                                isEnemyDropdownVisible = false;
                            }
                        }

                        GUILayout.EndScrollView();
                    }

                    if (selectedEnemyIndex != -1 && GUILayout.Button("Spawn Enemy"/*, GUILayout.Width(200), GUILayout.Height(40)*/, buttonStyle))
                    {
                        MonsterSpawner.SpawnMonster(enemyNames[selectedEnemyIndex]);
                        // selectedEnemyIndex = -1; // Uncomment if you want to reset the selection after spawning
                    }
                }
                GUILayout.EndVertical();
                GUILayout.BeginHorizontal(boxStyle);
                    if (GUILayout.Button("Remove Spawned Enemies", buttonStyle)) // Removes all manually spawned enemies
                    {
                        BotHandler.instance.DestroyAll();
                    }
                GUILayout.EndVertical();
            }
            if (tabSelected == 4)
            {
                foreach (var keybind in keybinds)
                {
                    GUILayout.BeginHorizontal(boxStyle);

                    // Assuming a desired width of 200 and height of 30 for both label and button
                    GUILayout.Label(keybind.Key, labelStyle, GUILayout.Width(200), GUILayout.Height(30));

                    if (GUILayout.Button(keybind.Value.ToString(), buttonStyle, GUILayout.Width(200), GUILayout.Height(30)) && !waitingForKey)
                    {
                        currentKeyToRebind = keybind.Key;
                        waitingForKey = true; // Set the flag to true to start waiting for a key press.
                    }

                    GUILayout.EndHorizontal();
                }
            }
            // Declare a boolean to keep track of whether the UI needs to be rebuilt

            // Declare a list to store the processed player IDs
            if (tabSelected == 5) // Player management tab
            {
                // Refresh the player list with valid and unique players
                HashSet<Player> uniquePlayers = new HashSet<Player>();
                List<Player> updatedPlayers = new List<Player>();

                foreach (Player player in FindObjectsOfType<Player>())
                {
                    if (player != null && player.refs != null && player.refs.view != null && player.refs.view.Controller != null && player != player.ai)
                    {
                        // Add to HashSet which automatically handles duplicates
                        if (uniquePlayers.Add(player))
                        {
                            updatedPlayers.Add(player);
                        }
                    }
                }

                PlayerControllers = updatedPlayers; // Update the main list with filtered results


                GUIStyle playerNameStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 16, // Set the font size larger
                    alignment = TextAnchor.MiddleCenter // Align player name in the center
                };

                scrollViewStyle = new GUIStyle(GUI.skin.scrollView);
                scrollPositionPlayer = GUILayout.BeginScrollView(scrollPositionPlayer, scrollViewStyle);


                // Player Name
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace(); // Center player name horizontally
                userInputText = GUILayout.TextField(userInputText, 25, GUILayout.Width(200), GUILayout.Height(30));
                GUILayout.FlexibleSpace(); // Center player name horizontally
                GUILayout.EndHorizontal();

                foreach (Player player in PlayerControllers)
                {
                    GUILayout.Space(10f); // Add spacing between player entries

                    // Group each player and their actions in a box, with elements centered
                    GUILayout.BeginVertical(GUI.skin.box);

                    // Player Name
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace(); // Center player name horizontally
                    string playerName = player.refs.view.Controller.ToString();
                    GUILayout.Label("Player: " + playerName, playerNameStyle);
                    GUILayout.FlexibleSpace(); // Center player name horizontally
                    GUILayout.EndHorizontal();

                    // Action Buttons Group
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace(); // Center buttons horizontally

                    // Kill Button
                    if (GUILayout.Button("Kill", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        MethodInfo methodInfo = typeof(Player).GetMethod("CallTakeDamageAndAddForceAndFall", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (methodInfo != null)
                        {
                            methodInfo.Invoke(player, new object[] { 99999f, new Vector3(UnityEngine.Random.Range(-0, 0f), UnityEngine.Random.Range(-0f, 0f), 5f), 2.5f });
                        }
                    }

                    // Revive Button
                    if (GUILayout.Button("Revive", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        player.CallRevive();
                    }
                    if (GUILayout.Button("Apply Text", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        Face.UpdatePlayerFaceAndName(player, userInputText);
                      
                    }
                        
                    GUILayout.FlexibleSpace(); // Center buttons horizontally
                    GUILayout.EndHorizontal(); // End Action Buttons Group

                    GUILayout.Space(10f); // Add spacing between button groups

                    // Additional Actions Buttons Group
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace(); // Center buttons horizontally

                    // GooBall Button
                    if (GUILayout.Button("GooBall", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        SpawnGooBall(player);
                    }

                    // Bomb Button
                    if (GUILayout.Button("Bomb", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        SpawnItem("Bomb",player);
                    }

                    if (GUILayout.Button("jail time", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {

                        if (player != null && player.refs != null && player.refs.view != null)
                        {
                            foreach (Bot_Weeping Weeping in FindObjectsOfType<Bot_Weeping>())
                            {
                                PhotonView photonView = Weeping.GetComponent<PhotonView>();
                                if (photonView != null)
                                {
                                    int playerViewID = player.refs.view.ViewID;
                                    photonView.RPC("RPCA_CapturePlayer", RpcTarget.All, playerViewID);
                                }
                                else
                                {
                                    Debug.LogError("No PhotonView component found on the Bot_Weeping object!");
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Selected player or required components are not available.");
                        }
                    }






                    GUILayout.FlexibleSpace(); // Center buttons horizontally
                    GUILayout.EndHorizontal(); // End Additional Actions Buttons Group

                    GUILayout.Space(10f); // Add spacing between button groups

                    // Utility Actions Buttons Group
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace(); // Center buttons horizontally

                    // CantMove Button
                    if (GUILayout.Button("CantMove", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        CantMove(player);
                    }

                    // Web Button
                    if (GUILayout.Button("Web", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        PhotonNetwork.Instantiate("Web", player.HeadPosition(), Quaternion.identity, 0, null);
                    }

                    // Fly Button
                    if (GUILayout.Button("Fly", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        SuperRagdoll(player);
                    }
                        
                    GUILayout.FlexibleSpace(); // Center buttons horizontally
                    GUILayout.EndHorizontal(); // End Utility Actions Buttons Group

                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace(); // Center buttons horizontally
                    if (GUILayout.Button("CURSEPLAYER", buttonStyle, GUILayout.Width(100), GUILayout.Height(30)))
                    {

                        Instantiate<GameObject>(bombeee.explosion, player.transform.position, player.transform.rotation);

                    }

                    GUILayout.FlexibleSpace(); // Center buttons horizontally
                    GUILayout.EndHorizontal(); // End Utility Actions Buttons Group

                    GUILayout.EndVertical(); // End group for each player

                    // Add a separator line between player entries
                    GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                }

                GUILayout.EndScrollView();
            }





            GUI.DragWindow();
        }


        // Function to spawn a GooBall item
        void SpawnGooBall(Player player)
        {
            var items = SingletonAsset<ItemDatabase>.Instance.lastLoadedItems;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == "GooBall")
                {
                    Item GooItem = items[i];
                    ItemGooBall Goo = GooItem.itemObject.GetComponent<ItemGooBall>();

                    PhotonNetwork.Instantiate(Goo.explodedGoopPref.name, player.HeadPosition(), Quaternion.identity, 0, null);
                    break; // Exit loop once GooBall is spawned
                }
            }
        }


        IEnumerator SpawnAndFollowBombs()
        {

            // Wait for a specified amount of time before making bombs follow
            yield return new WaitForSeconds(1f);  // Delay for 2 seconds

            // Iterate through all dropped items to configure following behavior
            foreach (ItemInstance item in Cheat.droppedItems)
            {
                if (item == null || item.item == null)
                {
                    continue;
                }
                
                // Check if the item is named 'Bomb'
                if (item.item.displayName == "")
                {
                    // Attach the FollowPlayer script if not already attached
                    FollowPlayer followScript = item.gameObject.GetComponent<FollowPlayer>() ?? item.gameObject.AddComponent<FollowPlayer>();
                    // Configure properties of the FollowPlayer script
                    followScript.followSpeed = 2.0f;
                    followScript.offset = new Vector3(0, 2, 0);
                    followScript.followRange = 1.0f;
                }
            }
        }




        Texture2D MakeTex(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = color; // Sets the entire texture to the specified color
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply(); // Apply all SetPixel calls
            return result;
        }
      
        


        private string MakeEnable(string label, bool toggle)
        {
            string status = toggle ? "<color=green>ON</color>" : "<color=red>OFF</color>";
            return $"{label} {status}";
        }
    }
}
