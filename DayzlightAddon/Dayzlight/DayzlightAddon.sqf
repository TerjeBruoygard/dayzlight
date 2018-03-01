if (!isDedicated) exitWith {};
diag_log format["[DAYZLIGHT] Start initializing..."];
DAYZLIGHT_DB_CREDENTIALS = loadFile "\Dayzlight\DB.ini";
DAYZLIGHT_DLLNAME = "DayzlightAddon";
DAYZLIGHT_EXTFNC_INIT = "INIT";
DAYZLIGHT_EXTFNC_STAT = "STAT";
DAYZLIGHT_STAT_CYCLE_TIME = 15;

private["_mapSWcorner", "_mapNEcorner", "_mapinfoarr", "_plmov", "_result", "_tStart", "_tEnd", "_vehModel", "_vehType"];
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
			_vehModel = "";
			_vehType = 0;
			if (_x != vehicle _x) then {
				_vehModel = typeOf vehicle _x;
				if (vehicle _x isKindOf "Land") then {
					_vehType = 1;
				};
				if (vehicle _x isKindOf "Sea") then {
					_vehType = 2;
				};
				if (vehicle _x isKindOf "Air") then {
					_vehType = 3;
				};
			};

			_plmov set [count _plmov, [getPlayerUID _x, name _x, getPos vehicle _x, getDir vehicle _x, _vehModel, _vehType]];
		} forEach playableUnits;

		_result = DAYZLIGHT_DLLNAME callExtension str [DAYZLIGHT_EXTFNC_STAT, [diag_fpsMin, diag_fps, _plmov]];
		_tEnd = diag_tickTime;

		diag_log format["[DAYZLIGHT] Tick executed in %1 sec with message: %2", _tEnd - _tStart, _result];
		uiSleep DAYZLIGHT_STAT_CYCLE_TIME;
	};
}
else {
	diag_log format["[DAYZLIGHT] Initialization failed: %1", _result];
};