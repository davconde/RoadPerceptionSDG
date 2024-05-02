# RoadPerceptionSDG
Road zone Synthetic Data Generator based on Unity Perception

## Installation
- Download and install an Unity Editor version satisfying [Perception requirements](https://github.com/Unity-Technologies/com.unity.perception/blob/main/com.unity.perception/Documentation~/SetupSteps.md) (Unity 2021.3.x for Perception 1.0.0-preview.1).

- Clone this repository:
```shell
git clone https://github.com/davconde/RoadPerceptionSDG.git
```

- Open this project, download the [Objects.unitypackage](https://universidadevigo-my.sharepoint.com/:u:/g/personal/david_conde_morales_uvigo_gal/EabQfLdzXyFJsSUp8SCEzuoBwZcrtHm5NMMdSmxQvJeXwA?e=THrGYh) and import it in `Assets/Objects`.

## Set Up
- For adding new objects or classes, take as example the subdirectories in `Assets/Objects/Foreground`. Every Prefab added in the respective directory must be defined as a Prefab Variant with `Assets/Object/Foreground/ForegroundObject`defined as its base.

- Change class objects instantiation probability weights in the `Global Settings` component of the `Game Controller` object.

- Randomize lighting parameters in the `Directional Light` object's `Light Randomizer` component.

- In the `Main Camera` object, define pose randomization with the `Camera Randomizer` component, and general annotation settings in the `Perception Camera` component.

## Use
In the `Perception Camera` component of `Main Camera`, check the labelers to use. Data will be stored in `%userprofile%\AppData\LocalLow\DefaultCompany\SDG`. Click on Play button to start the simulation.

For dataset processing and annotation conversion, use [pysolotools](https://github.com/Unity-Technologies/pysolotools).
