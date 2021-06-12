using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCoordinator : MonoBehaviour
{
    // Purpose
    //      Spawn waves of enemies
    public enum Team { Player, Enemy };
    private List<Creature> player_creatures;
    private List<Creature> enemy_creatures;

    private static CreatureCoordinator _instance;
    public static CreatureCoordinator Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public List<Creature> GetPlayerCreatures()
    {
        return player_creatures;
    }
    public List<Creature> GetEnemies()
    {
        return enemy_creatures;
    }
    public List<Creature> GetAllies(Team team)
    {
        switch (team)
        {
            case Team.Enemy:
                return GetEnemies();
            case Team.Player:
                return GetPlayerCreatures();
            default:
                Debug.LogError("Unlisted team");
                return new List<Creature>();
        }
    }
    public List<Creature> GetOpponents(Team team)
    {
        switch (team)
        {
            case Team.Enemy:
                return GetPlayerCreatures();
            case Team.Player:
                return GetEnemies();
            default:
                Debug.LogError("Unlisted team");
                return new List<Creature>();
        }
    }
}
