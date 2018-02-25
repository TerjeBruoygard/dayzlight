# Dayzlight

Monitoring and administrating system for Arma 2 (DayZ Mod) server.

All features now in development:
- General server information. *(not implemented)*
- Dynamic map with visualization of all players movement. *(partially implemented)*
- List of players with statistics. *(not implemented)*
- Server chat and log. *(not implemented)*
	
![Dayzlight webapp workspace](/README_IMG_V2.jpg)
	

### How to build

1. Install MySQL server, Visual Studio 2017 and NET Framework 4.6.2 *(If not installed)*

2. Open solution using Visual Studio 2017.

3. Restore all nuget packages.

4. For build addon and tests use x86 compiler mode.

5. For build common and webapp use Any CPU compiler mode. 


### How to deploy

1. Copy all from 
```DayzlightAddon\bin\{Release|Debug}\``` 
to your Arma 2 (Dayz Mod) root server folder.

2. Copy all from 
```DayzlightWebapp\``` 
to 
```{Arma2_RootServerFolder}\Dayzlight\DayzlightWebapp\```


### How to use

1. In end of your init.sqf mission file add this code:
```
if (isDedicated) then { 
    [] execVM "\Dayzlight\DayzlightAddon.sqf"; 
};
```

4. Edit connection string to MySQL Server in
```{Arma2_RootServerFolder}\Dayzlight\DB.ini```
- *Schema name should be different from your Dayz Mod database.*
- *Schema in database will be created automatically on first webapp or addon run. Do not create empty schema manually. EntityFramework will do it itself, otherwise errors are possible.*
- *MySQL user must have all rights on server for automatically create and configure schema.*

5. Host 
```{Arma2_RootServerFolder}\Dayzlight\DayzlightWebapp``` 
as IIS service. *(Required IIS 6+ and NET Framework 4.5+ Classic)*

6. In web browser visit IIS host address. 
- *Internet explorer not supported. Recommended to use Google Chrome or another modern web browser.*
- *Default login: ```admin``` with password: ```admin```*
- *Login and password can been changed manually in ```adminentities``` table in database.*


### How to add support for another maps

1. Run Arma 2 (Dayz Mod) server with Dayzlight addon (read *how to use* for this).

2. Open table
```serverrestartentities```
and copy name of your server map from column
```worldname```.

3. Create folder with name of your map in
```DayzlightWebapp/Content/Maps/```
folder.

4. Find or create image with your map.

5. Copy this image to map folder with name
```map.png```.

As example see how to implemented namalsk map
```DayzlightWebapp/Content/Maps/namalsk/map.png```.

**I will be very grateful, if you share your map with me and another people by sending pull request to this repository.**


### How it works

Addon and Webapp work separately from each other. 
You can start and stop them in any order without any consequences.

- Addon using for collecting, processing and saving states from Arma 2 (Dayz Mod) server to MySQL database.

- Webapp using for displaying collected states from databse in a convenient format as a web pages in web browser.

###### Addon

1. Addon loading to server process as [Arma 2 Extension](https://community.bistudio.com/wiki/Extensions).

2. Script ```DayzlightAddon.sqf``` executing on server side and time by time call addon functions and send states of server and players for processing.

3. Addon receive states from script and write received states to MySQL database using EntityFramework.

###### Webapp

1. Webapp loading as APS.NET MVC web site in IIS Manager.

2. Load states from MySQL database and generate web pages by user requests from web browser in real time.


### Troubleshooting

- Addon errors see in your .rpt file of Arma 2 (Dayz Mod) server.

- Webapp errors see in IIS console and logs.
