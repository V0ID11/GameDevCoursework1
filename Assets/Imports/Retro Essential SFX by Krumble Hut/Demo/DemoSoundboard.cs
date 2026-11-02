using UnityEngine;

namespace KrumbleHut.RetroEssentialSFX
{
    public class DemoSoundboard : MonoBehaviour //DEMO USE ONLY
    {
        // This script is designed to be used in a demo scene to showcase the sound effects.
        // It provides methods to play various sound effects and loops, simulating a soundboard.
        // The audio source and clips are assigned in the Unity Inspector.

        [Header("Audio Source")]
        [SerializeField] private AudioSource audioSource;

        [Header("UI Sounds")]
        [SerializeField] private AudioClip[] uiSounds;

        [Header("Player Sounds")]
        [SerializeField] private AudioClip[] playerSounds;

        [Header("Combat Sounds")]
        [SerializeField] private AudioClip[] combatSounds;

        [Header("Game Sounds")]
        [SerializeField] private AudioClip[] gameSounds;

        [Header("Misc Sounds")]
        [SerializeField] private AudioClip[] misSounds;

        [Header("Loops")]
        [SerializeField] private AudioClip[] loops;

        [Header("Footsteps")]
        [SerializeField] private AudioClip[] footstepSounds;

        private int counter = 0;

        // ---------- PUBLIC METHODS ----------

