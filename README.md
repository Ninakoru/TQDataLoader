# TQDataLoader
Titan Quest Data Loader Tool

## What is this project?

This project is two tools in one for game modding, focusing on the Database. This is a project for personal use that may be interesting to use for other modders.

- The patcher program allots you to patch in changes into the database files, in a way you can track easily your changes, and build your patch to different database versions with little effort.

- The Data Loader program is just a CSV generator, generates CSV file with database relevant data (IE all charms, all monsters, etc), so you can compare and prepare your changes properly. The CSV files also contain Translated texts for some CSV types.

## Patcher - How to use it:

- Setup your following application config keys (app.config): "DbrTemplatesDirectory" found on your documents game folder, "TextResourcesArcFile", on the game root directory under the "Text" folder.

- You need first to extract the full database into a folder, with all the dbr files there, using ARZExtractor or ARZExplorer. This will be our "SourceDir" property on the patcher presets (Configuration/PatcherPresets.xml).

- Create your mod using ArtManager, comes with Titan Quest. But your database folder in the "DestinationDir" property on the patcher presets.

- Just edit the txt files to add anything you want to change. 

- Run the patcher, then build your database file using artmanager. You can left click on Records, then build,

## Patcher Validations:

Proper file structure.
Checks if the source path exist.
Checks against templates if the properties exist.
Checks a property is not assigned twice for the same path in all your configuration.
Some integrity checks, like too much commas in a line, too many decimal places.

After patching the file, if the destination file is the same, the patcher will not overwrite it.

## How the txt files work:

It will be always this format:

```
sourcePath1
sourcePath2
----
property1,value1
property2,value2
----
sourcePath3
sourcePath4
----
property3,value3
property4,value4
```

Adding entirely new files:

You can simply place your new files under a new/records/whateverpath, under the configuration folder, the patcher will copy the files into your mod folder.

Where source file path is a path of the file or files you want to change. You can use the asterisk (*) as wildcard to change many files at once.

As example:

We want to add 10 physical resistance to all boars in act one, if we check the path we find:

records\creature\monster\warbeast\am_boar_02.dbr
records\creature\monster\warbeast\am_boar_04.dbr
records\creature\monster\warbeast\am_boar_06.dbr
records\creature\monster\warbeast\am_boar_09.dbr
records\creature\monster\warbeast\bm_duskyboar_09.dbr
records\creature\monster\warbeast\bm_duskyboar_09_dead.dbr
records\creature\monster\warbeast\bm_duskyboar_09_hostile.dbr
records\creature\monster\warbeast\bm_duskyboar_11.dbr
records\creature\monster\warbeast\bm_duskyboar_13.dbr
records\creature\monster\warbeast\bm_duskyboar_15.dbr

So if we want to add it to all monsters in the folder, as all of them are boars is as simple as:

```
records\creature\monster\warbeast\*.dbr
----
defensivePhysical,10.000000,
```

Now instead, we want the small boars have 10% physical res, but the big ones 15% instead:

```
records\creature\monster\warbeast\am_boar_*.dbr
----
defensivePhysical,10.000000,
----
records\creature\monster\warbeast\bm_duskyboar_*.dbr
----
defensivePhysical,15.000000,
```
