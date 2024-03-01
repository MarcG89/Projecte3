param (
        [string]$Container,
        [string]$IPMongo,
        [string]$IPSQL,
        [string]$OutputRoute,
        [string]$UsernameSQL,
        [string]$PasswordSQL,
        [string]$UsernameMongo,
        [string]$PasswordMongo,
        [string]$UsernameGridFs,
        [string]$PasswordGridFS
        
    )
# ShowHelp does the documentation of the script
function ShowHelp {
    Write-Host @"
        Este script realiza la instalacion de Docker y configura los contenedores necesarios.
        Uso: .\Instalador.ps1 -Opcion <Opcion> -IPMongo <IP> -IPSQL <IP> [-UsernameSQL <Username>] [-PasswordSQL <Password>] [-UsernameMongo <Username>] [-PasswordMongo <Password>] [-UsernameGridFs <Username>] [-PasswordGridFS <Password>] [--help]
        Opciones:
        -Container     : Opcion para elegir el tipo de contenedor (mongo, postgres, mssql, mysql)
        -IPMongo       : IP para el contenedor MongoDB
        -IPSQL         : IP para SQL
        -UsernameSQL   : (Opcional) Nombre de usuario para SQL (Por defecto: SA)
        -PasswordSQL   : (Opcional) Contrasena para SQL (Por defecto: Passw0rd!)
        -UsernameMongo : (Opcional) Nombre de usuario para MongoDB (Por defecto: root)
        -PasswordMongo : (Opcional) Contrasena para MongoDB (Por defecto: a)
        -UsernameGridFs: (Opcional) Nombre de usuario para GridFS (Por defecto: root)
        -PasswordGridFS: (Opcional) Contrasena para GridFS (Por defecto: a)
        --help         : Muestra esta ayuda
"@ 
}
if ($args -contains "--help") {
    ShowHelp
    exit
}

#Function that install Docker
function DockerInstall {
    Start-Process -Wait -FilePath ".\Dependencies\DockerDesktopInstaller.exe"
}
# Function that Validates de Installation of Docker
function DockerValidation {
    if (Test-Path "C:\Program Files\Docker\Docker\Docker Desktop.exe") {
        return $true
    } else {
        return $false
    }
}
# Function that executes de Docker if isn't it
function DockerExecutionValidator {
    $dockerServiceStatus = Get-Service -Name "com.docker.service" -ErrorAction SilentlyContinue

    if ($dockerServiceStatus -eq $null) {
        Start-Service -Name $dockerServiceName
        Write-Host "Iniciando el servicio de Docker..."

        do {
            Start-Sleep -Seconds 2
            $dockerServiceStatus = Get-Service -Name $dockerServiceName -ErrorAction SilentlyContinue
        } until ($dockerServiceStatus.Status -eq "Running")
    }
    else {
        Write-Host "El servicio de Docker ya esta en ejecucion."
    }
}
# Funcition that realize the Container installation
# Param: $Opcion (is the second container you want to create)
function ContainerInstallation {
    param (
        [string]$Opcio
    )
    $dockerComposeCommand = Get-Command docker-compose -ErrorAction SilentlyContinue
    if ($dockerComposeCommand -eq $null) {
        Write-Host "El comando 'docker-compose' no está instalado. Por favor, instale Docker Compose para continuar."
        return
    }

    switch ($Opcio) {
        "mongo" {
            Start-Process docker-compose -ArgumentList "-f .\Dependencies\docker-compose-mongo.yaml up" -NoNewWindow -Wait
            Start-Sleep -Seconds 15
            docker-compose -f .\Dependencies\docker-compose-mongo.yaml down
        }
        "postgres" {
            Start-Process docker-compose -ArgumentList "-f .\Dependencies\docker-compose-postgres.yaml up" -NoNewWindow -Wait
            Start-Sleep -Seconds 15
            docker-compose -f .\Dependencies\docker-compose-postgres.yaml down
        }
        "mssql" {
            Start-Process docker-compose -ArgumentList "-f .\Dependencies\docker-compose-mssql.yaml up" -NoNewWindow -Wait
            Start-Sleep -Seconds 15
            docker-compose -f .\Dependencies\docker-compose-mssql.yaml down
        }
        "mysql" {
            Start-Process docker-compose -ArgumentList "-f .\Dependencies\docker-compose-mysql.yaml up" -NoNewWindow -Wait
            Start-Sleep -Seconds 15
            docker-compose -f .\Dependencies\docker-compose-mysql.yaml down
        }
        Default {
            Write-Error "La opción introducida es invalida"
        }
    }
}
# EdicioMongo does de files edition of the api of mongo
# Param: IP (is the host ip)
# Param: Username (is the username of the database)
# Param: Password (is the password of the database)
function EdicioMongo {
    param (
        [string]$IP = "localhost",
        [string]$Username = "root",
        [string]$Password = "a"
    )
    $filePath1 = ".\APIS\ApiMongoMusica\appsettings.json"
    $content1 = Get-Content -Path $filePath1 -Raw | ConvertFrom-Json
    if ($content1.MongoMusicInfoDatabase) {
        $content1.MongoMusicInfoDatabase.ConnectionString = "mongodb://${Username}:${Password}@${IP}:27017/admin"
        $newContent1 = $content1 | ConvertTo-Json -Depth 100
        Set-Content -Path $filePath1 -Value $newContent1 -Encoding UTF8
    } else {
        Write-Host "La propiedad 'MongoMusicInfoDatabase' no existe en el archivo JSON."
    }

    $filePath2 = ".\APIS\ApiMongoMusica\Properties\launchSettings.json"
    $content2 = Get-Content -Path $filePath2 -Raw | ConvertFrom-Json
    $content2.profiles.http.applicationUrl = "http://${IP}:5042"
    $content2.profiles.https.applicationUrl = "https://${IP}:7145;http://${IP}:5042"
    $content2.iisSettings.iisExpress.applicationUrl = "http://${IP}:53760"
    $newContent2 = $content2 | ConvertTo-Json -Depth 100 
    Set-Content -Path $filePath2 -Value $newContent2 -Encoding UTF8
}
# EdicioSql does de files edition of the api of sql
# Param: IP (is the host ip)
# Param: Username (is the username of the database)
# Param: Password (is the password of the database)
function EdicioSql {
    param (
        [string]$IP = "localhost",
        [string]$Port = "5042",
        [string]$Username = "SA",
        [String]$Password = "Passw0rd!"
    )
    $filePath1 = ".\APIS\ApiMusicInfo\appsettings.json"
    $content1 = Get-Content -Path $filePath1 -Raw | ConvertFrom-Json
    try {
        $content1.ConnectionStrings.DefaultConnection = "Server=${IP};Database=MusicInfoDB;integrated security=false;User Id=${Username};Password=${Password};encrypt=false;MultipleActiveResultSets=true"
    }
    catch {
    }
    
    $newContent1 = $content1 | ConvertTo-Json -Depth 10
    Set-Content -Path $filePath1 -Value $newContent1
    $filePath2 = ".\APIS\ApiMusicInfo\Properties\launchSettings.json"
    $content2 = Get-Content -Path $filePath2 -Raw | ConvertFrom-Json
    $content2.profiles.http.applicationUrl = "http://${IP}:${Port}"
    $content2.profiles.https.applicationUrl = "https://${IP}:7145;http://${IP}:${Port}"
    $content2.iisSettings.iisExpress.applicationUrl = "http://${IP}:53760"
    $newContent2 = $content2 | ConvertTo-Json -Depth 10
    Set-Content -Path $filePath2 -Value $newContent2
}
# EdicioGridFS does de files edition of the api of gridfs
# Param: IP (is the host ip)
# Param: Username (is the username of the database)
# Param: Password (is the password of the database)
function EdicioGridFs {
    param (
        [string]$IP = "localhost",
        [string]$Username = "root",
        [String]$Password = "a"
    )
    $filePath1 = ".\APIS\ApiMusica\appsettings.json"
    $content1 = Get-Content -Path $filePath1 -Raw | ConvertFrom-Json
    try {
        $content1.MongoMusicInfoDatabase.ConnectionString = "mongodb://${Username}:${Password}@${IP}:27017/admin"
    }
    catch {
    }
    
    $newContent1 = $content1 | ConvertTo-Json -Depth 10
    Set-Content -Path $filePath1 -Value $newContent1
    $filePath2 = ".\APIS\ApiMusica\Properties\launchSettings.json"
    $content2 = Get-Content -Path $filePath2 -Raw | ConvertFrom-Json
    $content2.profiles.http.applicationUrl = "http://${IP}:5042"
    $content2.profiles.https.applicationUrl = "https://${IP}:7145;http://${IP}:5042"
    $content2.iisSettings.iisExpress.applicationUrl = "http://${IP}:53760"
    $newContent2 = $content2 | ConvertTo-Json -Depth 10
    Set-Content -Path $filePath2 -Value $newContent2
}
# ApiConfiguration execute the all above configurations of apis
function ApiConfiguration {
    EdicioMongo
    EdicioSql
    EdicioGridFs
}

