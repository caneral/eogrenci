using System;
namespace Core.Entities
{
    /// <summary>
    /// Is entity soft deleted ?
    /// </summary>
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}
