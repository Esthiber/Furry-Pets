using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Define un contrato estándar para las operaciones CRUD (Crear, Leer, Actualizar, Borrar)
    /// para un tipo de entidad genérico 'T'.
    /// Esta interfaz promueve la consistencia a través de todas las clases de servicio de la aplicación.
    /// </summary>
    /// <typeparam name="T">El tipo de la entidad para la cual se implementan las operaciones CRUD.</typeparam>
    public interface ICRUD<T>
    {
        #region Create / Update Operations

        /// <summary>
        /// Inserta de forma asíncrona un nuevo elemento en el repositorio de datos.
        /// </summary>
        /// <param name="elem">El elemento de tipo 'T' a ser insertado.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es 'true' si el elemento fue insertado exitosamente; de lo contrario, 'false'.
        /// </returns>
        Task<bool> InsertAsync(T elem);

        /// <summary>
        /// Actualiza de forma asíncrona un elemento existente en el repositorio de datos.
        /// </summary>
        /// <param name="elem">El elemento de tipo 'T' con los datos actualizados.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es 'true' si el elemento fue actualizado exitosamente; de lo contrario, 'false'.
        /// </returns>
        Task<bool> UpdateAsync(T elem);

        /// <summary>
        /// Guarda un elemento de forma asíncrona (Upsert). Si el elemento es nuevo, lo inserta;
        /// si ya existe, lo actualiza.
        /// </summary>
        /// <param name="elem">El elemento de tipo 'T' a ser guardado.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es 'true' si la operación de guardado (inserción o actualización) fue exitosa;
        /// de lo contrario, 'false'.
        /// </returns>
        Task<bool> SaveAsync(T elem);

        #endregion

        #region Read Operations

        /// <summary>
        /// Busca de forma asíncrona un elemento por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del elemento a buscar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es el elemento de tipo 'T' si se encuentra;
        /// de lo contrario, podría ser 'null' o un nuevo objeto por defecto, dependiendo de la implementación.
        /// </returns>
        Task<T> SearchByIdAsync(int id);

        /// <summary>
        /// Obtiene de forma asíncrona una lista de elementos que cumplen con un criterio específico.
        /// </summary>
        /// <param name="criteria">
        /// Una expresión lambda que define el criterio de filtrado (ej. e => e.IsActive == true).
        /// </param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es una lista de elementos de tipo 'T' que cumplen con el criterio.
        /// </returns>
        Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Comprueba de forma asíncrona si un elemento con el identificador especificado existe.
        /// </summary>
        /// <param name="id">El identificador único del elemento a verificar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es 'true' si el elemento existe; de lo contrario, 'false'.
        /// </returns>
        Task<bool> ExistAsync(int id);

        #endregion

        #region Delete Operations

        /// <summary>
        /// Elimina de forma asíncrona un elemento del repositorio de datos, identificado por su ID.
        /// La implementación decidirá si se trata de un borrado físico (hard delete) o lógico (soft delete).
        /// </summary>
        /// <param name="id">El identificador único del elemento a eliminar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es 'true' si el elemento fue eliminado exitosamente; de lo contrario, 'false'.
        /// </returns>
        Task<bool> DeleteAsync(int id);

        #endregion
    }
}