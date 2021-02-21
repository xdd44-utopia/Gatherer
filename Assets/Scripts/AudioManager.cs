using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	public Sound[] chords;
	// Start is called before the first frame update
	void Awake()
	{
		
		foreach(Sound s in sounds){
			s.source=gameObject.AddComponent<AudioSource>();
			s.source.clip=s.clip;
			s.source.pitch=s.pitch;
			s.source.volume=s.volume;
			s.source.loop=s.loop;
		}
		foreach(Sound s in chords){
			s.source=gameObject.AddComponent<AudioSource>();
			s.source.clip=s.clip;
			s.source.pitch=s.pitch;
			s.source.volume=s.volume;
			s.source.loop=s.loop;
		}
	}

	// Update is called once per frame
	public void Play(string name, float volumeMultiplier)
	{
		volumeMultiplier *= volumeMultiplier;
		Sound s=Array.Find(sounds,sound=>sound.name==name);
		if(s==null)
		return;
		s.source.volume = s.volume * volumeMultiplier;
		if(name=="UnitAttack"){
            int v = UnityEngine.Random.Range(0, chords.Length-1);
            s.source = chords[v].source;
		}
		if(s.source.isPlaying){
			Sound s2=s;
			s2.source.Play();
		}else{
		s.source.Play();
		}
	}
}
