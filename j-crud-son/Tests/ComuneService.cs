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
        
        public ComuneService() : base("C:\\Progetti\\j-crud-son\\j-crud-son\\comuni.json", "Data")
        {
        }
    }
}