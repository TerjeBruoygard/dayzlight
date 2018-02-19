if (!isDedicated) exitWith {};
diag_log format["[DAYZLIGHT] Start initializing..."];
DAYZLIGHT_DB_CREDENTIALS = loadFile "DB.ini";
DAYZLIGHT_DLLNAME = "DayzlightAddon";
DAYZLIGHT_EXTFNC_INIT = "INIT";
DAYZLIGHT_EXTFNC_STAT = "STAT";
DAYZLIGHT_STAT_CYCLE_TIME = 30;

private["_mapSWcorner", "_mapNEcorner", "_mapinfoarr", "_plmov", "_result", "_tStart", "_tEnd"];
_tStart = diag_tickTime;
_mapSWcorner = getArray (configfile >> "CfgWorlds" >> worldName >> "SWcorner");
_mapNEcorner = getArray (configfile >> "CfgWorlds" >> worldName >> "NEcorner");
_mapinfoarr = [DAYZLIGHT_DB_CREDENTIALS, worldName, _mapSWcorner, _mapNEcorner, getMarkerPos "respawn_west"];

_result = DAYZLIGHT_DLLNAME callExtension str [DAYZLIGHT_EXTFNC_INIT, _mapinfoarr];
_tEnd = diag_tickTime;

if (_result == "OK") then {
	diag_log format["[DAYZLIGHT] Successfully inititalized in %1sec.", _tEnd - _tStart];
	while { true } do {
		_tStart = diag_tickTime;
		_plmov = [];
		{
			_plmov set [count _plmov, [getPlayerUID _x, getPos _x, getDir _x]];
			uiSleep 0.1;
		} forEach playableUnits;

		_result = DAYZLIGHT_DLLNAME callExtension str [DAYZLIGHT_EXTFNC_STAT, [_plmov]];
		_tEnd = diag_tickTime;

		diag_log format["[DAYZLIGHT] Tick executed in %1 sec with message: %2", _tEnd - _tStart, _result];
		uiSleep DAYZLIGHT_STAT_CYCLE_TIME;
	};
}
else {
	diag_log format["[DAYZLIGHT] Initialization failed: %1", _result];
};