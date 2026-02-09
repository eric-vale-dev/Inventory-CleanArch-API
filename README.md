# Sistema de Inventario (Clean Architecture)

Backend desarrollado con .NET 8 siguiendo los principios de Clean Architecture. Incluye API RESTful, validaciones con FluentValidation, persistencia con Entity Framework Core y base de datos PostgreSQL en Docker.

## 🚀 Tecnologías
* **.NET 8** (Web API)
* **Entity Framework Core** (ORM)
* **PostgreSQL** (Base de datos)
* **Docker** (Contenedorización)
* **FluentValidation** (Reglas de negocio)
* **Swagger** (Documentación de API)

## 🛠️ Cómo ejecutar el proyecto

1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/TU_USUARIO/InventarioCleanArch.git](https://github.com/TU_USUARIO/InventarioCleanArch.git)
    ```

2.  **Levantar la Base de Datos (Docker):**
    Asegúrate de tener Docker corriendo.
    ```bash
    docker compose up -d
    ```

3.  **Ejecutar las migraciones (Crear tablas):**
    ```bash
    dotnet ef database update --project Inventario.Infrastructure --startup-project Inventario.API
    ```

4.  **Correr la API:**
    ```bash
    dotnet run --project Inventario.API/Inventario.API.csproj
    ```

5.  **Ver documentación:**
    Abre tu navegador en: `http://localhost:5071/swagger` (o el puerto que te indique la consola).
