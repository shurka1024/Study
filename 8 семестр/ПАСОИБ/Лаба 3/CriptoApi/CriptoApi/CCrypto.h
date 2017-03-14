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
	char inputFile[255] = "1.txt";														// Исходный файл
	char outputFile[255] = "2.txt";														// Выходной файл
	
private:
	HCRYPTPROV hProv = NULL;															// Криптопровайдер
	HCRYPTKEY hKey = NULL;																// Криптоключ

};