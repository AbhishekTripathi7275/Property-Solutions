using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI_PropertySearchingSite.Data;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            var user = await dc.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == EncodePasswordToBase64(password));
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public void Register(string userName, string password)
        {
            User user = new User();
            user.UserName = userName;
            user.Password = EncodePasswordToBase64(password);
            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.Users.AnyAsync(x => x.UserName == userName);
        }

        public static string EncodePasswordToBase64(string password)
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
