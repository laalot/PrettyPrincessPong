using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarmonyGenerator
{
    private List<int> notesInScale = new List<int> { 0, 2, 4, 5, 7, 9, 11, 12 };
    private List<int> harmonicIntervalsMajor = new List<int> { 0, 2, 4, 5, 7, 9 };
    private List<int> harmonicIntervalsMinor = new List<int> { 3, 8, 10, 12 };

    private int currentNote;
    private System.Random random = new System.Random();

    public HarmonyGenerator()
    {
        currentNote = notesInScale[0];
    }

    public int NextNote()
    {
        var result = currentNote;

        var harmonic = harmonicIntervalsMinor.Union(harmonicIntervalsMajor).SelectMany(m => new[] { currentNote + m, currentNote - m });
        var harmonicInScale = harmonic.Intersect(notesInScale).ToList();
        currentNote = harmonicInScale[random.Next(0, harmonicInScale.Count)];

        return result;
    }
}
