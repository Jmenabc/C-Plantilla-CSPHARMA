using System;
using System.Collections.Generic;

namespace DAL.Modelos;

public  class User
{
    public string Id { get; set; }

    public string UsuarioNick { get; set; } = null!;

    public string UsuarioPassword { get; set; } = null!;

    public string? Isadmin { get; set; }

    //Constructor

    public User(string ID, string UsuarioNick1,String UsuarioPasssword, string IsAdmin)
    {
        Id = ID;
        UsuarioNick = UsuarioNick1;
        UsuarioPassword = UsuarioPasssword;
        Isadmin = IsAdmin;

    }
}
