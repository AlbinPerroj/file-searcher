#include "file_handler.h"
#include <iomanip>
#include <fstream>

void file_handler::findMinAndMaxValue()
{
	ifstream fin;

	fin.open(fileName);

	bool valuesFlag = false;
	bool variablesFlag = false;
	string line;

	while (getline(fin, line)) {
		int lineSize = line.size();

		if (line == "-VARIABLES-") {
			variablesFlag = true;
			continue;
		}

		if (line == "-VALUES-") {
			variablesFlag = false;
			valuesFlag = true;
			continue;
		}

		if (variablesFlag) {
			int equalPos = line.find_first_of('=', 0);

			search_status status;
			status.channel = line.substr(equalPos + 1, lineSize - equalPos);
			int channelCode = stoi(line.substr(1, equalPos - 1));

			searchStatus.insert(pair<int, search_status>(channelCode, status));
		}

		if (valuesFlag) {
			int firstColon = line.find_first_of(':', 0);
			int firstSemiColon = line.find_first_of(';', 0);
			int lastSemiColon = line.find_last_of(';', lineSize);


			int currentChannel = stoi(line.substr(1, firstColon - 1));
			double currentValue = stod(line.substr(firstColon + 1, firstSemiColon - firstColon - 1));
			string currentDateTime = line.substr(lastSemiColon + 1, lineSize - lastSemiColon);

			if (searchStatus.at(currentChannel).minValue > currentValue)
			{
				searchStatus.at(currentChannel).minValue = currentValue;
				searchStatus.at(currentChannel).minValueDate = currentDateTime;
			}

			if (searchStatus.at(currentChannel).maxValue < currentValue)
			{
				searchStatus.at(currentChannel).maxValue = currentValue;
				searchStatus.at(currentChannel).maxValueDate = currentDateTime;
			}
		}
	}

	fin.close();
}

void file_handler::writeSearchResultToFile() {
	ofstream outf;
	outf << fixed << setprecision(6);

	outf.open(resultFileName);

	for (int i = 0; i < searchStatus.size(); i++) {
		search_status status = searchStatus.at(i);
		outf << status.toString() << endl;
	}

	outf.close();
}