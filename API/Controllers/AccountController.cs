using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context=context;
        }

        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto){

            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken already!");
            using var hmac=new HMACSHA512();

            var user =new AppUser{
                UserName=registerDto.Username.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto){

            var user=await _context.AppUsers.SingleOrDefaultAsync(x=>
            x.UserName==loginDto.UserName);

            if(user==null) return Unauthorized("invalid username");

            //providing Salt key to generate the computed hash for specific password
            using var hmac=new HMACSHA512(user.PasswordSalt);

            //computes the Hashed data of user entered password by using 'hmac' with the provided Saltkey
            var compHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i=0;i<compHash.Length;i++){
                
                if(compHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }

            return user;

        }
        private async Task<bool> UserExists(string username){
            return await _context.AppUsers.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}