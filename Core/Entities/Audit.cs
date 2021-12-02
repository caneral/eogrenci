using System;
namespace Core.Entities
{
    /// <summary>
    /// DB Kayıt bilgileri  
    /// </summary>
    public abstract class Audit
    {
        /// <summary>
        /// Oluşturan Kullanıcı Id
        /// </summary>
        public int CUserId { get; set; }

        /// <summary>
        /// Değiştiren Kullanıcı Id
        /// </summary>
        public int? MUserId { get; set; }

        /// <summary>
        /// Oluşturulma Tarihi
        /// </summary>
        public DateTime CDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Değiştirilme Tarihi
        /// </summary>
        public DateTime? MDate { get; set; }
    }
}
