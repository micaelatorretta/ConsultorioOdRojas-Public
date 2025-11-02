# Guía para ejecutar el proyecto en entorno local

API en ASP.NET Core + Frontend en Vue


## 1) Descargar el proyecto

1. Hacer clic en **Code → Download ZIP** o clonar el repositorio con Git (`git clone ...`).
2. Descomprimir el archivo (si se descargó como ZIP).

## 2) Ejecutar el backend (API)

1. Abrir **Visual Studio 2022**.
2. Seleccionar **Archivo → Abrir → Proyecto o Solución**, y abrir la solucion del proyecto (archivo finalizado con la extension .sln).
3. Una vez cargado, hacer clic derecho sobre el proyecto principal (el que contiene la API) y seleccionar **“Establecer como proyecto de inicio”**.
4. Restaurar los paquetes NuGet (opción disponible al hacer clic derecho sobre la solución).
5. Ejecutar el proyecto presionando el botón **“Iniciar”** (o `F5`).
6. Cuando compile, aparecerá una dirección `https://localhost:5100`.

Si se abre una página con **Swagger**, significa que la API está funcionando correctamente.

En caso de que aparezcan errores de compilación relacionados con archivos faltantes, es probable que la solución no se haya restaurado o compilado correctamente.
Se recomienda realizar una **limpieza** y **recompilación completa** de la solución (Clean + Rebuild) o cerrar y volver a abrir Visual Studio antes de intentarlo nuevamente.


## 3) Ejecutar el frontend (Vue)

1. Abrir la carpeta `Frontend/` con **Visual Studio Code**.
2. En la terminal integrada, ejecutar:

   ```
   npm install
   ```

   Esto descargará todas las dependencias necesarias del proyecto.
3. Una vez finalizado, ejecutar:

   ```
   npm run serve
   ```
   
4. Cuando el proceso termine, aparecerá la URL `http://localhost:8080`.
    Abrir esa dirección en el navegador para visualizar el sitio.


## 4) Verificar el funcionamiento conjunto

1. Confirmar que la **API** está corriendo en Visual Studio.
2. Abrir el **frontend** desde el navegador.
3. Realizar una acción que consuma datos (por ejemplo, listar o crear registros).

 Si la API responde correctamente, el sistema está funcionando de forma local.


## 5) Actualizar dependencias y recompilar

### Frontend

* Actualizar los paquetes:

  ```
  npm update
  ```
* Si el entorno presenta errores, puede eliminarse la carpeta `node_modules` y el archivo `package-lock.json`, y luego ejecutar nuevamente `npm install`.

### Backend

* En Visual Studio:

  * Ir a **Herramientas → Administrador de paquetes NuGet → Administrar paquetes para la solución**.
  * En la pestaña **Actualizaciones**, actualizar los paquetes necesarios.
* Finalmente, ejecutar una **recompilación completa** de la solución (**Rebuild Solution**).


## 6) Resumen

* Levantar la **API** desde Visual Studio 2022.
* Levantar el **frontend** desde Visual Studio Code con `npm run serve`.
* Si la compilación genera errores sobre archivos inexistentes, se debe a una **compilación incompleta o incorrecta de la solución**, no a la ausencia real de esos archivos.
  Realizar una restauración de paquetes y recompilación suele resolver el problema.
