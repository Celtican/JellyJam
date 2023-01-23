# Q & A

## How to change the pitch dynamically, in real time?

Unfortunately that is **not possible** with Introloop. Getting scheduling to work with constant pitch change is already complicated! If that is going to be possible in the future, it needs to reschedule on **every bits of pitch change** and I think that's not a good idea for accuracy and performance. What if the pitch change happen right before the seam and the new schedule could not be fulfilled?

## Can I import time cues/loop points from my wav file header?

No, Introloop works on what imported `AudioClip` gives. As long as `AudioClip` doesn't include that information Introloop couldn't use it either.

## What about multiple loop points that I could somehow "script" it to go in order?

Currently Introloop has only 2 loop points. I do have some idea how "`ScriptedIntroloopAudio`" could work by having any number of loop points, and you could specify a looping sequence for it to go through. Or even set some kind of boolean so that the next time it arrives at a certain loop point, it will go to a different place. (For raising tension of the music, in sync, based on some conditions from the game.)

I doubt there are much demand for this niche feature on top of already nich plugin like this, but I do want it for myself. For example, I could make a music that loops the exact same 3 bars then go to final bar with cymbal and transition, with an audio file that contains only 1 bar and that final bar for extreme game size optimization.

But this will make it very awkward for the composer that he compose a full music, but when exporting he must carefully export only one of each repeating part. And it won't make sense as a single `AudioClip` file anymore. Probably we need something new like `AudioParts` that we can assign multiple `AudioClip` instead so we could save space. In the end, this feels like an entirely new system than what would fit in Introloop.

