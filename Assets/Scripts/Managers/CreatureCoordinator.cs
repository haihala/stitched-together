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

    public CreatureBehavior NoBehavior;

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

    public void CreateBasicEnemyWithoutReturning()
    {
        // These are for buttons.
        // Apparently button functions can't have return values.

        CreateBasicEnemy();
    }

    public Creature CreateBasicEnemy()
    {
        GameObject basic = CreatureFactory.Create(basic_torso, basic_arm);
        basic.transform.position = enemy_spawnpoint.position;
        Creature creature = basic.GetComponent<Creature>();
        creature.team = Team.Enemy;
        AddCreature(
            creature,
            Team.Enemy
            );
        return creature;
    }


    public void CreateBasicPlayerUnitWithoutReturning()
    {
        // These are for buttons.
        // Apparently button functions can't have return values.

        CreateBasicPlayerUnit();
    }

    public Creature CreateBasicPlayerUnit()
    {
        GameObject basic = CreatureFactory.Create(basic_torso, basic_arm);
        basic.transform.position = player_spawnpoint.position;
        Creature creature = basic.GetComponent<Creature>();
        creature.team = Team.Player;
        AddCreature(
            creature,
            Team.Player
            );
        return creature;
    }

    public void CreateFodderWithoutReturning()
    {
        // These are for buttons.
        // Apparently button functions can't have return values.

        CreateFodder();
    }

    public Creature CreateFodder()
    {
        GameObject basic = CreatureFactory.Create(basic_torso, basic_arm);
        basic.transform.position = fodder_spawnpoint.position;
        basic.GetComponent<Creature>().team = Team.Player;
        basic.GetComponent<Creature>().behavior = NoBehavior;
        basic.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Creature creature = basic.GetComponent<Creature>();
        AddCreature(
            creature,
            Team.Player
            );
        return creature;
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
        switch (creature.team)
        {
            case Team.Player:
                player_creatures.Remove(creature);
                break;
            case Team.Enemy:
                enemy_creatures.Remove(creature);
                CreateFodder();
                break;
        }
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