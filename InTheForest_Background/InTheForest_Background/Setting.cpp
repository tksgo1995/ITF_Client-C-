#include"setting.h"

// 후킹 키보드, 마우스
HHOOK _khook;
HHOOK _mhook;

KBDLLHOOKSTRUCT kbdStruct;

int time;

LRESULT CALLBACK mouseProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	MOUSEHOOKSTRUCT* pMouseStruct = (MOUSEHOOKSTRUCT*)lParam;
	if (pMouseStruct != NULL)
	{
		if (wParam == WM_LBUTTONDOWN)
		{
			//MessageBox(NULL, _T("lbutton"), _T("lbutton"), MB_OK);
			time_init();

		}
		if (wParam == WM_RBUTTONDOWN)
		{
			//MessageBox(NULL, _T("rbutton"), _T("rbutton"), MB_OK);
			time_init();
		}
	}
	return CallNextHookEx(_mhook, nCode, wParam, lParam);
}

LRESULT CALLBACK keyProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode >= 0)
	{
		if (wParam == WM_KEYDOWN)
		{
			kbdStruct = *((KBDLLHOOKSTRUCT*)lParam);
			time_init();
		}
	}

	return CallNextHookEx(_khook, nCode, wParam, lParam);
}

void SetHook()
{
	if (!(_khook = SetWindowsHookEx(WH_KEYBOARD_LL, keyProc, NULL, 0)))
	{
		MessageBox(NULL, _T("Failed to install hook!"), _T("Error"), MB_OK);
	}

	if (!(_mhook = SetWindowsHookEx(WH_MOUSE_LL, mouseProc, NULL, 0)))
	{
		MessageBox(NULL, _T("Failed to install hook!"), _T("Error"), MB_OK);
	}
}

void ReleaseHook()
{
	UnhookWindowsHookEx(_khook);
	UnhookWindowsHookEx(_mhook);
}

void time_init()
{
	time = 0;
}

// 기능은 여기다가 구현
void InitProcess(int* mask)
{
	//*mask = ConnectSocket("13.209.50.15", 7777);
	//*mask = ConnectSocket("ec2-13-209-50-15.ap-northeast-2.compute.amazonaws.com", 7777);
	*mask = 0;
	UpdatePolicy(*mask);
}

// 마무리 작업
void EndProcess(void)
{
	return;
}

void RegSet(HKEY hk, const TCHAR* path, int value, const TCHAR* setValue)
{
	HKEY hKey = NULL;
	DWORD error = 0;

	RegOpenKeyEx(hk, path, 0, KEY_ALL_ACCESS, &hKey);
	if (error != ERROR_SUCCESS)
	{
		RegCreateKeyEx(hk, path, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_CREATE_SUB_KEY, NULL, &hKey, NULL);
		RegOpenKeyEx(hk, path, 0, KEY_ALL_ACCESS, &hKey);
	}
	RegSetValueEx(hKey, setValue, 0, REG_DWORD, (LPBYTE)&value, sizeof(DWORD));
	RegCloseKey(hKey);
}

