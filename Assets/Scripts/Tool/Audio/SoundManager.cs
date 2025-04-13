using System;
using System.Collections.Generic;
using UnityEngine;

namespace TastyTools
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoSingleton<SoundManager>
	{
		public bool mute;

		public bool logSounds;

		[SerializeField]
		private SoundClip[] soundClips;

		private Dictionary<string, SoundClip> sounds;

		private Queue<AudioSource> sourcePool;

		[Header("Source Pooling")]
		[SerializeField]
		private int poolSize;

		protected override void Awake()
		{
			this.sourcePool = new Queue<AudioSource>();
			AudioSource component = base.GetComponent<AudioSource>();
			this.sourcePool.Enqueue(component);
			while (this.sourcePool.Count < this.poolSize)
			{
				AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
				audioSource.loop = component.loop;
				audioSource.volume = component.volume;
				audioSource.spatialBlend = component.spatialBlend;
				audioSource.spread = component.spread;
				audioSource.minDistance = component.minDistance;
				audioSource.playOnAwake = component.playOnAwake;
				audioSource.outputAudioMixerGroup = component.outputAudioMixerGroup;
				this.sourcePool.Enqueue(audioSource);
			}

			this.sounds = new Dictionary<string, SoundManager.SoundClip>();
			foreach (SoundManager.SoundClip soundClip in this.soundClips)
			{
				if (soundClip.volume < 0f)
				{
					soundClip.volume = 1f;
				}
				this.sounds.Add(soundClip.name, soundClip);
				soundClip.timeLastPlayed = -soundClip.cooldown;
			}
		}

		public void PlaySound(string name, float volume = -1f)
		{
			if (this.mute)
			{
				return;
			}

			if (!this.sounds.ContainsKey(name))
			{
				if (this.logSounds)
				{
					Debug.LogWarningFormat("Invalid sound key {0}", new object[]
					{
						name
					});
				}
				return;
			}

			if (this.logSounds)
			{
				Debug.Log(string.Format("Play Sound {0} at volume {1}", name, volume));
			}

			AudioSource freeSource = this.GetFreeSource();

			SoundManager.SoundClip soundClip = this.sounds[name];
			if (Time.time - soundClip.timeLastPlayed < soundClip.cooldown)
			{
				return;
			}

			freeSource.pitch = 1f + (UnityEngine.Random.value * soundClip.pitchVariance * 2f - soundClip.pitchVariance);
			freeSource.loop = false;
			AudioClip clip = soundClip.sound;
			if (soundClip.variations != null && soundClip.variations.Length != 0)
			{
				int num = (soundClip.lastVariationPlayed < 0) ? UnityEngine.Random.Range(0, soundClip.variations.Length) : UnityEngine.Random.Range(0, soundClip.variations.Length - 1);
				if (soundClip.lastVariationPlayed >= 0 && num >= soundClip.lastVariationPlayed)
				{
					num++;
				}
				clip = soundClip.variations[num];
				soundClip.lastVariationPlayed = num;
			}

			freeSource.PlayOneShot(clip, (volume < 0f) ? soundClip.volume : volume);
			soundClip.timeLastPlayed = Time.time;
		}

		public void PlaySound(AudioClip clip, float volume = 1f)
		{
			if (this.mute)
			{
				return;
			}
			Debug.LogFormat("Play Sound {0}", new object[]
			{
				base.name
			});
			AudioSource freeSource = this.GetFreeSource();
			if (freeSource.isPlaying)
			{
				freeSource.Stop();
			}
			freeSource.loop = false;
			freeSource.clip = null;
			freeSource.PlayOneShot(clip, volume);
		}

		public void PlaySoundPitched(string name, float pitch)
		{
			if (this.mute)
			{
				return;
			}
			if (!this.sounds.ContainsKey(name) || this.sounds[name].sound == null)
			{
				Debug.LogWarningFormat("Invalid sound key {0}", new object[]
				{
					name
				});
				return;
			}
			Debug.LogFormat("Play Sound {0}", new object[]
			{
				name
			});
			AudioSource freeSource = this.GetFreeSource();

			SoundManager.SoundClip soundClip = this.sounds[name];
			freeSource.pitch = pitch;
			freeSource.loop = false;
			freeSource.clip = null;
			freeSource.PlayOneShot(soundClip.sound, soundClip.volume);
		}

		public void PlaySoundContinuous(string name, bool loop = false)
		{
			if (this.mute || !this.sounds.ContainsKey(name) || this.sounds[name].sound == null)
			{
				return;
			}
			AudioSource freeSource = this.GetFreeSource();
			if (freeSource.isPlaying)
			{
				freeSource.Stop();
			}
			freeSource.clip = this.sounds[name].sound;
			freeSource.volume = this.sounds[name].volume;
			freeSource.loop = loop;
			freeSource.Play();
		}

		public void StopSound(string name)
		{
			if (!this.sounds.ContainsKey(name) || this.sounds[name].sound == null)
			{
				return;
			}
			AudioClip sound = this.sounds[name].sound;
			foreach (AudioSource audioSource in this.sourcePool)
			{
				if (audioSource.clip == sound)
				{
					audioSource.Stop();
					audioSource.clip = null;
					break;
				}
			}
		}

		private AudioSource GetFreeSource()
		{
			AudioSource audioSource = this.sourcePool.Dequeue();
			this.sourcePool.Enqueue(audioSource);
			int num = 0;
			while (audioSource.isPlaying)
			{
				audioSource = this.sourcePool.Dequeue();
				this.sourcePool.Enqueue(audioSource);
				if (num >= this.poolSize)
				{
					break;
				}
				num++;
			}
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = null;
			return audioSource;
		}

		[Serializable]
		private class SoundClip
		{
			public string name;

			public AudioClip sound;

			public AudioClip[] variations;

			public float volume = -1f;

			public float pitchVariance;

			public float cooldown;

			[HideInInspector]
			public int lastVariationPlayed = -1;

			[HideInInspector]
			public float timeLastPlayed;
		}
	}
}

