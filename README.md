# Game Development Progress Report

## Introduction
This repository tracks the development progress of a game project. Below is an overview of the current status and features implemented.

## Feature Status

| Feature                                             | Status    | Explanations                                         |
|-----------------------------------------------------|-----------|-------------------------------------------------------|
| **All Mechanics**                                   |           |                                                       |
| Talent System                                       | +         | Done.                                                 |
| AI System                                           | +         | Done, needs fine-tuning visually.                     |
| Gun System                                          | +         | Done, with multiple guns.                             |
| Collecting Health and Ammo from the Environment    | +         | Done, with multiple guns.                             |
| UI Elements                                         | +         | Done.                                                 |
| **Optional Mechanics**                              | +         | Partially done.                                       |
| |         |                                             |
| **Character**                                       |           |                                                       |
| Must have a gun.                                    | +         | Two guns, press Q for change.                         |
| Must have a certain amount of current health        | +         | Done.                                                 |
| Must have maximum amount of health                  | +         | Done.                                                 |
| Must sprint. (Bind to L-Shift preferable)           | +         | Done, L-Shift bound.                                  |
| Must have a leveling system                         | +         | Done.                                                 |
| The character gains experience by killing enemies   | +         | Done.                                                 |
| The character gains 1 talent point for each level up| +         | Done.                                                 |
| |         |                                             |
| **Gun**                                             |           |                                                       |
| Must shoot bullets with raycast (line tracing)      | +         | Done.                                                 |
| Must have maximum amount of bullet                  | +         | Done.                                                 |
| Must have certain amount of bullet at the beginning of the game | + | Done.                                       |
| **Collecting Bullet from Environment**              |           |                                                       |
| In Scene, there can be at most of 3 (three) Collectible ammo items | + | Done, with 2 guns, both have 3 collectible ammo items. |
| Once you collect an ammo item, there must be a certain amount of time interval before spawning next ammo item | + | Done.                                            |
| The Amount of bullets must be shown on top of the ammo item | + | Done.                                                 |
| If you collect an ammo item and the amount of collectible bullets exceeds the current maximum amount of ammo, the character must collect needed amount of bullets from the ground. The remaining amount of bullets will stay on the ground. If the all amount of collectible ammo is equal to 3, then no spawning ammo | + | Done, for each weapon and ammo type. |
| Once collected, the Bullet collectible will spawn at random location and avoid two or more collectibles being spawned at same location | + | Done.                                     |
| |         |                                             |
| **Collecting Health from Environment**              |           |                                                       |
| In level, there can be at most of 1 (one) Collectible health | + | Done.                                             |
| Once you collect a health item, there must be a certain time interval before the next health item spawns | + | Done.                                          |
| The Amount of collectible health must be shown on top of the health item | + | Done.                                          |
| If you collect an health item and if it exceed the current amount of health. The character will heal needed amount of health. Then the collectible health will be destroyed, Contrary to the ammo | + | Done. |
| Once collected, the Health collectible will spawn at random location and avoid two or more collectibles being spawned at same location | + | Done. |
| |         |                                             |
| **Talent System**                                   |           |                                                       |
| The character has 2 categories of upgradable talents. One for the Character itself, the other one for the gun that the character uses. | + | Done, 3 categories, a category was added for 2nd gun type. |
| |         |                                             |
| **Character Talents**                              |           |                                                       |
| Maximum walk and sprint speed (5 level, 1 talent point for each level) | + | Done.                                                 |
| Maximum jump height (5 level, 1 talent point for each level) | + | Done.                                                 |
| Maximum amount of health (5 level, 1 talent point for each level) | + | Done.                                                 |
| |         |                                             |
| **Gun Talents**                                     |           |                                                       |
| Damage amount (5 level, 1 talent point for each level) | + | Done.                                                 |
| Ammo capacity (5 level, 1 talent point for each level) | + | Done.                                                 |
| The pierce shot (1 level, 3 talent point) - The gun shots should pass through enemies instead of stopping at the first target. At the beginning of the game, shots cannot pierce the enemies until this talent is opened. | + | Done, not tested.                                    |
| |         |                                             |
| **Enemy**                                           |           |                                                       |
| The maximum number of enemies in the level/scene is 5. There can be at most 5 enemies at the same time in the scene. | + | Done, 3 melee, 2 ranged enemies.                    |
| Enemies must patrol at certain patrol points.       | +         | Enemies randomly find patrol points and move.         |
| Enemies must have a certain amount of health.      | +         | Done.                                                 |
| Enemies must have a certain attack damage.         | +         | Done.                                                 |
| Once any enemy see the character, It/they must pursue the character. | + | Done.                                               |
| If the character is missing for enemies, Enemies must return to the patrolling point. | - | Not Done, check optional.                            |
| If the enemy is close enough to the character, it must attack the character. | + | Done.                                               |
| Idle, walk, run, attack animations must be added to the enemies. | + | Done.                                               |
| The enemies are killed when they ran out of health. | + | Done.                                                 |
| Once the enemy is killed, there must be certain time interval before the next enemy spawns. | + | Done.                                            |
| Create at least two different enemies with different attributes(e.g health, damage) and color. | + | Done.  
| |         |                                             ||
| **Leveling Up**                                    |           |                                                       |
| When the enemy is killed by the character(us), the character gains experience point. | + | Done.                                            |
| The character needs experience points to level up. | +       | Done.                                                 |
| Every Character level has its own experience point amount. (From Character Level 1 to Level 2 needs 100 EXP) | + | Done. |
| As the character level increases, leveling up requires more experience points. (For example: Level 1 to Level 2 needs 100 EXP, Level 2 to level 3 needs 300 EXP). The needed experience points amount is all up to you. | + | Done. |
| |         |                                             |
| **UI**                                             |           |                                                       |
| When the player dies, there must be a simple UI page/indicator to restart the game. | + | Done.                                              |
| When in game, there must be an indicator that shows number of enemies that the character killed. | + | Done.         |
| When in game, there must be Health bar UI that shows the character’s health. | + | Done.                                       |
| When in game, there must be character Level bar UI that shows the current level and experience point. | + | Done.       |
| When in game, there must be current amount of ammo indicator in UI that shows the character’s ammo. | + | Done.        |
| When in a game, there must be health bar above the enemies that shows the enemy’s health amount. | + | Done.           |
| When in a game, the player can access talent system UI by pressing TAB. | + | Done.                                              |
| From talent system UI, the player the player can increase talent by spending talent point. Here is some tips that you can benefit from. | + | Done. |
| |         |                                             |
| **Optional**                                       |           |                                                       |
| Highest Score In the UI                            | +         | Not Done.                                             |
| The hit by the character and die animations of enemies. | +   | Done.                                                 |
| Rearrange the maximum number of enemies in the scene by leveling up the character. (For example: When reached 5 level, now the maximum number of enemies that can be exist in the scene is 6) | + | Not Done. |
| When shooting, bullets can leave trails behind them. | +     | Not Done.                                             |
| Enemies can patrol random patrol points. Every time it reaches the current patrol point, generate a random new patrol point. | + | Done. |
| Don’t exit the atmosphere :)                       | +         | Hopefully done.                                       |
| |         |                                             |
| **Additional**                                     |           |                                                       |
| Second gun is added with its own talent system and its own ammo pickup. | + | Done.                                             |
| Recoil and zoom system for the gun, right click for zoom. | + | Done.                                                |
| Enemies have ranged and melee attack types with projectiles. | + | Done.                                               |
| Enemies will not chase the player if they have no line of sight. | + | Done.                                           |
| Attack, Idle, Patrol, Flee, Die, Pool, Chase states for the enemy AI. | + | Done.                                          |
| Object Pooling for enemies and pickups.            | +         | Done.                                                 |
| Crosshair added.                                   | +         | Done.                                                 |
| Talent Panel also shows the character details in addition to talent tracking. | + | Done.                                           |
| Simple main menu implemented, two scenes in game.  | +         | Done.                                                 |
| Dotween for UI transitions.                        | +         | Done.                                                 |
| Modular approach for guns and ammo, you can add stuff with Scriptable objects. | + | Done.                                      |
| Escape gives game menu.                            | +         | Done.                                                 |
| Basic building behaviour to change building heights.| +       | Done.
