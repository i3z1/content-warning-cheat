using UnityEngine;
using Photon.Pun;

public static class Face
{
    //😀😂😅😆😎😉😊😁😍😋☹
    private static float nextBlinkTime = 5f;
    private static string currentFace = "😎";
    private static string normalFace = "😉";
    private static string blinkFace = "😁";
    private static string winkFace = "😋";
    private static float timer = 0;
    private static bool isExpressionActive = false;

    // Method to call each frame from another MonoBehaviour's Update
    public static void OnUpdate()
    {
        if (Time.time >= nextBlinkTime)
        {
            ChangeFace();
            nextBlinkTime = Time.time + UnityEngine.Random.Range(2, 5); // Schedule next change
        }

        if (isExpressionActive)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f) // Duration for showing special expressions
            {
                SetFace(normalFace);
                isExpressionActive = false;
            }
        }
    }
    public static void rainbow()
    {
        float time = Time.time;

        // Use Mathf.Sin to get a value between -1 and 1, then scale it to 0 to 1 for colors
        float red = (Mathf.Sin(time) + 1) / 2;
        float green = (Mathf.Sin(time + 2 * Mathf.PI / 3) + 1) / 2; // Offset by 2/3 π to desynchronize
        float blue = (Mathf.Sin(time + 4 * Mathf.PI / 3) + 1) / 2; // Offset by 4/3 π to desynchronize

        // Create a new color with the calculated RGB values
        Color newColor = new Color(red, green, blue);
        Player.localPlayer.refs.visor.ApplyVisorColor(newColor);
    }

    private static void ChangeFace()
    {
        // Randomly pick a face other than the normal one
        int faceChoice = UnityEngine.Random.Range(1, 4);
        switch (faceChoice)
        {
            case 1:
                currentFace = blinkFace;
                break;
            case 2:
                currentFace = winkFace;
                break;
            case 3:
                currentFace = normalFace; // Default back to normal occasionally
                break;
        }
        ShowExpressionBriefly();
    }

    private static void ShowExpressionBriefly()
    {
        SetFace(currentFace);
        timer = 0; // Reset timer
        isExpressionActive = true; // Start timer to reset face
    }

    private static void SetFace(string faceText)
    {
        float hue = PlayerPrefs.GetFloat("VisorColor", 0);
        int colorIndex = PlayerPrefs.GetInt("FaceColorIndex", 0);
        float faceRotation = PlayerPrefs.GetFloat("FaceRotation", 0);
        float faceSize = PlayerPrefs.GetFloat("FaceSize", 1);

        // Assuming Player.localPlayer and its refs are correctly set up
        if (Player.localPlayer != null && Player.localPlayer.refs != null && Player.localPlayer.refs.visor != null)
        {
            Player.localPlayer.refs.visor.SetAllFaceSettings(hue, colorIndex, faceText, faceRotation, faceSize);
            PlayerPrefs.SetString("FaceText", faceText);
            Debug.Log("Face set to: " + faceText);
        }
        else
        {
            Debug.LogError("Invalid player reference or missing components.");
        }
    }

    public static void UpdatePlayerFaceAndName(Player player, string faceText)
    {
        if (player != null && player.refs != null && player.refs.visor != null)
        {
            // Update player's name


            // Update face settings
            float hue = PlayerPrefs.GetFloat("VisorColor", 0);
            int colorIndex = PlayerPrefs.GetInt("FaceColorIndex", 0);
            float faceRotation = PlayerPrefs.GetFloat("FaceRotation", 0);
            float faceSize = PlayerPrefs.GetFloat("FaceSize", 1);

            // Set the new face settings using the userInputText
            player.refs.visor.SetAllFaceSettings(hue, colorIndex, faceText, faceRotation, faceSize);
            float time = Time.time;

            // Use Mathf.Sin to get a value between -1 and 1, then scale it to 0 to 1 for colors
            float red = (Mathf.Sin(time) + 1) / 2;
            float green = (Mathf.Sin(time + 2 * Mathf.PI / 3) + 1) / 2; // Offset by 2/3 π to desynchronize
            float blue = (Mathf.Sin(time + 4 * Mathf.PI / 3) + 1) / 2; // Offset by 4/3 π to desynchronize

            // Create a new color with the calculated RGB values
            Color newColor = new Color(red, green, blue);
            player.refs.visor.ApplyVisorColor(newColor);
            Debug.Log("Updated " + player + " with new face: " + faceText);
        }
        else
        {
            Debug.LogError("Invalid player reference or missing components.");
        }
    }
}
