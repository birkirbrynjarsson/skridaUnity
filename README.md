# Skrida - Skriðuklaustur AR

Skrida is an augmented reality, location-history themed game project built with Unity and targeted at mobile devices. Players discover clues and treasures through AR trackables, gain XP, level up, and progress through a narrative inspired by Skriðuklaustur history.

This repository contains the full Unity project, including assets, gameplay scripts, legacy plugin integrations, and platform configuration files.

## Table of Contents

- [Project Summary](#project-summary)
- [Demo Video](#demo-video)
- [Core Gameplay Loop](#core-gameplay-loop)
- [Technology Stack](#technology-stack)
- [Repository Structure](#repository-structure)
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Configuration](#configuration)
- [Running and Testing](#running-and-testing)
- [Building for Mobile](#building-for-mobile)
- [Data Model and Persistence](#data-model-and-persistence)
- [Key Scripts](#key-scripts)
- [Known Legacy Constraints](#known-legacy-constraints)
- [Troubleshooting](#troubleshooting)
- [Security and Secrets](#security-and-secrets)
- [Contributing](#contributing)

## Project Summary

The game combines:

- AR marker/target detection (Vuforia)
- Collectible clues and treasures
- XP and level progression
- Narrative text content and quiz/challenge content
- Local save data plus Firebase-backed content updates

At runtime, the game:

1. Loads player progress from local storage.
2. Loads local clue/message data and checks remote message version in Firebase.
3. Spawns AR interactions (scrolls/chests) when trackables are detected.
4. Awards XP for interactions and updates progression.
5. Saves updated player state locally and to Firebase.

## Demo Video

https://github.com/user-attachments/assets/919cf955-fc39-4cb3-afcc-1b2d3b6e6278

## Core Gameplay Loop

1. Scan an AR target.
2. A clue scroll or treasure chest appears.
3. Tap to collect/open.
4. Gain XP and potentially level up.
5. Unlock and review found clues/treasures in UI.
6. Repeat until full collection/progression milestones are reached.

## Technology Stack

- Unity Editor version: 2017.4.30f1
- Rendering/game engine: Unity 2017 LTS-era pipeline
- AR SDK: Vuforia (bundled in project)
- Backend/content sync: Firebase Realtime Database
- UI/animation plugins in project: DoozyUI, iTween, PlayMaker (plus other bundled asset packages)
- Primary scripting language: C#

Notable Android dependency lockfile:

- ProjectSettings/AndroidResolverDependencies.xml

Unity package manager usage appears minimal in this snapshot:

- UnityPackageManager/manifest.json currently has an empty dependencies object.

## Repository Structure

Top-level folders:

- Assets: all Unity assets, scripts, scenes, plugins, and third-party packages
- ProjectSettings: Unity project configuration files
- UnityPackageManager: package manager manifest
- QCAR: Vuforia/QCAR-related data

Primary game code and content live under:

- Assets/Assets/Scripts
- Assets/Assets/Scenes
- Assets/Assets/Resources
- Assets/Assets/Questions
- Assets/Assets/Database

Third-party and framework folders include:

- Assets/Vuforia
- Assets/Firebase
- Assets/DoozyUI
- Assets/PlayMaker
- Assets/iTween
- Assets/Plugins

## Prerequisites

For the most reliable results, use the same major tooling era as the project:

- Unity Hub with Unity Editor 2017.4.30f1 installed
- Xcode (for iOS builds)
- Android SDK/NDK + JDK compatible with Unity 2017 workflow (for Android builds)
- A Vuforia developer account and valid license key
- Firebase project access if you need live backend features

## Quick Start

1. Open Unity Hub.
2. Add this repository as an existing Unity project.
3. Open the project with Unity 2017.4.30f1.
4. Allow Unity to import and recompile all assets.
5. Open scene:
   - Assets/Assets/Scenes/Main 1.unity
6. Press Play in the editor for a smoke test.

Build settings note:

- The build settings metadata indicates Assets/Assets/Scenes/Main 1.unity as an enabled scene.

## Configuration

### Vuforia

- Confirm your Vuforia license key in the Vuforia configuration inside Unity.
- Ensure ARCamera and dataset configuration are valid for your targets.
- If camera initialization fails, inspect Vuforia-related error handling in Assets/Common/InitErrorHandler.cs.

### Firebase

The project includes Firebase setup files:

- Assets/google-services.json (Android)
- Assets/GoogleService-Info.plist (iOS)

Database usage in gameplay code is configured in:

- Assets/Assets/Scripts/DatabaseControllerScript.cs

Observed database URL in code:

- https://skriduklaustur-unity.firebaseio.com/

If you are onboarding a new environment:

1. Replace Firebase config files with environment-appropriate ones.
2. Verify database read/write rules for required collections (messages, challenges, players).
3. Confirm network access from test devices.

### Android Resolver

Google Play Services and Firebase Android artifacts are managed via:

- ProjectSettings/AndroidResolverDependencies.xml

If Android dependency conflicts appear, run the External Dependency Manager resolution flow from Unity editor menus.

## Running and Testing

Editor validation checklist:

1. Open Main 1 scene.
2. Verify no critical compile errors.
3. Validate UI initialization (player profile, XP bar, clue list).
4. Validate that local save data is created on first launch.
5. Validate Firebase fetch behavior for messages/challenges.
6. Validate AR target detection and interaction callbacks.

Device validation checklist:

1. Camera permission prompt is shown/handled.
2. AR targets are detected in realistic lighting.
3. Scroll/chest interactions trigger XP and saved progress.
4. Relaunch app and confirm persistence from prior session.

## Building for Mobile

### Android

1. Switch platform to Android in Build Settings.
2. Confirm package identifier, version code, min/target SDK in Player Settings.
3. Validate keystore settings before release builds.
4. Build APK/AAB and deploy to device.

### iOS

1. Switch platform to iOS in Build Settings.
2. Confirm bundle identifier and signing setup.
3. Build Xcode project from Unity.
4. Complete signing/capabilities in Xcode and run on device.

## Data Model and Persistence

### Player Data

Player progress is represented by GameData and related classes:

- GameData
- FoundMessage
- FoundTreasure
- Message

Primary behaviors:

- New player initialization with generated playerId
- XP, level, title progression
- Found clues/treasures tracking
- Serialization to local storage

### Local Files

Local persistence is implemented with binary serialization and app persistent data path:

- save.dat (player state)
- messages.dat (cached messages)
- messagesVersion.dat (message version)

### Remote Sync

Remote data flow managed by DatabaseControllerScript:

- Fetches messagesVersion
- Pulls messages if remote version differs from local version
- Fetches challenges
- Pushes player state updates to players/{playerId}

## Key Scripts

Gameplay and systems scripts of interest:

- Assets/Assets/Scripts/PlayerControllerScript.cs
  - Player profile, XP progression, level/title updates, save/load hooks
- Assets/Assets/Scripts/ClueControllerScript.cs
  - Found clue lifecycle, notification badges, clue list ordering
- Assets/Assets/Scripts/ItemControllerScript.cs
  - Treasure progression and inventory count updates
- Assets/Assets/Scripts/DatabaseControllerScript.cs
  - Firebase synchronization, local cache/version logic
- Assets/Assets/Scripts/ScrollTrackableScript.cs
  - AR clue target detection and spawned scroll interactions
- Assets/Assets/Scripts/ChestTrackableScript.cs
  - AR treasure target detection and chest collection flow
- Assets/Assets/Scripts/messageScript.cs
  - Clue UI behavior and open-state XP awards

## Known Legacy Constraints

Because this project is pinned to an older Unity generation and plugin stack, expect migration work if upgrading:

- Unity 2017-era APIs and plugin integrations
- Legacy Firebase SDK package set
- Legacy Vuforia package layout
- Older Android support library artifacts (pre-AndroidX)

If upgrading Unity, plan for staged migration with branch isolation and full device regression testing.

## Troubleshooting

### Unity import/compile issues

- Ensure the editor version is 2017.4.30f1.
- Reimport all assets if scripts/plugins fail to resolve.
- Check plugin platform settings for Android/iOS compatibility.

### Vuforia initialization errors

- Verify license key validity and product type.
- Confirm camera permissions on the device.
- Check runtime logs and InitErrorHandler output.

### Firebase data not loading

- Verify firebase config files match the active Firebase project.
- Confirm realtime database URL and data schema.
- Check network connectivity and Firebase rules.

### Missing or broken references in scene

- Open Main 1 scene and inspect missing script components.
- Confirm required prefabs exist under Resources paths used at runtime.

## Security and Secrets

- Treat Firebase credentials/config files as environment-specific.
- Rotate keys/config for production if this repository has been shared widely.
- Avoid committing private service-account credentials.
- Keep signing keystores secure and out of public repos where possible.

## Contributing

Recommended contribution workflow:

1. Create a feature branch.
2. Make focused changes with clear commit messages.
3. Validate in editor and on at least one physical device.
4. Document any scene/prefab wiring changes in your pull request.
5. Include migration notes when touching legacy plugin integrations.

---

If you plan to modernize this project, start by documenting current runtime behavior (scene flow, AR targets, backend schema) before changing Unity or SDK versions. That baseline will reduce regression risk significantly.
