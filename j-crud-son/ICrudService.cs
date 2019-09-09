using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace j_crud_son
{
    public interface ICrudService<T> where T : Entity
    {
        /// <summary>
        /// Converte il JToken del nodo passato nell'entità del service
        /// </summary>
        /// <param name="obj">Oggetto JToken</param>
        /// <returns>Entità derivata</returns>
        T ToEntity(JToken obj);

        /// <summary>
        /// Converte l'entità in JToken
        /// </summary>
        /// <param name="entity">Entità da convertire</param>
        /// <returns>JToken derivato</returns>
        JToken ToJToken(T entity);
        
        /// <summary>
        /// Carica un enumerable di entità
        /// </summary>
        /// <param name="filter">Funzione per filtrare le entitá (null equivale a x => true)</param>
        /// <returns>Enumerable delle entità che rispettano il filtro passato</returns>
        IEnumerable<T> Load(Func<T, bool> filter = null);
        
        /// <summary>
        /// Carica una singola entità dato l'id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entità corrispondente, null se non trovata</returns>
        T Load(long id);

        /// <summary>
        /// Carica un enumerable di entità trasformato tramite la funzione select
        /// </summary>
        /// <param name="filter">Funzione per filtrare le entitá (null equivale a x => true)</param>
        /// <param name="select">Funzione di selezione sull'entità</param>
        /// <typeparam name="TSelect">Tipo di ritorno della funzione select</typeparam>
        /// <returns>Enumerable di oggetti TSelect</returns>
        IEnumerable<TSelect> LoadAndSelect<TSelect>(Func<T, bool> filter, Func<T, TSelect> select);
        
        /// <summary>
        /// Inserisce o aggiorna l'entità passata sul file json (id == 0 -> inserimento)
        /// </summary>
        /// <param name="entity">Entità da salvare</param>
        /// <param name="forzaInserimento">Forza un nuovo inserimento anche se id != 0</param>
        /// <returns>Id entità salvata</returns>
        long Save(T entity, bool forzaInserimento = false);
        
        /// <summary>
        /// Elimina l'entità passata
        /// </summary>
        /// <param name="entity">Entità da eliminare</param>
        /// <returns>1</returns>
        long Delete(T entity);
        
        /// <summary>
        /// Elimina l'entità avente l'id passato
        /// </summary>
        /// <param name="id">Id entità da eliminare</param>
        /// <returns>1</returns>
        long Delete(long id);
    }
}