        public void PlayLaser()
        {
            if (counter > 2)
            {
                counter = 0;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlaySlash()
        {
            audioSource.PlayOneShot(combatSounds[3]);
        }
        public void PlaySpit()
        {
            audioSource.PlayOneShot(combatSounds[4]);
        }
        public void PlayCharging() //5 - 6
        {
            if (counter > 6 || counter < 5)
            {
                counter = 5;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayDamage()
        {
            if (counter > 10 || counter < 7)
            {
                counter = 7;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayBigDamage()
        {
            if (counter > 13 || counter < 11)
            {
                counter = 11;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayCrit()
        {
            if (counter > 15 || counter < 14)
            {
                counter = 14;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayExplode()
        {
            if (counter > 20 || counter < 17)
            {
                counter = 17;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayShoot()
        {
            if (counter > 26 || counter < 24)
            {
                counter = 24;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayImpact()
        {
            if (counter > 23 || counter < 21)
            {
                counter = 21;
            }
            audioSource.PlayOneShot(combatSounds[counter]);
            counter++;
        }
        public void PlayDeath()
        {
            audioSource.PlayOneShot(combatSounds[16]);
        }
        public void PlayJump()
        {
            if (counter > 3)
            {
                counter = 0;
            }

            audioSource.PlayOneShot(playerSounds[counter]);
            counter++;
        }
        public void PlayMenuOpen()
        {
            if (counter > 6 || counter < 5)
            {
                counter = 5;
            }

            audioSource.PlayOneShot(playerSounds[counter]);
            counter++;
        }
        public void PlayMenuClose()
        {
            audioSource.PlayOneShot(playerSounds[4]);
        }
        public void PlayPickup()
        {
            if (counter > 9 || counter < 7)
            {
                counter = 7;
            }

            audioSource.PlayOneShot(playerSounds[counter]);
            counter++;
        }
        public void PlayPickupCoin()
        {
            audioSource.PlayOneShot(playerSounds[10]);
        }
        public void PlayDash()
        {
            if (counter > 12 || counter < 11)
            {
                counter = 11;
            }

            audioSource.PlayOneShot(playerSounds[counter]);
            counter++;
        }
        public void PlayPlayerDeath()
        {
            if (counter > 14 || counter < 13)
            {
                counter = 13;
            }

            audioSource.PlayOneShot(playerSounds[counter]);
            counter++;
        }
        public void PlayPowerup()
        {
            if (counter > 17 || counter < 15)
            {
                counter = 15;
            }

            audioSource.PlayOneShot(playerSounds[counter]);
            counter++;
        }
        public void PlayUIBackCancel()
        {
            audioSource.PlayOneShot(uiSounds[0]);
        }
        public void PlayUIBlip()
        {
            if (counter > 3 || counter < 1)
            {
                counter = 1;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayUIClick()
        {
            if (counter > 5 || counter < 4)
            {
                counter = 4;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayUIClickBlip()
        {
            if (counter > 7 || counter < 6)
            {
                counter = 6;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayUIConfirm()
        {
            if (counter > 11 || counter < 8)
            {
                counter = 8;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayUIError()
        {
            if (counter > 13 || counter < 12)
            {
                counter = 12;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayUIHover()
        {
            audioSource.PlayOneShot(uiSounds[14]);
        }
        public void PlayUIPopupClose()
        {
            if (counter > 16 || counter < 15)
            {
                counter = 15;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayUIPopupOpen()
        {
            if (counter > 18 || counter < 17)
            {
                counter = 17;
            }

            audioSource.PlayOneShot(uiSounds[counter]);
            counter++;
        }
        public void PlayGameOver()
        {
            audioSource.PlayOneShot(gameSounds[0]);
        }

        public void PlaySecret()
        {
            audioSource.PlayOneShot(gameSounds[1]);
        }

        public void PlaySuccess()
        {
            if (counter > 3 || counter < 2)
            {
                counter = 2;
            }

            audioSource.PlayOneShot(gameSounds[counter]);
            counter++;
        }

        public void PlayWarp()
        {
            if (counter > 6 || counter < 4)
            {
                counter = 4;
            }

            audioSource.PlayOneShot(gameSounds[counter]);
            counter++;
        }
        // ---------- MISC SOUNDS ----------

        public void PlayAlarm()
        {
            audioSource.PlayOneShot(misSounds[0]);
        }

        public void PlayBeep()
        {
            audioSource.PlayOneShot(misSounds[1]);
        }

        public void PlayConfused()
        {
            audioSource.PlayOneShot(misSounds[2]);
        }

        public void PlayDissolve()
        {
            audioSource.PlayOneShot(misSounds[3]);
        }

        public void PlayHammerHit()
        {
            audioSource.PlayOneShot(misSounds[4]);
        }

        public void PlayPluck()
        {
            audioSource.PlayOneShot(misSounds[5]);
        }

        public void PlaySplash()
        {
            audioSource.PlayOneShot(misSounds[6]);
        }

        public void PlayTelephone()
        {
            audioSource.PlayOneShot(misSounds[7]);
        }

        public void PlayVoiceGrunt()
        {
            audioSource.PlayOneShot(misSounds[8]);
        }

        // ---------- LOOPS ----------

        public void PlayGameOverLoop()
        {
            audioSource.clip = loops[0];
            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayMusicLoop()
        {
            audioSource.clip = loops[1];
            audioSource.loop = true;
            audioSource.Play();
        }

        public void StopLoop()
        {
            audioSource.loop = false;
            audioSource.Stop();
        }

        // ---------- FOOTSTEPS ----------

        public void PlayFootstepDirt()
        {
            if (counter > 2 || counter < 0)
            {
                counter = 0;
            }

            audioSource.PlayOneShot(footstepSounds[counter]);
            counter++;
        }

        public void PlayFootstepGrass()
        {
            if (counter > 5 || counter < 3)
            {
                counter = 3;
            }

            audioSource.PlayOneShot(footstepSounds[counter]);
            counter++;
        }

        public void PlayFootstepMuted()
        {
            if (counter > 8 || counter < 6)
            {
                counter = 6;
            }

            audioSource.PlayOneShot(footstepSounds[counter]);
            counter++;
        }

        public void PlayFootstepQuick()
        {
            if (counter > 10 || counter < 9)
            {
                counter = 9;
            }

            audioSource.PlayOneShot(footstepSounds[counter]);
            counter++;
        }





        // ---------- CORE PLAYBACK ----------

        private void PlayRandom(AudioClip[] clips)
        {
            if (clips == null || clips.Length == 0)
            {
                Debug.LogWarning("DemoSoundboard: No clips assigned.");
                return;
            }

            AudioClip clip = clips[Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(clip);
        }

        public void PlaySound(int category, int index)
        {
            audioSource.pitch = 1f;
            switch (category)
            {
                case 0:
                    audioSource.PlayOneShot(uiSounds[index]);
                    break;
                    case 1:
                    audioSource.PlayOneShot(playerSounds[index]);
                    break;
                    case 2:
                    audioSource.PlayOneShot(combatSounds[index]);
                    break;
                    case 3:
                    audioSource.PlayOneShot(gameSounds[index]);
                    break;
                    case 4:
                    audioSource.PlayOneShot(misSounds[index]);
                    break;
            }
        }
        public void PlaySound(int category, int index, float minPitch, float maxPitch)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            switch (category)
            {
                case 0:
                    audioSource.PlayOneShot(uiSounds[index]);
                    break;
                case 1:
                    audioSource.PlayOneShot(playerSounds[index]);
                    break;
                case 2:
                    audioSource.PlayOneShot(combatSounds[index]);
                    break;
                case 3:
                    audioSource.PlayOneShot(gameSounds[index]);
                    break;
                case 4:
                    audioSource.PlayOneShot(misSounds[index]);
                    break;
            }
        }

        // ---------- OPTIONAL UTILITY ----------

        public void SetMasterVolume(float volume)
        {
            audioSource.volume = Mathf.Clamp01(volume);
        }
    }
}
