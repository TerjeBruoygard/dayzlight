# Dayzlight

Monitoring and administrating system for Arma 2 (DayZ Mod) server.

All features now in development:
- General server information. *(not implemented)*
- Dynamic map with visualization of all players movement. *(partially implemented)*
- List of players with statistics. *(not implemented)*
- Server chat and log. *(not implemented)*
	
![Dayzlight webapp workspace](/README_IMG.jpg)
	

### How to build

1. Install MySQL server, Visual Studio 2017 and NET Framework 4.6.2 *(If not installed)*

2. Open solution using Visual Studio 2017.

3. Restore all nuget packages.

4. For build addon and tests use x86 compiler mode.

5. For build common and webapp use Any CPU compiler mode. 


### How to deploy

1. Copy all from *"DayzlightAddon\bin\{Release|Debug}\"* to your Arma 2 (Dayz Mod) root server folder.

2. Copy all from *"DayzlightWebapp\"* to *"{Arma2_RootServerFolder}\Dayzlight\DayzlightWebapp\"*


### How to use

1. In end of your init.sqf mission file add this code:
```
if (isDedicated) then { 
    [] execVM "\Dayzlight\DayzlightAddon.sqf"; 
};
```

4. Edit connection string to MySQL Server in *"{Arma2_RootServerFolder}\Dayzlight\DB.ini"*
- *Schema name should be different from your Dayz Mod database.*
- *Schema in database will be created automatically on first webapp or addon run. Do not create empty schema manually. EntityFramework will do it itself, otherwise errors are possible.*
- *MySQL user must have all rights on server for automatically create and configure schema.*

5. Host *"{Arma2_RootServerFolder}\Dayzlight\DayzlightWebapp"* as IIS service. *(Required IIS 6+ and NET Framework 4.5+ Classic)*

6. In web browser visit IIS host address.


### Troubleshooting

- Addon errors see in your .rpt file of Arma 2 (Dayz Mod) server.

- Webapp errors see in IIS console and logs.