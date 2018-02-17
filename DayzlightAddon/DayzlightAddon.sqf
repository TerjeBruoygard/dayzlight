if (!isDedicated) exitWith {};
diag_log format["[DAYZLIGHT] Start initializing..."];

DAYZLIGHT_DB_CREDENTIALS = "Persist Security Info=False;server=localhost;database=namalsk_reloaded;uid=mysql;password=mysql";

// DO NOT EDIT BELOW!
DAYZLIGHT_DLLNAME = "DayzlightAddon";
DAYZLIGHT_EXTFNC_INIT = "INIT";
DAYZLIGHT_EXTFNC_STAT = "STAT";
DAYZLIGHT_STAT_CYCLE_TIME = 30;

private["_mapSWcorner", "_mapNEcorner", "_mapinfoarr", "_plmov", "_result"];
_mapSWcorner = getArray (configfile >> "CfgWorlds" >> worldName >> "SWcorner");
_mapNEcorner = getArray (configfile >> "CfgWorlds" >> worldName >> "NEcorner");
_mapinfoarr = [DAYZLIGHT_DB_CREDENTIALS, worldName, _mapSWcorner, _mapNEcorner, getMarkerPos "respawn_west"];

_result = DAYZLIGHT_DLLNAME callExtension str [DAYZLIGHT_EXTFNC_INIT, _mapinfoarr];
if (_result == "OK") then {
	diag_log format["[DAYZLIGHT] Successfully inititalized."];
	while { true } do {
		_plmov = [];
		{
			_plmov set [count _plmov, [getPlayerUID _x, getPos _x, getDir _x]];
			uiSleep 0.1;
		} forEach playableUnits;

		_result = DAYZLIGHT_DLLNAME callExtension str [DAYZLIGHT_EXTFNC_STAT, [_plmov]];
		diag_log format["[DAYZLIGHT] Tick: %1", _result];
		uiSleep DAYZLIGHT_STAT_CYCLE_TIME;
	};
}
else {
	diag_log format["[DAYZLIGHT] Initialization failed: %1", _result];
};