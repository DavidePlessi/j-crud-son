﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace j_crud_son
{
    public interface ICrudService<T> where T : Entity
    {
        T ToEntity(JToken obj);
        JToken ToJToken(T entity);
        long NextId();
        IEnumerable<T> Load(Func<T, bool> filter = null);
        T Load(long id);
        IEnumerable<TSelect> LoadAndSelect<TSelect>(Func<T, bool> filter, Func<T, TSelect> select);
        T FirstOrDefault(Func<T, bool> filter = null);
        long Save(T entity, bool forzaInserimento = false);
        long Delete(T entity);
        long Delete(long id);
    }

    public abstract class CrudService<T> : ICrudService<T> where T : Entity
    {
        private JObject _jObject;
        private JArray _set;
        private string _jsonPath;

        public CrudService( 
            string jsonPath, 
            string entityName,
            JObject jObject = null,
            JArray set = null
        )
        {
            _jsonPath = jsonPath;
            _jObject = jObject ?? JObject.Parse(File.ReadAllText(jsonPath));
            _set = set ?? (JArray) _jObject[entityName];
        }

        public T ToEntity(JToken obj)
        {
            return obj.ToObject<T>();
        }

        public JToken ToJToken(T entity)
        {
            return JToken.FromObject(entity);
        }

        public long NextId()
        {
            var lastId = _set.Select(ToEntity).Max(x => x.Id);

            return lastId + 1;
        }

        private void SaveChanges()
        {
            var output =Newtonsoft.Json.JsonConvert
                .SerializeObject(_jObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_jsonPath, output);
        }

        public IEnumerable<T> Load(Func<T, bool> filter = null)
        {
            var objs = _set
                .Select(ToEntity);
                
            if(filter != null)    
                objs = objs.Where(filter);
            
            return objs;
        }

        public T Load(long id)
        {
            var obj = _set
                .Select(ToEntity)
                .FirstOrDefault(x => x.Id == id);
            return obj;
        }

        public IEnumerable<TSelect> LoadAndSelect<TSelect>(Func<T, bool> filter, Func<T, TSelect> select)
        {
            var objs = _set
                .Select(ToEntity)
                .Where(filter)
                .Select(select);
            
            return objs;
        }

        public T FirstOrDefault(Func<T, bool> filter = null)
        {
            var objs = _set
                .Select(ToEntity)
                .FirstOrDefault(filter ?? (x => true));
            
            return objs;
        }

        public long Save(T entity, bool forzaInserimento = false)
        {
            var inCreazione = entity.Id == 0 || forzaInserimento;

            if (inCreazione)
                entity.Id = NextId();
            else
                _set.Remove(_set
                    .FirstOrDefault(x => x["Id"].Value<long>() == entity.Id)
                );
            
            _set.Add(ToJToken(entity));
            
            SaveChanges();
            
            return entity.Id;
        }

        public long Delete(T entity)
        {
            return Delete(entity.Id);
        }

        public long Delete(long id)
        {
            _set.Remove(_set
                .FirstOrDefault(x => x["Id"].Value<long>() == id)
            );
            
            SaveChanges();

            return 1;
        }
    }
}