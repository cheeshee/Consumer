using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerProperties 
{
    public enum Size { SMALL, NORMAL, LARGE}
    public enum CharacterType {VILLAGER, CHILD, ANIMAL, MONSTER, BOSS_MONSTER}
}

public struct InputProperties
{
    public enum Slots {FIRST, SECOND, THIRD, FOURTH, FIFTH, SIXTH, SEVENTH, EIGHTH}
    public static readonly string FIRST = "First";
    public static readonly string SECOND = "Second";
    public static readonly string THIRD = "Third";
    public static readonly string FOURTH = "Fourth";
    public static readonly string FIFTH = "Fifth";
    public static readonly string SIXTH = "Sixth";
    public static readonly string SEVENTH = "Seventh";
    public static readonly string EIGHTH = "Eighth";
}

// UPDATE EVERYTIME THE LAYERS ARE CHANGED
public enum Layers {Default, TransparentFX, Ignore_Raycast, NULL3, Water, UI, NULL6, NULL7, Ground, Player}