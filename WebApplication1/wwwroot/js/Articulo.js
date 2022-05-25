

function CompletarTablaArticulos() {
    VaciarFormulario();
    $('#ArticuloID').val(0)
    $.ajax({
        type: "POST",
        url: '../../Articulos/BuscarArticulos',
        data: {},
        success: function (listadoarticulossmostrar) {
            $("#tbody-articulos").empty();
            $.each(listadoarticulossmostrar, function (index, articulo) {

                let claseEliminado = '';
                let botones = '<button type="button" onclick="BuscarArticulo(' + articulo.articuloID + ')" class="btn btn-outline-primary btn-sm" style="margin-right:5px">Editar</button>' +
                    '<button type="button" onclick="EliminarArticulos(' + articulo.articuloID + ',1)" class="btn btn-outline-danger btn-sm">Eliminar</button>';

                let SignoPesos = '$'
                let SignoPorcentaje = '%'

                if (articulo.eliminado) {
                    claseEliminado = 'table-danger';
                    botones = '<button type="button" onclick="EliminarArticulos(' + articulo.articuloID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                }

                $("#tbody-articulos").append('<tr class=' + claseEliminado + '>' +
                    '<td class="text-center border-dark">' + articulo.descripcion + '</td>' +
                    '<td class="text-center border-dark">' + articulo.subrubroNombre + '</td>' +
                    '<td class="text-center border-dark">' + articulo.ultActString + '</td>' +
                    '<td class="text-right border-dark">' + SignoPesos + articulo.precioCosto +  '</td>' +
                    '<td class="text-right border-dark">' + SignoPorcentaje + articulo.porcentajeGanancia + '</td>' +
                    '<td class="text-right border-dark">' + SignoPesos + articulo.precioVenta + '</td>' +
                    '<td class="text-center border-dark">' + botones + '</td>' +
                    '</tr>');
            });
        },
        error: function (data) {
        }
    });
}


function AbrirModal() {
    $("#Titulo-Modal-Articulo").text("Nuevo Articulo");
    $("#ArticuloID").val(0);
    $("#SubRubroID").val(0);
    $("#RubroID").val(0);
    BuscarSubRubros();
    $("#exampleModal").modal("show");


}


function VaciarFormulario() {
    $("#ArticuloID").val(0);
    $("#SubRubroID").val(0);
    $("#RubroID").val(0);
    $("#ArticuloNombre").val('');
    $("#PrecioCosto").val('');
    $("#PorcentajeGanancia").val('');
    $("#PrecioVenta").val('');
    $("#Error-ArticuloNombre").text("");
}

function CalcularImportes(origen) {
    let costo = $("#PrecioCosto").val();
    let ganancia = $("#PorcentajeGanancia").val();
    let venta = $("#PrecioVenta").val();

    //SI MODIFICA PRECIO DE COSTO
    if (origen == 1) {
        //CALCULAR SOLO EL PRECIO DE VENTA
        let ventaArticulo = costo * ((ganancia / 100) + 1);
        $("#PrecioVenta").val(ventaArticulo);
    }

    //MODIFICA EL PORCENTAJE DE GANANCIA
    if (origen == 2) {
        //CALCULAR NUEVO PRECIO DE VENTA
        let costoArticulo = costo * ((ganancia / 100) + 1);
        $("#PrecioVenta").val(costoArticulo);
    }

    //SI MODIFICA PRECIO DE VENTA
    if (origen == 3) {
        //calcular ganancia
        let costoCalculado = venta * 100 / costo - 100;
        $("#PorcentajeGanancia").val(costoCalculado);
    }
}


$("#RubroID").change(function () {
    BuscarSubRubros();
});


function BuscarSubRubros() {
    //Se limpia el contenido del dropdownlist
    $("#SubRubroID").empty();
    $.ajax({
        type: 'POST',
        //Llamado al metodo en el controlador
        url: "../../Subrubros/ComboSubRubro",
        dataType: 'json',
        //Parametros que se envian al metodo del controlador
        data: { id: $("#RubroID").val() },
        //En caso de resultado exitoso
        success: function (subRubros) {
            if (subRubros.length == 0) {
                $("#SubRubroID").append('<option value="' + "0" + '">' + "[NO EXISTEN SUBRUBROS]" + '</option>');
            }
            else {
                $.each(subRubros, function (i, subRubro) {
                    $("#SubRubroID").append('<option value="' + subRubro.value + '">' +
                        subRubro.text + '</option>');
                });
            }
        },
        ////Mensaje de error en caso de fallo
        error: function (ex) {
        }
    });
    return false;
}

function GuardarArticulo() {
    $("#Error-ArticuloNombre").text("");
    let articuloID = $('#ArticuloID').val();
    let articuloNombre = $('#ArticuloNombre').val().trim();
    let preciocosto = $('#PrecioCosto').val();
    let porcentajeganancia = $('#PorcentajeGanancia').val();
    let precioventa = $('#PrecioVenta').val();
    let subrubroID = $('#SubRubroID').val();
    if (articuloNombre != "" && articuloNombre != null) {
        $.ajax({
            type: "POST",
            url: '../../Articulos/GuardarArticulo',
            data: { ArticuloID: articuloID, Descripcion: articuloNombre, PrecioCosto: preciocosto, PorcentajeGanancia: porcentajeganancia, PrecioVenta: precioventa, SubRubroID: subrubroID},
            success: function (resultado) {
                if (resultado == 0) {
                    $("#exampleModal").modal('hide')
                    CompletarTablaArticulos();
                }
                if (resultado == 2) {
                    $("#Error-ArticuloNombre").text("El Articulo ingresado ya existe");
                }
            },
            error: function (data) {
            }
        });
    }
    else {
        $("#Error-ArticuloNombre").text("Debe ingresar un Articulo");
    }

}



function BuscarArticulo(articuloID) {
    $("#Titulo-Modal-Articulo").text("Editar Articulo")
    $("#ArticuloID").val(articuloID);
    $.ajax({
        type: "POST",
        url: '../../Articulos/BuscarArticulo',
        data: { ArticuloID: articuloID},
        success: function (articulo) {
            $("#ArticuloNombre").val(articulo.descripcion);
            $("#RubroID").val(articulo.rubroID);
            BuscarSubRubros();
            $("#SubRubroID").val(articulo.subRubroID);
            $('#PrecioCosto').val(articulo.precioCosto);
            $('#PorcentajeGanancia').val(articulo.porcentajeGanancia);
            $('#PrecioVenta').val(articulo.precioVenta);


            $("#exampleModal").modal("show");
        },
        error: function (data) {
        }
    });
}






function EliminarArticulos (articuloID, elimina) {
    $.ajax({
        type: "POST",
        url: '../../Articulos/EliminarArticulos',
        data: { ArticuloID: articuloID, Elimina: elimina },
        success: function (articulos) {
            CompletarTablaArticulos();
        },
        error: function (data) {
        }
    });
}