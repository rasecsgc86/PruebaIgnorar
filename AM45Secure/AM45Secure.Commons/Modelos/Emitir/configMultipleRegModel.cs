using System;

// INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class configMultipleRegModel
    {
            public int idAseguradora { get; set; }
            public string Aseguradora { get; set; }
            public int idProducto { get; set; }
            public string Producto { get; set; }
            public int idPerfil { get; set; }
            public String Perfil { get; set; }
            public int idUsuario { get; set; }
            public String Usuario { get; set; }
            public int iPermiteEmisionMultiple { get; set; }
            public int iPermiteContrato { get; set; }
    }
}



  