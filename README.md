# GeoTab Device Manager

Este proyecto es una aplicación de consola .NET que se conecta a la API de GeoTab para obtener información de dispositivos y escribir esos datos en archivos CSV.

## Requisitos

- .NET SDK 6.0 o superior
- Credenciales de la API de GeoTab
- Archivo de configuración `appsettings.json`

## Instalación

1. **Clona el repositorio:**

    ```bash
    git clone https://github.com/tu-usuario/geotab-device-manager.git
    cd geotab-device-manager
    ```

2. **Configura el archivo `appsettings.json`:**

    Crea un archivo `appsettings.json` en el directorio raíz del proyecto con el siguiente contenido:

    ```json
    {
      "APISettings": {
        "User": "tu_usuario@geotab.com",
        "Password": "tu_contraseña",
        "Database": "demo_candidates_net",
        "Server": "mypreview.geotab.com"
      }
    }
    ```

3. **Restaura los paquetes NuGet:**

    ```bash
    dotnet restore
    ```

## Uso

1. **Construir el proyecto:**

    ```bash
    dotnet build
    ```

2. **Ejecutar la aplicación:**

    ```bash
    dotnet run
    ```

    La aplicación te pedirá que confirmes si deseas descargar nuevos datos de vehículos. Escribe `y` para proceder.

3. **Reintentos automáticos:**

    La aplicación intentará automáticamente reintentar las solicitudes fallidas debido a límites de API o problemas de servicio.

## Estructura del Proyecto

- `Program.cs`: Contiene el punto de entrada de la aplicación.
- `DeviceManager.cs`: Contiene la lógica para interactuar con la API de GeoTab.
- `FileManager.cs`: Contiene la lógica para escribir los datos de los dispositivos en archivos CSV.
- `DeviceDto.cs`: Define la estructura de los datos del dispositivo.
- `appsettings.json`: Contiene la configuración de la API de GeoTab.

## Ejemplo de Uso

La aplicación se conectará a la API de GeoTab, recuperará la información de los dispositivos y la escribirá en archivos CSV. Si se supera el límite de llamadas a la API, la aplicación esperará 60 segundos antes de reintentar.

```bash
Did you want to download new vehicle data? (Y/N) #
y
Searching ... #
------------------------
10 devices were found #
File writted in /full/path/to/devices/
------------------------
Completed#
