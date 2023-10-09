using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;  

    private void OnGUI()
    {
        GUIStyle style = new()
        {
            fontSize = 50,
        };
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(200, 0, 400, 400), FirebaseAuthManager.Instance.result, style);
    }
    
    public void SingUp()
    {
        string e = email.text;
        string pw = password.text;

        FirebaseAuthManager.Instance.SignUp(e, pw);
    }

    public void SignIn()
    {
       
        string e = email.text;
        string pw = password.text;

       FirebaseAuthManager.Instance.SignIn(e, pw);           
    }

    public void SignOut()
    {
        FirebaseAuthManager.Instance.SignOut();
    }

    public void Skip()
    {
        SceneManager.LoadScene("MainLobby");
    }
}
