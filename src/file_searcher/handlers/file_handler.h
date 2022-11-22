#pragma once
#include <string>
#include <map>
#include "../models/search_status.h"

using namespace std;

class file_handler
{
	public:
		string fileName;
		string resultFileName;
		map<int, search_status> searchStatus;
		
		void findMinAndMaxValue();
		void findMinAndMaxValueUsingMultimap();
		void writeSearchResultToFile();

	private:
		string valuesConst = "-VALUES-";
		string variablesConst = "-VARIABLES-";

};