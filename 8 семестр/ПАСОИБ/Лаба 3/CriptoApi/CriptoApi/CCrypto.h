#pragma once
#include <iostream>
#include <Windows.h>
class CCrypto
{
public:
	CCrypto();
	bool CryptoEncryptFile();															// Шифрование файла
	bool CryptoDecryptFile();															// Дешифрование файла

public:
	char inputFile[255] = "D:\\Study\\8 семестр\\ПАСОИБ\\Лаба 3\\CriptoApi\\Debug\\1.txt";														// Исходный файл
	char outputFile[255] = "2.txt";														// Выходной файл
	
private:
	HCRYPTPROV hProv = NULL;															// Криптопровайдер
	HCRYPTKEY hKey = NULL;																// Криптоключ

	char* szPassword = "password";
	//char szPassword[512];
	DWORD dwPasswordLen = 0;
	LPBYTE pEncryptedData = NULL;
	DWORD dwKeyLen = 0, dwValLen = 0;
	DWORD dwEncryptedDataLen = 0;
};