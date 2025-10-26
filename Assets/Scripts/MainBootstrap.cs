using System;
using System.Collections;
using System.Threading;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using Zenject;

public class MainBootstrap : MonoBehaviour
{
    [Inject] private IFirebaseService _firibaseService;

    private void Start()
    {
        StartCoroutine(_firibaseService.Initialize());
    }
}