void UpdatePolicy(int mask)
{
	HKEY hKey = NULL;
	DWORD dwVal = 0, error = 0;

	//mask = 0;

	// 1 TaskMgr enable(0), disable(1)
	if (GET_BIT(mask, 0)) RegSet(HKEY_CURRENT_USER, _T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\"), 1, _T("DisableTaskMgr"));
	else RegSet(HKEY_CURRENT_USER, _T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\"), 0, _T("DisableTaskMgr"));

	// 2 regedit enable(0), disable(1)
	if (GET_BIT(mask, 1)) RegSet(HKEY_CURRENT_USER, _T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\"), 1, _T("DisableRegistryTools"));
	else RegSet(HKEY_CURRENT_USER, _T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\"), 0, _T("DisableRegistryTools"));

	// 3 cmd enable(0), disable(2)
	if (GET_BIT(mask, 2)) RegSet(HKEY_CURRENT_USER, _T("Software\\Policies\\Microsoft\\Windows\\System\\"), 2, _T("DisableCMD"));
	else RegSet(HKEY_CURRENT_USER, _T("Software\\Policies\\Microsoft\\Windows\\System\\"), 0, _T("DisableCMD"));

	// 4 snipping tools disable(1), enable(0)
	if (GET_BIT(mask, 3)) RegSet(HKEY_LOCAL_MACHINE, _T("SOFTWARE\\Policies\\Microsoft\\TabletPC\\"), 1, _T("DisableSnippingTool"));
	else RegSet(HKEY_LOCAL_MACHINE, _T("SOFTWARE\\Policies\\Microsoft\\TabletPC\\"), 0, _T("DisableSnippingTool"));

	// 5 usb쓰기 disable(1), enable(0)
	if (GET_BIT(mask, 4)) RegSet(HKEY_LOCAL_MACHINE, _T("SYSTEM\\CurrentControlSet\\Control\\StorageDevicepolicies\\"), 1, _T("WriteProtect"));
	else RegSet(HKEY_LOCAL_MACHINE, _T("SYSTEM\\CurrentControlSet\\Control\\StorageDevicepolicies\\"), 0, _T("WriteProtect"));

	// 6 usb차단 disable(4) enable
	if (GET_BIT(mask, 5)) RegSet(HKEY_LOCAL_MACHINE, _T("SYSTEM\\CurrentControlSet\\Services\\USBSTOR\\"), 4, _T("Start"));
	else RegSet(HKEY_LOCAL_MACHINE, _T("SYSTEM\\CurrentControlSet\\Services\\USBSTOR\\"), 0, _T("Start"));

	// 7 디스크차단(C드라이브) disable(4) enable(0)
	if (GET_BIT(mask, 6)) RegSet(HKEY_LOCAL_MACHINE, _T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\"), 4, _T("NoViewOnDrive"));
	else RegSet(HKEY_LOCAL_MACHINE, _T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\"), 0, _T("NoViewOnDrive"));
	// 최대값 255
}

int ConnectSocket(const char* ip, int port)
{
	TCHAR errorMSG[ERROR_SIZE] = { 0, };
	int result = 0;
	char error[256] = { 0, };

	WSADATA data;

	if (WSAStartup(MAKEWORD(2, 2), &data))
	{
		_stprintf_s(errorMSG, _T("WSAStartup error: %d"), WSAGetLastError());
		MessageBox(NULL, errorMSG, _T("ERROR"), MB_OK);
		return INT_MAX;
	}

	SOCKET csock = NULL;
	csock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (csock == INVALID_SOCKET)
	{
		_stprintf_s(errorMSG, _T("socket error: %d"), WSAGetLastError());
		MessageBox(NULL, errorMSG, _T("ERROR"), MB_OK);
		return INT_MAX;
	}

	sockaddr_in addr = { 0, };
	addr.sin_family = AF_INET;
	addr.sin_addr.s_addr = inet_addr(ip);
	addr.sin_port = htons(port);

	if ((connect(csock, (sockaddr*)& addr, sizeof(addr))) == SOCKET_ERROR)
	{
		_stprintf_s(errorMSG, _T("connect error: %d"), WSAGetLastError());
		MessageBox(NULL, errorMSG, _T("ERROR"), MB_OK);
		return INT_MAX;
	}

	char mask[MASK_SIZE] = { 0, };

	if ((recv(csock, mask, MASK_SIZE, 0)) == SOCKET_ERROR)
	{
		_stprintf_s(errorMSG, _T("recv error: %d"), WSAGetLastError());
		MessageBox(NULL, errorMSG, _T("ERROR"), MB_OK);
		return INT_MAX;
	}

	if ((closesocket(csock)) == SOCKET_ERROR)
	{
		_stprintf_s(errorMSG, _T("closesocket error: %d"), WSAGetLastError());
		MessageBox(NULL, errorMSG, _T("ERROR"), MB_OK);
		return INT_MAX;
	}

	//MessageBox(NULL, _T(mask), _T(mask), MB_OK);
	result = atoi(mask);

	return result;
}
// 에러가나면 mask 최대치로해서 모든 기능 차단