#include <Windows.h>
#include <stdlib.h>

BOOL CALLBACK EnumWindowsCB(
	HWND hwnd, 
	LPARAM lParam)
{
	DWORD pid = 0;
	GetWindowThreadProcessId(hwnd, &pid);
	if (pid != (DWORD)lParam)
		return TRUE;

	DWORD dwAffinity = 0;

	if (!GetWindowDisplayAffinity(hwnd, &dwAffinity))
		return TRUE;

	if (dwAffinity == WDA_NONE)
		SetWindowDisplayAffinity(hwnd, WDA_EXCLUDEFROMCAPTURE);
	
	return TRUE;
}

BOOL APIENTRY DllMain(
	HMODULE hModule,
	const DWORD ulReasonForCall,
	LPVOID lpReserved)
{

	UNREFERENCED_PARAMETER(hModule);
	UNREFERENCED_PARAMETER(lpReserved);
		
	if (ulReasonForCall == DLL_PROCESS_ATTACH)
		EnumWindows(EnumWindowsCB, (LPARAM)GetCurrentProcessId());

	return TRUE;
}

