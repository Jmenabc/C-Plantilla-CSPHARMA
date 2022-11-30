using System;
using System.Collections.Generic;

namespace DAL.Modelos;

public partial class DlkCatAccEmpleado
{
    public string MdUuid { get; set; } = null!;

    public string? MdDate { get; set; }

    public string CodEmpleado { get; set; }

    public string ClaveEmpleado { get; set; } = null!;

    public string NivelAccesoEmpleado { get; set; }


    public DlkCatAccEmpleado(string MdUuid,string MdDate, string CodEmpleado, string ClaveEmpleado, string NivelAccesoEmpleado) {
        this.MdUuid = MdUuid;
        this.MdDate = MdDate;
        this.CodEmpleado = CodEmpleado;
        this.ClaveEmpleado= ClaveEmpleado;
        this.NivelAccesoEmpleado = NivelAccesoEmpleado;

    }
}
