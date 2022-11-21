#pragma once
#include <string>
#include <map>

using namespace std;

class search_status
{
	public:
		string channel;
		double minValue = numeric_limits<double>::max();
		string minValueDate;
		double maxValue = numeric_limits<double>::lowest();
		string maxValueDate;
		multimap<double, string, greater<double>> data;
		string toString();
};
