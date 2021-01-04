using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Setting up singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);
    }
   public enum Classes
    {
        SHERIFF,
        DEPUTY,
        CIVILIAN,
        BANDIT,
        NUM_CLASSES
    }

}
