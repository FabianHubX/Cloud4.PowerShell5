# Availability Set

## Available Availability 

Sets Loads all available availability sets: 

`GET-CLOUD4AVAILABILITYSET`

## Loading an Availability Set

 Loads a specific availability set using its ID: 

`GET-CLOUD4AVAILABILITYSET`

The following parameters must be specified: 

`-ID  
-VIRTUALDATACENTERID` 

## Creating an Availability 

Set Creates a new availability set: 

`NEW-CLOUD4AVAILABILITYSET`

The following parameters must be specified: 

`-NAME Name of the availability set  
-VIRTUALDATACENTERID virtual data center GUID   
-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the Availability Set object as a return value.

## Updating an Availability Set 

Creates a new availability set: 

`UPDATE CLOUD4AVAILABILITYSET`

The following parameters must be specified: 

`-ID  
-NAME Name of the availability set   
-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the Availability Set object as a return value.

## Deleting an Availability 

Set Deletes the Availability Set. 

`REMOVE-CLOUD4AVAILABILITYSET`

The following parameters must be specified: 

`-ID` 

