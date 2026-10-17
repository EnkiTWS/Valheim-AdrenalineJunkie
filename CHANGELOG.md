# Changelog

All notable changes to AdrenalineJunkie will be documented here.

## [1.0.2] - Choose Your Dose!

### Added
- **EnableDelayMultiplier** configuration option.
- Independent true/false toggle for modifying adrenaline decay delay.
- Vanilla decay-delay behavior when the toggle is disabled.

### Changed
- Custom decay-delay adjustment is now **opt-in**, disabled by default.
- Updated plugin naming and version information to AdrenalineJunkie 1.0.2.
- Updated configuration feedback to distinguish vanilla and modified decay-delay behavior.

### Notes
- Gain Multiplier defaults to `1.25x`.
- Decay Multiplier defaults to `0.50x`.
- Delay Multiplier defaults to `1.50x`, but only applies when enabled.
- Configuration changes can be made in-game using Shudnal's ConfigurationManager.
- Changes to decay delay take effect when a new delay countdown begins.

## [1.0.0] - Get Your Fix!

### Initial Release
- Configurable adrenaline gain multiplier.
- Configurable adrenaline decay multiplier.
- Configurable adrenaline decay-delay multiplier.
- Preserves vanilla adrenaline curves.
- Client-side functionality for single-player and dedicated multiplayer servers.
- Compatible with BepInEx 5.

---

*Your Viking has a problem. Fortunately, it's configurable.*
