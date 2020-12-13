using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class Account
    {
        public Account()
        {
            FavoritePlaylist = new HashSet<FavoritePlaylist>();
            TimeSubmit = new HashSet<TimeSubmit>();
        }

        public Guid Id { get; set; }
        public string FirebaseProvider { get; set; }
        public string FirebaseUid { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public bool IsDelete { get; set; }
        public bool IsVip { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string FcmToken { get; set; }
        public Guid? OwnersCode { get; set; }

        public virtual ICollection<FavoritePlaylist> FavoritePlaylist { get; set; }
        public virtual ICollection<TimeSubmit> TimeSubmit { get; set; }
    }
}
