// file_searcher.cpp : Defines the entry point for the application.
//

#include "file_searcher.h"
#include <chrono>
#include "file_handler.h"

using namespace std::chrono;

int main(int argc, char* argv[])
{
    if (argc < 4) {
        std::cout << "Cmd line arguments missing" << endl;
        std::cout << "ex: program.exe inputfile outputfile";
        return 1;
    }

    file_handler reader;
    reader.fileName = argv[1];
    reader.resultFileName = argv[2];
    string methodFlag = argv[3];

    auto start = high_resolution_clock::now();

    if (methodFlag == "0") {
        reader.findMinAndMaxValue();
    }
    else if (methodFlag == "1") {
        reader.findMinAndMaxValueOne();
    }

    reader.writeSearchResultToFile();

    auto stop = high_resolution_clock::now();

    std::cout << "Execution time is [milliseconds]: " << duration_cast<milliseconds>(stop - start).count() << endl;

    return 0;
}
