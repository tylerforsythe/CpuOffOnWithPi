
# post build event syntax
# powershell.exe -ExecutionPolicy Bypass -NoProfile -NonInteractive -File PostBuild.ps1 " $(ProjectDir) " " $(TargetDir) "

# I used to pass in arguments frmo the project post-build event, but I decided I want to better control deployment
$arg0 = "C:\home\projects\Custom Tools\CpuOffOnWithPi\CpuOffOnWithPi.WebAPI\"; #$args[0];
$arg1 = "C:\home\projects\Custom Tools\CpuOffOnWithPi\CpuOffOnWithPi.WebAPI\bin\Debug\"; #$args[1];

$projectDir = $arg0.Trim();
$targetDir = $arg1.Trim() + "\*";

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
