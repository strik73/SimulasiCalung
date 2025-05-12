using System;

[Serializable]
public class SongList
{
    public SongData[] songs;
}

[Serializable]
public class NoteInfo
{
    public int pitch;
    public float time;
}

[Serializable]
public class SongData
{
    public string name;
    public NoteInfo[] notes;
}
