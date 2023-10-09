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
    FirebaseAuth auth; // 로그인, 회원가입 정보
    FirebaseUser user; // 로그인이 완료된 유저 정보   
    public string UserId => user.UserId; // 유저 아이디

    public FirebaseUser User => user; // 유저 정보   

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
                result = "회원가입 취소";
                return;
            }
            if (task.IsFaulted)
            {

                result = "회원가입 실패 : " + task.Exception.InnerException.GetBaseException().Message;
                return;
            }
            result = "회원가입 완료";            
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
                result = "로그인 취소";
                return;
            }
            if (task.IsFaulted)
            {
                result = "로그인 실패 : " + task.Exception.InnerException.GetBaseException().Message;
                return;
            }

            task.Wait();
            user = task.Result.User;
            result = "로그인 완료";
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
        result = "로그아웃";
        auth.SignOut();
    }
}
