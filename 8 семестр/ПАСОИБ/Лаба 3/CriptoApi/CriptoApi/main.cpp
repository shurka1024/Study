
#ifndef _WIN32_WINNT		// Allow use of features specific to Windows 2000 or later.                   
#define _WIN32_WINNT 0x0500	// Change this to the appropriate value to target other versions of Windows.
#endif						

#include <stdio.h>
#include <tchar.h>
#include <windows.h>
#include <iostream>
#include "CCrypto.h"

#pragma comment(lib, "Crypt32")

BYTE* Encrypt(char *str, char *password)
{
	HCRYPTPROV hCryptProv = 0;
	HCRYPTKEY hKey = 0;
	HCRYPTHASH hHash = 0;
	BYTE    *pBuffer = 0;
	DWORD   dwBufferLen = strlen(str);

	CryptAcquireContext(&hCryptProv, NULL, NULL, PROV_RSA_SCHANNEL, 0);
	CryptCreateHash(hCryptProv, CALG_SHA, 0, 0, &hHash);
	CryptHashData(hHash, (BYTE*)password, strlen(password), 0);
	CryptDeriveKey(hCryptProv, CALG_3DES, hHash, 0, &hKey);
	CryptEncrypt(hKey, 0, TRUE, 0, 0, &dwBufferLen, strlen(str));

	pBuffer = (BYTE*)malloc(dwBufferLen);
	memcpy(pBuffer, str, dwBufferLen);
	CryptEncrypt(hKey, 0, TRUE, 0, pBuffer, &dwBufferLen, strlen(str));

	CryptDestroyKey(hKey);
	CryptDestroyHash(hHash);
	CryptReleaseContext(hCryptProv, 0);

	return pBuffer;
}

int main()
{
	//char* in = "hello";
	////scanf("%s", in);
	//BYTE* out = Encrypt(in, "1");
	//cout << out << endl;

	CCrypto crypto;
	crypto.CryptoEncryptFile();
	crypto.CryptoDecryptFile();
	system("pause");
}