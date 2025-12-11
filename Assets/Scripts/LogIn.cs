using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEditor;
using SceneManagement = UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class LogIn : MonoBehaviour
{
     public TMP_InputField emailInputField;
     public TMP_InputField passwordInputField;

    public void signIn()
     {
          
          var LogInTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInputField.text, passwordInputField.text);
          LogInTask.ContinueWith(task =>
          {
               
               if (task.IsCanceled)
               {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
               }
               if (task.IsFaulted)
               {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
               }

               if (task.IsCompleted)
               {
                    FirebaseUser newUser = task.Result.User;
                    Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                   
               
                    //SceneManagement.SceneManager.LoadScene("SampleScene");
               }

          });

          

     }

}
