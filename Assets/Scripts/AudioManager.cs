﻿using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	// Start is called before the first frame update
	void Awake()
	{
		foreach(Sound s in sounds){
			s.source=gameObject.AddComponent<AudioSource>();
			s.source.clip=s.clip;
			s.source.pitch=s.pitch;
			s.source.volume=s.volume;
		}
	}

	void Start(){
		Play("Theme", 1);
	}

	// Update is called once per frame
	public void Play(string name, float volumeMultiplier)
	{
		volumeMultiplier *= volumeMultiplier;
		Sound s=Array.Find(sounds,sound=>sound.name==name);
		if(s==null)
		return;
		s.source.volume = s.volume * volumeMultiplier;
		s.source.Play();
	}
}
