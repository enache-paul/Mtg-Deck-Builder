using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace mtg_lib.Services;

using mtg_lib.Data;

public class UsersService
{
    private mtgContext _context;

    public UsersService()
    {
        this._context = new mtgContext();
    }

    public bool CreateNewUser(string email, string password)
    {
        try
        {
            User user = new User
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }
        catch (DbUpdateException ex)
        {
            //TODO: Log this error
            return false;
        }
    }

    public int GetUserId(string email) {
        return  _context.Users.First(user => user.Email == email).Id;
    }

    public bool AuthenticateUser(string email, string password)
    {
        try
        {
            var searchedUser = _context.Users.First(user => user.Email == email);
            
            bool passwordMatchesHash = BCrypt.Net.BCrypt.Verify(password, searchedUser.Password);
            
            return passwordMatchesHash;

        }
        catch (InvalidOperationException ex)
        {
            // TODO: log incorrect login
            return false;
        }
    }
}