using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCoordinator : MonoBehaviour
{
    // Purpose
    //      Spawn waves of enemies
    //      Keep track of creatures on the battlefield
    public Transform enemy_spawnpoint;
    public Transform player_spawnpoint;
    public GameObject basic_torso;
    public GameObject basic_arm;

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
            player_creatures = new List<Creature>();
            enemy_creatures = new List<Creature>();
        }
    }

    public void CreateBasicEnemy()
    {
        GameObject basic_creature = CreatureFactory.Create(basic_torso, basic_arm);
        basic_creature.transform.position = enemy_spawnpoint.position;
        basic_creature.GetComponent<Creature>().team = Team.Enemy;
    }

    public void CreateBasicPlayerUnit()
    {
        GameObject basic_creature = CreatureFactory.Create(basic_torso, basic_arm);
        basic_creature.transform.position = player_spawnpoint.position;
        basic_creature.GetComponent<Creature>().team = Team.Player;
    }

    public void AddCreature(Creature creature, Team team)
    {
        switch (team)
        {
            case Team.Enemy:
                enemy_creatures.Add(creature);
                break;
            case Team.Player:
                player_creatures.Add(creature);
                break;
            default:
                Debug.LogError("Unlisted team");
                break;
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

public enum Team { Player, Enemy };