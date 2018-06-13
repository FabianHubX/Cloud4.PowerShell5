
IMPORT-MODULE "CLOUD4.POWERSHELL5.MODULE.PSD1"

$username = "<username>"
$password = '<password>' | ConvertTo-SecureString -asPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential($username,$password)

Open-Cloud4Connection -Credential $cred -LoginUrl "https://login.swiss.cloud" -ApiUrl "https://api.swiss.cloud"
