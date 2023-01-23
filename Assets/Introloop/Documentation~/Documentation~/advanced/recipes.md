# Recipes

These are collected from real users's queries over the years. Thanks for all your feedback!

## Transition from field music to battle music, then resume the field music at the same point
    
Use `IntroloopPlayer.GetPlayheadTime()` to remember the time. Then you can freely call a new `Play` to crossfade or change into your battle music. Later after the battle has ended, you could call `Play` the field music again **but** with that remembered time to start from the same point.

Alternatively, use [different singleton instances](../getting-started/introloop-player.md#defining-more-singleton-instances) for field and battle music. That way you don't even need to remember the play head time of the field music, just `Pause` then `Resume` later.

## Play inn music when goes to sleep then resume the previously playing music once it ends

The core problem of this is that you don't want to hard-code a wait in the game to wait for the sleep music, you want to "queue" the next music as soon as this non-looping sleep music ends.

The problem is we don't have any kind of callbacks or even a way to check if a sleep music had ended or not. There is no `isPlaying` on `IntroloopPlayer` like what you would expected on `AudioSource` so you can repeatedly query when it ends.

As for why, scheduling methods complicates the play state of `AudioSource` that it is not possible to reliably say it is "really playing" or "playing but actually just preparing".

In Introloop you can instead do this : you know that your sleep music `IntroloopAudio` is non-looping. (can also be checked at runtime : `.Loop` is `false`.) Then you can start a coroutine that wait for `IntroloopAudio.ClipLength` amount of time. You have indirectly wait for the sleep music. If you have set any `Pitch` on the `IntroloopAudio`, this `ClipLength` will be reflecting that as well.

Note : For aesthetic purpose it is recommended **not to** resume from the same position of music before you go to sleep. It is a new day, it may sounds better that the village music also starts from the beginning. (with nice intro, etc.)

## Have a pitched up version of the same music

For example you have a song for when the hero must run away before the time runs out, but when the time is under a minute you want to change to a version that is pitched up (speed up).

You can manually create a new `IntroloopAudio` asset then **assign it the same `AudioClip`**. Then you can have a different pitch settings and name it accordingly e.g. `RunawayHurry`. You do not waste double memory either when transitioning to a speed up version, as they point to the same `AudioClip` resource.