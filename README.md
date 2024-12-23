# AddressableAudioClip
This repo contains a Unity script (AudioClipLoader.cs) that simplifies the management of audio clips using Unity's Addressables system. It provides a robust solution for dynamically loading, shuffling, and unloading audio assets in your Unity projects. Project also includes sample using with AudioManager.

## Features
- **Dynamic Song Loading:** Load songs asynchronously using Unity Addressables.
- **Automatic Unloading:** Free up memory by unloading songs when the scene changes.
- **Shuffle Songs:** Built-in functionality to shuffle the list of available songs using the Fisher-Yates algorithm.
- **Singleton Pattern:** Ensures a single instance for easy access across your project.

## Dependencies
- Unity Addressables System
- Unity Scene Management

## How to Use
1. Add your audio clips as Addressable assets in Unity.
2. Assign the `AssetReferenceAudioClip` list in the Unity Inspector.
3. Call the `CallSong(int index)` method to load and play a song.
4. Use `ShuffleSongs()` to randomize the song list.
5. Songs are automatically unloaded when the scene changes.

## NOTE
- You can check out AudioManager.cs for a simple example of how to use it.
