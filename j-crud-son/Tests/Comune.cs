namespace j_crud_son.Tests
{
    public class Comune : Entity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string NotActive { get; set; }
        public int Order { get; set; }
    }
}