# 🚗 uPark — Aplicación Móvil de Estacionamientos Universitarios

## 📋 Descripción
uPark es una aplicación móvil desarrollada con .NET MAUI que permite a los estudiantes universitarios consultar en tiempo real la disponibilidad de espacios en los estacionamientos del campus (IMM y UMAD).

## 👥 Autores
- Derek R. Ramírez Enríquez
- Pablo D. Lara Pérez
- Santiago S. Santos del Valle
- Omar Ganem

## 📚 Materia
Desarrollo para Plataformas Móviles I  
Docente: Alejandro Barroeta Martínez

---

## 🛠️ Tecnologías utilizadas
- .NET MAUI (Multi-platform App UI)
- Entity Framework Core 8.0
- SQLite (base de datos local)
- C# / XAML
- Visual Studio 2022
- GitHub Desktop

---

## 🏗️ Estructura del proyecto
UPark.sln
├── UPark/                  → Aplicación móvil MAUI (pantallas, navegación)
├── UPark.Data/             → Biblioteca de clases (Modelos + DbContext)
└── UPark.Migrations/       → Proyecto de consola (migraciones EF Core)

---

## 🗄️ Modelo de datos

| Entidad | Descripción |
|---|---|
| `Usuario` | Estudiante con matrícula, nombre, correo y contraseña |
| `Estacionamiento` | IMM o UMAD con ubicación y capacidad |
| `Espacio` | Lugar individual con estado libre/ocupado |
| `Notificacion` | Aviso de cambio de disponibilidad |

---

## 📱 Pantallas implementadas

| Pantalla | Descripción |
|---|---|
| Login | Acceso con matrícula y contraseña |
| Registro | Creación de cuenta de estudiante |
| Olvidaste Contraseña | Recuperación por correo institucional |
| Home | Selección de estacionamiento IMM o UMAD |
| Mapa | Vista de espacios disponibles y ocupados |
| Detalle Espacio | Información específica de un espacio |
| Notificaciones | Avisos sobre cambios de disponibilidad |
| Mi Cuenta | Perfil del estudiante |

---

## ✅ Funcionalidades

- 🔐 Login con validación de matrícula (solo números) y ojito para contraseña
- 🗺️ Mapa visual de espacios con colores (verde = libre, rojo = ocupado)
- 🔔 Pantalla de notificaciones con alertas en tiempo real
- 🔄 Pull to refresh en Home, Mapa y Notificaciones
- ⚠️ Validaciones visuales con DataTriggers en XAML
- 👤 Perfil de estudiante con datos de sesión

---

## 🗃️ Base de datos

La base de datos SQLite se genera automáticamente al ejecutar el proyecto `UPark.Migrations`.

**Tablas creadas:**
- Usuarios
- Estacionamientos (con datos iniciales: IMM y UMAD)
- Espacios
- Notificaciones

---

## 🚀 Cómo ejecutar el proyecto

1. Clona el repositorio
2. Abre `UPark.sln` en Visual Studio 2022
3. Restaura los paquetes NuGet
4. Ejecuta la migración inicial:
Add-Migration InitialCreate -Project UPark.Migrations -StartupProject UPark.Migrations
Update-Database -Project UPark.Migrations -StartupProject UPark.Migrations
5. Establece `UPark` como proyecto de inicio
6. Presiona **F5** para ejecutar

---

## 🌿 Ramas de GitHub

| Rama | Descripción |
|---|---|
| `main` | Versión estable y final |
| `develop` | Rama de desarrollo |

---

## 📅 Fecha de entrega
20 de abril de 2026
