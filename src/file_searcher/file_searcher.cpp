// file_searcher.cpp : Defines the entry point for the application.
//
#include "file_searcher.h"

using namespace std::chrono;

int main(int argc, char* argv[])
{
    
    if (argc < 3) {
        std::cout << "cmd line arguments missing" << endl;
        std::cout << "file_searcher.exe [req: input file path] [req: output file name] [opt: flag for different search approach]";
        return 1;
    }

    file_handler reader;
    reader.fileName = argv[1];
    reader.resultFileName = argv[2];

    string methodFlag;
    if (argc >=4) {
        methodFlag = argv[3];
    }
    else {
        methodFlag = "0";
    }

    auto start = high_resolution_clock::now();

    if (methodFlag == "0") {
        std::cout << "Calling findMinAndMaxValue" << endl;
        reader.findMinAndMaxValue();
    }
    else if (methodFlag == "1") {
        std::cout << "Calling findMinAndMaxValueUsingMultimap" << endl;
        reader.findMinAndMaxValueUsingMultimap();
    }

    reader.writeSearchResultToFile();

    auto stop = high_resolution_clock::now();

    std::cout << "Execution time is: " << duration_cast<microseconds>(stop - start).count() << " microseconds." << endl;

    return 0;
}
