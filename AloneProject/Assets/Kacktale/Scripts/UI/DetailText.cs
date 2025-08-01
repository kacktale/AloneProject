using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerText
{
    public const string Player1 = "Kris";
    public const string Player2 = "Susie";
    public const string Player3 = "Ralshae";
    public const string PlayerYou = "You";
}

public class SkillText
{
    public const string spinOne = PlayerText.PlayerYou + " Spined for a second!\n But Nothing Happend...";
    public const string spinTwo = PlayerText.Player1 + " and " + PlayerText.Player2 + "Spined for a minite!\n" + PlayerText.Player1 + " felt Dissy";
    public const string spinAll = "All Spined for a Hour!\n But Nothing Happend...";
    public const string rudeBuster = PlayerText.Player2 + " Used ";
    public const string Heal = PlayerText.Player3 + " Used ";
    public const string Sleep = PlayerText.Player3 + " Used ";
}

public class HealText
{
    public const string Player1 = PlayerText.Player1 + " Used ";
    public const string Player2 = PlayerText.Player2 + " Used ";
    public const string Player3 = PlayerText.Player3 + " Used ";
}

public class SpareText
{
    public const string Player1 = PlayerText.Player1 + " Spared ";
    public const string Player2 = PlayerText.Player2 + " Spared ";
    public const string Player3 = PlayerText.Player3 + " Spared ";
    public const string failedSpare = "\n But Nothing Happend...";
}