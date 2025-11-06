$ErrorActionPreference = "Stop"
$appPath = ".\src\bin\Debug\net9.0-windows10.0.19041.0\win10-x64\MSISDNWebClient.exe"

Write-Host "🚀 Iniciando MSISDN Web Client..." -ForegroundColor Cyan
Write-Host "Ruta: $appPath" -ForegroundColor Gray

try {
    $process = Start-Process -FilePath $appPath -Wait -PassThru -NoNewWindow
    $exitCode = $process.ExitCode
    Write-Host "⚠️ La aplicación se cerró con código: $exitCode" -ForegroundColor Yellow
} catch {
    Write-Host "❌ Error al iniciar: $_" -ForegroundColor Red
}

Write-Host "`nPresiona Enter para continuar..."
Read-Host
