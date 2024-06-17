# Revit Parameter Scanner Add-In

This is an add-in for Autodesk Revit that allows users to search for elements based on parameter values.

## Features

- Search elements by parameter name and value.
- Isolate or select found elements in the view.

## Setup Instructions

1. Clone this repository.
2. Open the solution in Visual Studio.
3. Build the solution to generate the add-in DLL.
4. Copy the generated DLL (ParameterScanner.dll) and the add-in manifest file (ParameterScannerManifest) to the Revit add-ins folder.

Alternative method, you can simply download the ParameterScanner.zip zip file and extract it's contents into the Revit add-ins folder.

## Note: If you use this method your pc may bloc the .dll file so before you open revit follow these steps

1. Right click on the ParameterScanner.dll file.
2. In the genral tab go to the bottom part of window and find the security warning that has blocked this file.
3. Click the checkbox that says to "Unblock" file.

## Usage

1. Open Revit.
2. Navigate to the Add-Ins tab.
3. Click on "Parameter Scanner" to run the add-in.
4. Enter the parameter name and value to search for.
5. Click on "Isolate in View" or "Select" to interact with the elements found.
