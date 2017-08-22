#include <stdio.h>
#include <tchar.h>
#include <windows.h>
#include <iostream>
#include <string>
#include "CCrypto.h"

int main()
{
	CCrypto crypto;
	crypto.First();
	while (1)
	{
		system("cls");
		std::cout << "1. Encrypt file" << std::endl;
		std::cout << "2. Decrypt file" << std::endl;
		std::cout << "3. Exit" << std::endl;

		int nomber = 0;
		std::cin >> nomber;
		char filePath[255];

		switch (nomber)
		{
		case 1:
			crypto.CryptoEncryptFile();
			break;
		case 2:
			crypto.CryptoDecryptFile();
			break;
		case 3:
			return 0;
		}
		std::cout << std::endl;
		system("pause");
	}
}