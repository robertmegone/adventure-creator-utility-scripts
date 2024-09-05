# Adventure Creator Utility Scripts

A handful of scripts I've written to help identify issues with Adventure Creator (AC) implementations.

These are a little stale now, but I'm sharing incase they are useful to somebody. Feel free to use as you see fit, without restriction.
Contributions are welcome, as are submissions of new scripts.

## Scripts

### Check for AC actions with breakpoints enabled
- **Description:** Grep inside the Assets folder for breakpoints that remain on.

### CheckCutsceneAutosave.cs
- **Description:** Checks all cutscenes across the project to ensure that they have the autosave option enabled.
- **Usage:** Opens an editor window with a button to check all scenes. Logs results to a file named `autosave_logs.txt`.

### FindActionRename.cs
- **Description:** Searches scenes for action lists where a hotspot is renamed.
- **Usage:** Opens an editor window with a button to start the search. Logs results to a file named `hotspotrename.txt`.

### FindCharRename.cs
- **Description:** Finds scenes with action lists that rename a character.
- **Usage:** Opens an editor window with a button to start the search. Logs results to a file named `charrename.txt`.

### FindDialogOptionRename.cs
- **Description:** Finds scenes with action lists that have dialog options renamed.
- **Usage:** Opens an editor window with a button to start the search. Logs results to a file named `dialogoptionrename.txt`.

### HotspotLabelChecker.cs
- **Description:** Searches scenes and logs hotspot labels.
- **Usage:** Opens an editor window with a button to check hotspot labels. Logs results to a file named `HotspotLabels.txt`.

### HotspotSpriteRenderer.cs
- **Description:** Hotspot Highlighting script. Displays a sprite over on screen hotspots upon keypress
- **Usage:** Attach to persistent engine, add a sprite and adjust parameters to your needs, press space during gameplay to activate.

### ObjectSoundFinder.cs
- **Description:** Searches for object sound action usage, helpful to find sounds not sent through a sound manager.
- **Usage:** Run and check log file output.