#define _CRT_SECURE_NO_WARNINGS
#ifndef _WIN32_WINNT		// Allow use of features specific to Windows 2000 or later.                   
#define _WIN32_WINNT 0x0500	// Change this to the appropriate value to target other versions of Windows.
#endif

#include <fstream>
#include "CCrypto.h"

#pragma comment(lib, "Crypt32")

CCrypto::CCrypto()
{
	//inputFile = "1.txt";
	//outputFile = "2.txt";
}

bool CCrypto::CryptoEncryptFile()
{
	bool result = false;
	BOOL bStatus = FALSE;

	/*
	* We suppose here that the default container exists and
	* that it contains an RSA exchange key pair .
	*/
	bStatus = CryptAcquireContext(&hProv,
		NULL, /* default container */
		MS_ENHANCED_PROV,
		PROV_RSA_FULL,
		0);
	if (!bStatus)
	{
		//printf("CryptAcquireContext failed with error 0x%.8X\n", GetLastError());
		//goto error;
		if (CryptAcquireContext(
			&hProv,
			NULL,
			MS_ENHANCED_PROV,
			PROV_RSA_FULL,
			CRYPT_NEWKEYSET))
		{
			printf(TEXT("CryptAcquireContext succeeded.\n"));
		}
		else
		{
			printf(TEXT("Could not create the default key container.\n"));
		}
	}

	bStatus = CryptGetUserKey(hProv,
		AT_KEYEXCHANGE,
		&hKey);
	if (!bStatus)
	{
		//printf("CryptGetUserKey failed with error 0x%.8X\n", GetLastError());
		//goto error;
		if (NTE_NO_KEY == GetLastError())
		{
			// No exchange key exists. Try to create one.
			if (!CryptGenKey(
				hProv,
				AT_KEYEXCHANGE,
				CRYPT_EXPORTABLE,
				&hKey))
			{
				printf("Could not create a user public key.\n");
				return false;
			}
		}
		else
		{
			printf("User public key is not available and may not exist.\n");
			return false;
		}
	}

	/*
	* get the size of the key
	*/
	dwValLen = sizeof(DWORD);
	bStatus = CryptGetKeyParam(hKey,
		KP_KEYLEN,
		(LPBYTE)&dwKeyLen,
		&dwValLen,
		0);
	if (!bStatus)
	{
		printf("CryptGetKeyParam failed with error 0x%.8X\n", GetLastError());
		return false;
	}

	/*
	* Allocate input/output buffer
	*/
	dwKeyLen = (dwKeyLen + 7) / 8; /* tranform to bytes length */
	pEncryptedData = (LPBYTE)LocalAlloc(0, dwKeyLen);
	if (!pEncryptedData)
	{
		printf("LocalAlloc failed with error 0x%.8X\n", GetLastError());
		return false;
	}

	/*
	* copy password to the buffer
	*/

	//FILE *file = fopen(inputFile, "rb");
	//fseek(file, 0, SEEK_END);
	//dwPasswordLen = ftell(file);

	//fseek(file, 0, SEEK_SET);
	//while (!feof(file))
	//{
	//	getc(file);
	//}

	char buffer[10];

	std::ifstream file1(inputFile);
	file1.read(buffer, 10);
	file1.close();
	szPassword = &buffer[0];
	dwPasswordLen = (DWORD)strlen(szPassword);
	CopyMemory(pEncryptedData, szPassword, dwPasswordLen);
	dwEncryptedDataLen = dwPasswordLen;

	bStatus = CryptEncrypt(hKey,
		NULL,
		TRUE,
		0,
		pEncryptedData,
		&dwEncryptedDataLen,
		dwKeyLen);
	if (!bStatus)
	{
		printf("CryptEncrypt failed with error 0x%.8X\n", GetLastError());
		return false;
	}

	printf("Password encrypted successfully :\n\tlength = %d bytes.\n\tValue = ", (int)dwEncryptedDataLen);
	for (DWORD i = 0; i < dwEncryptedDataLen; i++)
		printf("%.2X", pEncryptedData[i]);
	printf("\n\n");

	/*
	* verifying encryption result
	*/
	printf("Verifying encryption result...");


	return result;
}

bool CCrypto::CryptoDecryptFile()
{
	BOOL bStatus = FALSE;
	bool result = false;
	bStatus = CryptDecrypt(hKey,
		NULL,
		TRUE,
		0,
		pEncryptedData,
		&dwEncryptedDataLen);
	if (!bStatus)
	{
		printf("CryptDecrypt failed with error 0x%.8X\n", GetLastError());
		//goto error;
	}

	if ((dwEncryptedDataLen != dwPasswordLen) ||
		(0 != memcmp(pEncryptedData, szPassword, dwPasswordLen)))
	{
		printf("\nVerification failed!!\n\n");
	}
	else
	{
		printf("\nSucess.\n\n");
	}

	for (DWORD i = 0; i < dwPasswordLen; i++)
		printf("%c", pEncryptedData[i]);

	return result;
}
