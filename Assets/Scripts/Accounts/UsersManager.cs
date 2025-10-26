using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct User
{
    public string RealName;
    public string DisplayName;
    public string Age;
    public string Email;
    public string Gender;
    public string Birthday;
}

public class UsersManager : MonoBehaviour
{
    [SerializeField] private List<User> _users = new List<User>();
    public List<User> Users => _users;

    [SerializeField] private GameObject _userCard;
    [SerializeField] private Transform _canvas;

    private void OnEnable()
    {
        FirebaseLoadData firebaseLoader = FindObjectOfType<FirebaseLoadData>();
        if (firebaseLoader != null)
        {
            firebaseLoader.OnUsersConverted += OnUsersLoaded;
        }
    }

    private void OnDisable()
    {
        FirebaseLoadData firebaseLoader = FindObjectOfType<FirebaseLoadData>();
        if (firebaseLoader != null)
        {
            firebaseLoader.OnUsersConverted -= OnUsersLoaded;
        }
    }

    private void OnUsersLoaded(List<User> users)
    {
        _users.Clear();
        _users.AddRange(users);
        InitializeUserCards();
    }

    private void InitializeUserCards()
    {
        foreach (Transform child in _canvas)
        {
            Destroy(child.gameObject);
        }

        foreach (var user in _users)
        {
            var userCard = Instantiate(_userCard, _canvas);
            UserCard cardComponent = userCard.GetComponent<UserCard>();
            if (cardComponent != null)
            {
                cardComponent.Initialize(user);
            }
            else
            {
                Debug.LogWarning("UserCard component not found on prefab!");
            }
        }

        Debug.Log($"Created {_users.Count} user cards");
    }
}