using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEditor;
using System;
using Google;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class FirebaseAuthManager : Singleton<FirebaseAuthManager>
{
    FirebaseAuth auth; // �α���, ȸ������ ����
    FirebaseUser user; // �α����� �Ϸ�� ���� ����   
    public string UserId => user.UserId; // ���� ���̵�

    public FirebaseUser User => user; // ���� ����   

    public string result;

    protected override void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    public void SignUp(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                result = "ȸ������ ���";
                return;
            }
            if (task.IsFaulted)
            {

                result = "ȸ������ ���� : " + task.Exception.InnerException.GetBaseException().Message;
                return;
            }
            result = "ȸ������ �Ϸ�";            
        }
        );            
        return;
    }

    public async void SignIn(string email, string password)
    {               
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {          
            if (task.IsCanceled)
            {
                result = "�α��� ���";
                return;
            }
            if (task.IsFaulted)
            {
                result = "�α��� ���� : " + task.Exception.InnerException.GetBaseException().Message;
                return;
            }

            task.Wait();
            user = task.Result.User;
            result = "�α��� �Ϸ�";
        }
        );
        
        if(user != null)
        {
            SceneManager.LoadScene("MainLobby");
        }
        return;
    }

    public void SignOut()
    {
        result = "�α׾ƿ�";
        auth.SignOut();
    }
}
