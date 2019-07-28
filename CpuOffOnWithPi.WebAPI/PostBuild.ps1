
$projectDir = $args[0].Trim();
$targetDir = $args[1].Trim() + "\*";

$destinationRoot = "\\readyshare\USB_Storage\software-update\pi3portal\webapi\";

Copy-Item -Path $targetDir -Recurse -Destination $destinationRoot -Container -Force

$updateFilePath = $destinationRoot + "UPDATE.txt";
$fileExist = Test-Path $updateFilePath;
if (!$fileExist) {
	#Remove-Item $updateFilePath
	New-Item -ItemType file $updateFilePath
}

$configPath = "\\readyshare\USB_Storage\software-update\pi3portal\webapi\CpuOffOnWithPi.WebAPI.exe.config";
Remove-Item $configPath

Start-Sleep -s 1

Invoke-WebRequest 'http://pi3portal.local/API/Values/Update'
