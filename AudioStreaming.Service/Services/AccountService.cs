
using AudioStreaming.Data.Entity;
using AudioStreaming.Data.UnitOfWork;
using AudioStreaming.Services.Helpers;
using AudioStreaming.Services.Responses;

using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public AccountService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(string idToken, string fcmToken)
        {
            FirebaseToken decodedToken = null;
            try
            {
                decodedToken = await FirebaseAuth.DefaultInstance
        .       VerifyIdTokenAsync(idToken);
            }
            catch (Exception)
            {
                return null;
            }

            String firebaseUid = decodedToken.Uid;

            object tmp = null;
            String email ="";
            String phoneNumber = "";
            String firebaseProvider = "";
            String name = "";

            decodedToken.Claims.TryGetValue("firebase", out tmp);
            if (tmp.ToString().Contains("google"))
            {
                firebaseProvider = "google.com";
            }
            else if (tmp.ToString().Contains("facebook"))
            {
                firebaseProvider = "facebook.com";
            }
            else if (tmp.ToString().Contains("email"))
            {
                firebaseProvider = "email";
            }
            else return null;

            decodedToken.Claims.TryGetValue("email", out tmp);
            if(tmp!=null)   email = tmp.ToString();

            decodedToken.Claims.TryGetValue("phoneNumber", out tmp);
            if (tmp != null)  phoneNumber = tmp.ToString();

            decodedToken.Claims.TryGetValue("name", out tmp);
            if (tmp != null)  name = tmp.ToString();
            Account account = _unitOfWork.Repository<Data.Entity.Account>().Find(x => x.FirebaseUid == firebaseUid);

            if (account == null)
            {
                _unitOfWork.Repository<Account>().Insert(
                    new Account()
                    {
                        Id = Guid.NewGuid(),
                        PhoneNumber = phoneNumber,
                        Email = email,
                        CreateDate = GetTimeNowVN.GetTimeNowVietNam(),
                        IsDelete = false,
                        IsVip = false,
                        ModifyDate = GetTimeNowVN.GetTimeNowVietNam(),
                        FullName= name,
                        Role = Int32.Parse(Role.User),
                        FcmToken   = fcmToken,
                        FirebaseUid = firebaseUid,
                        FirebaseProvider = firebaseProvider
                    }
                );
                _unitOfWork.Commit();
                account = _unitOfWork.Repository<Account>().Find(x => x.FirebaseUid == firebaseUid && !x.IsDelete);
            }
            else
            {
                account.FcmToken = fcmToken;
                account.ModifyDate = GetTimeNowVN.GetTimeNowVietNam();
                _unitOfWork.Repository<Account>().Update(account, account.Id);
                _unitOfWork.Commit();
            }

            return new AuthenticateResponse()
            {
                Id = account.Id,
                FullName = account.FullName,
                Email = account.Email,
                IsVip = account.IsVip,
                Role = account.Role,
                Token = await generateJwtToken(account, ""),
                ModifyDate = account.ModifyDate,
                FirebaseUid = account.FirebaseUid,
            };
        }

        public async Task<AuthenticateResponse> AuthenticateByIdAsync(Guid Id)
        {
            var account = _unitOfWork.Repository<Account>().Find(a =>a.Id == Id && !a.IsDelete);
            if (account == null) return null;
            return new AuthenticateResponse()
            {
                Id = account.Id,
                FullName = account.FullName,
                Email = account.Email,
                IsVip = account.IsVip,
                Role = account.Role,
                Token =await generateJwtToken(account,""),
                ModifyDate =account.ModifyDate,
                FirebaseUid = account.FirebaseUid,
            };

        }

        public async Task<string> AuthenticateWebAdminAsync(string idToken)
        {
            FirebaseToken decodedToken = null;
            try
            {
                decodedToken = await FirebaseAuth.DefaultInstance
        .VerifyIdTokenAsync(idToken);
            }
            catch (Exception)
            {
                return null;
            }
            String firebaseUid = decodedToken.Uid;
            Console.WriteLine(firebaseUid);
            object tmp = null;
            String email = "";
            String phoneNumber = "";
            String firebaseProvider = "";

            decodedToken.Claims.TryGetValue("firebase", out tmp);
            if (tmp.ToString().Contains("google"))
            {
                firebaseProvider = "google.com";
            }
            else if (tmp.ToString().Contains("facebook"))
            {
                firebaseProvider = "facebook.com";
            }
            else return null;

            decodedToken.Claims.TryGetValue("email", out tmp);
            if (tmp != null) email = tmp.ToString();

            decodedToken.Claims.TryGetValue("phoneNumber", out tmp);
            if (tmp != null) phoneNumber = tmp.ToString();

            decodedToken.Claims.TryGetValue("name", out tmp);
            String name = tmp.ToString();

            Account account = _unitOfWork.Repository<Account>().Find(x => x.FirebaseUid == firebaseUid && !x.IsDelete);

            if (account == null)
            {

                return null;
            }
            else
            {
                if(account.Role != 0)
                {
                    return null;
                }
                if(account.OwnersCode == null)
                {
                    return null;
                }
            }
            Guid ownerCode = new Guid(account.OwnersCode.ToString());
            var brandName = _unitOfWork.Repository<Data.Entity.Brand>().GetById(ownerCode).BrandName;
            var jwt = await generateJwtToken(account, brandName);
            return jwt;


        }

        public async Task<IList<DTO.Account>> GetAll()
        {
            var accounts = _unitOfWork.Repository<Account>().GetAll();
            IList<DTO.Account> accountsDto = new List<DTO.Account>();
            foreach (var item in accounts)
            {
                accountsDto.Add(new DTO.Account()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Email = item.Email,
                    IsVip = item.IsVip,
                    Role = item.Role,
                    CreateDate = item.CreateDate,
                    IsDelete = item.IsDelete,
                    ModifyDate = item.ModifyDate,
                    PhoneNumber = item.PhoneNumber,
                });
            }
            return accountsDto;
        }


        private async Task<string> generateJwtToken(Account account, string brandName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role , account.Role.ToString()),
                new Claim(ClaimTypes.Name , account.FullName),
                new Claim("OwnerCode" , account.OwnersCode.ToString()),
                new Claim("OwnerBrand" , brandName),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["AppSettings:Issuer"],
                _config["AppSettings:Issuer"],
                claims,
                expires: GetTimeNowVN.GetTimeNowVietNam().AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
