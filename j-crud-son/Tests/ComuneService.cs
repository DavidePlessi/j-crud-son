using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using Newtonsoft.Json.Linq;

namespace j_crud_son.Tests
{
    public interface IComuneService : ICrudService<Comune>
    {
    }

    public class ComuneService : CrudService<Comune>, IComuneService
    {
        public ComuneService(string jsonPath, string entityName) : base(jsonPath, entityName)
        {
        }
        
        public ComuneService() : base("./comuni.json", "Data")
        {
        }
    }
}