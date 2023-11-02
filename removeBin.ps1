$rootDirectory = $(pwd).Path

$timeout = 2 # Timeout duration in seconds

# Function to check if a folder is named ".git"
function IsGitVsFolder($folder) {
    return ($folder.Name -eq ".git" -or $folder.Name -eq ".vs") -and $folder.Attributes -eq "Directory"
}

# Function to delete a folder recursively
function RemoveFolder($folder) {
    Write-Host "Deleting folder: $($folder.FullName)"
    Remove-Item $folder.FullName -Force -Recurse
}

# Function to search for and remove "bin" or "obj" folders recursively
function RemoveBinAndObjFolders($directory) {
    $folders = Get-ChildItem $directory -Directory -Force

    foreach ($folder in $folders) {
        if (IsGitVsFolder $folder) {
            continue
        }

        if ($folder.Name -eq "bin" -or $folder.Name -eq "obj") {
            Write-Host($folder.FullName)
            RemoveFolder $folder
            
        }
        else {
            RemoveBinAndObjFolders $folder.FullName
        }
    }
}

# Start the recursive search and deletion process
RemoveBinAndObjFolders $rootDirectory
Start-Sleep -Seconds $timeout
RemoveBinAndObjFolders $rootDirectory

$zipFileName = "src.zip"

# Check if the src.zip file exists in the current folder
$zipFilePath = Join-Path -Path $rootDirectory -ChildPath $zipFileName
if (Test-Path -Path $zipFilePath -PathType Leaf) {
    # Remove the src.zip file
    Remove-Item -Path $zipFilePath -Force
    Write-Host "src.zip file has been removed."
}
else {
    Write-Host "src.zip file does not exist in the current folder."
}


# Function to create a zip file from the contents of the current directory

<#
function ZipCurrentDirectory() {

    $excludedFolders = @(".git", ".vs")

    $sourcePath = Join-Path -Path $rootDirectory -ChildPath $zipFileName

    Write-Output "Creating zip file: $zipFileName"
    Write-Output "Adding files from: $sourcePath"

    if (Test-Path -Path $sourcePath) {
        Remove-Item -Path $sourcePath -Force
    }

    try {
        Add-Type -A 'System.IO.Compression.FileSystem'
        [System.IO.Compression.ZipFile]::CreateFromDirectory($rootDirectory, $sourcePath, "Optimal", $false)
        Write-Output "Zip file created successfully."
    }
    catch {
        Write-Output "Failed to create the zip file: $_"
    }
}


# Call the zip function
ZipCurrentDirectory

#>