namespace RestWithAsp.NetUdemy.Model
{
    // Contrato entre aributos
    // e a estrutura da tabela
    //[DataContract]
    public class User
    {
        public long? Id { get; set; }
        public string Login { get; set; }
        public string AccessKey { get; set; }
    }
}
