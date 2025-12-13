using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEditor;
using SceneManagement = UnityEngine.SceneManagement;

public class Player
{
    public float happiness;
    public float cleanliness;
    public float hunger;
}

public class LogIn : MonoBehaviour
{
     public TMP_InputField emailInputField;
     public TMP_InputField passwordInputField;
     public TMP_Text errorText;

    public void signIn()
    {
          
        var LogInTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInputField.text, passwordInputField.text);
        LogInTask.ContinueWith(task =>
        {
               
        if (task.IsCanceled || task.IsFaulted)
        {
            // // Unwrap AggregateException
            // errorText.text = "Email or password is incorrect. Please try again.";
            // if (task.Exception != null)
            // {
            //     var flattened = task.Exception.Flatten();
            //     foreach (var inner in flattened.InnerExceptions)
            //     {
            //         string lower = inner.Message.ToLower();

            //         if (lower.Contains("invalid email"))
            //             errorText.text = "Please enter a valid email address.";
            //         else if (lower.Contains("wrong password"))
            //             errorText.text = "Email or password is incorrect.";
            //         else if (lower.Contains("user not found"))
            //             errorText.text = "No account found with this email.";
            //         else if (lower.Contains("network error"))
            //             errorText.text = "Network error. Check your connection.";
            //         // Add more cases as needed
            //     }
            // }
        }
            


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
                    DataSnapshot snapshot = task.Result;

                    if (!snapshot.Exists)
                    {
                        Debug.Log("No creature data found");
                        return;
                    }

                    string playerJson = snapshot.GetRawJsonValue();
                    Player playerData = JsonUtility.FromJson<Player>(playerJson);

                    GameManager.instance.UpdateCreatureStats(
                        playerData.happiness,
                        playerData.cleanliness,
                        playerData.hunger
                    );
                    errorText.text = "";

                    Debug.Log("Stats loaded, switching scene");

                    SceneManagement.SceneManager.LoadScene("SampleScene");
            }
            });
        }
        });
     }
}
