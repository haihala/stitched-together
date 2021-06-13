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
    public float spawn_interval_start;
    public float spawn_interval_ramp;
    public float spawn_amount_ramp;
    public float spawn_interval_min;

    private float spawn_interval;
    private float next_spawn = 0;
    private float extra_enemy_count = 1;
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
            spawn_interval = spawn_interval_start;
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

        CreateFodderCreature();
    }

    public Creature CreateFodderCreature()
    {
        GameObject basic = CreatureFactory.Create(basic_torso, basic_arm);
        basic.transform.position = fodder_spawnpoint.position;
        basic.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Creature creature = basic.GetComponent<Creature>();
        creature.team = Team.Player;
        basic.GetComponent<SpringJoint2D>().enabled = false;
        HangerSpawner hs = basic.GetComponent<HangerSpawner>();
        Destroy(hs.hanger);
        creature.behavior = NoBehavior;

        AddCreature(
            creature,
            Team.Player
            );
        return creature;
    }

    private void Update()
    {
        if (Time.time > next_spawn)
        {
            next_spawn = Time.time + spawn_interval;
            spawn_interval = Mathf.Max(spawn_interval - spawn_interval_ramp, spawn_interval_min);
            CreateFodderBodyPart();
            if (spawn_interval == spawn_interval_min)
            {
                for (int i = 0; i < (int)extra_enemy_count; i++)
                {
                    CreateBasicEnemy();
                }
                extra_enemy_count += spawn_amount_ramp;
            }
        }
    }

    public GameObject CreateFodderBodyPart()
    {
        float randval = UnityEngine.Random.Range(0, 100);
        if (randval > 80)
        {
            GameObject body = Instantiate(basic_torso);
            body.transform.position = fodder_spawnpoint.position;
            body.GetComponent<SpringJoint2D>().enabled = false;
            HangerSpawner hs = body.GetComponent<HangerSpawner>();
            Destroy(hs.hanger);
            body.GetComponent<Creature>().behavior = NoBehavior;
            return body;
        }
        else
        {
            GameObject limb = Instantiate(basic_arm);
            limb.transform.position = fodder_spawnpoint.position;
            Rigidbody2D rb = limb.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            Creature.SetLayer(
                limb,
                LayerMask.NameToLayer("Default")
            );
            Creature.SetSpringsEnabled(limb, false);
            return limb;
        }
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
                CreateFodderBodyPart();
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