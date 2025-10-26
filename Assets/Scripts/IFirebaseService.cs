using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections;

public interface IFirebaseService
{
    FirebaseApp App { get; }
    FirebaseAuth Auth { get; }
    FirebaseFirestore Firestore { get; }
    IEnumerator Initialize();
    event Action Initialized;
}