# MoveFiles move the instalation files to the output that the user introduce (except the instalation of Docker)
#param: OutputPath (is the output path that the user introduce)
function MoveFiles {
    param (
        [string]$OutputPath
    )
    Copy-Item -Path ".\APIS\ApiMongoMusica" -Destination $OutputPath -Recurse -Force
    Copy-Item -Path ".\APIS\ApiMusica" -Destination $OutputPath -Recurse -Force
    Copy-Item -Path ".\APIS\ApiMusicInfo" -Destination $OutputPath -Recurse -Force
    Copy-Item -Path "mongodata" -Destination $OutputPath -Recurse -Force
    if(Test-Path "mysqldata") {
        Copy-Item -Path "mysqldata" -Destination $OutputPath -Recurse -Force
    } elseif (Test-Path "mssqldata") {
        Copy-Item -Path "mssqldata" -Destination $OutputPath -Recurse -Force
    } elseif (Test-Path "postgresql_data") {
        Copy-Item -Path "postgresql_data" -Destination $OutputPath -Recurse -Force
    }

    

}
# Function Main does de principal job of the script
# param: ALL (all the params are documeted in ShowHelp)
function Main {
    if (-not ($Container -and
              $IPMongo -and
              $IPSQL -and
              $OutputRoute)) {
        Write-Host "Falta uno o mas parametros obligatorios. Revise la llamada al script. Utiliza el comando --help para saber la funcion"
        return
    }
    if (DockerValidation) {
        DockerExecutionValidator
        ContainerInstallation -Opcio mongo
        ContainerInstallation -Opcio $Container
        ApiConfiguration -Container mongo -IP $IPMongo
        ApiConfiguration -Container $Container
        MoveFiles -OutputPath $OutputRoute
        Write-Host "Instalacio Finalitzada!"
    } else {
        Write-Host "Error de instalacion de Docker vuelve a ejecutar el programa!"
    }
}

$isInstalled1 = DockerValidation
if ($isInstalled1) {
    Main
} else {
    DockerInstall
    Main
}
