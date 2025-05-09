using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SongData
{
    public string name;
    public int[] sequence;
}
[Serializable]
public class SongList
{
    public SongData[] songs;
}
