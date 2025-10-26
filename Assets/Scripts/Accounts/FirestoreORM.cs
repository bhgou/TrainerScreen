using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class FirestoreORM
{
    private static FirebaseFirestore _db;

    public static void Initialize()
    {
        if (_db == null)
        {
            _db = FirebaseFirestore.DefaultInstance;
        }
    }

    public static async Task<List<T>> GetAllUser<T>() where T : class
    {
        try
        {
            if (_db == null)
            {
                Debug.LogError("Firestore not initialized. Call Initialize() first.");
                return new List<T>();
            }

            CollectionReference collectionReference = _db.Collection("Users");
            QuerySnapshot snapshot = await collectionReference.GetSnapshotAsync();
            var result = new List<T>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    try
                    {
                        T user = document.ConvertTo<T>();
                        if (user != null)
                        {
                            result.Add(user);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning($"Failed to convert document {document.Id} to type {typeof(T).Name}: {ex.Message}");
                        // ѕродолжаем обработку других документов
                        continue;
                    }
                }
            }
            return result;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in GetAllUser: {ex}");
            return new List<T>();
        }
    }
}