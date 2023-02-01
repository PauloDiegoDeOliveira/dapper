namespace Empresa.Dapper.Domain.Entitys.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }

        public string Status { get; private set; }

        public DateTime? CriadoEm { get; private set; } = DateTime.Now;

        public DateTime? AlteradoEm { get; private set; }

        public EntityBase()
        { }

        public void ChangeStatusValue(string status)
        {
            Status = status;
        }

        public void GuidValue(Guid id)
        {
            Id = id;
        }

        public bool VerificaRegraStatus(string status)
        {
            Dictionary<string, List<string>> regra = new()
            {
                { "Ativo", new List<string> { "Inativo", "Ativo" } },
                { "Inativo", new List<string> { "Ativo", "Inativo" } }
            };

            if (!regra.ContainsKey(Status))
                return false;

            if (regra[Status].Contains(status))
                return true;

            return false;
        }
    }
}