# Gateway Connection

## Available virtual S2S Gateway Connection

Loads all available virtual S2S Gateway Connections: 

`GET-CLOUD4VGWNETCONNECTION`

The following parameters must be specified: 

`-VIRTUALGATEWAYID` **`<GUID of vGW>`**

## Loading a virtual S2S Gateway Connection

Loads a specific virtual S2S Gateway Connection by its ID: 

`GET-CLOUD4VGWNETCONNECTION`

The following parameters must be specified: '

`-ID` **`<GUID of vGWNetConn>`**

`-VIRTUALGATEWAYID` **`<GUID of vGW>`**

## Creating a virtual S2S Gateway Connection

Creates a new virtual S2S Gateway Connection: 

`NEW-CLOUD4VGWNETCONNECTION`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual Load S2S Gateway)`** ``

`-VIRTUALGATEWAYID` **`<GUID of vGW>`**

`-SHAREDSECRET` **`(Shared Secrect for Tunnel)`**

`-DESTINATIONIPADDRESS` **`(IP Address of Destination)`**

`-DESTINATIONPREFIX` **`(Array of IP Ranges like x.x.x.x/x)`**

`-AUTHENTICATIONMETHOD`  **`(PSK)`**

`-DIFFIEHELLMANGROUP` **`(Group1, Group2, Group14, ECP256, ECP384 or Group24)`**

`-ENCRYPTIONALGORITHM` **`(DES, DES3, AES128, AES192 or AES256)`**

`-INTEGRITYALGORITHM` **`(MD5, SHA1, SHA256 or SHA384)`**

`-AUTHENTICATIONTRANFORMATIONCONSTANT` **`(None, MD596, SHA196, SHA256128, GCMAES128, GCMAES192 or GCMAES256)`**

`-CIPHERTRANSFORMATIONCONSTANT` **`(None, DES, DES3, AES128, AES192, AES256, GCMAES128, GCMAES192 or GCMAES256)`**

`-PERFECTFORWARDSECRECY` **`(None, PFS1, PFS2, PFS2048, ECP256, ECP384, PFSMM or PFS24)`**

`-MAINMODESALIFETIMEKILOBYTES` **`(1024 until Max Integer)`**

`-MAINMODESALIFETIMESECONDS` **`(300 until Max Integer)`**

`-QUICKMODEIDLEDISCONNECTSECONDS` **`(300 until Max Integer)`**

`-QUICKMODESALIFETIMESECONDS` **`(300 until Max Integer)`**

`-QUICKMODESALIFETIMEKILOBYTES` **`(1024 until Max Integer)`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a virtual S2S Gateway Connection

Deletes the virtual S2S Gateway. 

`REMOVE-CLOUD4VGWNETCONNECTION`

The following parameters must be specified: 

`-ID` **`<GUID of vGWNetConn>`**

`-VIRTUALGATEWAYID` **`<GUID of vGW>`**

