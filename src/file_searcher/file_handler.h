#pragma once
#include <string>
#include <map>
#include "search_status.h"

using namespace std;

class file_handler
{
	public:
		string fileName;
		string resultFileName;
		map<int, search_status> searchStatus;
		
		void findMinAndMaxValue();
		void findMinAndMaxValueOne();
		void writeSearchResultToFile();
};