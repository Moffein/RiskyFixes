This mod compiles a bunch of fixes for Vanilla bugs, most of these come from RiskyMod.

Vanilla-Compatible, some tweaks are client-side, some are server-side. Check the config, all tweaks can be toggled!

## General Fixes

- Prismatic Trials are now available in modded. (No Leaderboards)
- Slayer damagetype (bonus to low health) now applies to procs.
- Bullet Attacks no longer can hit yourself.
- Disconnected players no longer count towards difficulty scaling.
- Lower chance of players having hidden names online.
- Removed main menu advertisement.
- Small holdouts always charge at full speed if 1 player is in the radius.
	- Affects Moon Escape and Void Fields.
	- Moon Pillars and Void Signals already have this behavior enabled.
- Fixed multiple Gold Shrines being able to spawn on the same stage.

## Item Fixes

- Focus Crystal no longer applies to self damage.
- Safer Spaces
	- Fixed item proccing while already invulnerable.
	- Fixed item cooldown being calculated as if it has +1 stacks.
- Symbiotic Scorpion no longer procs on self damage.
- Voidsent Flame no longer procs on Newt due to the potential for crashes.
- Charged Perforator now inherits crit instead of rerolling.
- Longstanding Solitude no longer causes nullrefs.
- Breaching Fin no longer reapplies its damage bonus in the same proc chain.
- War Bonds stacks no longer persist between stages.

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

- CHEF
	- Added missing Mechanical bodyflag.
	- Sear
		- Enabled vertical aiming and now follows aim direction instead of model direction.
	
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
	- Fixed errors related to utility skills. (Needs testing)

## Artifact Fixes

- Enigma now removes all non-Enigma equipments from the drop pool at the start of a run.
- Vengeance Clones now have the same level as players.

## Enemy Fixes

- Fixed enemy AI breaking at close range.

- Artifact Reliquary
	- No longer affected by healing.
	
- Halcyonite
	- Fixed weirdness with the Whirlwind and not being affected by stuns.
	
- Wandering Vagrant
	- Fixed tracking bombs being indestructable.

- Magma Worm
	- No longer dies to fall damage.
	
- Clay Dunestrider and Grandparent
	- Ghosts no longer teamkill players.
	
- Mithrix (Phase 4)
	- No longer skippable.
	
## Minion Fixes

- Fixed minion AI attempting to retaliate against the player.
	- Note: This fixes it in normal runs, but also affects Chaos runs where this behavior is intended.
	
## Stage Fixes

- Commencement
	- Fixed Void team enemies not being killed at the start of Mithrix's bossfight.

## Installation
Place RiskyFixes.dll in /Risk of Rain 2/BepInEx/plugins/

## Credits

- Nuxlar
	- BulletFix
	- DetectionFix
	- WormCritFix
	- Rex Utility Crash Fix
	
- Goorahk
	- Charged Perforator crit inherit fix.
	- Vagrant Tracking Bomb invulnerability fix.