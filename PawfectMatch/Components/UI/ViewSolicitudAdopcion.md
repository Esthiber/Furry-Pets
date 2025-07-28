# ViewSolicitudAdopcion Page

## Descripción
Página Blazor accesible desde URL para visualizar registros de solicitudes de adopción de manera detallada y profesional. Incluye información completa de la mascota, adoptante, cronología de la solicitud y acciones administrativas.

## URL de Acceso
**Ruta:** `/solicitudes/detalle/{id:int}`

**Ejemplos:**
- `/solicitudes/detalle/1` - Ver solicitud ID 1
- `/solicitudes/detalle/25` - Ver solicitud ID 25

## Características
- ? Página independiente accesible desde URL
- ? Navegación con breadcrumbs
- ? Visualización completa de información de solicitudes
- ? Diseño responsivo para móviles y escritorio
- ? Estados visuales para diferentes tipos de solicitud (Pendiente, Aprobada, Rechazada)
- ? Cronología visual del progreso de la solicitud
- ? Acciones administrativas integradas (Aprobar/Rechazar)
- ? Funcionalidad de impresión
- ? Carga asíncrona de datos
- ? Estados de carga y error
- ? Accesibilidad y navegación por teclado
- ? Estilos de impresión optimizados

## Parámetros de Ruta

| Parámetro | Tipo | Descripción |
|-----------|------|-------------|
| `id` | `int` | ID de la solicitud de adopción a mostrar |

## Navegación

### Desde otras páginas
```razor
<!-- Enlace directo -->
<a href="/solicitudes/detalle/@solicitudId">Ver Solicitud</a>

<!-- Con URL Constants -->
<a href="@($"{Urls.Solicitudes.Detalle}{solicitudId}")">Ver Solicitud</a>

<!-- Navegación programática -->
NavigationManager.NavigateTo($"{Urls.Solicitudes.Detalle}{solicitudId}");
```

### Breadcrumb Navigation
La página incluye navegación breadcrumb automática:
- **Administración** ? Panel principal
- **Solicitudes** ? Lista de solicitudes  
- **Solicitud #ID** ? Página actual

## Permisos Requeridos
La página requiere el permiso `Permissions.AdoptionRequests.ViewDetails` para acceder.

**Configuración de autorización:**
```razor
@attribute [Authorize(Policy = Permissions.AdoptionRequests.ViewDetails)]
```

### Permisos para acciones
- `Approve`: Para mostrar botón de aprobar
- `Reject`: Para mostrar botón de rechazar

## Dependencias de Servicios
- `SolicitudesAdopcionesService` - Carga y actualización de datos
- `NavigationManager` - Navegación entre páginas
- `IJSRuntime` - Funcionalidad de impresión y alertas

## Funcionalidades Principales

### 1. Visualización de Datos
- **Header**: ID, fecha y estado con badges visuales
- **Mascota**: Foto, nombre, raza, tamaño, sexo, edad
- **Adoptante**: Información de contacto completa
- **Timeline**: Cronología visual del progreso

### 2. Acciones Administrativas
- **Aprobar Solicitud**: Solo para solicitudes pendientes
- **Rechazar Solicitud**: Solo para solicitudes pendientes
- **Imprimir**: Función de impresión optimizada
- **Volver**: Navegación de regreso a lista

### 3. Estados de la Página

#### Loading State
```razor
<div class="loading-state">
    <div class="spinner-border text-primary"></div>
    <p>Cargando información de la solicitud...</p>
</div>
```

#### Error State  
```razor
<div class="error-state">
    <i class="bi bi-exclamation-triangle"></i>
    <h4>Solicitud no encontrada</h4>
    <p>No se pudo cargar la información...</p>
</div>
```

#### Success State
Muestra toda la información organizadamente con diseño de tarjeta profesional.

## Integración con URLs Constants

### Actualización en Urls.cs
```csharp
public static class Solicitudes
{
    public const string Index = "/solicitudes";
    public const string Detalle = "/solicitudes/detalle/";
    // Otros URLs...
}
```

### Uso en componentes
```razor
<!-- En IndexSolicitudes.razor -->
<a href="@($"{Urls.Solicitudes.Detalle}{solicitud.SolicitudesAdopcionesID}")">
    Ver Detalles
</a>

<!-- En SolicitudesAdopcionAdmin.razor -->
NavigationManager.NavigateTo($"{Urls.Solicitudes.Detalle}{solicitudId}");
```

## Responsive Design

### Desktop (?768px)
- Layout de dos columnas para información de mascota/adoptante
- Breadcrumbs en línea horizontal
- Botones de acción en fila

### Mobile (<768px)
- Layout de columna única
- Breadcrumbs apilados
- Botones de acción centrados y expandidos

### Print Styles
- Oculta breadcrumbs y botones de acción
- Optimiza colores para impresión
- Ajusta tamaños de fuente

## SEO y Metadatos

### Page Title
```razor
<PageTitle>Solicitud de Adopción #@id | Pawfect Match</PageTitle>
```

### Estructura Semántica
- Navegación con `<nav>` y `aria-label`
- Headings jerárquicos (h4, h5, h6)
- Landmarks semánticos

## Manejo de Errores

### Solicitud no encontrada
- Muestra mensaje de error amigable
- Botón para volver a la lista
- No rompe la aplicación

### Errores de red
- Logs en consola para debugging
- Mensajes de error al usuario
- Mantiene estado consistente

## Testing

### URLs de Prueba
```
/solicitudes/detalle/1
/solicitudes/detalle/999  (no existe)
/solicitudes/detalle/abc  (inválido - redirige a 404)
```

### Estados a Probar
- Solicitud pendiente (con botones de acción)
- Solicitud aprobada (solo visualización)
- Solicitud rechazada (solo visualización)
- Solicitud no encontrada (error state)

## Performance

### Optimizaciones
- Carga asíncrona de datos con `SearchByIdAsync`
- Estados de loading para UX fluida
- Navegación incluye datos relacionados (Include)
- CSS scoped para evitar conflictos

### Mejores Prácticas
- Manejo de excepciones robusto
- StateHasChanged() solo cuando necesario
- Validación de permisos a nivel de página
- URLs amigables y RESTful

## Migración desde Componente

### Antes (Componente)
```razor
<ViewSolicitudAdopcion SolicitudId="@id" ShowActions="true" />
```

### Después (Página)
```razor
<!-- Acceso directo desde URL -->
/solicitudes/detalle/123

<!-- O navegación programática -->
NavigationManager.NavigateTo($"/solicitudes/detalle/{id}");
```

La migración a página independiente proporciona:
- URLs directas compartibles
- Mejor SEO
- Navegación más intuitiva
- Experiencia de usuario mejorada