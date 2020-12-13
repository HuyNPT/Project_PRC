using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class Account
    {
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
    }
}
