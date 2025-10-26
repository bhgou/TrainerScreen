using Firebase;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class FirebaseLoadData : MonoBehaviour
{
    [Header("Users")]
    private List<UserData> _data;
    public List<UserData> Data => _data;

    public System.Action<List<UserData>> OnDataLoaded;
    public System.Action<List<User>> OnUsersConverted; 

    [SerializeField] private UsersManager _usersManager;

    private void Start()
    {
        StartCoroutine(InitializeFirebaseCoroutine());
    }

    private IEnumerator InitializeFirebaseCoroutine()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        var dependencyStatus = task.Result;
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirestoreORM.Initialize();
            yield return StartCoroutine(LoadUserData());
        }
        else
        {
            Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
        }
    }

    private IEnumerator LoadUserData()
    {
        var userDataTask = FirestoreORM.GetAllUser<UserData>();
        yield return new WaitUntil(() => userDataTask.IsCompleted);

        if (userDataTask.IsFaulted)
        {
            Debug.LogError($"Error loading user data: {userDataTask.Exception}");
            yield break;
        }

        _data = userDataTask.Result;
        OnDataLoaded?.Invoke(_data);
        Debug.Log($"Successfully loaded {_data.Count} users");

        List<User> convertedUsers = new List<User>();
        foreach (var data in _data)
        {
            User user = CurrentUser(data);
            convertedUsers.Add(user);
            _usersManager.Users.Add(user);
        }

        OnUsersConverted?.Invoke(convertedUsers);
    }

    public static User CurrentUser(UserData data)
    {
        return new User
        {
            RealName = data.RealName,
            DisplayName = data.DisplayName,
            Age = data.profile.age.ToString(),
            Email = data.basicInfo.email,
            Gender = data.profile.gender,
            Birthday = data.profile.birthDate.ToDateTime().ToString("MMMM dd, yyyy") ?? "Unknown"
        };
    }
}