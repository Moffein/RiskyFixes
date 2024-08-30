This mod compiles a bunch of fixes for Vanilla bugs, most of these come from RiskyMod.

Unfortunately, this mod doesn't fix any SotS-specific issues as a lot of the jank is deeply-embedded things that Gearbox should be fixing instead.

Vanilla-Compatible, some tweaks are client-side, some are server-side. Check the config, all tweaks can be toggled!

## General Fixes

- Prismatic Trials are now available in modded. (No Leaderboards)
- Slayer damagetype (bonus to low health) now applies to procs.
- Bullet Attacks no longer can hit yourself.
- Disconnected players no longer count towards difficulty scaling.
- Lower chance of players having hidden names online.
- Removed main menu advertisement.

## Item Fixes

- Focus Crystal no longer applies to self damage.
- Safer Spaces
	- Fixed item proccing while already invulnerable.
	- Fixed item cooldown being calculated as if it has +1 stacks.
- Symbiotic Scorpion no longer procs on self damage.
- Voidsent Flame no longer procs on Newt due to the potential for crashes.

## Survivor Fixes
	
- Acrid
	- Now has spawn protection at the start of a run.
	
- Artificer
	- Primary projectiles no longer disappear at range.

- Bandit
	- Primaries now have a shot radius like most other bullet attacks in the game.
	- Knife hitbox no longer gets cancelled by other animations.
	
- Captain
	- Orbital Skills are now usable in Hidden Realms.
	
- Commando
	- Double Tap no longer has a hidden reload state that lowers the fire rate cap.
	
- Huntress
	- Flurry no longer loses arrows at high attack speeds.
	
- Mercenary
	- Eviscerate no longer gets stuck on allies.
	
- MUL-T
	- Fixed Nailgun ending burst not triggering when cancelled.
	- Nailgun shots now have a shot radius like most other bullet attacks in the game.
	
- Railgunner
	- Attacks no longer cancel Bungus.
	
- REX
	- Fixed nullref on kill with DIRECTIVE: Inject.
	- Fixed errors related to utility skills. (Needs testing)
	
- Seeker
	- Fixed nullref when using M2.

## Artifact Fixes

- Enigma now removes all non-Enigma equipments from the drop pool at the start of a run.
- Vengeance Clones now have the same level as players.

## Enemy Fixes

- Fixed enemy AI breaking at close range.

- Artifact Reliquary
	- No longer affected by healing.

- Magma Worm
	- No longer dies to fall damage.
	- Fixed Railgunner headshots not critting.
	
- Clay Dunestrider and Grandparent
	- Ghosts no longer teamkill players.
	
- Mithrix (Phase 4)
	- No longer skippable.
	
## Minion Fixes

- Fixed minion AI attempting to retaliate against the player.
	- Note: This fixes it in normal runs, but also affects Chaos runs where this behavior is intended.

## Installation
Place RiskyFixes.dll in /Risk of Rain 2/BepInEx/plugins/

## Credits

- Nuxlar
	- BulletFix
	- DetectionFix
	- WormCritFix
	- Rex Utility Crash Fix