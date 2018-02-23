# Dayzlight

Monitoring and administrating system for Arma 2 (DayZ Mod) server.

All features now in development:
- General server information. *(not implemented)*
- Dynamic map with visualization of all players movement. *(partially implemented)*
- List of players with statistics. *(not implemented)*
- Server chat and log. *(not implemented)*
	
![Dayzlight webapp workspace](/README_IMG.jpg)
	

### How to build and configurate

1. Open solution using Visual Studio 2017.

2. Restore all depends as nuget packets.

3. For build addon and tests *(use only x86 compiler mode)*.

4. For build common and webapp *(use only Any CPU compiler mode)*. 


### How to use

1. Install MySQL server *(If not installed)*

2. Copy DayzlightAddon.dll and Dayzlight folder from addon bin folder to your ArmA 2 (DayZ Mod) server root folder.

3. In end of your init.sqf mission file add this code:
```
if (isDedicated) then { 
    [] execVM "\Dayzlight\DayzlightAddon.sqf"; 
};
```

4. Edit connection string to MySQL Server in DB.ini (in both Addon and Webapp)
- *Schema name should be different from your Dayz Mod database.*
- *Schema in database will be created automatically on first webapp or addon run. Do not create empty schema manually. EntityFramework will do it itself, otherwise errors are possible.*
- *MySQL user must have all rights on server for automatically create and configure schema.*

5. Host DayzlightWebapp as IIS service.

6. In web browser visit IIS host address.


### Troubleshooting

- Addon errors see in your .rpt file of Arma 2 (Dayz Mod) server.

- Webapp errors see in IIS console and logs.