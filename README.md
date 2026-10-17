# AdrenalineJunkie ⚔️🔥

**Your Viking has a problem. Fortunately, it's configurable.**

Do you live for the thrill of battle? The rush of a perfectly timed dodge? The glorious chaos of being surrounded by enemies while your Viking makes increasingly questionable life Choices?
Then why does your adrenaline disappear the moment things get interesting?!

**AdrenalineJunkie** puts you in control of your adrenaline habit.

Whether you just want to take the edge off vanilla's aggressive decay or turn your Viking into a caffeine-fueled combat enthusiast, the choice is yours.

## Get Your Fix

Four configuration options let you fine-tune your adrenaline experience:

- **Adrenaline Gain Multiplier:** Earn adrenaline faster or slower. Because sometimes one near-death experience just isn't enough.
- **Adrenaline Decay Multiplier:** Control how quickly adrenaline naturally drains. Keep the rush going long after the screaming stops.
- **Enable Delay Multiplier:** Decide whether you want to modify the delay before adrenaline starts decaying. Disabled by default for players who prefer vanilla timing.
- **Adrenaline Delay Multiplier:** When enabled, adjust how long adrenaline sticks around before decay begins. Savor the moment!

## The Default Dose

AdrenalineJunkie ships with settings designed to make adrenaline feel more rewarding without completely throwing balance out the window.

| Setting | Default | Effect |
|---|---|---|
| Gain Multiplier | `1.25` | 25% more adrenaline gained |
| Decay Multiplier | `0.50` | 50% slower natural decay |
| Enable Delay Multiplier | `false` | Vanilla decay delay |
| Delay Multiplier | `1.50` | 50% longer delay when enabled |

**Want vanilla behavior?** Set the gain and decay multipliers to `1.0` and leave the delay toggle disabled.

**Want the original AdrenalineJunkie experience?** Enable the delay toggle to activate the `1.50x` delay multiplier.

Want to experiment? Go nuts. It's your Viking, your adrenaline, and your questionable decisions.

## Configuration

Configuration files are generated automatically after launching the game with AdrenalineJunkie installed.

Settings can be adjusted through the BepInEx configuration file or in-game using **Shudnal's ConfigurationManager**.

### In-Game Configuration

For convenient configuration without leaving the game:

1. Install Shudnal's ConfigurationManager.
2. Launch Valheim and press `F1`.
3. Find **AdrenalineJunkie** in the mod list.
4. Adjust your multipliers and toggle the custom decay delay as desired.

Changes to gain and decay multipliers apply without restarting the game. Changes to the decay delay affect the next delay countdown after adrenaline is gained.

**Note:** The Delay Multiplier setting may remain visible even when its toggle is disabled. It has no effect until Enable Delay Multiplier is turned on.

## ✨ Features

- Fully configurable adrenaline gain and natural decay
- Optional decay-delay adjustment with an independent toggle
- Preserves Valheim's original adrenaline curves
- Does not change maximum adrenaline or activation thresholds
- Lightweight, client-side implementation
- Compatible with dedicated multiplayer servers without server installation
- Live configuration adjustments
- No unnecessary bells, whistles, or suspiciously glowing potions

## 🌐 Multiplayer

AdrenalineJunkie is a **client-side mod**. The dedicated server does not require installation.

Each player can choose their own settings.

For a consistent multiplayer experience, we recommend agreeing on settings with your fellow Vikings.

The mod does not enforce server-wide configuration values.

## 📦 Requirements

- Valheim
- BepInExPack for Valheim

**Optional:** Shudnal's ConfigurationManager for in-game configuration.

## 🛠️ Installation

### Using a Mod Manager

Install AdrenalineJunkie through Thunderstore Mod Manager or r2modman, along with its required BepInEx dependency.

### Manual Installation

1. Install BepInExPack for Valheim.
2. Place `AdrenalineJunkie.dll` in your `BepInEx/plugins` folder.
3. Launch Valheim.
4. Adjust the generated configuration file or use the optional in-game ConfigurationManager.


---

## ⚠️ A Totally Serious Disclaimer

**AdrenalineJunkie:** Because the only thing that should run out faster than your adrenaline is your supply of healing meads.

*Use responsibly. Side effects may include excessive confidence, reckless charging, and suddenly believing you can solo an Ashlands fortress while screaming "LEEEEERRROOOOOY!"*

