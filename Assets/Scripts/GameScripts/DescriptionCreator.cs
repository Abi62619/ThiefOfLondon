
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class DescriptionCreator : MonoBehaviour
{

    public TMP_Text outputText;

    public bool isShimiHappy;
    public bool isShimiAngry ;
    private bool isAmiable;
    private bool isStubborn;
    public bool isLateToEnd;
    public bool isEarlyToEnd;
    public bool isOnTime;
    private bool isDistractable;
    private bool isToThePoint;
    private bool isDisciplined;

    public bool isNoMovement;
    public bool isRunOnly;
    public bool isWalkOnly;
    private bool isLazySleepy;
    private bool isIntense;
    private bool isSerene;

    public bool hasAllItems;
    public bool hasAllHats;
    public bool completedAllQuests;
    public bool exploredWholeMap;

    private bool isHardworking;
    private bool isCurious;
    private bool isHungryCreative;
    string characterName;
    private System.Random rng = new System.Random();

    public void GenerateDescription()
    {
        characterName = PlayerPrefs.GetString("CharacterName", "Your Bito Bito");
        StringBuilder description = new StringBuilder();
        int totalTraits = CountActiveTraits();

        if (totalTraits == 0)
        {
            description.Append($"{characterName} was born thoroughly average. Neither cheerful nor grumpy, they simply existed. Time never seemed to matter—never early, never late. They didn’t run, nor did they pause to enjoy the scenery. Most items they passed by, and quests were left half-done. No curiosity, no ambition, no defining spark. A figure passing quietly through the world without ever leaving a mark.");
            // return description.ToString();
        }
        if (totalTraits == 1)
        {
            description.Append($"In every other respect {characterName} was born thoroughly average. A figure passing quietly through the world without ever leaving a mark.However ");
            // return description.ToString();
        }
        // trait relations 
        if (isShimiHappy == true)
        {
            isAmiable = true;
        }
        if (isShimiAngry == true)
        {
            isStubborn = true;
        }
        if (completedAllQuests == true)
        {
            isHardworking = true;
        }
        if (exploredWholeMap == true)
        {
            isCurious = true;
        }
        if (hasAllHats == true)
        {
            isHungryCreative = true;
        }
        if (isLateToEnd == true)
        {
            isDistractable = true;
        }
        if (isEarlyToEnd == true)
        {
            isToThePoint = true;
        }
        if (isOnTime == true)
        {
            isDisciplined = true;
        }
        if (isNoMovement == true)
        {
            isLazySleepy = true;
        }
        if (isRunOnly == true)
        {
            isIntense = true;
        }
        if (isWalkOnly == true)
        {
            isSerene = true;
        }

        // Emotion & Personality
        if (isShimiHappy)
            description.Append(RandomLine(new string[]
            {
                $"{characterName} moves through life with a bright spark.",
                $"{characterName} carries an infectious joy, glowing with happiness.",
                $" {characterName} brings light wherever they go."
            }) + " ");

        else if (isShimiAngry)
            description.Append(RandomLine(new string[]
            {
                $"{characterName} walks with vigor , icnoring kind souls.",
                $"thoughtlessness burns within {characterName}, leading them to icnor others.",
                $"{characterName}'s presence causes others anger as they carelessly brush past them ."
            }) + " ");

        if (isAmiable)
            description.Append(RandomLine(new string[]
            {
                "Naturally amiable, they draw people in.",
                "With kindness at their core, they win others with ease.",
                "Their amiable spirit makes strangers into friends."
            }) + " ");

        else if (isStubborn)
            description.Append(RandomLine(new string[]
            {
                "A stubborn resolve shapes their every decision.",
                "Once they decide, nothing can sway them.",
                "Their will is as unyielding as stone."
            }) + " ");

        // Punctuality & Focus
        if (isEarlyToEnd)
            description.Append(RandomLine(new string[]
            {
                "They’re always early, cutting straight to the point.",
                "Arriving before anyone else, they waste no time.",
                "Punctuality defines them—they prefer to end things early."
            }) + " ");

        else if (isLateToEnd)
            description.Append(RandomLine(new string[]
            {
                "Often running behind, they tend to linger.",
                "They drift toward endings at their own slow pace.",
                "Always a step late, they savor the journey more than the finish."
            }) + " ");

        else if (isOnTime)
            description.Append(RandomLine(new string[]
            {
                "They arrive precisely when they mean to—on time.",
                "Perfectly punctual, they’re never early, never late.",
                "Their timing is exact, as though guided by a clockwork heart."
            }) + " ");

        if (isDistractable)
            description.Append(RandomLine(new string[]
            {
                "Their thoughts often drift elsewhere.",
                "Focus slips away as their mind chases every passing idea.",
                "Easily distracted, they wander in both body and thought."
            }) + " ");

        else if (isToThePoint)
            description.Append(RandomLine(new string[]
            {
                "They cut through nonsense with a sharp mind.",
                "Direct and efficient, they waste no words.",
                "Every action and word is to the point."
            }) + " ");

        else if (isDisciplined)
            description.Append(RandomLine(new string[]
            {
                "Discipline defines their every move.",
                "They live by rules and structure.",
                "A disciplined soul, unwavering in focus."
            }) + " ");

        // Movement & Energy
        if (isRunOnly)
            description.Append(RandomLine(new string[]
            {
                "Always running, they carry boundless intensity.",
                "Never walking, they rush headlong into life.",
                "Running is their only pace—restless and urgent."
            }) + " ");

        else if (isWalkOnly)
            description.Append(RandomLine(new string[]
            {
                "Their pace is calm and deliberate.",
                "They never rush, preferring to walk steadily.",
                "Life for them is a measured walk."
            }) + " ");

        else if (isNoMovement)
            description.Append(RandomLine(new string[]
            {
                "They rarely move at all, staying in one place.",
                "Motionless, they prefer stillness over action.",
                "Movement is foreign to them—they stay rooted."
            }) + " ");

        if (isLazySleepy)
            description.Append(RandomLine(new string[]
            {
                "A lazy, sleepy aura surrounds them.",
                "They seem forever on the edge of a nap.",
                "Their energy drifts away like drowsy clouds."
            }) + " ");

        else if (isIntense)
            description.Append(RandomLine(new string[]
            {
                "Everything they do is done with intense passion.",
                "Intensity burns behind their every move.",
                "Their spirit radiates unstoppable energy."
            }) + " ");

        else if (isSerene)
            description.Append(RandomLine(new string[]
            {
                "Their presence feels serene, like a still pond.",
                "Serenity follows them like a soft breeze.",
                "They radiate calmness, easing those nearby."
            }) + " ");

        // Completionism
        if (hasAllItems)
            description.Append(RandomLine(new string[]
            {
                "Their bag overflows with collected trinkets.",
                "They’ve gathered every item the world offers.",
                "No object escapes their collection."
            }) + " ");

        if (hasAllHats)
            description.Append(RandomLine(new string[]
            {
                "Every hat imaginable rests in their possession.",
                "Their collection of hats borders on obsession.",
                "From crowns to caps, they own them all."
            }) + " ");

        if (completedAllQuests)
            description.Append(RandomLine(new string[]
            {
                "No quest is too small—they finish everything.",
                "Every challenge is completed with care.",
                "They leave no quest unfinished."
            }) + " ");

        if (exploredWholeMap)
            description.Append(RandomLine(new string[]
            {
                "They’ve explored every corner of the world.",
                "No nook or crannie can holdsecrets from them.",
                "Every path, cave, and mountain—visited and conquered."
            }) + " ");

        // Internal Traits
        if (isHardworking)
            description.Append(RandomLine(new string[]
            {
                "They work tirelessly, never backing down.",
                "Hard work fuels their every success.",
                "Relentless effort defines them."
            }) + " ");

        if (isCurious)
            description.Append(RandomLine(new string[]
            {
                "Curiosity fuels their every step.",
                "They’re always eager to discover the unknown.",
                "Questions lead them from one wonder to the next."
            }) + " ");

        if (isHungryCreative)
            description.Append(RandomLine(new string[]
            {
                "A hungry, creative energy drives them forward.",
                "Their imagination burns as bright as their hunger for more.",
                "Creativity and hunger mix into relentless invention."
            }) + " ");
        outputText.text = description.ToString();
        // return description.ToString().Trim();
    }

    private string RandomLine(string[] options)
    {
        return options[rng.Next(options.Length)];
    }

    private int CountActiveTraits()
    {
        int count = 0;
        bool[] traits = new bool[]
        {
            isShimiHappy, isShimiAngry, isAmiable, isStubborn,
            isLateToEnd, isEarlyToEnd, isOnTime, isDistractable, isToThePoint, isDisciplined,
            isNoMovement, isRunOnly, isWalkOnly, isLazySleepy, isIntense, isSerene,
            hasAllItems, hasAllHats, completedAllQuests, exploredWholeMap,
            isHardworking, isCurious, isHungryCreative
        };

        foreach (bool trait in traits)
        {
            if (trait) count++;
        }
        return count;
    }
}


