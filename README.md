# 🐦 Little Bird

A 3D first-person survival game where you must defend your nest from waves of forest animals. Shoot enemies, collect veggie powerups, and keep your nest alive!

---

## 🎮 Controls

| Action | Input |
|--------|-------|
| Move | WASD |
| Look | Mouse |
| Jump | Spacebar |
| Shoot | Left Click |
| Pause | Escape |

---

## 🌟 Features

- First-person shooter gameplay with raycast shooting
- 3 enemy types: Wolf, Fox, and Bull
- Enemy AI with Patrol / Chase / Attack states using NavMesh pathfinding
- Bull enemy that exclusively targets the nest
- Nest health system — game ends if the nest is destroyed
- 3 collectable veggie powerups:
  - 🥕 Carrot — Speed boost
  - 🍆 Eggplant — Restore health
  - 🍄 Mushroom — Jump boost
- Veggie pickup UI popups with color coding
- Camera shake on taking damage
- Pause menu with resume/quit options
- Main menu, Tutorial screen, and Game Over screen
- World space enemy health bars
- Terrain with painted foliage and props

---

## 🏗️ Build Instructions

1. Clone or download the project
2. Open in **Unity 2022+** (URP)
3. Make sure the following packages are installed:
   - Cinemachine (v2)
   - Input System
   - TextMeshPro
   - Terrain Tools
4. Open **File → Build Profiles** and confirm scene order:
   - 0: MainMenu
   - 1: Tutorial
   - 2: Main
   - 3: GameOver
5. Hit **Build and Run**

---

## 🎨 Asset Attributions

- **Starter Assets First Person Controller** — Unity Technologies
- **Quaternius Animal Pack** — Quaternius (CC0) — quaternius.com
- **MicroBar** — Microlight (Asset Store)
- **AllSky Free** — Richard Whitelock (Asset Store)
- **Prefab Painter 2** — Staggart Creations (Asset Store)
- Veggie collectibles — 2D sprites on Quads
- Slingshot — (https://skfb.ly/oosNB) — justincrazyeyes

---

## ⚠️ Known Issues

- Some lag during gameplay

---

## 🛠️ Built With

- Unity 2022+ (URP)
- C#
- Unity NavMesh
- Unity New Input System
- Cinemachine
- TextMeshPro
