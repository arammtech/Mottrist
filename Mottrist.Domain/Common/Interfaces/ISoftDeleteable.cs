namespace Mottrist.Domain.Common.Interfaces
{
    public interface ISoftDeleteable
    {
        bool IsDeleted { get; set; }
        DateTime? DateDeleted { get; set; }

        void Delete()
        {
            IsDeleted = true;
            DateDeleted = DateTime.UtcNow;
        }

        void UndoDelete()
        {
            IsDeleted = false;
            DateDeleted = null;
        }
    }
}
