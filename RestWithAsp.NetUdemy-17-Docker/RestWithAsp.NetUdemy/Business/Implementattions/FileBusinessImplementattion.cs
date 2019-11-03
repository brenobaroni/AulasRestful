using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Security.Configuration;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using RestWithAspNetUdemy.Security.Configuration;
using System.IO;

namespace RestWithASPNETUdemy.Business.Implementattions
{
    public class FileBusinessImplementattion : IFileBusiness
    {
        public byte[] GetPDFFiles()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\Other\\aspnet-life-cycles-events.pdf";
            return File.ReadAllBytes(fullPath);

        }
    }
}