using System.Runtime.Serialization;
namespace RestWithAsp.NetUdemy.Model.Base
{
    // Contrato entre aributos
    // e a estrutura da tabela
    //[DataContract]
    public class BaseEntity
    {
        public long? Id { get; set; }
    }
}
