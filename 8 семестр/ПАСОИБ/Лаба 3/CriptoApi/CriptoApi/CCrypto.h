#pragma once
#include <iostream>
#include <Windows.h>
class CCrypto
{
public:
	CCrypto();
	bool CryptoEncryptFile();															// ���������� �����
	bool CryptoDecryptFile();															// ������������ �����

public:
	char inputFile[255] = "D:\\Study\\8 �������\\������\\���� 3\\CriptoApi\\Debug\\1.txt";														// �������� ����
	char outputFile[255] = "2.txt";														// �������� ����
	
private:
	HCRYPTPROV hProv = NULL;															// ���������������
	HCRYPTKEY hKey = NULL;																// ����������

	char* szPassword = "password";
	//char szPassword[512];
	DWORD dwPasswordLen = 0;
	LPBYTE pEncryptedData = NULL;
	DWORD dwKeyLen = 0, dwValLen = 0;
	DWORD dwEncryptedDataLen = 0;
};