using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCoordinator : MonoBehaviour
{
    // Purpose
    //      Spawn waves of enemies
    //      Keep track of creatures on the battlefield
    public Transform enemy_spawnpoint;
    public Transform player_spawnpoint;
    public Transform fodder_spawnpoint;
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
        CreatureCoordinator.Instance.AddCreature(
            basic_creature.GetComponent<Creature>(),
            Team.Enemy
            );
    }

    public void CreateBasicPlayerUnit()
    {
        GameObject basic_creature = CreatureFactory.Create(basic_torso, basic_arm);
        basic_creature.transform.position = player_spawnpoint.position;
        basic_creature.GetComponent<Creature>().team = Team.Player;
        CreatureCoordinator.Instance.AddCreature(
            basic_creature.GetComponent<Creature>(),
            Team.Player
            );
    }

    public void CreateFodder()
    {
        GameObject basic_creature = CreatureFactory.Create(basic_torso, basic_arm);
        basic_creature.transform.position = fodder_spawnpoint.position;
        basic_creature.GetComponent<Creature>().team = Team.Player;
        CreatureCoordinator.Instance.AddCreature(
            basic_creature.GetComponent<Creature>(),
            Team.Player
            );
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
        }
    }

    public void RemoveCreature(Creature creature)
    {
        player_creatures.Remove(creature);
        enemy_creatures.Remove(creature);
    }

    public List<Creature> GetPlayerUnits()
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
                return GetPlayerUnits();
        }
        throw new Exception("Team switch defaulted");
    }

    public List<Creature> GetOpponents(Team team)
    {
        switch (team)
        {
            case Team.Enemy:
                return GetPlayerUnits();
            case Team.Player:
                return GetEnemies();
        }
        throw new Exception("Team switch defaulted");
    }
}

public enum Team { Player, Enemy };