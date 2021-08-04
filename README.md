# Emily's Little Warehouse Version 1.0

Copyleft 2021 - Emily Lovelace

## What?

Emily's Little Warehouse is a Snipe-IT client for Windows Mobile 6+ - It was made to re-use a Motorola/Zebra/Symbol MC65 barcode reader that is available for very cheap online.

## Features

The client was made to, well, to my needs, so basically only thing that has been implemented was what I needed

At this moment, it support:

Reading informations of an item, including custom fields
Adding new item (Specifying Model isn't supported, you need to set a model in the config file)
Moving item to a Location ID (scan item, then scan location)
Moving item to a Location name (batch move, select location, and everything you scan will be moved there)
Creating new locations (Autofill of some information possible, look at config)
Generating label for location and items, ****Possibly print****
Search item using keyword
Add an item to a list/display that list.

****PRINTING IS LIMITED****

With the existing code, the only compatible printer is a cat printer (GB01), and it require another devices with Python 3 to interface with the printer, for that reason, the printing feature can be disabled easily in the configuration.

If you want to try it, you will need to modify https://gist.github.com/xssfox/b911e0781a763d258d21262c5fdd2dec to open access to the webserver to the local network and to setup the IP address of the device running that script in the configuration.

## Configuration

Unfortunately I couldn't finish the configuration UI, so, for now (and probably forever, I doubt there will be many updates), you need to edit the App.config file with a text editor.

ServerAddress : The address of your SnipeIT Server - Don't forget that Windows Mobile is outdated and SSL might be an issue

ServerKey : Your SnipeIT API key

Storage: Full path to where you want the application to store it's file (Moving logs, List, Generated labels)

EarlyConnect: Ask the application to do a request to the server immediately at login, force connection early, it sometime fix issue with slow connection, at the cost of a slower initial loading.

NyaPrint: TRUE or FALSE - Enable the print feature
    NyaPrintServer: full HTTP address of the device running the printing python script.

AutofillAddress: TRUE or FALSE - Enable the address autofill feature for new locations
    Address : Address to autofill
    Address2 : Address 2 to autofill
    ZipCode : Zip Code to autofill
    City : City to autofill
    Country : Country to autofill
    Currency : Currency to autofill

ModelID : The Model ID used for new items

LICENSE:

Emily's Little Warehouse use the library Newtonsoft.Json.Net.Compact under the MIT License.
Emily's Little Warehouse use the library ZXing.Net under Apache License 2.0.
Emily's Little Warehouse own code is licensied under the MIT License.
