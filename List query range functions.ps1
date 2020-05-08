$rootDirectory = "C:\AOSService\PackagesLocalDirectory"

cd $rootDirectory
ls "*.xml" -Recurse | % {
    $matches = (gc $_.FullName -ReadCount 0 | Select-String '\[QueryRangeFunction(?:Attribute)?\(?\)?\]\s+?public static ([^\r\n ]+) ([^\r\n)]+)' -AllMatches).Matches

    if ($matches.Count -ne 0)
    {
        $file = $_.Name.Substring(0, $_.Name.Length -4)
        #$fullName = $_.FullName

        foreach ($m in $matches) {
            Write-Host "$($m.Groups[1]) " -NoNewline
            Write-Host "$($file)." -NoNewline -ForegroundColor Gray
            Write-Host "$($m.Groups[2]))"
        }
    }
}
