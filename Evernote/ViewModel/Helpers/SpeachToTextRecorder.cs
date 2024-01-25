using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Evernote.ViewModel.Helpers
{
    public static class SpeechToTextRecorder
    {
        private const string Region = "germanywestcentral";
        private const string ApiKey = "986a546b737b4964a698418afe101b30";

        public static async Task<string> Record()
        {
            var speechConfig = SpeechConfig.FromSubscription(Region, ApiKey);
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = await recognizer.RecognizeOnceAsync();

            return result.Text;
        }
    }
}
