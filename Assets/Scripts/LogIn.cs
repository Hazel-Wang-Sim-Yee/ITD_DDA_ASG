using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEditor;
using SceneManagement = UnityEngine.SceneManagement;

public class Player
{
    public string userId;
    public string name;
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
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            errorText.text = "Login Failed. Please check your email and password.";
            return;
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
                Debug.Log(task.Result.GetRawJsonValue());
                DataSnapshot snapshot = task.Result;

                if (!snapshot.Exists)
                {
                    Debug.Log("No creature data found");
                    return;
                }

                string playerJson = snapshot.GetRawJsonValue();
                Player playerData = JsonUtility.FromJson<Player>(playerJson);

                GameManager.instance.UpdateCreatureStats(
                    playerData.userId,
                    playerData.name,
                    playerData.happiness,
                    playerData.cleanliness,
                    playerData.hunger
                );
                
                errorText.text = "";


                SceneManagement.SceneManager.LoadScene("SampleScene");
            }
            });
        }
        });
     }
}
