using System;
using System.Collections.Generic;

namespace DAL.Modelos;

public partial class User
{
    public string Id { get; set; }

    public string UsuarioNick { get; set; } = null!;

    public string UsuarioPassword { get; set; } = null!;

    public string? Isadmin { get; set; }

    public User(string id, string usuarioNick, string usuarioPassword, string? isadmin)
    {
        Id = id;
        UsuarioNick = usuarioNick;
        UsuarioPassword = usuarioPassword;
        Isadmin = isadmin;
    }
}
