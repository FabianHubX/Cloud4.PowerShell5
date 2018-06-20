
$vdc = Get-Cloud4vDC -FilterByName "Demo vDC"
$vnet = Get-Cloud4vNet -VirtualDatacenterId $vdc.Id

$gwsubnet = New-Cloud4vSubNet -Name "GWSubNet1" -VirtualNetworkId $vnet.Id -AddressSpace "192.168.10.0/24" -IsGatewaySubnet $true -Wait

$gw = New-Cloud4vGW -Name "Gateway1" -VirtualDataCenterId $vdc.Id -VirtualSubNetId $gwsubnet.Id -Wait

$gwcon = New-Cloud4vGWNetConnection -VirtualGatewayId $gw.Id -DestinationIpAddress 23.97.130.185 -DestinationPrefix "172.16.0.0/16" -AuthenticationMethod PSK -DiffieHellmanGroup Group2 -EncryptionAlgorithm AES256 -IntegrityAlgorithm SHA256 -AuthenticationTranformationConstant SHA256128 -CipherTransformationConstant AES256 -PerfectForwardSecrecy PFS2 -MainModeSALifeTimeKiloBytes 102400000 -MainModeSALifeTimeSeconds 28800 -QuickModeIdleDisconnectSeconds 300 -QuickModeSALifeTimeSeconds 28800 -QuickModeSALifeTimeKiloBytes 102400000 -Name "Gatewway1Conn1" -SharedSecret "Windows2012Windows2012" -Wait
