
return; # ABORT for now -- change is happening!

# Copy the www directory to the output dir.
# This is simpler than trying to remember to make everything in the www folder "copy if changed" build action

$projectDir = $args[0].Trim();
$targetDir = $args[1].Trim();

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
