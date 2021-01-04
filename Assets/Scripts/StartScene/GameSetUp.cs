using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    //Setting up singleton
    public static GameSetUp Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);
    }

    //Dictionary holding connection num, and player class
    public Dictionary<int, GameManager.Classes> players = new Dictionary<int, GameManager.Classes>();

    void Start()
    {
      
    }

    
    void Update()
    {
        
    }
}
