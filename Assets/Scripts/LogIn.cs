/*
* Author: Emilie
* Date: 2025-12-09
* Description: Handles user login using Firebase Authentication and retrieves player data from Firebase Realtime Database.
*/
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Player
{
    public string userId; // Added userId to match retrieval
    public string name; // Creature's name
    public float happiness; // Creature's happiness level
    public float cleanliness; // Creature's cleanliness level
    public float hunger; // Creature's hunger level
    public int stage; // Creature's growth stage
}

public class LogIn : MonoBehaviour
{
    public static LogIn instance; // Singleton instance
     public TMP_InputField emailInputField; // Input field for email
     public TMP_InputField passwordInputField; // Input field for password
     public TMP_Text errorText; // Text element to display error messages

    // When Log In Button is clicked
    public void signIn()
    {
        var LogInTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInputField.text, passwordInputField.text);
        LogInTask.ContinueWith(task =>
        {
               
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            errorText.text = "Login Failed. Please check your email and password.";
            return;
        }

        // Successful login
        if (task.IsCompleted)
        {
            FirebaseUser newUser = task.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            var reference = FirebaseDatabase.DefaultInstance.RootReference;

            string userId = newUser.UserId;
            reference.Child("players").Child(userId).Child("creature").GetValueAsync().ContinueWithOnMainThread(task =>
            {
            if (task.IsFaulted)
            {
                    Debug.LogError("Error retrieving data: " + task.Exception);
                    return;
            }

            if (task.IsCompleted)
            {
                Debug.Log(task.Result.GetRawJsonValue());
                DataSnapshot snapshot = task.Result;

                if (!snapshot.Exists)
                {
                    Debug.Log("No creature data found");
                    return;
                }

                string playerJson = snapshot.GetRawJsonValue();
                Player playerData = JsonUtility.FromJson<Player>(playerJson);

                StatManager.instance.RetrieveCreatureStats(
                    userId,
                    playerData.name,
                    playerData.happiness,
                    playerData.cleanliness,
                    playerData.hunger,
                    playerData.stage
                );
                
                errorText.text = "";

                Debug.Log("Login successful, loading game scene...");
                if (playerData.stage == 1)
                    {
                       SceneManager.LoadScene("Naming&Choosing"); 
                    }
                else
                    {
                       SceneManager.LoadScene("SampleScene"); 
                    }
                
            }
            });
        }
        });
     }

        // When Sign Up Button is clicked
     public void GoToSignUpSite()
     {
        Debug.Log("Opening sign-up site...");
        Application.OpenURL("https://itdddaprojecthe.web.app/");
     }
}
