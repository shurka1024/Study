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
	char inputFile[255] = "1.txt";														// �������� ����
	char outputFile[255] = "2.txt";														// �������� ����
	
private:
	HCRYPTPROV hProv = NULL;															// ���������������
	HCRYPTKEY hKey = NULL;																// ����������

};