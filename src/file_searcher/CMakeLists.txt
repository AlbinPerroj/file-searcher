# CMakeList.txt : CMake project for file_searcher, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

project ("file_searcher")

# Add source to this project's executable.
add_executable (file_searcher "file_searcher.cpp" "file_searcher.h" "file_handler.cpp" "file_handler.h" "search_status.cpp" "search_status.h")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET file_searcher PROPERTY CXX_STANDARD 20)
endif()

# TODO: Add tests and install targets if needed.
