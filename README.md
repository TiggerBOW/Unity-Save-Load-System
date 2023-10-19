# Unity-Save-Load-System
This is a Unity project that demonstrates a robust save and load system for your game or application. It includes a SaveLoadManager script that can handle data serialization and deserialization, allowing you to save and load game progress, player settings, and more.

## Features

- Save and load game data for multiple slots.
- Create and manage save slots.
- Easily serialize and deserialize data using BinaryFormatter.
- Delete specific saved data or entire save slots.
- Flexible and customizable for your specific use case.

## How to Use

1. Attach the `SaveLoadManager` script to an empty GameObject in your Unity project.
2. Implement the `ISaveable` interface in your data classes.
3. Use the `SaveData<T>(T data)` method to save data of type `T`.
4. Use the `LoadData<T>()` method to load data of type `T`.
5. Use the `DeleteData<T>(string optionalPath)` method to delete specific data.
6. Use the `DeleteSlot(int slotIndex)` method to delete a save slot.
7. Use the `DeleteAllData()` method to reset to factory settings.

Make sure to customize the save paths and data classes according to your specific requirements.

## Acknowledgments

- Special thanks to the Unity community for valuable insights and contributions.
- Inspired by the need for a simple and flexible save/load system in Unity projects.
