# run.md

## ProductCatalog - Gu�a r�pida de ejecuci�n

### 1. Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.
- Editor recomendado: Visual Studio 2022+ o VS Code.

---

### 2. Restaurar dependencias

Abre una terminal en la ra�z del proyecto y ejecuta:
dotnet restore

---

### 3. Ejecutar la API

Inicia la API con: dotnet run --project ProductCatalog.Api


Por defecto, la API estar� disponible en:
http://localhost:5014/ 

---

### 4. Probar la API con Swagger

Abre tu navegador y visita:

- http://localhost:5014/swagger/index.html

Aqu� puedes explorar y probar todos los endpoints de la API de forma interactiva.

---

### 5. Ejecutar las pruebas unitarias

Para correr todas las pruebas del proyecto, ejecuta: dotnet test

---

### 6. Notas adicionales

- Todos los datos de productos se almacenan en el archivo `products.json` generado autom�ticamente en la carpeta de salida de la API.
- No se requiere configuraci�n de base de datos ni pasos adicionales.
- Si tienes problemas con certificados HTTPS en desarrollo, puedes usar la URL HTTP.

---

�Listo! Con estos pasos puedes ejecutar y probar toda la soluci�n ProductCatalog.