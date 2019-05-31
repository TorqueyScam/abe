# TMFBalancedInteractables
Attempts to balance the spawn of certain interactables when using TMF in RoR2.

One thing I noticed after playing with too many friends was the sheer quantity of "good" 3d printers, which made the game feel way too easy. Thus, this is my solution.


##Installation:

Requires Bepinex, R2API. TMF suggested - does not modify spawnrates for <=4 players.

Place inside of Risk of Rain 2/Bepinex/Plugins/


##Configuration:

By default, the config printerScalar is set to 1. This provides base scaling such that the more players that join, 3D printers and lunar pods will decrease proportionally.

With a 1.0 printerScalar, at 4 players, the weight of 3D printer spawns is 100% of vanilla. At 8 players, it is 50% of vanilla. at 16 players, it is 25% of vanilla.
printerScalar is a scalar upon the modified weight of printer spawns. This means that 8 players with a 2.0 value is the same as playing vanilla. 16 players with a 4.0 value is the same as playing vanilla.
printerScalar is inteded to act as a scalar for fine tuning where the player scaling may not be precisely accurate for the prefrences of the host/group. My advice is to keep between a range of 0-4 for decreasing spawns; anything above this will definitely increase spawn rate for any amount of players.

use "printerScalar {(double)value}" in console to change scalar on next map.


##Changelog

v1.0.1 - Implemented console command.

v1.0.0 - Released.
