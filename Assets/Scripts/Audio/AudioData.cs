using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioClip", menuName = "Audio/Create Audio clip SO")]
public class AudioData : ScriptableObject
{
    public string audioName;
    public AudioClip audioClip;
}
