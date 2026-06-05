<#
.SYNOPSIS
Render PlantUML diagrams. Docker-first, falls back to Java+plantuml.jar (requires Java 11+ and Graphviz dot).

USAGE
./scripts/render_plantuml.ps1 [-PumlPath <path-to-puml>]
#>
[CmdletBinding()]
param(
	[string]$PumlPath = "$(Join-Path $PSScriptRoot '..\diagrams\CineScopeClassDiagram.puml')"
)

function Write-ErrorAndExit([string]$msg, [int]$code = 1) {
	Write-Error $msg
	exit $code
}

try {
	$root = Resolve-Path (Join-Path $PSScriptRoot '..')
	$puml = Resolve-Path -LiteralPath $PumlPath -ErrorAction Stop
} catch {
	Write-ErrorAndExit "PUML file not found: $PumlPath" 2
}

$outDir = Join-Path $root 'diagrams\output'
New-Item -ItemType Directory -Path $outDir -Force | Out-Null
$jar = Join-Path $root 'plantuml.jar'

# Use Docker if available
if (Get-Command docker -ErrorAction SilentlyContinue) {
	Write-Host 'Using Docker to render PlantUML...'
	$relPath = $puml.Path.Substring($root.Path.Length).TrimStart('\','/') -replace '\\','/'
	$work = $root.Path -replace '\\','/'

    # Use ${work} to avoid PowerShell interpreting the following ':' as part of a variable name
	$dockerArgs = @('run','--rm','-v',"${work}:/workspace",'plantuml/plantuml','-tpng',"/workspace/$relPath",'-o','/workspace/diagrams/output')
	$proc = Start-Process -FilePath docker -ArgumentList $dockerArgs -NoNewWindow -PassThru -Wait
	if ($proc.ExitCode -ne 0) { Write-ErrorAndExit "Docker plantuml failed with exit code $($proc.ExitCode)" 3 }
	Write-Host "Rendered PNG(s) written to: $outDir"
	exit 0
}

# Fall back to Java + plantuml.jar
if (Get-Command java -ErrorAction SilentlyContinue) {
	Write-Host 'Java found. Checking version...'
	$verOut = & java -XshowSettings:properties -version 2>&1
	# Robustly extract the Java version string from the output. Avoid using automatic $Matches which may be null.
	$joined = $verOut -join "`n"
	$m = [regex]::Match($joined, '"(?<v>[^"]+)"')
	if ($m.Success) { $ver = $m.Groups['v'].Value } else { $ver = $null }
	if (-not $ver) { Write-ErrorAndExit 'Unable to determine Java version.' 4 }
	try {
        if ($ver -like '1.*') {
            $parts = $ver.Split('.')
            $major = [int]$parts[1]
        } else {
            $parts = $ver.Split('.')
            $major = [int]$parts[0]
        }
    } catch {
        Write-ErrorAndExit "Unable to parse Java major version from '$ver'" 4
    }
	if ($major -lt 11) { Write-ErrorAndExit "Java version $ver detected. PlantUML requires Java 11+. Install Java 11+ or use Docker." 5 }

	if (-not (Test-Path $jar)) { Write-ErrorAndExit "plantuml.jar not found at $jar. Place plantuml.jar in project root or use Docker." 6 }
	if (-not (Get-Command dot -ErrorAction SilentlyContinue)) { Write-ErrorAndExit "Graphviz 'dot' not found on PATH. Install Graphviz or use Docker." 7 }

	Write-Host 'Running plantuml.jar to render...'
	& java -jar $jar -tpng -o $outDir $puml.Path
	if ($LASTEXITCODE -ne 0) { Write-ErrorAndExit "plantuml.jar failed with exit code $LASTEXITCODE" 8 }
	Write-Host "Rendered PNG(s) written to: $outDir"
	exit 0
}

Write-ErrorAndExit 'Neither Docker nor Java available. Install Docker, or Java 11+ and Graphviz.' 9
