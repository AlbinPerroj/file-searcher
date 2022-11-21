#include "search_status.h"

string search_status::toString() 
{
	return channel + ';' + to_string(minValue) + ';' + minValueDate + ';' + to_string(maxValue) + ';' + maxValueDate;
}