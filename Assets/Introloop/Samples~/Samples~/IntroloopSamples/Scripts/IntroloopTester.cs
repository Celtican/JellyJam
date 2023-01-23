using System;
using System.Collections;
using E7.Introloop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace E7.IntroloopDemo
{
    internal class IntroloopTester : MonoBehaviour
    {
        [Tooltip("This is connected to nothing in the scene, but to a prefab asset. Before the first use of " +
                 "singleton instance, we call SetSingletonInstanceTemplateSource so the spawned singleton " +
                 "has the right output AudioMixerGroup.")]
        [SerializeField] private AudioSource templateForSingletonInstance;

        [Space]
        [SerializeField] private IntroloopAudio[] allIntroloopAudio;

        [SerializeField] private IntroloopTestSwitches testSwitches;

        [SerializeField] private Text songNameText;
        [SerializeField] private Text genreText;

        [SerializeField] private Button[] playButtons;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button rememberAndStopButton;
        [SerializeField] private Toggle useFadeToggle;

        /// <summary>
        ///     Press "Remember and stop" button to make the next play use the time when it stopped.
        /// </summary>
        private float stopTime;

        private IEnumerator Start()
        {
            // This make the singleton output audio to desired AudioMixerGroup,
            // because it has been connected to the template audio source.
            IntroloopPlayer.SetSingletonInstanceTemplateSource(templateForSingletonInstance);

            songNameText.text = "";
            genreText.text = "";
            yield break;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                DSPPause();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                DSPResume();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                playButtons[0].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                playButtons[1].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                playButtons[2].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                playButtons[3].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                playButtons[4].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                playButtons[5].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                playButtons[6].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                playButtons[7].onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                pauseButton.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                resumeButton.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                stopButton.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                rememberAndStopButton.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                useFadeToggle.isOn = !useFadeToggle.isOn;
                useFadeToggle.onValueChanged.Invoke(useFadeToggle.isOn);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                IntroloopPlayer.Instance.Preload(allIntroloopAudio[0]);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("IntroloopDemo");
            }
        }

        public void PlayIntroloop()
        {
            IntroloopPlayer.Instance.Play(allIntroloopAudio[0]);
        }

        public void PlayIndex(int arrayMember)
        {
            IntroloopPlayer.Instance.Play(allIntroloopAudio[arrayMember],
                testSwitches.UsingFade ? testSwitches.FadeTime : 0,
                stopTime);
            UpdateSongInformation(arrayMember);
        }

        public void Preload(int arrayMember)
        {
            IntroloopPlayer.Instance.Preload(allIntroloopAudio[arrayMember]);
        }

        private void UpdateSongInformation(int arrayMember)
        {
            var length = allIntroloopAudio[arrayMember].ClipLength;
            string songName = "", genre = "";
            switch (arrayMember)
            {
                case 0:
                {
                    songName = "5argon - Assault";
                    genre = "Psy Techno";
                    break;
                }
                case 1:
                {
                    songName = "5argon - Assault (End)";
                    genre = "Psy Techno";
                    break;
                }
                case 2:
                {
                    songName = "5argon - Compete";
                    genre = "Latin Beats";
                    break;
                }
                case 3:
                {
                    songName = "5argon - Compete (End)";
                    genre = "Latin Beats";
                    break;
                }
                case 4:
                {
                    songName = "5argon - Otter's Celebration (Prepare)";
                    genre = "Funk";
                    break;
                }
                case 5:
                {
                    songName = "5argon - Otter's Celebration";
                    genre = "Funk";
                    break;
                }
                case 6:
                {
                    songName = "5argon - Maid Battle (RPG Arrange)";
                    genre = "Jazz Fusion";
                    break;
                }
                case 7:
                {
                    songName = "5argon - Assault (Pitch 0.4)";
                    genre = "Psy Techno";
                    break;
                }
            }

            var lengthTime = TimeSpan.FromSeconds(length);
            songName += " [" + $"{lengthTime.Minutes:D1}:{lengthTime.Seconds:D2}" + "]";
            songNameText.text = songName;
            genreText.text = genre;
        }

        public void DSPPause()
        {
            AudioListener.pause = true;
        }

        public void DSPResume()
        {
            AudioListener.pause = false;
        }

        public void Pause()
        {
            if (testSwitches.UsingFade)
            {
                IntroloopPlayer.Instance.Pause(testSwitches.FadeTime);
            }
            else
            {
                IntroloopPlayer.Instance.Pause();
            }
        }

        public void Resume()
        {
            IntroloopPlayer.Instance.Resume(testSwitches.UsingFade ? testSwitches.FadeTime : 0);
        }

        public void Stop(bool rememberStopTime)
        {
            stopTime = rememberStopTime ? IntroloopPlayer.Instance.GetPlayheadTime() : 0;
            if (testSwitches.UsingFade)
            {
                IntroloopPlayer.Instance.Stop(testSwitches.FadeTime);
            }
            else
            {
                IntroloopPlayer.Instance.Stop();
            }

            songNameText.text = "";
            genreText.text = "";
        }
    }
}