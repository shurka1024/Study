#define _CRT_SECURE_NO_WARNINGS
#ifndef _WIN32_WINNT		// Allow use of features specific to Windows 2000 or later.                   
#define _WIN32_WINNT 0x0500	// Change this to the appropriate value to target other versions of Windows.
#endif

#include <fstream>
#include "CCrypto.h"

#pragma comment(lib, "Crypt32")

const int MAX_LEN = 10;
const int len = 128;

CCrypto::CCrypto()
{
	//inputFile = "1.txt";
	//outputFile = "2.txt";
}

bool CCrypto::CryptoEncryptFile()
{
	bool result = false;
	BOOL bStatus = FALSE;

#pragma region CryptAcquireContext

	bStatus = CryptAcquireContext(&hProv,
		NULL, // Контейнер по умолчанию
		MS_ENHANCED_PROV,
		PROV_RSA_FULL,
		0);
	if (!bStatus)
	{
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

#pragma endregion

#pragma region CryptGetUserKey

	bStatus = CryptGetUserKey(hProv,
		AT_KEYEXCHANGE,
		&hKey);
	if (!bStatus)
	{
		if (NTE_NO_KEY == GetLastError())
		{
			// Нет ключа. Сгенерируем ключ
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

#pragma endregion

#pragma region CryptGetKeyParam

	dwValLen = sizeof(DWORD);			// Размер ключа
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

#pragma endregion

#pragma region CryptEncrypt

	//Выделим буффер
	dwKeyLen = (dwKeyLen + 7) / 8;			// Преобразуем в байты
	//pEncryptedData = (LPBYTE)LocalAlloc(0, dwKeyLen);
	//if (!pEncryptedData)
	//{
	//	printf("LocalAlloc failed with error 0x%.8X\n", GetLastError());
	//	return false;
	//}

	char buffer[MAX_LEN];
	

	FILE *f1, *f2;
	f1 = fopen(inputFile, "rb+");
	f2 = fopen(outputFile, "wb+");
	char buf;

	while (!feof(f1))
	{
		pEncryptedData = (LPBYTE)LocalAlloc(0, dwKeyLen);
		for (int i = 0; i < MAX_LEN; ++i)
			buffer[i] = '\0';
		fread(&buffer, MAX_LEN, 1, f1);
		//for (int i = 0; i < MAX_LEN; ++i)
		//{
		//	fread(&buf, sizeof(char), 1, f1);
		//	buffer[i] = buf;
		//}

		//std::ifstream file1(inputFile);
		//file1.read(buffer, MAX_LEN);
		//file1.close();
		szPassword = &buffer[0];
		dwPasswordLen = MAX_LEN;
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

		for (int i = 0; i < len; ++i)
		{
			buf = pEncryptedData[i];
			fwrite(&buf, sizeof(char), 1, f2);
		}

		//printf("Password encrypted successfully :\n\tlength = %d bytes.\n\tValue = ", (int)dwEncryptedDataLen);
		//for (DWORD i = 0; i < dwEncryptedDataLen; i++)
		//	printf("%.2X", pEncryptedData[i]);
		//printf("\n\n");

		/*
		* verifying encryption result
		*/
		//printf("Verifying encryption result...");
	}
	fclose(f2);
	fclose(f1);

#pragma endregion

	return result;
}

bool CCrypto::CryptoDecryptFile()
{
	BOOL bStatus = FALSE;
	bool result = false;
	char buffer[len];

	FILE *f;
	char buf;
	f = fopen(outputFile, "rb+");
	while (!feof(f))
	{
		fread(&buffer, len, 1, f);

		szPassword = &buffer[0];
		dwPasswordLen = len;
		dwEncryptedDataLen = len;
		pEncryptedData = NULL;
		//dwKeyLen = (dwKeyLen + 7) / 8; /* tranform to bytes length */
		pEncryptedData = (LPBYTE)LocalAlloc(0, dwKeyLen);

		CopyMemory(pEncryptedData, szPassword, dwPasswordLen);
		bStatus = CryptDecrypt(hKey,
			NULL,
			TRUE,
			0,
			pEncryptedData,
			&dwEncryptedDataLen);
		//if (!bStatus)
		//{
		//	printf("CryptDecrypt failed with error 0x%.8X\n", GetLastError());
		//	//goto error;
		//}

		for (DWORD i = 0; i < MAX_LEN; i++)
			printf("%c", pEncryptedData[i]);
	}
	fclose(f);

	return result;
}
