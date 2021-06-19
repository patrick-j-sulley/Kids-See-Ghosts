using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using System;
using UManager = UnityEngine.SceneManagement;

/// <summary>
/// The AuthManager class handles all of the variables and functions required to perform the sign in / sign up services for the connected
/// google firebase service.
/// </summary>
public class AuthManager : MonoBehaviour {

    /// <summary>
    /// The FirebaseAuth variable that is used to run commands and functions from the Firebase library
    /// </summary>
    Firebase.Auth.FirebaseAuth auth;

    /// <summary>
    /// The AuthCallback delegate that covers the handling of each of the tasks and operations
    /// </summary>
    /// <param name="task">
    /// The task that is sent from the connected Firebase system
    /// </param>
    /// <param name="operation">
    /// The operation that is sent from the connected Firebase system
    /// </param>
    /// <returns></returns>
    public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation);

    //public event AuthCallback authCallback;

    /// <summary>
    /// Records down the last task performened by the specified FirebaseUser
    /// </summary>
    public Task<FirebaseUser> theLastTask;

    /// <summary>
    /// While active, it sets the AuthManager variable to be the default instance
    /// </summary>
    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    /// <summary>
    /// The SignUp function that connects to the sign up service for the connected firebase service and returns either a successful or 
    /// unsuccessful sign up result
    /// </summary>
    /// <param name="GoodResult">
    /// The good result action that is ran in the case of a successful sign up
    /// </param>
    /// <param name="BadResult">
    /// The bad result action that is ran in the case of an unsuccessful sign up
    /// </param>
    /// <param name="email">
    /// The email string that is passed through and sent to the sign up service in the connected firebase service
    /// </param>
    /// <param name="password">
    /// The password string that is passed through and sent to the sign up service in the connected firebase service
    /// </param>
    public void SignUp(Action GoodResult, Action BadResult, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {

            theLastTask = task;
            if (task.IsFaulted || task.IsCanceled)
            {
                Dispatcher.RunOnMainThread(BadResult);
            }
            else if (task.IsCompleted)
            {
                Dispatcher.RunOnMainThread(GoodResult);

            }

        });
    }

    /// <summary>
    /// The SignIn function that connects to the sign in service for the connected firebase service and returns either a successful or 
    /// unsuccessful sign in result
    /// </summary>
    /// <param name="GoodResult">
    /// The good result action that is ran in the case of a successful sign in
    /// </param>
    /// <param name="BadResult">
    /// The bad result action that is ran in the case of an unsuccessful sign in
    /// </param>
    /// <param name="email">
    /// The email string that is passed through and sent to the sign in service in the connected firebase service
    /// </param>
    /// <param name="password">
    /// The password string that is passed through and sent to the sign in service in the connected firebase service
    /// </param>
    public void SignIn(Action GoodResult, Action BadResult, string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {

            theLastTask = task;
            if (task.IsFaulted || task.IsCanceled)
            {
                Dispatcher.RunOnMainThread(BadResult);
            }
            else if (task.IsCompleted)
            {
                Dispatcher.RunOnMainThread(GoodResult);
            }

        });
    }
}
