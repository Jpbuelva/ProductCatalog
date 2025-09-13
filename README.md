# ProductCatalog

API RESTful para la gesti�n de productos, inspirada en la p�gina de detalle de MercadoLibre. 
Implementada en .NET 8, siguiendo buenas pr�cticas de arquitectura y desarrollo backend.

## Caracter�sticas principales

- CRUD de productos (crear, leer, actualizar, eliminar)
- Persistencia en archivos locales (`products.json`)
- Manejo global de errores con middleware
- Mapeo entre entidades y DTOs con AutoMapper
- Validaciones con FluentValidation
- Pruebas unitarias con xUnit y Moq
- Documentaci�n interactiva con Swagger

## Estructura del proyecto

- `ProductCatalog.Api`: API y controladores
- `ProductCatalog.Application`: Servicios, DTOs, mapeos, validaciones
- `ProductCatalog.Domain`: Entidades de dominio
- `ProductCatalog.Infrastructure`: Persistencia en archivos
- `ProductCatalog.Tests`: Pruebas unitarias

## Endpoints principales

- **GET** `/api/products`  
  Lista todos los productos

- **GET** `/api/products/{id}`  
  Obtiene un producto por ID

- **POST** `/api/products`  
  Crea un nuevo producto

- **PUT** `/api/products/{id}`  
  Actualiza un producto existente

- **DELETE** `/api/products/{id}`  
  Elimina un producto

Consulta la documentaci�n interactiva en `/swagger` para m�s detalles y pruebas.

## Ejecuci�n r�pida

Consulta el archivo [`run.md`](./run.md) para instrucciones paso a paso sobre c�mo restaurar, ejecutar y probar la soluci�n.

## Notas

- Todos los datos se almacenan en el archivo `products.json` en la carpeta de salida de la API.
- No se requiere base de datos ni configuraci�n adicional.
- El proyecto est� preparado para ser ejecutado y probado f�cilmente en cualquier entorno compatible con .NET 8.