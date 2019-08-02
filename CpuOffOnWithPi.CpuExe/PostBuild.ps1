
# Copy the www directory to the output dir.
# This is simpler than trying to remember to make everything in the www folder "copy if changed" build action

# post build event syntax
# powershell.exe -ExecutionPolicy Bypass -NoProfile -NonInteractive -File PostBuild.ps1 " $(ProjectDir) " " $(TargetDir) "

# I used to pass in arguments frmo the project post-build event, but I decided I want to better control deployment
$arg0 = "C:\home\projects\Custom Tools\CpuOffOnWithPi\CpuOffOnWithPi.CpuExe\"; #$args[0];
$arg1 = "C:\home\projects\Custom Tools\CpuOffOnWithPi\CpuOffOnWithPi.CpuExe\bin\Debug\"; #$args[1];

$projectDir = $arg0.Trim();
$targetDir = $arg1.Trim();

Write-Host "Project: " $projectDir
Write-Host "Target: " $targetDir

$sourceRoot = $projectDir + "www\";
$destinationRoot = $targetDir;

Write-Host "Source: " $sourceRoot
Write-Host "Dest: " $destinationRoot

$folderExist = Test-Path $destinationRoot;
if (!$folderExist) {
	New-Item $destinationRoot -ItemType "directory"
}

Copy-Item -Path $sourceRoot -Recurse -Destination $destinationRoot -Container -Force


# Now prep the CpuExe update and execute on remote server

$projectDir = $arg0.Trim();
$targetDir = $arg1.Trim() + "\*";

$destinationRoot = "\\readyshare\USB_Storage\software-update\fs\cpuexe\";

Copy-Item -Path $targetDir -Recurse -Destination $destinationRoot -Container -Force

$updateFilePath = $destinationRoot + "UPDATE.txt";
$fileExist = Test-Path $updateFilePath;
if (!$fileExist) {
	#Remove-Item $updateFilePath
	New-Item -ItemType file $updateFilePath
}

$configPath = "\\readyshare\USB_Storage\software-update\fs\cpuexe\CpuOffOnWithPi.CpuExe.exe.config";
Remove-Item $configPath

Start-Sleep -s 1

Invoke-WebRequest 'http://fs.local/API/Values/Update'
