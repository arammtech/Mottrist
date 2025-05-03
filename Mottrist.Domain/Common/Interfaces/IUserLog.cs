namespace Mottrist.Domain.Common.Interfaces
{
    public interface ICreateAt
    {
        DateTime CreatedAt { get; set; }
    }

    public interface IUpdateAt
    {
        DateTime? UpdatedAt { get; set; }
    }

    public interface ICreatedBy
    {
        int CreatedBy { get; set; }
    }

    public interface IUpdatedBy
    {
        int? UpdatedBy { get; set; }
    }

    public interface IUserLog : ICreateAt, IUpdateAt, ICreatedBy, IUpdatedBy
    {
        
    }
}
