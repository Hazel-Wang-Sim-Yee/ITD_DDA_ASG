/*
* Author: Emilie
* Date: 2025-12-09
* Description: Manages the stats of the creature and handles data retrieval and updates with Firebase.
*/
using UnityEngine;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StatManager : MonoBehaviour
{
    public static StatManager instance; // Singleton instance

    public float happiness; // Creature's happiness level
    public float cleanliness; // Creature's cleanliness level
    public float hunger; // Creature's hunger level
    public string name; // Creature's name
    public string userId; // User ID
    public int stage; // Creature's growth stage
    public float awakeness; // Creature's awakeness level

    public float Recreation; // Happiness stat
    public float Cleanliness; // Cleanliness stat
    public float Fullness; // Hunger stat
    public string BonName; // Creature's name
    public string UserId; // User ID
    public int Stage; // Growth stage
    public float Awakeness; // Awakeness stat


    private void Awake()
    {
        // Initialize the singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        awakeness = 100f;
        Awakeness = awakeness;
    }

    // Retrieve creature stats from Firebase
    public void RetrieveCreatureStats(string userId, string name, float happiness, float cleanliness, float hunger, int stage) // used in login code
    {
        UserId = userId;
        BonName = name;
        Recreation = happiness;
        Cleanliness = cleanliness;
        Fullness = hunger;
        Stage = stage;
    }

    // Update creature name
    public void UpdateCreatureName(string name) // used in naming code
    {
        BonName = name;
    }

    // Update creature data in Firebase
    public void updateDataFromPlayer(string userId, float Recreation, float Cleanliness, float Fullness)
     {
          // 1. Get userId
          if (string.IsNullOrEmpty(userId))
          {
               Debug.LogError("UserId is missing");
               return;
          }

          // 2. Read level input (your existing logic)
          /*var levelField = GameObject.Find("updateLevel");
          if (levelField == null)
          {
               Debug.LogWarning("Level input GameObject not found.");
               return;
          }

          var levelInput = levelField.GetComponent<TMP_InputField>();
          if (levelInput == null)
          {
               Debug.LogWarning("TMP_InputField missing.");
               return;
          }

          if (!int.TryParse(levelInput.text.Trim(), out int newLevel))
          {
               Debug.LogWarning("Invalid level input: " + levelInput.text);
               return;
          }*/

          // 3. Firebase reference
          DatabaseReference creatureRef =
               FirebaseDatabase.DefaultInstance
               .RootReference
               .Child("players")
               .Child(userId)
               .Child("creature");

          // 4. Update fields directly (NO dictionary)
          //creatureRef.Child("stage").SetValueAsync(newLevel);
          creatureRef.Child("happiness").SetValueAsync(Recreation);
          creatureRef.Child("cleanliness").SetValueAsync(Cleanliness);
          creatureRef.Child("hunger").SetValueAsync(Fullness);
          creatureRef.Child("name").SetValueAsync(BonName);

          Debug.Log("Creature data updated");
     }

    // Update awakeness stat
     public void UpdateAwakeness()
    {
        awakeness -= 50f;
        if (awakeness < 0f)
        {
            awakeness = 0f;
        }
        Awakeness = awakeness;
    }
}
