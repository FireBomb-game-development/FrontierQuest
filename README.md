# FrontierQuest

FrontierQuest is a Unity 2D action RPG prototype focused on platforming, melee combat, enemy AI, elemental status effects, stat driven damage, buffs, chests, VFX, and a skill tree UI.

The current project contains one enabled scene:

- `Assets/Scenes/SampleScene.unity`

## Unity Version

Open the project with:

- Unity `6000.0.60f1`
- Universal Render Pipeline `17.0.4`

The project uses Unity Package Manager dependencies stored in `Packages/manifest.json`.

Key packages:

- `com.unity.render-pipelines.universal`
- `com.unity.inputsystem`
- `com.unity.cinemachine`
- `com.unity.2d.tilemap`
- `com.unity.ugui`
- `com.unity.visualscripting`
- `com.unity.test-framework`

## How To Run

1. Open Unity Hub.
2. Add this folder as an existing project.
3. Use Unity `6000.0.60f1`.
4. Open `Assets/Scenes/SampleScene.unity`.
5. Press Play.

## Controls

Controls are defined in `Assets/Input System/PlayerInputSet.inputactions`.

| Action | Input |
| --- | --- |
| Move | `WASD` |
| Jump | `Space` |
| Dash | `Left Shift` |
| Attack | Left mouse button |
| Counter attack | `Q` |

## Gameplay Systems

### Player

The player is implemented in `Assets/Scripts/Player`.

Main capabilities:

- Ground movement
- Jumping and falling
- Wall slide and wall jump
- Dash
- Basic attack combo
- Jump attack
- Counter attack
- Death state

Player behavior is state driven. `Player.cs` creates and owns the player states, while classes under `Assets/Scripts/Player/PlayerStates` handle state specific behavior.

### Enemy AI

Enemy logic lives under `Assets/Scripts/Enemy`.

The current enemy implementation is `Enemy_Skeleton`, backed by reusable enemy states:

- Idle
- Move
- Battle
- Attack
- Stunned
- Dead

Enemies use raycast based player detection and can enter battle behavior when the player is detected. Skeleton enemies implement `ICounterable`, allowing the player's counter attack to stun them during a counter window.

### State Machine

Shared state machine infrastructure is in `Assets/Scripts/StateMachine`.

The project uses a small custom state machine:

- `StateMachine` tracks the active state and handles transitions.
- `EntityState` is the shared base state.
- `PlayerState` adds player input and player animation behavior.
- `EnemyState` adds enemy specific behavior.

### Entity Layer

Shared entity components are in `Assets/Scripts/Entity`.

Important components:

- `Entity`: base movement, facing, collision checks, knockback, slowdown, and animation trigger routing.
- `Entity_Health`: damage intake, mitigation, evasion, health regeneration, knockback, and death.
- `Entity_Combat`: target detection, physical damage, elemental damage, status application, and on hit VFX.
- `Entity_Stats`: stat calculations for health, damage, armor, evasion, critical hits, elemental damage, and resistance.
- `Entity_StatusHandler`: burn, chill, and electrify status effects.
- `Entity_VFX`: entity visual feedback and hit effects.

### Stats

Stats are organized under `Assets/Scripts/Stats`.

The stat system supports:

- Base values
- Named temporary modifiers
- Resource stats
- Major stats
- Offensive stats
- Defensive stats

Default stat ScriptableObjects are stored in:

- `Assets/Data/DefaultStatData/Default Stat Setup- Player.asset`
- `Assets/Data/DefaultStatData/Default Stat Setup - Skelaton.asset`

### Combat And Status Effects

Combat combines physical and elemental damage.

Physical damage includes:

- Base damage
- Strength scaling
- Critical chance
- Critical power
- Armor reduction
- Armor mitigation
- Evasion

Elemental damage includes:

- Fire
- Ice
- Lightning

Status effects:

- Fire applies burn damage over time.
- Ice applies a slowdown effect.
- Lightning builds electrify charge and triggers a lightning strike when fully charged.

### Interactable Objects

Interactable objects are under `Assets/Scripts/InteractiveObjects`.

Current objects:

- `Object_Chest`: reacts to damage, opens, plays VFX, and applies knockback motion.
- `Object_Buff`: applies temporary stat modifiers, floats visually, then removes the buff after its duration.

### Skill System And UI

Skill data is stored as ScriptableObjects using `Skill_DataSO`.

Current skill data assets:

- `Assets/Data/Skill data - Healing wisp.asset`
- `Assets/Data/Skill data - Time Echo.asset`
- `Assets/Data/Skill data - Timeless attack.asset`

Skill tree and tooltip UI scripts are under `Assets/Scripts/UI`.

Important UI scripts:

- `UI_SkillTree`
- `UI_TreeNode`
- `UI_TreeConnectionHandler`
- `Ui_TreeConnection`
- `Ui_ToolTip`
- `UI_SkillToolTip`
- `Ui_MiniHealthBar`

## Project Structure

```text
Assets/
  Animations/          Animation assets
  Data/                ScriptableObject data for skills and default stats
  Graphics/            Sprites, tiles, decorations, characters, and VFX textures
  Input System/        Unity Input System action assets and generated C# wrapper
  Materials/           Materials and physics materials
  Prefab/              Enemy, object, and VFX prefabs
  Scenes/              Unity scenes
  Scripts/             Runtime gameplay code
  Settings/            URP and render settings
  TextMesh Pro/        TextMesh Pro resources
  Tile Palette/        Tile palette and tile assets
Packages/              Unity package manifest and lock file
ProjectSettings/       Unity project settings
UserSettings/          Local Unity editor settings
```

## Prefabs

Current prefabs in `Assets/Prefab`:

- `Enemy_Skeleton.prefab`
- `Object_Buff.prefab`
- `Object_Chest.prefab`
- `VFX_LightningStrike.prefab`
- `VFX_OnCritHit.prefab`
- `VFX_OnHit.prefab`

## Development Notes

- Keep Unity generated `.meta` files committed with their assets.
- Add new player behavior as a `PlayerState` when it represents a distinct movement or combat mode.
- Add new enemy behavior as an `EnemyState` when it needs transition logic or timing.
- Add persistent gameplay values through ScriptableObjects when they should be editable in the Unity Inspector.
- Use `StatType` and `EntityStats.GetStatByType` when adding buffs that modify stats.
- Input changes should be made in `PlayerInputSet.inputactions`, then the generated `PlayerInputSet.cs` should be updated by Unity.

## Known Scope

This repository currently appears to be a prototype project. It includes runtime gameplay systems and content assets, but no dedicated automated test suite or build scripts were found in the project root.
