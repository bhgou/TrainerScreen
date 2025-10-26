using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Database;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseService : IFirebaseService
{
    public FirebaseApp App { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseFirestore Firestore { get; private set; }
    public FirebaseDatabase Database { get; private set; }
    public event Action Initialized;

    // Добавьте URL вашей базы данных
    private const string DATABASE_URL = "https://arfitness-1e168-default-rtdb.firebaseio.com/";

    public IEnumerator Initialize()
    {
        var task = InitializeAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError($"Firebase initialization failed: {task.Exception}");
        }
        else
        {
            Debug.Log("Firebase initialized successfully from MainBootstrap");
        }
    }

    public async Task InitializeAsync()
    {
        if (App != null) return;

        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == DependencyStatus.Available)
        {
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            Firestore = FirebaseFirestore.DefaultInstance;

            try
            {
                Database = FirebaseDatabase.GetInstance(App, DATABASE_URL);
                Debug.Log($"Firebase Database initialized with URL: {DATABASE_URL}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to initialize Firebase Database: {ex}");
            }

            Initialized?.Invoke();
            Debug.Log("Firebase initialized successfully with all services");
        }
        else
        {
            Debug.LogError($"Could not resolve dependencies: {dependencyStatus}");
        }
    }
}