using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string MusicName;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(MusicName);
    }
}
