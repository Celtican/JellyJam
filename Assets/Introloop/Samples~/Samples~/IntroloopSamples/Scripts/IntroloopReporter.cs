using System.Text;
using E7.Introloop;
using UnityEngine;
using UnityEngine.UI;

namespace E7.IntroloopDemo
{
    internal class IntroloopReporter : MonoBehaviour
    {
        [SerializeField] private Text text1;
        [SerializeField] private Text text2;
        private string[] debug1, debug2;
        private StringBuilder sb;

        // Use this for initialization
        private void Start()
        {
            sb = new StringBuilder();
        }

        // Update is called once per frame
        private void Update()
        {
            debug1 = IntroloopPlayer.Instance.GetDebugStringsTrack1();
            debug2 = IntroloopPlayer.Instance.GetDebugStringsTrack2();
            sb.Length = 0;
            sb.Append("DSP TIME : " + AudioSettings.dspTime + "\n");
            sb.Append("<color=\"yellow\">= Track 1 =\n</color>");
            foreach (var s in debug1)
            {
                sb.Append(s);
                sb.Append("\n");
            }

            text1.text = sb.ToString();
            sb.Length = 0;
            sb.Append("\n");
            sb.Append("<color=\"yellow\">= Track 2 =\n</color>");
            foreach (var s in debug2)
            {
                sb.Append(s);
                sb.Append("\n");
            }

            text2.text = sb.ToString();
        }
    }
}