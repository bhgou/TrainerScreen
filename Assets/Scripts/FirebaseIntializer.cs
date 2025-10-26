using Firebase;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseIntializer : MonoBehaviour
{
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
    }
    private void OnDependencyStatusReceived(Task<DependencyStatus> task)
    {
        try
        {
            if (!task.IsCompletedSuccessfully)
            {
                throw new System.Exception("Exception: ",task.Exception);
            }
            var status = task.Result;
            if(status != DependencyStatus.Available)
            {
                throw new System.Exception($"Exception: {status}");
            }
        }
        catch (Exception e) 
        {
            Debug.LogException(e);
        }
    }
}