But these are likely not in this near future. Not when the [DOTS Audio](https://forum.unity.com/threads/dots-audio-discussion.651982/) is being worked on and I think I could use it when the API is fully fledged out. I could do that all completely on thread for you if I wait for the API! I am working on a new audio plugin that is feature parity with external audio engine. For example, you could use a timeline to script sequences of audio. That plugin likely could do what I described and priced higher than Introloop which is only for specific purpose. Those feature would use DOTS Audio API, I don't know if I could port them to be based on "scheduled" API for Introloop.

## Suddenly it is as if the prefab I placed in `Resources/Introloop` folder stoped working

In version 6.0.0 there was a big overhaul to the API, to reduce "magical" aspects of the plugin. To apply template source in the new version, call [`SetSingletonInstanceTemplateSource`](../getting-started/introloop-player.md#setting-template-source-for-the-singleton-instance).

Also see the [Jukebox demo](../getting-started/demo-and-samples.md#jukebox), it uses singleton instance yet starts with connected `AudioMixerGroup`. That means `SetSingletonInstanceTemplateSource` is used in the demo.

## How to hide the Scene view icon of IntroloopPlayer?

Click on "Gizmos" in your Scene tab, and then **left click on the icon image, not on the little arrow or the checkbox**. It will be greyed out and disappear from the scene view.

## What kind of things can mess up Introloop's scheduling?

Introloop uses [`AudioSettings.dspTime`](http://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html) for scheduling audio pieces, which is independent of normal game's clock. This means if you implement [`Time.timeScale`](http://docs.unity3d.com/ScriptReference/Time-timeScale.html) for pausing the game or for some other reason, it will have no effect on Introloop and it will continue playing. (You can have music still playing while the game pause, for example.)

However there are some situations that can mess up the timing and can produce weird behaviours. For example when creating games in Unity and Introloop is playing, if you make the Unity window inactive everything will stop and resume nicely (The playing audio, [`AudioSettings.dspTime`](http://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html) and [`Time.time`](http://docs.unity3d.com/ScriptReference/Time-time.html))

On the other hand if you do things like right-clicking on any `GameObject` in your Hierarchy and keep the popup menu open, you will notice that your game stopped updating but the music still plays. This will cause Introloop to miss the schedule and I can't fix it because OS-level behaviour is not in my control. **This kind of behaviour can only happen in the Editor** though, so you should not have to worry about it in real game.

Lastly, "scheduled" methods works on DSP time. Things like `AudioListener.pause = true;` will completely stop all scheduling as well.

## I'm sure my boundaries are dead exact but I can still hear a bit of discontinuity in the game!

Actually even the time with 3 decimal places is not enough, since the actual unit for location in audio is **sample**. The time will be converted to sample by Unity, and this is where an audio might slip off a few samples producing an audible seams.

I use time in seconds since it is easier to get and in most case produce good result. But if your audio is not seamless (and you are sure that your time is exact), you can try adding very small number to both boundaries **by the same amount**. Since the audio bits after the boundaries should sound the same, doing this should not change the song at all but you will get a new transition point. This new seam might work out better than the previous point. Try this several times to find out the best spot. (Remember the original point too! You can't go back more than that.)

## I want to use "samples" unit instead of "seconds" on specifying loop points for accuracy

We can't, the first reason is that [`PlayScheduled`](https://docs.unity3d.com/ScriptReference/AudioSource.PlayScheduled.html) that Introloop is utilizing is using seconds. I want to use the same unit as what is the most accurate method of playing in Unity.

I can make it so your samples converts to seconds. Sameples sounded super-accurate but it depends on sample rate. And in Unity you can change sampling rate of an imported `AudioClip` at will, not to mention the engine may do it anyways like in Android that it sounds like 24000Hz at runtime. So your hard coded samples is now wrong as your song could ended up having more or less samples depending on this settings.

## When will this be usable with WebGL build?

This limitation is actually on Unity's side. [This page lists current limitations](https://docs.unity3d.com/Manual/webgl-audio.html). It says scheduling methods **are supported**, but when I test it the `PlayScheduled` method always start audio from the beginning. So the information in that page are **wrong**.

Even in 2019 WebGL still cannot do scheduling. I don't know when will we able to do it.

## Is there a way to change a track's volume in real time?

Introloop uses 4 `AudioSource` in tandem, and changes their volume for crossfading, etc. regulary so it is not easy to directly manipulate them. For global volume/sfx that you might adjust in option screen, the current best practice is using the Audio Mixer. You can route Introloop's audio to your mixer by following [this tutorial](http://www.exceed7.com/introloop/getting-started.html).

After this, the level of that track you routed to can be controlled from script. Mainly via `SetFloat/GetFloat` which you can then link to your slider UI. [More info](https://unity3d.com/learn/tutorials/modules/beginner/5-pre-order-beta/exposed-audiomixer-parameters).


## How could Introloop accurately loop the music? As I understand Unity Update API will respond only as quickly as the next frame update.

Introloop does not split the music clip or buffer data at any point. You are correct about Unity's Update API, but Unity has an another timing mechanism specifically for audio, [dspTime](https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html).

There are 3 methods that utilize this, `AudioSource.PlayScheduled`, `SetScheduledStartTime`, and `SetScheduledEndTime`. After you call them, when dspTime comes to that point you specify an audio will play regardless of if it is in-between Update frame or not. Therefore I can set it up to loop to itself without cutting the asset. (Actually it is not looping to itself but a duplicate of itself that is waiting at the seam. When dspTime arrives one will stop and the other one will begin.)

The difficulties of using this is it is like setting a trap. You have to plan things ahead of time to also allows it to loads up the audio. Introloop's strategy is to schedule when there is halfway to go approaching the loop point. The schedule cannot be cancelled, if user decides to stop, pause or change music in the period after it already scheduled we also have to reschedule to override the old ones. The schedule can even be met while an `AudioSource` is stopping or pausing but luckily will not take effect until it is playing again, so we can reschedule before resuming to prevent it jumping to a wrong place. Lastly, with pitch all scheduling calculations will have to be scaled accordingly since `dspTime` don't know a thing about the speed which audio plays.

## Whats the difference between I write an simple script to monitor the playback time and set to start point when it reaches the loop end point?
    
Is that "simple script" to monitor works on `Update`? It will not trigger exactly at the loop point in that case since it is frame-based. There is no way the frame updates exactly the moment you want to "stitch" the audio.

Instead we need the scheduling trio : [this](https://docs.unity3d.com/ScriptReference/AudioSource.PlayScheduled.html), [this](https://docs.unity3d.com/ScriptReference/AudioSource.SetScheduledEndTime.html), and [this](https://docs.unity3d.com/ScriptReference/AudioSource.SetScheduledStartTime.html) to plan ahead of time when to start-stop each source. These methods cannot make start/stop occur immediately, **but precisely in the future**.

Introloop is a plugin that holds 4 `AudioSource` and do all 3 scheduled calls for you at an appropriate time to make it transition from intro to loop, and from loop to loop again, all within a single coherent `AudioClip`.

## I need to modify the code. Can you explain the inner workings of Introloop so that I can get started?

The **IntroloopPlayer** `GameObject` that appears when you call `IntroloopPlayer.Instance.xxxx` contains 2 `IntroloopTrack`.

Each `IntroloopTrack` represent one `IntroloopAudio` and we needed 2 because when cross-fading we must hear 2 audio at the same time at some point. Each call to `Play` method, Introloop will alternate between these two `IntroloopTrack`.

One `IntroloopTrack` contains 2 Unity's `AudioSource` so in total Introloop achieve it's playback function with 4 `AudioSource`. We need 2 `AudioSource` because of intro and looping functionality. When approaching loop point, the other `AudioSource` will be scheduled and waiting to take the baton from previous one, while the previous one is also scheduled to stop at the same time. The scheduling is accurate because it uses [`AudioSettings.dspTime`](http://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html) function in Unity which is independent from game's timescale. Scheduling will happen when one `AudioSource` is reaching halfway til the switching point.

Each `AudioSource` actually loads the same audio data (from the same `IntroloopAudio`) so memory usage is **not** doubled.

## Memory footprint of Introloop

The memory overhead will be the same as Unity's normal audio loading and playing, as it is basically just 4 AudioSources that loads the same audio. This means the memory depends on your import settings of the said audio. (Thus "Streaming" would use the least memory as it loads just around the playhead.) You can determine the memory overhead by dragging your 3.5MB 32-bit song to the AudioSource (with your desired loading type) and look at the Profiler. (Beware that just clicking the file might cause a false memory overhead as the Profiler profiles memories used for previewing in the editor)

Other than that, the additional memory overhead is from storing the GameObjects and variables that manages the looping mechanism. I don't think that is significant compared to the audio data itself.

However Introloop does something more that are different from just issuing `.Play()` `.Stop()` directly to the `AudioSource` :

1.  When you stop the audio (and the audio finished fading out completely, if you choose to stop with a fade out), it is automatically unloaded from memory. This is not the case when you just call `Stop` to the AudioSource. This is in effect if you choose "Decompress on load" or "Compressed in memory" type.
2.  If you cross fade the audio, there will be a moment when both audio are in memory. This means if both audio file has "Decompress On Load" you will have the whole size of 2 audios in the memory in that cross fading moment. However, when it finished cross fading the faded out ones will be automatically unloaded. If your memory allowance is very critical you might want to avoid the cross fading by fading out the first song then begin playing the second song later.