using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RandomAudioPlayer : MonoBehaviour {

    [SerializeField]
    AudioClip[] tracks;

    [SerializeField]
    AudioSource previewAudioSource;

    AudioSource audioSource;

    float loudestBeat = 0f;
    bool isEstimating;

    [SerializeField]
    float beatThreshold = 0.9f;

    void Start () {

        Random.InitState (420);

        audioSource = GetComponent<AudioSource> ();

    }

    void PlayNewTrack () {
        CancelInvoke ("PlayNewTrack");
        CancelInvoke ("PlayActualTrack");

        loudestBeat = 0f;
        previewAudioSource.Stop ();
        audioSource.Stop ();

        int trackDice = Random.Range (0, tracks.Length);
        previewAudioSource.clip = tracks[trackDice];
        audioSource.clip = tracks[trackDice];

        previewAudioSource.Play ();

        Invoke ("PlayActualTrack", 2f);
    }

    void PlayActualTrack () {
        audioSource.Play ();
        Invoke ("PlayNewTrack", audioSource.clip.length + 1f);
    }

    void Update () {
        if (Input.GetButtonDown ("XRI_Left_MenuButton")) {
            PlayNewTrack ();
        }
        if (previewAudioSource.isPlaying) {

            float[] spectrum = new float[64];

            previewAudioSource.GetSpectrumData (spectrum, 0, FFTWindow.Rectangular);

            for (int i = 1; i < spectrum.Length - 1; i++) {
                if (spectrum[i] > loudestBeat) {
                    Invoke ("Beat", 2f);
                    loudestBeat = spectrum[i];
                }
                if (spectrum[i] > loudestBeat * beatThreshold) {
                    Invoke ("Beat", 2f);
                }
            }
        }

    }

    void Beat () {

        HapticFeedback (Random.Range (0.5f, 0.9f));
    }

    void HapticFeedback (float magnitude) {
        // Haptic Feedback On Collison
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice> ();
        UnityEngine.XR.InputDevices.GetDevices (devices);

        foreach (var device in devices) {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities (out capabilities)) {
                if (capabilities.supportsImpulse) {
                    uint channel = 0;
                    float amplitude = magnitude;
                    float duration = 0.05f;
                    device.SendHapticImpulse (channel, amplitude, duration);
                }
            }
        }
    }

}