using UnityEngine;

public class FirebaseServiceBehaviour : MonoBehaviour
{
    [SerializeField] private FirebaseService firebaseService;

    public IFirebaseService Service => firebaseService;

    private void Awake()
    {
        if (firebaseService == null)
        {
            firebaseService = new FirebaseService();
        }
    }
}