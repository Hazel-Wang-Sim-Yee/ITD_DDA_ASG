using UnityEngine;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    public float happiness;
    public float cleanliness;
    public float hunger;
    public string name;
    public string userId;
    public int stage;
    public float awakeness;

    public float Recreation;
    public float Cleanliness;
    public float Fullness;
    public string BonName;
    public string UserId;
    public int Stage;
    public float Awakeness;


    private void Awake()
    {
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

    public void RetrieveCreatureStats(string userId, string name, float happiness, float cleanliness, float hunger, int stage) // used in login code
    {
        UserId = userId;
        BonName = name;
        Recreation = happiness;
        Cleanliness = cleanliness;
        Fullness = hunger;
        Stage = stage;


        Debug.Log("Creature stats retrieved");
    }

    public void UpdateCreatureName(string name) // used in naming code
    {
        BonName = name;
        Debug.Log("Creature name updated to: " + BonName);
    }

    /*public void UpdateCreatureStats() // used in game manager code
    {
        userId = StatManager.instance.UserId;
        BonName = StatManager.instance.BonName;
        Recreation = StatManager.instance.Recreation;
        Cleanliness = StatManager.instance.Cleanliness;
        Fullness = StatManager.instance.Fullness;
        Debug.Log("Creature stats updated");
    }*/

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
