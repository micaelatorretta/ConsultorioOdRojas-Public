# Guía de deploy

Esta guía describe, a alto nivel, cómo publicar la API hecha en ASP.NET Core y un frontend en Vue dentro del **mismo repositorio** de GitHub, usando **GitHub Actions** para CI/CD.

---

## 1) Enfoque general

* **Frontend (Vue):** se construye y se publica en **GitHub Pages** (hosting estático, CDN global).
* **API (ASP.NET Core):** se publica en **Azure App Service (Linux/.NET)**.
* **Monorepo:** un único repo con dos carpetas (por ejemplo `api/` y `frontend/`) y **dos workflows** independientes en `.github/workflows/`.

## 2) Requisitos previos

* Cuenta de **GitHub** con GitHub Pages habilitado.
* Cuenta de **Azure** (o el proveedor elegido) y un **App Service** creado para .NET.
* Acceso para crear **Secrets** en el repositorio (para credenciales de publicación).
* Proyecto **Vue 3** (Vite) y **ASP.NET Core** configurados y corriendo localmente.

---

## 3) Estructura sugerida del repositorio

* Carpeta `api/` → código de la API (ASP.NET Core).
* Carpeta `frontend/` → código del cliente (Vue).
* Carpeta `.github/workflows/` → dos workflows: uno para **API** y otro para **frontend**.
* `README.md` → esta guía.

> No es obligatorio este nombre de carpetas, pero sí separar claramente ambos proyectos.

---

## 4) Flujo de CI/CD

* Cada push o merge a la rama principal:

  * Si cambió algo en `frontend/`, se dispara el workflow del **frontend**, construye el sitio y lo sube a **GitHub Pages**.
  * Si cambió algo en `api/`, se dispara el workflow de la **API**, compila y publica en **Azure App Service**.
* Cada workflow es independiente, con sus propios triggers y permisos.

---

## 5) Preparación del frontend (Vue) para producción

* Definir variables de entorno para apuntar a la URL pública de la API (por ejemplo, `VITE_API_BASE`).
* Configurar la **base pública** del sitio para Pages (si el sitio vive en `https://usuario.github.io/repositorio/`, ajustar la base del build para rutas relativas).
* Decidir la estrategia de enrutamiento:

  * **Hash mode** (no requiere rewrites del servidor en Pages), o
  * Mantener rutas simples si no se usan rutas profundas.

---

## 6) Publicación del frontend en GitHub Pages

* Activar **GitHub Pages** en Settings → Pages → “GitHub Actions”.
* Crear un **workflow** de Pages:

  * Pasos: checkout → instalación de dependencias → build → upload artifact → deploy to Pages.
  * Limitar el trigger a cambios dentro de `frontend/`.
* Validar la URL resultante: `https://<usuario>.github.io/<nombre-del-repo>/`.

---

## 7) Preparación de la API (ASP.NET Core) para producción

* Leer **configuración sensible** (connection strings, claves) desde variables de entorno/app settings del proveedor (en Azure: “Configuration” → “Application settings”).
* Habilitar **CORS** para permitir el origen del frontend (la URL final de GitHub Pages o dominio propio).
* Asegurar logging básico y health endpoint si se desea monitoreo.

---

## 8) Publicación de la API en Azure App Service

* Crear el **App Service**.
* Descargar el **Publish Profile** desde Azure y guardarlo como **Secret** del repo en GitHub (por ejemplo: `AZURE_WEBAPP_PUBLISH_PROFILE`).
* Crear un **workflow** de deploy para la API:

  * Pasos: checkout → setup .NET → restore/build/test → publish → deploy a App Service usando el Publish Profile.
  * Limitar el trigger a cambios dentro de `api/`.
* Verificar la URL pública: `https://<appservice>.azurewebsites.net/`.

> Si se usa Docker/Contenedores: el workflow construye la imagen, la sube a un registry (ACR/Docker Hub) y el App Service “for Containers” hace pull. El esquema CI/CD sigue siendo igual.

---

## 9) Conexión Frontend ↔ API

* En el **frontend**, la variable de entorno de producción debe apuntar a la URL pública de la API.
* En la **API**, la policy de **CORS** debe permitir esa URL concreta (GitHub Pages o dominio custom).
* Si se usan cookies/credenciales, habilitar “credentials” en CORS y configurar el cliente para enviarlas.

---

## 10) Ramas, calidad y seguridad (recomendado)

* **Branch protection** sobre `main`.
* Workflows de **CI** en Pull Requests (lint del frontend, build del backend y tests).
* Escaneo de dependencias (Dependabot) y actualización automática de parches menores.

---

## 11) Dominios personalizados (opcional)

* **Frontend (Pages):** agregar dominio custom desde Settings → Pages. Si hay dominio propio, crear el registro DNS y (si aplica) un archivo `CNAME` en el build.
* **API (Azure):** agregar dominio custom en “Custom domains” y habilitar TLS/SSL.

---

## 12) Observabilidad y monitoreo (opcional)

* **API:** Application Insights (logs, métricas, trazas).
* **Frontend:** métricas de Pages y/o analítica web si corresponde.

---

## 13) Troubleshooting común

* Rutas del frontend rotas → ajustar la base pública del build o usar hash mode.
* Errores CORS → revisar que el origen coincida exactamente con la URL del frontend.
* Fallos de deploy en la API → confirmar el nombre del App Service y el Secret del Publish Profile.
* 404 en rutas profundas en Pages → preferir hash mode si no se usan rewrites.

---

## 14) Costos y límites

* **GitHub Pages:** sin costo adicional (sitio estático).
* **Azure App Service:** costo según plan; hay planes básicos de bajo costo para ambientes pequeños.
* **GitHub Actions:** minutos gratuitos con límites; considerar caching y builds eficientes.

---

## 15) Resumen ejecutivo

* Un repo, dos carpetas, dos pipelines.
* Frontend estático a Pages; backend dinámico a App Service.
* Variables de entorno, CORS y rutas bien configuradas garantizan que ambos trabajen juntos.
* Todo el proceso es reproducible y auditable vía GitHub Actions.
