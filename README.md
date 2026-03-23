# SistemaAccesoWeb

Proyecto desarrollado en ASP.NET Core MVC como parte de una prueba técnica para Programador Web.

## Tecnologías utilizadas

- ASP.NET Core MVC
- SQL Server
- Bootstrap
- C#

## Funcionalidades implementadas

- Inicio de sesión
- Validación de usuario
- Manejo de errores de autenticación
- Bloqueo temporal de cuenta por intentos fallidos
- Pantalla de cuenta bloqueada
- Perfil de usuario
- Control de sesión por inactividad con aviso previo
- Expiración de sesión y retorno al login

## Configuración de base de datos

1. Ejecutar el script `database.sql` para crear la base de datos y la tabla `Usuarios`.
2. Actualizar la cadena de conexión en `appsettings.json` según el entorno local.

Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=SistemaAccesoWebDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
3. Ejecutar el proyecto desde Visual Studio.

## Usuario de prueba

Usuario: 46844596
Contraseña: Admin123

## Notas
Durante el desarrollo se utilizó un tiempo corto de sesión para probar el flujo de inactividad.
La versión final del proyecto deja configurado un tiempo más realista para el timeout de sesión.
