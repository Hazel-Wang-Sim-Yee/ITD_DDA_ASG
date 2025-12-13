using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class ExitButton : MonoBehaviour
{
     public void updateDataFromPlayer()
     {
          // 1. Get userId
          string userId = GameManager.instance.userId;
          if (string.IsNullOrEmpty(userId))
          {
               Debug.LogError("UserId is missing");
               return;
          }

          // 2. Read level input (your existing logic)
          var levelField = GameObject.Find("updateLevel");
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
          }

          // 3. Firebase reference
          DatabaseReference creatureRef =
               FirebaseDatabase.DefaultInstance
               .RootReference
               .Child("players")
               .Child(userId)
               .Child("creature");

          // 4. Update fields directly (NO dictionary)
          creatureRef.Child("stage").SetValueAsync(newLevel);
          creatureRef.Child("happiness").SetValueAsync(GameManager.instance.Recreation);
          creatureRef.Child("cleanliness").SetValueAsync(GameManager.instance.Cleanliness);
          creatureRef.Child("hunger").SetValueAsync(GameManager.instance.Fullness);

          Debug.Log("Creature data updated");
     }

     public void OnExitButtonClick()
     {
          updateDataFromPlayer();
          SceneManager.LoadScene("LogInPage");
     }
}
