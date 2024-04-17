using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExampleAssembly {
    class ESP : MonoBehaviour {
        public static bool playerBox = true;
        public static bool monsterBox = true;
        public static bool playerName = true;
        public static bool crosshair = true;
        public static bool item = true;
        public static bool Chams = false;

        private static readonly float crosshairScale = 7f;
        private static readonly float lineThickness = 1.75f;

        private static Material chamsMaterial;

        public static Camera mainCam;
        private static bool chamsEnabled = false;
        private static Dictionary<Renderer, Material[]> originalMaterialsDict = new Dictionary<Renderer, Material[]>();

        public void Start() {
            chamsMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };

            chamsMaterial.SetInt("_SrcBlend", 5);
            chamsMaterial.SetInt("_DstBlend", 10);
            chamsMaterial.SetInt("_Cull", 0);
            chamsMaterial.SetInt("_ZTest", 8); // 8 = see through walls.
            chamsMaterial.SetInt("_ZWrite", 0);
            chamsMaterial.SetColor("_Color", Color.magenta);

            mainCam = Camera.main;

        }

        private static void DoChams()
        {

            if (!Chams)
            {
                return;
            }
            foreach (Player player in FindObjectsOfType<Player>())
            {
                if (player == null)
                {
                    continue;
                }

                foreach (Renderer renderer in player?.gameObject?.GetComponentsInChildren<Renderer>())
                {
                    if (Chams)
                    {
                        // Store original materials before applying chams
                        if (!originalMaterialsDict.ContainsKey(renderer))
                        {
                            originalMaterialsDict.Add(renderer, renderer.materials);
                        }
                        renderer.material = chamsMaterial;
                    }
                    else
                    {
                        // Restore original materials when chams are disabled
                        if (originalMaterialsDict.ContainsKey(renderer))
                        {
                            renderer.materials = originalMaterialsDict[renderer];
                            originalMaterialsDict.Remove(renderer); // Clean up dictionary
                        }
                    }
                }

            }
        }

         public void OnGUI()
         {
            if (Event.current.type != EventType.Repaint)
            {
                return;
            }
            PlayerName();
            PlayerBox();
            Crosshair();
            Items();
            //DoChams();

         }

        private static void Items()
        {
            if (!item)
            {
                return;
            }
            // Make sure there are dropped items to process
            if (Cheat.droppedItems.Length > 0)
            {
                foreach (ItemInstance item in Cheat.droppedItems)
                {
                    // Continue only if the item is not null and has a non-empty display name
                    if (item == null || string.IsNullOrWhiteSpace(item.item.displayName))
                    {
                        continue;
                    }

                    // Get the screen position of the item
                    Vector3 w2s = mainCam.WorldToScreenPoint(item.transform.position);
                    w2s.y = Screen.height - (w2s.y + 1f); // Adjust for y coordinate

                    // Calculate the distance from the camera to the item
                    float distance = Vector3.Distance(mainCam.transform.position, item.transform.position);

                    if (ESPUtils.IsOnScreen(w2s))
                    {
                        // Format the item name and distance for display
                        string displayText = $"{item.item.displayName} ({distance:F1}m)";
                        // Draw the string on the screen
                        ESPUtils.DrawString(w2s, displayText, Color.green, true, 12, FontStyle.BoldAndItalic, 1);
                    }
                }
            }
        }


        private static void PlayerBox()
        {
            if (!playerBox)
            {
                return;
            }

            if (Cheat.players.Length > 0)
            {
                foreach (Player player in Cheat.players)
                {
                    if (player != null && player != Player.localPlayer)
                    {
                        if (player.ai && !monsterBox)
                            continue;

                        Vector3 w2sHead = mainCam.WorldToScreenPoint(player.HeadPosition());
                        Vector3 w2sBottom = mainCam.WorldToScreenPoint(player.data.groundPos);

                        float height = Mathf.Abs(w2sHead.y - w2sBottom.y);

                        if (ESPUtils.IsOnScreen(w2sHead))
                        {
                            // Since the box is drawn with a 20f offset from the head position downward, adjust text position accordingly
                            Vector2 namePosition = new Vector2(w2sHead.x, Screen.height - w2sHead.y - 20f); // Positioned exactly at the top of the box

                            if (!player.ai)
                            {
                                ESPUtils.CornerBox(new Vector2(w2sHead.x, Screen.height - w2sHead.y - 20f), height / 2f, height + 20f, 2f, Color.cyan, true);
                                int fontSize = 15; // Define `fontSize` appropriately based on your needs

                                // Draw the player's name and health in bold and blue exactly at the top of the box
                                ESPUtils.DrawString(namePosition, player.refs.view.Controller.ToString(), Color.blue, true, fontSize, FontStyle.BoldAndItalic);
                            }
                        }
                    }
                }
            }
        }

        


        private static void PlayerName()
        {
            if (!playerName)
            {
                return;
            }

            if (Cheat.players.Length > 0)
            {
                foreach (Player player in Cheat.players)
                {
                    if (player != null && player != Player.localPlayer)
                    {
                        if (player.ai && !monsterBox)
                            continue;
                        Vector3 w2s = mainCam.WorldToScreenPoint(player.data.groundPos);
                        w2s.y = Screen.height - (w2s.y + 1f);

                        if (ESPUtils.IsOnScreen(w2s))
                        {
                            if (!player.ai)
                                continue;
                            else
                                ESPUtils.DrawString(w2s, player.gameObject.name.Replace("(Clone)", ""), Color.red, true, 12, FontStyle.Bold, 1);
                        }
                    }
                }
            }
        }

        private static void Crosshair() {
            if (!crosshair) {
                return;
            }

            Color32 col = new Color32(30, 144, 255, 255);

            Vector2 lineHorizontalStart = new Vector2(Screen.width / 2 - crosshairScale, Screen.height / 2);
            Vector2 lineHorizontalEnd = new Vector2(Screen.width / 2 + crosshairScale, Screen.height / 2);

            Vector2 lineVerticalStart = new Vector2(Screen.width / 2, Screen.height / 2 - crosshairScale);
            Vector2 lineVerticalEnd = new Vector2(Screen.width / 2, Screen.height / 2 + crosshairScale);

            ESPUtils.DrawLine(lineHorizontalStart, lineHorizontalEnd, col, lineThickness);
            ESPUtils.DrawLine(lineVerticalStart, lineVerticalEnd, col, lineThickness);
        }
    }
}
